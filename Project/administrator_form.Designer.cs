namespace SchoolJournal
{
    partial class administrator_form
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
            administrator_label = new Label();
            educator_button = new Button();
            groups_button = new Button();
            kinder_button = new Button();
            exit_button = new Button();
            SuspendLayout();
            // 
            // administrator_label
            // 
            administrator_label.AutoSize = true;
            administrator_label.Font = new Font("Segoe UI", 20F);
            administrator_label.Location = new Point(168, 42);
            administrator_label.Name = "administrator_label";
            administrator_label.Size = new Size(242, 46);
            administrator_label.TabIndex = 0;
            administrator_label.Text = "Директор";
            administrator_label.Click += administrator_label_Click;
            // 
            // educator_button
            // 
            educator_button.Font = new Font("Segoe UI", 15F);
            educator_button.Location = new Point(28, 126);
            educator_button.Name = "educator_button";
            educator_button.Size = new Size(152, 58);
            educator_button.TabIndex = 1;
            educator_button.Text = "Персонал";
            educator_button.UseVisualStyleBackColor = true;
            educator_button.Click += educator_button_Click;
            // 
            // groups_button
            // 
            groups_button.Font = new Font("Segoe UI", 15F);
            groups_button.Location = new Point(216, 126);
            groups_button.Name = "groups_button";
            groups_button.Size = new Size(152, 58);
            groups_button.TabIndex = 2;
            groups_button.Text = "Класи";
            groups_button.UseVisualStyleBackColor = true;
            groups_button.Click += groups_button_Click;
            // 
            // kinder_button
            // 
            kinder_button.Font = new Font("Segoe UI", 15F);
            kinder_button.Location = new Point(408, 126);
            kinder_button.Name = "kinder_button";
            kinder_button.Size = new Size(152, 58);
            kinder_button.TabIndex = 3;
            kinder_button.Text = "Учні";
            kinder_button.UseVisualStyleBackColor = true;
            kinder_button.Click += kinder_button_Click;
            // 
            // exit_button
            // 
            exit_button.Location = new Point(526, 12);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(94, 29);
            exit_button.TabIndex = 4;
            exit_button.Text = "Вийти ";
            exit_button.UseVisualStyleBackColor = true;
            exit_button.Click += exit_button_Click;
            // 
            // administrator_form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 553);
            Controls.Add(exit_button);
            Controls.Add(kinder_button);
            Controls.Add(groups_button);
            Controls.Add(educator_button);
            Controls.Add(administrator_label);
            Name = "administrator_form";
            Text = "Панель директора";
            Load += administrator_form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label administrator_label;
        private Button educator_button;
        private Button groups_button;
        private Button kinder_button;
        private Button exit_button;
    }
}