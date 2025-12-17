using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Data;

namespace SchoolJournal
{
    /// <summary>
    /// Портал учня: "Мої бали" та "Бали інших".
    /// </summary>
    public sealed class student_portal : Form
    {
        private readonly int _studentId;

        private Label _lblTitle = null!;
        private Label _lblAvg = null!;
        private Button _btnMy = null!;
        private Button _btnOthers = null!;
        private Button _btnLogout = null!;
        private DataGridView _grid = null!;

        public student_portal(int studentId)
        {
            _studentId = studentId;

            Text = "Портал учня";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 600);

            BuildUi();
            Load += (_, __) => LoadMyGrades();
        }

        private void BuildUi()
        {
            _lblTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(20, 15),
                Text = "Оцінки"
            };
            Controls.Add(_lblTitle);

            _lblAvg = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 48),
                Text = "Середній бал: —"
            };
            Controls.Add(_lblAvg);

            _btnLogout = new Button
            {
                Text = "Вийти",
                Size = new Size(100, 35),
                Location = new Point(860, 15)
            };
            _btnLogout.Click += (_, __) =>
            {
                var roles = new role_selection_form();
                roles.Show();
                Hide();
            };
            Controls.Add(_btnLogout);

            _btnMy = new Button
            {
                Text = "Мої бали",
                Size = new Size(140, 35),
                Location = new Point(20, 80)
            };
            _btnMy.Click += (_, __) => LoadMyGrades();
            Controls.Add(_btnMy);

            _btnOthers = new Button
            {
                Text = "Бали інших",
                Size = new Size(140, 35),
                Location = new Point(170, 80)
            };
            _btnOthers.Click += (_, __) => LoadOthersGrades();
            Controls.Add(_btnOthers);

            _grid = new DataGridView
            {
                Location = new Point(20, 130),
                Size = new Size(940, 410),
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            Controls.Add(_grid);
        }

        private void LoadMyGrades()
        {
            using var db = new SchoolDbContext();

            var student = db.Children
                .Include(s => s.Group)
                .FirstOrDefault(s => s.Id == _studentId);

            if (student == null)
            {
                MessageBox.Show("Учня не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _lblTitle.Text = $"Учень: {student.FullName}";

            var rows = db.Grades
                .Where(g => g.StudentId == _studentId)
                .Include(g => g.Teacher)
                .OrderByDescending(g => g.DateAssigned)
                .Select(g => new
                {
                    g.Subject,
                    Grade = g.Value,
                    Date = g.DateAssigned.ToString("yyyy-MM-dd"),
                    Teacher = g.Teacher != null ? g.Teacher.FullName : "—",
                    g.Comment
                })
                .ToList();

            _grid.DataSource = rows;
            SetGridHeaders(myMode: true);

            if (rows.Count == 0)
            {
                _lblAvg.Text = "Середній бал: —";
            }
            else
            {
                double avg = db.Grades.Where(g => g.StudentId == _studentId).Average(g => (double)g.Value);
                _lblAvg.Text = $"Середній бал: {avg:0.00}";
            }
        }

        private void LoadOthersGrades()
        {
            using var db = new SchoolDbContext();

            // Показуємо "інших" у межах того ж класу/класу.
            var me = db.Children.FirstOrDefault(s => s.Id == _studentId);
            if (me == null)
            {
                MessageBox.Show("Учня не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int groupId = me.GroupId;

            var rows = db.Grades
                .Where(g => g.Student.GroupId == groupId && g.StudentId != _studentId)
                .Include(g => g.Teacher)
                .Include(g => g.Student)
                .OrderByDescending(g => g.DateAssigned)
                .Select(g => new
                {
                    Student = g.Student != null ? g.Student.FullName : "—",
                    g.Subject,
                    Grade = g.Value,
                    Date = g.DateAssigned.ToString("yyyy-MM-dd"),
                    Teacher = g.Teacher != null ? g.Teacher.FullName : "—"
                })
                .ToList();

            _grid.DataSource = rows;
            SetGridHeaders(myMode: false);
            _lblAvg.Text = "Середній бал: —";
        }

        private void SetGridHeaders(bool myMode)
        {
            if (_grid.Columns["Student"] != null) _grid.Columns["Student"].HeaderText = "Учень";
            if (_grid.Columns["Subject"] != null) _grid.Columns["Subject"].HeaderText = "Предмет";
            if (_grid.Columns["Grade"] != null) _grid.Columns["Grade"].HeaderText = "Оцінка";
            if (_grid.Columns["Date"] != null) _grid.Columns["Date"].HeaderText = "Дата";
            if (_grid.Columns["Teacher"] != null) _grid.Columns["Teacher"].HeaderText = "Вчитель";
            if (_grid.Columns["Comment"] != null)
            {
                _grid.Columns["Comment"].HeaderText = "Коментар";
                _grid.Columns["Comment"].Visible = myMode; // коментар показуємо лише у "моїх".
            }
        }
    }
}
