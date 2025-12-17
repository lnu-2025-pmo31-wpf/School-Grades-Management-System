namespace SchoolJournal
{
    partial class admin_kinder
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
            kinder_label = new Label();
            exit_button = new Button();
            txt_search = new TextBox();
            button_search = new Button();
            label_search = new Label();
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
            // kinder_label
            // 
            kinder_label.AutoSize = true;
            kinder_label.Font = new Font("Segoe UI", 20F);
            kinder_label.Location = new Point(231, 12);
            kinder_label.Name = "kinder_label";
            kinder_label.Size = new Size(326, 46);
            kinder_label.TabIndex = 21;
            kinder_label.Text = "Управління учнями  ";
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
            // txt_search
            // 
            txt_search.Font = new Font("Segoe UI", 15F);
            txt_search.Location = new Point(154, 88);
            txt_search.Name = "txt_search";
            txt_search.Size = new Size(222, 41);
            txt_search.TabIndex = 23;
            // 
            // button_search
            // 
            button_search.Font = new Font("Segoe UI", 15F);
            button_search.Location = new Point(382, 86);
            button_search.Name = "button_search";
            button_search.Size = new Size(121, 43);
            button_search.TabIndex = 24;
            button_search.Text = "Ok";
            button_search.UseVisualStyleBackColor = true;
            // 
            // label_search
            // 
            label_search.AutoSize = true;
            label_search.Font = new Font("Segoe UI", 15F);
            label_search.Location = new Point(56, 86);
            label_search.Name = "label_search";
            label_search.Size = new Size(92, 35);
            label_search.TabIndex = 25;
            label_search.Text = "Пошук";
            // 
            // admin_kinder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label_search);
            Controls.Add(button_search);
            Controls.Add(txt_search);
            Controls.Add(back_button);
            Controls.Add(kinder_label);
            Controls.Add(exit_button);
            Name = "admin_kinder";
            Text = "Учні";
            Load += admin_kinder_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button back_button;
        private Label kinder_label;
        private Button exit_button;
        private TextBox txt_search;
        private Button button_search;
        private Label label_search;
    }
}