namespace SchoolJournal
{
    partial class admin_groups
    {
        private const string V = "admin_groups";

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
            groups_label = new Label();
            exit_button = new Button();
            dataGridView1 = new DataGridView();
            add_groups_button = new Button();
            edit_button = new Button();
            remove_button = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // back_button
            // 
            back_button.Location = new Point(12, 12);
            back_button.Name = "back_button";
            back_button.Size = new Size(94, 29);
            back_button.TabIndex = 11;
            back_button.Text = "Назад";
            back_button.UseVisualStyleBackColor = true;
            back_button.Click += back_button_Click;
            // 
            // groups_label
            // 
            groups_label.AutoSize = true;
            groups_label.Font = new Font("Segoe UI", 20F);
            groups_label.Location = new Point(198, 37);
            groups_label.Name = "groups_label";
            groups_label.Size = new Size(343, 46);
            groups_label.TabIndex = 10;
            groups_label.Text = "Управління класами:";
            // 
            // exit_button
            // 
            exit_button.Location = new Point(694, 12);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(94, 29);
            exit_button.TabIndex = 9;
            exit_button.Text = "Вийти ";
            exit_button.UseVisualStyleBackColor = true;
            exit_button.Click += exit_button_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(104, 195);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(593, 188);
            dataGridView1.TabIndex = 12;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // add_groups_button
            // 
            add_groups_button.Font = new Font("Segoe UI", 15F);
            add_groups_button.Location = new Point(44, 110);
            add_groups_button.Name = "add_groups_button";
            add_groups_button.Size = new Size(178, 58);
            add_groups_button.TabIndex = 13;
            add_groups_button.Text = "Додати клас";
            add_groups_button.UseVisualStyleBackColor = true;
            add_groups_button.Click += add_groups_button_Click;
            // 
            // edit_button
            // 
            edit_button.Font = new Font("Segoe UI", 15F);
            edit_button.Location = new Point(310, 110);
            edit_button.Name = "edit_button";
            edit_button.Size = new Size(152, 58);
            edit_button.TabIndex = 14;
            edit_button.Text = "Редагувати";
            edit_button.UseVisualStyleBackColor = true;
            edit_button.Click += edit_button_Click;
            // 
            // remove_button
            // 
            remove_button.Font = new Font("Segoe UI", 15F);
            remove_button.Location = new Point(560, 110);
            remove_button.Name = "remove_button";
            remove_button.Size = new Size(152, 58);
            remove_button.TabIndex = 15;
            remove_button.Text = "Видалити";
            remove_button.UseVisualStyleBackColor = true;
            remove_button.Click += remove_button_Click;
            // 
            // admin_groups
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(remove_button);
            Controls.Add(edit_button);
            Controls.Add(add_groups_button);
            Controls.Add(dataGridView1);
            Controls.Add(back_button);
            Controls.Add(groups_label);
            Controls.Add(exit_button);
            Name = "admin_groups";
            Text = "Класи";
            Load += admin_groups_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button back_button;
        private Label groups_label;
        private Button exit_button;
        private DataGridView dataGridView1;
        private Button add_groups_button;
        private Button edit_button;
        private Button remove_button;
    }
}