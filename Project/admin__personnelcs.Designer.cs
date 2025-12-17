namespace SchoolJournal
{
    partial class educators_label
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
            dataGridView1 = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            Name = new DataGridViewTextBoxColumn();
            Group = new DataGridViewTextBoxColumn();
            Phone = new DataGridViewTextBoxColumn();
            Email = new DataGridViewTextBoxColumn();
            add_button = new Button();
            educ_label = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // back_button
            // 
            back_button.Location = new Point(12, 12);
            back_button.Name = "back_button";
            back_button.Size = new Size(94, 29);
            back_button.TabIndex = 22;
            back_button.Text = "Назад";
            back_button.UseVisualStyleBackColor = true;
            // 
            // exit_button
            // 
            exit_button.Location = new Point(694, 12);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(94, 29);
            exit_button.TabIndex = 20;
            exit_button.Text = "Вийти ";
            exit_button.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { id, Name, Group, Phone, Email });
            dataGridView1.Location = new Point(32, 61);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(732, 279);
            dataGridView1.TabIndex = 23;
            // 
            // id
            // 
            id.HeaderText = "ID";
            id.MinimumWidth = 6;
            id.Name = "id";
            id.Width = 50;
            // 
            // Name
            // 
            Name.HeaderText = "ПІБ";
            Name.MinimumWidth = 6;
            Name.Name = "Name";
            Name.Width = 210;
            // 
            // Group
            // 
            Group.HeaderText = "Клас";
            Group.MinimumWidth = 6;
            Group.Name = "Group";
            // 
            // Phone
            // 
            Phone.HeaderText = "Phone";
            Phone.MinimumWidth = 6;
            Phone.Name = "Phone";
            Phone.Width = 150;
            // 
            // Email
            // 
            Email.HeaderText = "Email";
            Email.MinimumWidth = 6;
            Email.Name = "Email";
            Email.Width = 170;
            // 
            // add_button
            // 
            add_button.Location = new Point(311, 370);
            add_button.Name = "add_button";
            add_button.Size = new Size(165, 38);
            add_button.TabIndex = 24;
            add_button.Text = "Додати вчителя";
            add_button.UseVisualStyleBackColor = true;
            // 
            // educ_label
            // 
            educ_label.AutoSize = true;
            educ_label.Font = new Font("Segoe UI", 20F);
            educ_label.Location = new Point(288, 9);
            educ_label.Name = "educ_label";
            educ_label.Size = new Size(188, 46);
            educ_label.TabIndex = 25;
            educ_label.Text = "Вчителі";
            // 
            // educators_label
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(educ_label);
            Controls.Add(add_button);
            Controls.Add(dataGridView1);
            Controls.Add(back_button);
            Controls.Add(exit_button);
            //Name = "educators_label";
            Text = " admin__personnel";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button back_button;
        private Button exit_button;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn Name;
        private DataGridViewTextBoxColumn Group;
        private DataGridViewTextBoxColumn Phone;
        private DataGridViewTextBoxColumn Email;
        private Button add_button;
        private Label educ_label;
    }
}