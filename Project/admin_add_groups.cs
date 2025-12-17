using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SchoolJournal.Data;

namespace SchoolJournal
{
    public partial class admin_add_groups : Form
    {
        private sealed class TeacherComboItem
        {
            public int Id { get; set; }
            public string FullName { get; set; } = string.Empty;
        }

        public admin_add_groups()
        {
            InitializeComponent();
            SetupComboBoxes();
            Load += admin_add_groups_Load;
        }

        private void admin_add_groups_Load(object? sender, EventArgs e)
        {
            LoadTeachersCombo();
        }

        private void SetupComboBoxes()
        {
            // Важливо: коли ComboBox у режимі DropDown, користувач може ввести текст,
            // але SelectedItem залишиться null. Через це й з'являлась помилка "заповніть усі поля".
            age_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            count_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            teacher_comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            if (age_comboBox.SelectedIndex < 0 && age_comboBox.Items.Count > 0)
                age_comboBox.SelectedIndex = 0;

            if (count_comboBox.SelectedIndex < 0 && count_comboBox.Items.Count > 0)
                count_comboBox.SelectedIndex = 0;
        }

        private void LoadTeachersCombo()
        {
            using var db = new SchoolDbContext();

            var items = db.Teachers
                .OrderBy(t => t.FullName)
                .Select(t => new TeacherComboItem { Id = t.Id, FullName = t.FullName })
                .ToList();

            // Можна створити клас навіть якщо ще нема вчителів у БД
            items.Insert(0, new TeacherComboItem { Id = 0, FullName = "— (не вибрано)" });

            teacher_comboBox1.DataSource = items;
            teacher_comboBox1.DisplayMember = nameof(TeacherComboItem.FullName);
            teacher_comboBox1.ValueMember = nameof(TeacherComboItem.Id);
            teacher_comboBox1.SelectedValue = 0;
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

        // === ДОДАТИ ГРУПУ ===
        private void add_button_Click(object sender, EventArgs e)
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
                MessageBox.Show("Будь ласка, заповніть усі поля (крім вчителя — він необов'язковий на цьому кроці).",
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

            int teacherId = 0;
            if (teacher_comboBox1.SelectedValue is int id)
                teacherId = id;

            using var db = new SchoolDbContext();

            // Створюємо клас
            var newGroup = new GroupData
            {
                Name = name,
                AgeCategory = age,
                MaxChildren = maxChildren,
                CurrentChildren = 0,
                Teacher = "—",
                Room = room
            };

            db.Groups.Add(newGroup);
            db.SaveChanges();

            // Якщо вибрали вчителя — "прикріплюємо" його до нового класу як основного
            if (teacherId > 0)
            {
                var teacher = db.Teachers.FirstOrDefault(t => t.Id == teacherId);
                if (teacher != null)
                {
                    teacher.GroupId = newGroup.Id;
                    teacher.IsPrimary = true;

                    // У класі може бути лише один "основний" вчитель
                    var others = db.Teachers.Where(t => t.GroupId == newGroup.Id && t.Id != teacher.Id && t.IsPrimary);
                    foreach (var t in others) t.IsPrimary = false;
                }
            }

            // Оновлюємо кеш-поля в Groups (Teacher/CurrentChildren)
            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            MessageBox.Show("Клас успішно додано!",
                "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

            admin_groups groupsPage = new admin_groups();
            groupsPage.Show();
            this.Hide();
        }

        // Перетворює "15 учнів" → 15
        private int ParseCount(string text)
        {
            return int.Parse(new string(text.Where(char.IsDigit).ToArray()));
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void name_textBox_TextChanged(object sender, EventArgs e) { }
        private void age_comboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void count_comboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void teacher_comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void room_textBox_TextChanged(object sender, EventArgs e) { }
    }
}
