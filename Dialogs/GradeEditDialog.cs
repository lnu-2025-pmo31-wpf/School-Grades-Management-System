using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolJournal.Dialogs
{
    internal sealed class GradeEditDialog : Form
    {
        private readonly int _teacherId;
        private readonly int _studentId;

        private readonly TextBox _txtSubject = new();
        private readonly NumericUpDown _numValue = new();
        private readonly DateTimePicker _dtDate = new();
        private readonly TextBox _txtComment = new();

        public GradeData Grade { get; private set; }

        public GradeEditDialog(int teacherId, int studentId)
        {
            _teacherId = teacherId;
            _studentId = studentId;

            Text = "Додати оцінку";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(520, 320);

            Grade = new GradeData
            {
                TeacherId = _teacherId,
                StudentId = _studentId,
                DateAssigned = DateTime.Today
            };

            BuildUi();
        }

        private void BuildUi()
        {
            int xLbl = 20;
            int xCtrl = 160;

            var lbl1 = new Label { Text = "Предмет", AutoSize = true, Location = new Point(xLbl, 25) };
            _txtSubject.Location = new Point(xCtrl, 20);
            _txtSubject.Size = new Size(330, 27);

            var lbl2 = new Label { Text = "Оцінка", AutoSize = true, Location = new Point(xLbl, 65) };
            _numValue.Location = new Point(xCtrl, 60);
            _numValue.Size = new Size(120, 27);
            _numValue.Minimum = 1;
            _numValue.Maximum = 12;
            _numValue.Value = 10;

            var lbl3 = new Label { Text = "Дата", AutoSize = true, Location = new Point(xLbl, 105) };
            _dtDate.Location = new Point(xCtrl, 100);
            _dtDate.Size = new Size(160, 27);
            _dtDate.Format = DateTimePickerFormat.Short;

            var lbl4 = new Label { Text = "Коментар", AutoSize = true, Location = new Point(xLbl, 145) };
            _txtComment.Location = new Point(xCtrl, 140);
            _txtComment.Size = new Size(330, 100);
            _txtComment.Multiline = true;
            _txtComment.ScrollBars = ScrollBars.Vertical;

            var ok = new Button { Text = "Зберегти", DialogResult = DialogResult.OK, Location = new Point(290, 270), Size = new Size(100, 32) };
            var cancel = new Button { Text = "Скасувати", DialogResult = DialogResult.Cancel, Location = new Point(400, 270), Size = new Size(100, 32) };

            ok.Click += (_, __) =>
            {
                if (!TryBuildModel())
                    DialogResult = DialogResult.None;
            };

            Controls.AddRange(new Control[]
            {
                lbl1, _txtSubject,
                lbl2, _numValue,
                lbl3, _dtDate,
                lbl4, _txtComment,
                ok, cancel
            });

            AcceptButton = ok;
            CancelButton = cancel;
        }

        private bool TryBuildModel()
        {
            var subject = _txtSubject.Text.Trim();
            if (subject.Length < 2)
            {
                MessageBox.Show("Введіть предмет (мінімум 2 символи).", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Grade.Subject = subject;
            Grade.Value = (int)_numValue.Value;
            Grade.DateAssigned = _dtDate.Value.Date;
            Grade.Comment = _txtComment.Text.Trim();
            return true;
        }
    }
}
