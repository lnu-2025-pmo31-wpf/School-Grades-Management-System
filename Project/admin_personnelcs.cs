using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Data;
using SchoolJournal.Dialogs;

namespace SchoolJournal
{
    public partial class admin_personnelcs : Form
    {
        private Button _editButton = null!;
        private Button _removeButton = null!;

        public admin_personnelcs()
        {
            InitializeComponent();
            SetupExtraButtons();

            // Зручно: подвійний клік — редагувати
            dataGridView1.CellDoubleClick += (_, __) => EditSelectedTeacher();
            dataGridView1.KeyDown += DataGridView1_KeyDown;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void SetupExtraButtons()
        {
            _editButton = new Button
            {
                Text = "Редагувати",
                Font = add_button.Font,
                Size = new System.Drawing.Size(170, 58),
                Location = new System.Drawing.Point(90, 305)
            };
            _editButton.Click += (_, __) => EditSelectedTeacher();

            _removeButton = new Button
            {
                Text = "Видалити",
                Font = add_button.Font,
                Size = new System.Drawing.Size(170, 58),
                Location = new System.Drawing.Point(590, 305)
            };
            _removeButton.Click += (_, __) => RemoveSelectedTeacher();

            Controls.Add(_editButton);
            Controls.Add(_removeButton);
        }

        private void admin_personnelcs_Load(object sender, EventArgs e)
        {
            LoadTeachersToGrid();
        }

        private void LoadTeachersToGrid()
        {
            using var db = new SchoolDbContext();

            var rows = db.Teachers
                .Include(t => t.Group)
                .OrderBy(t => t.Id)
                .Select(t => new
                {
                    t.Id,
                    FullName = t.FullName,
                    Group = t.Group != null ? t.Group.Name : "—",
                    Position = t.Position,
                    Phone = t.Phone,
                    Email = t.Email,
                    Primary = t.IsPrimary ? "Так" : "Ні"
                })
                .ToList();

            dataGridView1.DataSource = rows;

            if (dataGridView1.Columns["FullName"] != null) dataGridView1.Columns["FullName"].HeaderText = "ПІБ";
            if (dataGridView1.Columns["Group"] != null) dataGridView1.Columns["Group"].HeaderText = "Клас";
            if (dataGridView1.Columns["Position"] != null) dataGridView1.Columns["Position"].HeaderText = "Посада";
            if (dataGridView1.Columns["Phone"] != null) dataGridView1.Columns["Phone"].HeaderText = "Телефон";
            if (dataGridView1.Columns["Primary"] != null) dataGridView1.Columns["Primary"].HeaderText = "Основний";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void DataGridView1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedTeacher();
                e.Handled = true;
            }
        }

        // === НАЗАД → administrator_form ===
        private void back_button_Click(object sender, EventArgs e)
        {
            administrator_form admin = new administrator_form();
            admin.Show();
            this.Hide();
        }

        // === ВИЙТИ → Form1 (логін) ===
        private void exit_button_Click(object sender, EventArgs e)
        {
            var roles = new role_selection_form();
            roles.Show();
            this.Hide();
        }

        // === ДОДАТИ ВИХОВАТЕЛЯ ===
        private void add_button_Click(object sender, EventArgs e)
        {
            using var dialog = new TeacherEditDialog();
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            using var db = new SchoolDbContext();

            // Якщо ставимо "Основний" — скидаємо інших у цьому класі
            if (dialog.Teacher.IsPrimary)
            {
                var others = db.Teachers.Where(t => t.GroupId == dialog.Teacher.GroupId && t.IsPrimary);
                foreach (var t in others) t.IsPrimary = false;
            }

            db.Teachers.Add(dialog.Teacher);
            db.SaveChanges();

            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            LoadTeachersToGrid();
        }

        private void EditSelectedTeacher()
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            var row = dataGridView1.SelectedRows[0];
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id)) return;

            using var db = new SchoolDbContext();
            var teacher = db.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null)
            {
                MessageBox.Show("Запис не знайдено в базі даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadTeachersToGrid();
                return;
            }

            using var dialog = new TeacherEditDialog(teacher);
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            // оновлюємо сутність
            teacher.FullName = dialog.Teacher.FullName;
            teacher.Phone = dialog.Teacher.Phone;
            teacher.Email = dialog.Teacher.Email;
            teacher.Position = dialog.Teacher.Position;
            teacher.GroupId = dialog.Teacher.GroupId;
            teacher.IsPrimary = dialog.Teacher.IsPrimary;

            if (teacher.IsPrimary)
            {
                var others = db.Teachers.Where(t => t.GroupId == teacher.GroupId && t.Id != teacher.Id && t.IsPrimary);
                foreach (var t in others) t.IsPrimary = false;
            }

            db.SaveChanges();

            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            LoadTeachersToGrid();
        }

        private void RemoveSelectedTeacher()
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            var row = dataGridView1.SelectedRows[0];
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id)) return;

            var confirm = MessageBox.Show(
                "Ви точно хочете видалити цього вчителя?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            using var db = new SchoolDbContext();
            var teacher = db.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null)
            {
                MessageBox.Show("Запис не знайдено в базі даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadTeachersToGrid();
                return;
            }

            // ВАЖЛИВО: у нас є залежності (Users, Grades), тому видаляємо в правильному порядку
            // 1) Оцінки, які виставляв цей вчитель
            var gradesToRemove = db.Grades.Where(g => g.TeacherId == id);
            db.Grades.RemoveRange(gradesToRemove);

            // 2) Обліковий запис вчителя
            var usersToRemove = db.Users.Where(u => u.TeacherId == id);
            db.Users.RemoveRange(usersToRemove);

            // 3) Сам вчитель
            db.Teachers.Remove(teacher);
            db.SaveChanges();

            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            LoadTeachersToGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Клік по рядку → коротка інформація
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            string id = row.Cells["Id"].Value?.ToString() ?? "";
            string name = row.Cells["FullName"].Value?.ToString() ?? "";
            string group = row.Cells["Group"].Value?.ToString() ?? "";
            string position = row.Cells["Position"].Value?.ToString() ?? "";
            string phone = row.Cells["Phone"].Value?.ToString() ?? "";
            string email = row.Cells["Email"].Value?.ToString() ?? "";
            string primary = row.Cells["Primary"].Value?.ToString() ?? "";

            MessageBox.Show(
                $"ID: {id}\n" +
                $"ПІБ: {name}\n" +
                $"Клас: {group}\n" +
                $"Посада: {position}\n" +
                $"Основний: {primary}\n" +
                $"Телефон: {phone}\n" +
                $"Email: {email}",
                "Інформація",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void educators_label_Click(object sender, EventArgs e)
        {
        }
    }
}
