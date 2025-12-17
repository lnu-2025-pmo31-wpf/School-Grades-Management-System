using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolJournal
{
    /// <summary>
    /// Перше вікно: вибір ролі. Далі відкриває форму логіну/паролю.
    /// </summary>
    public sealed class role_selection_form : Form
    {
        public role_selection_form()
        {
            Text = "Вибір ролі";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(560, 360);

            BuildUi();
        }

        private void BuildUi()
        {
            // Нормальна розмітка без "ручних" координат,
            // щоб елементи ніколи не накладались один на одного.
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                ColumnCount = 1,
                RowCount = 9
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));           // title
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));           // info
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));      // spacer
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));      // parent
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));      // student
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));      // teacher
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));      // director
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));      // filler
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));      // exit row

            var title = new Label
            {
                Text = "Оберіть роль для входу",
                AutoSize = true,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 6)
            };

            var info = new Label
            {
                Text = "Після вибору ролі відкриється вікно логіну та паролю.",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 0)
            };
            // Щоб текст красиво переносився і не ліз на кнопки
            info.MaximumSize = new Size(520, 0);

            var btnParent = MakeRoleButton("Батьки", UserRole.Parent);
            var btnStudent = MakeRoleButton("Учні", UserRole.Student);
            var btnTeacher = MakeRoleButton("Вчитель", UserRole.Teacher);
            var btnDirector = MakeRoleButton("Директор", UserRole.Director);

            var bottom = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Margin = new Padding(0)
            };

            var exit = new Button
            {
                Text = "Вийти",
                Size = new Size(120, 36),
                Margin = new Padding(0)
            };
            exit.Click += (_, __) => Close();
            bottom.Controls.Add(exit);

            layout.Controls.Add(title, 0, 0);
            layout.Controls.Add(info, 0, 1);
            layout.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, 2);
            layout.Controls.Add(btnParent, 0, 3);
            layout.Controls.Add(btnStudent, 0, 4);
            layout.Controls.Add(btnTeacher, 0, 5);
            layout.Controls.Add(btnDirector, 0, 6);
            layout.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, 7);
            layout.Controls.Add(bottom, 0, 8);

            Controls.Add(layout);
        }

        private Button MakeRoleButton(string text, UserRole role)
        {
            var btn = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12F),
                Margin = new Padding(0, 0, 0, 10)
            };
            btn.Click += (_, __) =>
            {
                var login = new Form1(role);
                login.Show();
                Hide();

                // якщо користувач закриє логін-форму — повертаємо роль-екран
                login.FormClosed += (_, __) => { if (!IsDisposed) Show(); };
            };

            return btn;
        }
    }
}
