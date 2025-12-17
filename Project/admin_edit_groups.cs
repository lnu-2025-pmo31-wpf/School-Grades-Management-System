using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SchoolJournal.Data;

namespace SchoolJournal
{
    public partial class admin_edit_groups : Form
    {
        private sealed class TeacherComboItem
        {
            public int Id { get; set; }
            public string FullName { get; set; } = string.Empty;
        }

        private readonly GroupData group;

        public admin_edit_groups(GroupData group)
        {
            InitializeComponent();
            this.group = group;

            SetupComboBoxes();
            Load += admin_edit_groups_Load;

            LoadGroupData();
        }

        private void admin_edit_groups_Load(object? sender, EventArgs e)
        {
            LoadTeachersCombo();
        }

        private void SetupComboBoxes()
        {
            age_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            count_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            teacher_comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadTeachersCombo()
        {
            using var db = new SchoolDbContext();

            var items = db.Teachers
                .OrderBy(t => t.FullName)
                .Select(t => new TeacherComboItem { Id = t.Id, FullName = t.FullName })
                .ToList();

            items.Insert(0, new TeacherComboItem { Id = 0, FullName = "— (не змінювати / не вибрано)" });

            teacher_comboBox1.DataSource = items;
            teacher_comboBox1.DisplayMember = nameof(TeacherComboItem.FullName);
            teacher_comboBox1.ValueMember = nameof(TeacherComboItem.Id);

            // Вибираємо основного вчителя цього класу (якщо є)
            int primaryTeacherId = db.Teachers
                .Where(t => t.GroupId == group.Id && t.IsPrimary)
                .Select(t => t.Id)
                .FirstOrDefault();

            teacher_comboBox1.SelectedValue = primaryTeacherId != 0 ? primaryTeacherId : 0;
        }

        private void LoadGroupData()
        {
            name_textBox.Text = group.Name;

            // ВАЖЛИВО: SelectedItem може не співпасти через різні тире "-" і "–".
            // Тому ставимо Text.
            age_comboBox.Text = group.AgeCategory;
            count_comboBox.Text = $"{group.MaxChildren} учнів";

            current_textBox.Text = group.CurrentChildren.ToString();
            room_textBox.Text = group.Room;
        }

        // === НАЗАД → admin_groups ===
        private void back_button_Click(object sender, EventArgs e)
        {
            admin_groups admin = new admin_groups();
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

        // === ЗБЕРЕГТИ ЗМІНИ ===
        private void edit_button_Click(object sender, EventArgs e)
        {
            string name = name_textBox.Text.Trim();
            string age = age_comboBox.Text.Trim();
            string countText = count_comboBox.Text;
            string room = room_textBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(age) ||
                string.IsNullOrWhiteSpace(countText) ||
                string.IsNullOrWhiteSpace(room))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(current_textBox.Text, out int currentChildren) || currentChildren < 0)
            {
                MessageBox.Show("Поле 'Поточна кількість' має бути числом (0 або більше).",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maxChildren;
            try
            {
                maxChildren = ParseCount(countText);
            }
            catch
            {
                MessageBox.Show("Некоректне значення 'Max Кількість'.",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (maxChildren <= 0)
            {
                MessageBox.Show("'Max Кількість' має бути більше 0.",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentChildren > maxChildren)
            {
                MessageBox.Show("Поточна кількість учнів не може бути більшою за максимальну.",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int teacherId = 0;
            if (teacher_comboBox1.SelectedValue is int id)
                teacherId = id;

            using var db = new SchoolDbContext();
            var entity = db.Groups.FirstOrDefault(g => g.Id == group.Id);

            if (entity == null)
            {
                MessageBox.Show("Групу не знайдено в базі даних.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            entity.Name = name;
            entity.AgeCategory = age;
            entity.MaxChildren = maxChildren;
            entity.CurrentChildren = currentChildren;
            entity.Room = room;

            // Якщо вибрали вчителя — робимо його основним для цього класу
            if (teacherId > 0)
            {
                var teacher = db.Teachers.FirstOrDefault(t => t.Id == teacherId);
                if (teacher != null)
                {
                    teacher.GroupId = entity.Id;
                    teacher.IsPrimary = true;

                    var others = db.Teachers.Where(t => t.GroupId == entity.Id && t.Id != teacher.Id && t.IsPrimary);
                    foreach (var t in others) t.IsPrimary = false;
                }
            }

            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            MessageBox.Show("Дані успішно оновлено!", "Успіх",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            admin_groups form = new admin_groups();
            form.Show();
            this.Hide();
        }

        // Витягуємо число з тексту типу "15 учнів"
        private int ParseCount(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            return int.Parse(new string(text.Where(char.IsDigit).ToArray()));
        }

        private void groups_label_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void name_textBox_TextChanged(object sender, EventArgs e) { }
        private void age_comboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void count_comboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void current_textBox_TextChanged(object sender, EventArgs e) { }
        private void teacher_comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void room_textBox_TextChanged(object sender, EventArgs e) { }
    }
}
