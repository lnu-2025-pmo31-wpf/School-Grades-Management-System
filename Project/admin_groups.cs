using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Data;

namespace SchoolJournal
{
    public partial class admin_groups : Form
    {
        public admin_groups()
        {
            InitializeComponent();
        }

        private void admin_groups_Load(object sender, EventArgs e)
        {
            LoadGroupsToGrid();
        }

        private void LoadGroupsToGrid()
        {
            using var db = new SchoolDbContext();

            // На старті краще оновити кеш-значення (Teacher + CurrentChildren)
            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            var groups = db.Groups
                .OrderBy(g => g.Id)
                .Select(g => new
                {
                    g.Id,
                    g.Name,
                    g.AgeCategory,
                    ChildrenCount = $"{g.CurrentChildren}/{g.MaxChildren}",
                    Teacher = g.Teacher,
                    g.Room
                })
                .ToList();

            dataGridView1.DataSource = groups;
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

        // === ДОДАТИ ГРУПУ admin_add_groups ===
        private void add_groups_button_Click(object sender, EventArgs e)
        {
            admin_add_groups add = new admin_add_groups();
            add.Show();
            this.Hide();
        }

        // === Редагування класу admin_edit_groups ===
        private void edit_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            var row = dataGridView1.SelectedRows[0];
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id)) return;

            using var db = new SchoolDbContext();
            var group = db.Groups.FirstOrDefault(g => g.Id == id);

            if (group == null)
            {
                MessageBox.Show("Групу не знайдено в базі даних.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadGroupsToGrid();
                return;
            }

            admin_edit_groups editForm = new admin_edit_groups(group);
            editForm.Show();
            this.Hide();
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            var row = dataGridView1.SelectedRows[0];
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id)) return;

            var confirm = MessageBox.Show(
                "Ви точно хочете видалити цей клас?\n\nУВАГА: будуть видалені і учні/вчителі цього класу.",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            using var db = new SchoolDbContext();
            var group = db.Groups
                .Include(g => g.Children)
                .Include(g => g.Teachers)
                .FirstOrDefault(g => g.Id == id);

            if (group == null)
            {
                MessageBox.Show("Групу не знайдено в базі даних.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadGroupsToGrid();
                return;
            }

            // ВАЖЛИВО: у нас є залежності (Users, Grades), тому видаляємо в правильному порядку

            var childIds = group.Children.Select(c => c.Id).ToList();
            var teacherIds = group.Teachers.Select(t => t.Id).ToList();

            // 1) Облікові записи (учні/батьки/вчителі)
            var usersToRemove = db.Users.Where(u =>
                (u.ChildId.HasValue && childIds.Contains(u.ChildId.Value)) ||
                (u.TeacherId.HasValue && teacherIds.Contains(u.TeacherId.Value)));
            db.Users.RemoveRange(usersToRemove);

            // 2) Оцінки (прив'язані і до учня, і до вчителя)
            var gradesToRemove = db.Grades.Where(gr =>
                childIds.Contains(gr.StudentId) ||
                teacherIds.Contains(gr.TeacherId));
            db.Grades.RemoveRange(gradesToRemove);

            // 3) Учні та вчителі цього класу
            db.Children.RemoveRange(group.Children);
            db.Teachers.RemoveRange(group.Teachers);

            // 4) Сам клас
            db.Groups.Remove(group);

            db.SaveChanges();

            LoadGroupsToGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            string id = row.Cells["Id"].Value?.ToString() ?? "";
            string name = row.Cells["Name"].Value?.ToString() ?? "";
            string age = row.Cells["AgeCategory"].Value?.ToString() ?? "";
            string count = row.Cells["ChildrenCount"].Value?.ToString() ?? "";
            string teacher = row.Cells["Teacher"].Value?.ToString() ?? "";
            string room = row.Cells["Room"].Value?.ToString() ?? "";

            MessageBox.Show(
                $"ID: {id}\n" +
                $"Назва класу: {name}\n" +
                $"Вікова категорія: {age}\n" +
                $"Учні: {count}\n" +
                $"Вчителі: {teacher}\n" +
                $"Кабінет: {room}",
                "Інформація про клас",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
