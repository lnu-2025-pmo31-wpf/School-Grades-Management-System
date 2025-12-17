using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SchoolJournal.Data;

namespace SchoolJournal.Dialogs
{
    internal sealed class TeacherEditDialog : Form
    {
        private readonly TextBox _txtFullName = new();
        private readonly TextBox _txtPhone = new();
        private readonly TextBox _txtEmail = new();
        private readonly TextBox _txtPosition = new();
        private readonly ComboBox _cbGroup = new();
        private readonly CheckBox _chkPrimary = new();

        public TeacherData Teacher { get; private set; }

        public TeacherEditDialog(TeacherData? existing = null)
        {
            Text = existing == null ? "Додати вчителя" : "Редагувати вчителя";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(520, 320);

            Teacher = existing == null
                ? new TeacherData()
                : new TeacherData
                {
                    Id = existing.Id,
                    FullName = existing.FullName,
                    Phone = existing.Phone,
                    Email = existing.Email,
                    Position = existing.Position,
                    IsPrimary = existing.IsPrimary,
                    GroupId = existing.GroupId
                };

            BuildUi();
            LoadGroups();
            BindFromModel();
        }

        private void BuildUi()
        {
            var lbl1 = new Label { Text = "ПІБ", AutoSize = true, Location = new Point(20, 20) };
            _txtFullName.Location = new Point(160, 16);
            _txtFullName.Size = new Size(330, 27);

            var lbl2 = new Label { Text = "Телефон", AutoSize = true, Location = new Point(20, 60) };
            _txtPhone.Location = new Point(160, 56);
            _txtPhone.Size = new Size(330, 27);

            var lbl3 = new Label { Text = "Email", AutoSize = true, Location = new Point(20, 100) };
            _txtEmail.Location = new Point(160, 96);
            _txtEmail.Size = new Size(330, 27);

            var lbl4 = new Label { Text = "Посада", AutoSize = true, Location = new Point(20, 140) };
            _txtPosition.Location = new Point(160, 136);
            _txtPosition.Size = new Size(330, 27);

            var lbl5 = new Label { Text = "Клас", AutoSize = true, Location = new Point(20, 180) };
            _cbGroup.Location = new Point(160, 176);
            _cbGroup.Size = new Size(330, 28);
            _cbGroup.DropDownStyle = ComboBoxStyle.DropDownList;

            _chkPrimary.Text = "Класний керівник";
            _chkPrimary.AutoSize = true;
            _chkPrimary.Location = new Point(160, 220);

            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(280, 260), Size = new Size(100, 32) };
            var btnCancel = new Button { Text = "Скасувати", DialogResult = DialogResult.Cancel, Location = new Point(390, 260), Size = new Size(100, 32) };

            btnOk.Click += (_, __) =>
            {
                if (!TryBuildModelFromUi())
                {
                    DialogResult = DialogResult.None;
                }
            };

            Controls.AddRange(new Control[]
            {
                lbl1, _txtFullName,
                lbl2, _txtPhone,
                lbl3, _txtEmail,
                lbl4, _txtPosition,
                lbl5, _cbGroup,
                _chkPrimary,
                btnOk, btnCancel
            });

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private void LoadGroups()
        {
            using var db = new SchoolDbContext();
            var groups = db.Groups
                .OrderBy(g => g.Name)
                .Select(g => new { g.Id, g.Name })
                .ToList();

            _cbGroup.DataSource = groups;
            _cbGroup.DisplayMember = "Name";
            _cbGroup.ValueMember = "Id";
        }

        private void BindFromModel()
        {
            _txtFullName.Text = Teacher.FullName;
            _txtPhone.Text = Teacher.Phone;
            _txtEmail.Text = Teacher.Email;
            _txtPosition.Text = string.IsNullOrWhiteSpace(Teacher.Position) ? "Вчитель" : Teacher.Position;
            _chkPrimary.Checked = Teacher.IsPrimary;

            if (Teacher.GroupId != 0)
                _cbGroup.SelectedValue = Teacher.GroupId;
            else if (_cbGroup.Items.Count > 0)
                _cbGroup.SelectedIndex = 0;
        }

        private bool TryBuildModelFromUi()
        {
            var fullName = _txtFullName.Text.Trim();
            if (fullName.Length < 3)
            {
                MessageBox.Show("Введи коректне ПІБ (мінімум 3 символи).", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_cbGroup.SelectedValue is not int groupId || groupId <= 0)
            {
                MessageBox.Show("Обери клас.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var position = _txtPosition.Text.Trim();
            if (position.Length == 0) position = "Вчитель";

            Teacher.FullName = fullName;
            Teacher.Phone = _txtPhone.Text.Trim();
            Teacher.Email = _txtEmail.Text.Trim();
            Teacher.Position = position;
            Teacher.IsPrimary = _chkPrimary.Checked;
            Teacher.GroupId = groupId;

            return true;
        }
    }
}
