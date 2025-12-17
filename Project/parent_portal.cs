using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Data;

namespace SchoolJournal
{
    public sealed class parent_portal : Form
    {
        private readonly int _childId;

        private Label _lblTitle = null!;
        private Label _lblAvg = null!;
        private Button _btnLogout = null!;
        private TextBox _txtInfo = null!;
        private DataGridView _grid = null!;

        public parent_portal(int childId)
        {
            _childId = childId;

            Text = "Портал батьків";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 500);

            BuildUi();
            Load += (_, __) => LoadData();
        }

        private void BuildUi()
        {
            _lblTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(20, 15),
                Text = "Інформація про учня"
            };
            Controls.Add(_lblTitle);

            _lblAvg = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 45),
                Text = "Середній бал: —"
            };
            Controls.Add(_lblAvg);

            _btnLogout = new Button
            {
                Text = "Вийти",
                Size = new Size(100, 35),
                Location = new Point(660, 15)
            };
            _btnLogout.Click += (_, __) =>
            {
                var roles = new role_selection_form();
                roles.Show();
                Hide();
            };
            Controls.Add(_btnLogout);

            _txtInfo = new TextBox
            {
                Location = new Point(20, 75),
                Size = new Size(740, 90),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(_txtInfo);

            _grid = new DataGridView
            {
                Location = new Point(20, 175),
                Size = new Size(740, 265),
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            Controls.Add(_grid);
        }

        private void LoadData()
        {
            using var db = new SchoolDbContext();

            var child = db.Children
                .Include(c => c.Group)
                .FirstOrDefault(c => c.Id == _childId);

            if (child == null)
            {
                MessageBox.Show("Дитину не знайдено.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string groupName = child.Group?.Name ?? "—";

            _lblTitle.Text = $"Учень: {child.FullName}";


            _txtInfo.Text =
                $"Учень: {child.FullName}\r\n" +
                $"Клас: {groupName}\r\n" +
                $"Контакти батьків: {child.ParentFullName} / {child.ParentPhone}\r\n";

            var grades = db.Grades
                .Where(g => g.StudentId == _childId)
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

            _grid.DataSource = grades;
            if (_grid.Columns["Subject"] != null) _grid.Columns["Subject"].HeaderText = "Предмет";
            if (_grid.Columns["Grade"] != null) _grid.Columns["Grade"].HeaderText = "Оцінка";
            if (_grid.Columns["Date"] != null) _grid.Columns["Date"].HeaderText = "Дата";
            if (_grid.Columns["Teacher"] != null) _grid.Columns["Teacher"].HeaderText = "Вчитель";
            if (_grid.Columns["Comment"] != null) _grid.Columns["Comment"].HeaderText = "Коментар";

            if (grades.Count == 0)
            {
                _lblAvg.Text = "Середній бал: —";
            }
            else
            {
                double avg = db.Grades.Where(g => g.StudentId == _childId).Average(g => (double)g.Value);
                _lblAvg.Text = $"Середній бал: {avg:0.00}";
            }
        }
    }
}
