namespace SchoolJournal
{
    partial class admin__groups
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            back_button = new Button();
            exit_button = new Button();
            grou_label = new Label();
            groups_button = new Button();
            edit_button = new Button();
            remove_button = new Button();
            dataGridView1 = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Name = new DataGridViewTextBoxColumn();
            AgeCategory = new DataGridViewTextBoxColumn();
            ChildrenCount = new DataGridViewTextBoxColumn();
            Teacher = new DataGridViewTextBoxColumn();
            Room = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // back_button
            // 
            back_button.Location = new Point(12, 12);
            back_button.Name = "back_button";
            back_button.Size = new Size(94, 29);
            back_button.TabIndex = 25;
            back_button.Text = "Назад";
            back_button.UseVisualStyleBackColor = true;
            // 
            // exit_button
            // 
            exit_button.Location = new Point(694, 12);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(94, 29);
            exit_button.TabIndex = 23;
            exit_button.Text = "Вийти ";
            exit_button.UseVisualStyleBackColor = true;
            // 
            // grou_label
            // 
            grou_label.AutoSize = true;
            grou_label.Font = new Font("Segoe UI", 20F);
            grou_label.Location = new Point(218, 12);
            grou_label.Name = "grou_label";
            grou_label.Size = new Size(336, 46);
            grou_label.TabIndex = 26;
            grou_label.Text = "Управління класами";
            // 
            // groups_button
            // 
            groups_button.Location = new Point(107, 75);
            groups_button.Name = "groups_button";
            groups_button.Size = new Size(128, 35);
            groups_button.TabIndex = 27;
            groups_button.Text = "Додати клас";
            groups_button.UseVisualStyleBackColor = true;
            // 
            // edit_button
            // 
            edit_button.Location = new Point(328, 75);
            edit_button.Name = "edit_button";
            edit_button.Size = new Size(123, 34);
            edit_button.TabIndex = 28;
            edit_button.Text = "Редагувати";
            edit_button.UseVisualStyleBackColor = true;
            // 
            // remove_button
            // 
            remove_button.Location = new Point(559, 78);
            remove_button.Name = "remove_button";
            remove_button.Size = new Size(105, 29);
            remove_button.TabIndex = 29;
            remove_button.Text = "Видалити";
            remove_button.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Id, Name, AgeCategory, ChildrenCount, Teacher, Room });
            dataGridView1.Location = new Point(84, 143);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(617, 238);
            dataGridView1.TabIndex = 30;
            // 
            // Id
            // 
            Id.HeaderText = "ID";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Width = 50;
            // 
            // Name
            // 
            Name.HeaderText = "Назва";
            Name.MinimumWidth = 6;
            Name.Name = "Name";
            Name.Width = 125;
            // 
            // AgeCategory
            // 
            AgeCategory.HeaderText = "Вік";
            AgeCategory.MinimumWidth = 6;
            AgeCategory.Name = "AgeCategory";
            AgeCategory.Width = 70;
            // 
            // ChildrenCount
            // 
            ChildrenCount.HeaderText = "Діти";
            ChildrenCount.MinimumWidth = 6;
            ChildrenCount.Name = "ChildrenCount";
            ChildrenCount.Width = 70;
            // 
            // Teacher
            // 
            Teacher.HeaderText = "Вчитель";
            Teacher.MinimumWidth = 6;
            Teacher.Name = "Teacher";
            Teacher.Width = 125;
            // 
            // Room
            // 
            Room.HeaderText = "Кімната";
            Room.MinimumWidth = 6;
            Room.Name = "Room";
            Room.Width = 125;
            // 
            // admin__groups
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(remove_button);
            Controls.Add(edit_button);
            Controls.Add(groups_button);
            Controls.Add(grou_label);
            Controls.Add(back_button);
            Controls.Add(exit_button);
            //Name = "admin__groups";
            Text = "Класи";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button back_button;
        private Button exit_button;
        private Label grou_label;
        private Button groups_button;
        private Button edit_button;
        private Button remove_button;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Name;
        private DataGridViewTextBoxColumn AgeCategory;
        private DataGridViewTextBoxColumn ChildrenCount;
        private DataGridViewTextBoxColumn Teacher;
        private DataGridViewTextBoxColumn Room;
    }
}