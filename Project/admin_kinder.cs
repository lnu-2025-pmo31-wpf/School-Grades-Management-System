using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Data;
using SchoolJournal.Dialogs;

namespace SchoolJournal
{
    public partial class admin_kinder : Form
    {
        private DataGridView _grid = null!;
        private Button _addButton = null!;
        private Button _editButton = null!;
        private Button _removeButton = null!;

        public admin_kinder()
        {
            InitializeComponent();
            BuildExtraUi();

            // події кнопок які в дизайнері не були підключені
            back_button.Click += back_button_Click;
            exit_button.Click += exit_button_Click;
            button_search.Click += button_search_Click;

            _grid.CellDoubleClick += (_, __) => EditSelectedChild();
            _grid.KeyDown += Grid_KeyDown;
        }

        private void BuildExtraUi()
        {
            _grid = new DataGridView
            {
                Location = new Point(12, 150),
                Size = new Size(776, 220),
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            _grid.CellContentClick += Grid_CellContentClick;
            Controls.Add(_grid);

            _addButton = new Button
            {
                Text = "Додати дитину",
                Font = new Font("Segoe UI", 12F),
                Size = new Size(180, 40),
                Location = new Point(120, 390)
            };
            _addButton.Click += (_, __) => AddChild();

            _editButton = new Button
            {
                Text = "Редагувати",
                Font = _addButton.Font,
                Size = new Size(180, 40),
                Location = new Point(310, 390)
            };
            _editButton.Click += (_, __) => EditSelectedChild();

            _removeButton = new Button
            {
                Text = "Видалити",
                Font = _addButton.Font,
                Size = new Size(180, 40),
                Location = new Point(500, 390)
            };
            _removeButton.Click += (_, __) => RemoveSelectedChild();

            Controls.AddRange(new Control[] { _addButton, _editButton, _removeButton });
        }

        private void admin_kinder_Load(object sender, EventArgs e)
        {
            LoadKidsToGrid();
        }

        private void LoadKidsToGrid(string? filter = null)
        {
            using var db = new SchoolDbContext();

            var query = db.Children
                .Include(c => c.Group)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.Trim();
                query = query.Where(c => c.FullName.Contains(filter));
            }

            var rows = query
                .OrderBy(c => c.Id)
                .Select(c => new
                {
                    c.Id,
                    FullName = c.FullName,
                    BirthDate = c.BirthDate.ToString("yyyy-MM-dd"),
                    Group = c.Group != null ? c.Group.Name : "—",
                    Parent = c.ParentFullName,
                    Phone = c.ParentPhone,
                    Address = c.Address,
                    Medical = c.MedicalNotes,
                    Notes = c.NotesForParents
                })
                .ToList();

            _grid.DataSource = rows;

            if (_grid.Columns["FullName"] != null) _grid.Columns["FullName"].HeaderText = "ПІБ";
            if (_grid.Columns["BirthDate"] != null) _grid.Columns["BirthDate"].HeaderText = "Дата нар.";
            if (_grid.Columns["Group"] != null) _grid.Columns["Group"].HeaderText = "Клас";
            if (_grid.Columns["Parent"] != null) _grid.Columns["Parent"].HeaderText = "Батьки";
            if (_grid.Columns["Phone"] != null) _grid.Columns["Phone"].HeaderText = "Телефон";
            if (_grid.Columns["Medical"] != null) _grid.Columns["Medical"].HeaderText = "Примітки";
        }

        private void Grid_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedChild();
                e.Handled = true;
            }
        }

        private void button_search_Click(object? sender, EventArgs e)
        {
            LoadKidsToGrid(txt_search.Text);
        }

        private void AddChild()
        {
            using var dialog = new ChildEditDialog();
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            using var db = new SchoolDbContext();
            db.Children.Add(dialog.Child);
            db.SaveChanges();

            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            LoadKidsToGrid(txt_search.Text);
        }

        private void EditSelectedChild()
        {
            if (_grid.SelectedRows.Count <= 0) return;
            var row = _grid.SelectedRows[0];
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id)) return;

            using var db = new SchoolDbContext();
            var child = db.Children.FirstOrDefault(c => c.Id == id);
            if (child == null)
            {
                MessageBox.Show("Запис не знайдено в базі даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadKidsToGrid(txt_search.Text);
                return;
            }

            using var dialog = new ChildEditDialog(child);
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            child.FullName = dialog.Child.FullName;
            child.BirthDate = dialog.Child.BirthDate;
            child.ParentFullName = dialog.Child.ParentFullName;
            child.ParentPhone = dialog.Child.ParentPhone;
            child.Address = dialog.Child.Address;
            child.MedicalNotes = dialog.Child.MedicalNotes;
            child.GroupId = dialog.Child.GroupId;

            db.SaveChanges();

            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            LoadKidsToGrid(txt_search.Text);
        }

        private void RemoveSelectedChild()
        {
            if (_grid.SelectedRows.Count <= 0) return;
            var row = _grid.SelectedRows[0];
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id)) return;

            var confirm = MessageBox.Show(
                "Ви точно хочете видалити цей запис?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            using var db = new SchoolDbContext();
            var child = db.Children.FirstOrDefault(c => c.Id == id);
            if (child == null)
            {
                MessageBox.Show("Запис не знайдено в базі даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadKidsToGrid(txt_search.Text);
                return;
            }

            db.Children.Remove(child);
            db.SaveChanges();

            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            LoadKidsToGrid(txt_search.Text);
        }

        private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = _grid.Rows[e.RowIndex];
            string id = row.Cells["Id"].Value?.ToString() ?? "";
            string name = row.Cells["FullName"].Value?.ToString() ?? "";
            string bday = row.Cells["BirthDate"].Value?.ToString() ?? "";
            string group = row.Cells["Group"].Value?.ToString() ?? "";
            string parent = row.Cells["Parent"].Value?.ToString() ?? "";
            string phone = row.Cells["Phone"].Value?.ToString() ?? "";
            string address = row.Cells["Address"].Value?.ToString() ?? "";
            string medical = row.Cells["Medical"].Value?.ToString() ?? "";

            MessageBox.Show(
                $"ID: {id}\n" +
                $"ПІБ: {name}\n" +
                $"Дата нар.: {bday}\n" +
                $"Клас: {group}\n" +
                $"Батьки: {parent}\n" +
                $"Телефон: {phone}\n" +
                $"Адреса: {address}\n" +
                $"Примітки: {medical}",
                "Інформація",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void back_button_Click(object? sender, EventArgs e)
        {
            administrator_form admin = new administrator_form();
            admin.Show();
            this.Hide();
        }

        private void exit_button_Click(object? sender, EventArgs e)
        {
            var roles = new role_selection_form();
            roles.Show();
            this.Hide();
        }
    }
}
