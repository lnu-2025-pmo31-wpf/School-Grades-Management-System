using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SchoolJournal.Data;

namespace SchoolJournal.Dialogs
{
    internal sealed class ChildEditDialog : Form
    {
        private readonly TextBox _txtFullName = new();
        private readonly DateTimePicker _dtBirth = new();
        private readonly TextBox _txtParentName = new();
        private readonly TextBox _txtParentPhone = new();
        private readonly TextBox _txtAddress = new();
        private readonly TextBox _txtMedical = new();
        private readonly TextBox _txtNotes = new();
        private readonly ComboBox _cbGroup = new();

        public ChildData Child { get; private set; }

        private readonly int? _forcedGroupId;

        public ChildEditDialog(ChildData? existing = null, int? forcedGroupId = null)
        {
            _forcedGroupId = forcedGroupId;
            Text = existing == null ? "Додати учня" : "Редагувати учня";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(600, 480);

            Child = existing == null
                ? new ChildData()
                : new ChildData
                {
                    Id = existing.Id,
                    FullName = existing.FullName,
                    BirthDate = existing.BirthDate,
                    ParentFullName = existing.ParentFullName,
                    ParentPhone = existing.ParentPhone,
                    Address = existing.Address,
                    MedicalNotes = existing.MedicalNotes,
                    NotesForParents = existing.NotesForParents,
                    GroupId = existing.GroupId
                };

            BuildUi();
            LoadGroups();
            BindFromModel();

            if (_forcedGroupId.HasValue)
            {
                _cbGroup.SelectedValue = _forcedGroupId.Value;
                _cbGroup.Enabled = false;
            }
        }

        private void BuildUi()
        {
            int xLbl = 20;
            int xCtrl = 220;

            var lbl1 = new Label { Text = "ПІБ учня", AutoSize = true, Location = new Point(xLbl, 20) };
            _txtFullName.Location = new Point(xCtrl, 16);
            _txtFullName.Size = new Size(360, 27);

            var lbl2 = new Label { Text = "Дата народження", AutoSize = true, Location = new Point(xLbl, 60) };
            _dtBirth.Location = new Point(xCtrl, 56);
            _dtBirth.Size = new Size(220, 27);
            _dtBirth.Format = DateTimePickerFormat.Short;

            var lbl3 = new Label { Text = "ПІБ одного з батьків", AutoSize = true, Location = new Point(xLbl, 100) };
            _txtParentName.Location = new Point(xCtrl, 96);
            _txtParentName.Size = new Size(360, 27);

            var lbl4 = new Label { Text = "Телефон батьків", AutoSize = true, Location = new Point(xLbl, 140) };
            _txtParentPhone.Location = new Point(xCtrl, 136);
            _txtParentPhone.Size = new Size(220, 27);

            var lbl5 = new Label { Text = "Адреса", AutoSize = true, Location = new Point(xLbl, 180) };
            _txtAddress.Location = new Point(xCtrl, 176);
            _txtAddress.Size = new Size(360, 27);

            var lbl6 = new Label { Text = "Медичні примітки", AutoSize = true, Location = new Point(xLbl, 220) };
            _txtMedical.Location = new Point(xCtrl, 216);
            _txtMedical.Size = new Size(360, 80);
            _txtMedical.Multiline = true;
            _txtMedical.ScrollBars = ScrollBars.Vertical;

            var lblNotes = new Label { Text = "Примітки для батьків", AutoSize = true, Location = new Point(xLbl, 310) };
            _txtNotes.Location = new Point(xCtrl, 306);
            _txtNotes.Size = new Size(360, 60);
            _txtNotes.Multiline = true;
            _txtNotes.ScrollBars = ScrollBars.Vertical;

            var lbl7 = new Label { Text = "Клас", AutoSize = true, Location = new Point(xLbl, 390) };
            _cbGroup.Location = new Point(xCtrl, 386);
            _cbGroup.Size = new Size(360, 28);
            _cbGroup.DropDownStyle = ComboBoxStyle.DropDownList;

            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(380, 440), Size = new Size(90, 32) };
            var btnCancel = new Button { Text = "Скасувати", DialogResult = DialogResult.Cancel, Location = new Point(480, 440), Size = new Size(100, 32) };

            btnOk.Click += (_, __) =>
            {
                if (!TryBuildModelFromUi())
                    DialogResult = DialogResult.None;
            };

            Controls.AddRange(new Control[]
            {
                lbl1, _txtFullName,
                lbl2, _dtBirth,
                lbl3, _txtParentName,
                lbl4, _txtParentPhone,
                lbl5, _txtAddress,
                lbl6, _txtMedical,
                lblNotes, _txtNotes,
                lbl7, _cbGroup,
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
            _txtFullName.Text = Child.FullName;
            _dtBirth.Value = (Child.BirthDate.Year < 1900) ? DateTime.Today : Child.BirthDate;
            _txtParentName.Text = Child.ParentFullName;
            _txtParentPhone.Text = Child.ParentPhone;
            _txtAddress.Text = Child.Address;
            _txtMedical.Text = Child.MedicalNotes;
            _txtNotes.Text = Child.NotesForParents;

            if (Child.GroupId != 0)
                _cbGroup.SelectedValue = Child.GroupId;
            else if (_cbGroup.Items.Count > 0)
                _cbGroup.SelectedIndex = 0;

            if (_forcedGroupId != null && _forcedGroupId.Value > 0)
            {
                _cbGroup.SelectedValue = _forcedGroupId.Value;
                _cbGroup.Enabled = false;
            }
        }

        private bool TryBuildModelFromUi()
        {
            var name = _txtFullName.Text.Trim();
            if (name.Length < 3)
            {
                MessageBox.Show("Введи коректне ПІБ учня (мінімум 3 символи).", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_cbGroup.SelectedValue is not int groupId || groupId <= 0)
            {
                MessageBox.Show("Обери клас.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Child.FullName = name;
            Child.BirthDate = _dtBirth.Value.Date;
            Child.ParentFullName = _txtParentName.Text.Trim();
            Child.ParentPhone = _txtParentPhone.Text.Trim();
            Child.Address = _txtAddress.Text.Trim();
            Child.MedicalNotes = _txtMedical.Text.Trim();
            Child.GroupId = groupId;

            return true;
        }
    }
}
