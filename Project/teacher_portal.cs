using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Data;
using SchoolJournal.Dialogs;

namespace SchoolJournal
{
    /// <summary>
    /// Портал вчителя: бачить свій клас, може додавати учнів у клас і ставити оцінки.
    /// </summary>
    public sealed class teacher_portal : Form
    {
        private readonly int _teacherId;

        private Label _lblHeader = null!;
        private Button _btnLogout = null!;

        private DataGridView _classGrid = null!;
        private DataGridView _studentsGrid = null!;
        private DataGridView _gradesGrid = null!;

        private Button _btnAddStudent = null!;
        private Button _btnAddGrade = null!;

        public teacher_portal(int teacherId)
        {
            _teacherId = teacherId;

            Text = "Портал вчителя";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 650);

            BuildUi();
            Load += (_, __) => LoadData();
        }

        private void BuildUi()
        {
            _lblHeader = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(20, 15),
                Text = "Портал вчителя"
            };
            Controls.Add(_lblHeader);

            _btnLogout = new Button
            {
                Text = "Вийти",
                Size = new Size(100, 35),
                Location = new Point(960, 15)
            };
            _btnLogout.Click += (_, __) =>
            {
                var roles = new role_selection_form();
                roles.Show();
                Hide();
            };
            Controls.Add(_btnLogout);

            var lblClass = new Label { Text = "Мій клас:", AutoSize = true, Location = new Point(20, 60) };
            Controls.Add(lblClass);

            _classGrid = new DataGridView
            {
                Location = new Point(20, 85),
                Size = new Size(1040, 120),
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            _classGrid.SelectionChanged += (_, __) => LoadStudentsForSelectedClass();
            Controls.Add(_classGrid);

            var lblStudents = new Label { Text = "Учні у класі:", AutoSize = true, Location = new Point(20, 215) };
            Controls.Add(lblStudents);

            _studentsGrid = new DataGridView
            {
                Location = new Point(20, 240),
                Size = new Size(520, 320),
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            _studentsGrid.SelectionChanged += (_, __) => LoadGradesForSelectedStudent();
            Controls.Add(_studentsGrid);

            var lblGrades = new Label { Text = "Оцінки обраного учня:", AutoSize = true, Location = new Point(560, 215) };
            Controls.Add(lblGrades);

            _gradesGrid = new DataGridView
            {
                Location = new Point(560, 240),
                Size = new Size(500, 320),
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            Controls.Add(_gradesGrid);

            _btnAddStudent = new Button
            {
                Text = "Додати учня",
                Size = new Size(160, 35),
                Location = new Point(20, 570)
            };
            _btnAddStudent.Click += (_, __) => AddStudentToSelectedClass();
            Controls.Add(_btnAddStudent);

            _btnAddGrade = new Button
            {
                Text = "Додати оцінку",
                Size = new Size(160, 35),
                Location = new Point(190, 570)
            };
            _btnAddGrade.Click += (_, __) => AddGradeForSelectedStudent();
            Controls.Add(_btnAddGrade);
        }

        private void LoadData()
        {
            using var db = new SchoolDbContext();

            var teacher = db.Teachers
                .Include(t => t.Group)
                .FirstOrDefault(t => t.Id == _teacherId);

            if (teacher == null)
            {
                MessageBox.Show("Вчителя не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _lblHeader.Text = $"Вчитель: {teacher.FullName}";

            var classes = db.Teachers
                .Where(t => t.Id == _teacherId)
                .Include(t => t.Group)
                .Select(t => t.Group!)
                .ToList();

            var rows = classes.Select(g => new
            {
                g.Id,
                Class = g.Name,
                Info = g.AgeCategory,
                Students = $"{g.CurrentChildren}/{g.MaxChildren}",
                Teachers = g.Teacher,
                g.Room
            }).ToList();

            _classGrid.DataSource = rows;
            if (_classGrid.Columns["Id"] != null) _classGrid.Columns["Id"].HeaderText = "ID";
            if (_classGrid.Columns["Class"] != null) _classGrid.Columns["Class"].HeaderText = "Клас";
            if (_classGrid.Columns["Info"] != null) _classGrid.Columns["Info"].HeaderText = "Інфо";
            if (_classGrid.Columns["Students"] != null) _classGrid.Columns["Students"].HeaderText = "Учні";
            if (_classGrid.Columns["Teachers"] != null) _classGrid.Columns["Teachers"].HeaderText = "Вчителі";
            if (_classGrid.Columns["Room"] != null) _classGrid.Columns["Room"].HeaderText = "Кабінет";

            if (_classGrid.Rows.Count > 0)
                _classGrid.Rows[0].Selected = true;

            LoadStudentsForSelectedClass();
        }

        private int? SelectedClassId()
        {
            if (_classGrid.CurrentRow == null) return null;
            if (_classGrid.CurrentRow.Cells["Id"]?.Value is int id) return id;
            if (int.TryParse(_classGrid.CurrentRow.Cells["Id"]?.Value?.ToString(), out int parsed)) return parsed;
            return null;
        }

        private int? SelectedStudentId()
        {
            if (_studentsGrid.CurrentRow == null) return null;
            if (_studentsGrid.CurrentRow.Cells["Id"]?.Value is int id) return id;
            if (int.TryParse(_studentsGrid.CurrentRow.Cells["Id"]?.Value?.ToString(), out int parsed)) return parsed;
            return null;
        }

        private void LoadStudentsForSelectedClass()
        {
            int? classId = SelectedClassId();
            if (classId == null)
            {
                _studentsGrid.DataSource = null;
                _gradesGrid.DataSource = null;
                return;
            }

            using var db = new SchoolDbContext();

            var students = db.Children
                .Where(s => s.GroupId == classId.Value)
                .OrderBy(s => s.FullName)
                .Select(s => new
                {
                    s.Id,
                    s.FullName,
                    BirthDate = s.BirthDate.ToString("yyyy-MM-dd"),
                    Parent = s.ParentFullName,
                    Phone = s.ParentPhone
                })
                .ToList();

            _studentsGrid.DataSource = students;
            if (_studentsGrid.Columns["Id"] != null) _studentsGrid.Columns["Id"].HeaderText = "ID";
            if (_studentsGrid.Columns["FullName"] != null) _studentsGrid.Columns["FullName"].HeaderText = "ПІБ";
            if (_studentsGrid.Columns["BirthDate"] != null) _studentsGrid.Columns["BirthDate"].HeaderText = "Дата нар.";
            if (_studentsGrid.Columns["Parent"] != null) _studentsGrid.Columns["Parent"].HeaderText = "Батьки";
            if (_studentsGrid.Columns["Phone"] != null) _studentsGrid.Columns["Phone"].HeaderText = "Телефон";

            if (_studentsGrid.Rows.Count > 0)
                _studentsGrid.Rows[0].Selected = true;

            LoadGradesForSelectedStudent();
        }

        private void LoadGradesForSelectedStudent()
        {
            int? studentId = SelectedStudentId();
            if (studentId == null)
            {
                _gradesGrid.DataSource = null;
                return;
            }

            using var db = new SchoolDbContext();

            var grades = db.Grades
                .Where(g => g.StudentId == studentId.Value)
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

            _gradesGrid.DataSource = grades;
            if (_gradesGrid.Columns["Subject"] != null) _gradesGrid.Columns["Subject"].HeaderText = "Предмет";
            if (_gradesGrid.Columns["Grade"] != null) _gradesGrid.Columns["Grade"].HeaderText = "Оцінка";
            if (_gradesGrid.Columns["Date"] != null) _gradesGrid.Columns["Date"].HeaderText = "Дата";
            if (_gradesGrid.Columns["Teacher"] != null) _gradesGrid.Columns["Teacher"].HeaderText = "Вчитель";
            if (_gradesGrid.Columns["Comment"] != null) _gradesGrid.Columns["Comment"].HeaderText = "Коментар";
        }

        private void AddStudentToSelectedClass()
        {
            int? classId = SelectedClassId();
            if (classId == null)
            {
                MessageBox.Show("Оберіть клас.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new ChildEditDialog(existing: null, forcedGroupId: classId.Value);

            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            using var db = new SchoolDbContext();
            db.Children.Add(dialog.Child);
            db.SaveChanges();

            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            LoadStudentsForSelectedClass();
        }

        private void AddGradeForSelectedStudent()
        {
            int? studentId = SelectedStudentId();
            if (studentId == null)
            {
                MessageBox.Show("Оберіть учня.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new GradeEditDialog(_teacherId, studentId.Value);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            using var db = new SchoolDbContext();
            db.Grades.Add(dlg.Grade);
            db.SaveChanges();

            LoadGradesForSelectedStudent();
        }
    }
}
