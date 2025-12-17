using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Data;

namespace SchoolJournal
{
    public partial class Form1 : Form
    {
        private readonly UserRole? _selectedRole;

        // Дані для входу тепер беруться з бази даних (таблиця Users)
        public Form1() : this(null) { }

        public Form1(UserRole? selectedRole)
        {
            _selectedRole = selectedRole;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void enter_button_Click(object sender, EventArgs e)
        {
            string login = logging_textBox.Text.Trim();
            string password = password_textBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Будь ласка, заповніть всі поля!",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            using var db = new SchoolDbContext();

            var user = db.Users
                .Include(u => u.Teacher)
                .Include(u => u.Child)
                .FirstOrDefault(u => u.Username == login);

            if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                MessageBox.Show(
                    "Неправильний логін або пароль!",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Якщо роль була обрана на першому екрані — не даємо зайти під іншою роллю.
            if (_selectedRole != null && user.Role != _selectedRole.Value)
            {
                MessageBox.Show(
                    $"Ви обрали роль: {_selectedRole.Value}, але цей акаунт має роль: {user.Role}.",
                    "Невідповідність ролі",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            Form nextForm;

            switch (user.Role)
            {
                case UserRole.Director:
                    nextForm = new administrator_form();
                    break;

                case UserRole.Teacher:
                    if (user.TeacherId == null)
                    {
                        MessageBox.Show("Для цього облікового запису не призначено вчителя.", "Помилка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    nextForm = new teacher_portal(user.TeacherId.Value);
                    break;

                case UserRole.Parent:
                    if (user.ChildId == null)
                    {
                        MessageBox.Show("Для цього облікового запису не призначено учня.", "Помилка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    nextForm = new parent_portal(user.ChildId.Value);
                    break;

                case UserRole.Student:
                    if (user.ChildId == null)
                    {
                        MessageBox.Show("Для цього облікового запису не призначено учня.", "Помилка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    nextForm = new student_portal(user.ChildId.Value);
                    break;

                default:
                    MessageBox.Show("Невідомий тип користувача.", "Помилка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            MessageBox.Show("Вхід успішний!", "Успіх",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Hide();
            nextForm.Show();
        }


       //Написати кращу логіку, добавити обмеження, закрити пароль зірочками, рішення при помилках і показ це вивід словами і що тре зробити 
        private void kinder_lebel_Click(object sender, EventArgs e)
        {

        }

        private void data_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logging_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void loggging_label_Click(object sender, EventArgs e)
        {

        }

        private void password_label_Click(object sender, EventArgs e)
        {

        }
    }
}



