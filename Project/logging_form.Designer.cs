namespace SchoolJournal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            kinder_lebel = new Label();
            loggging_label = new Label();
            password_label = new Label();
            logging_textBox = new TextBox();
            password_textBox = new TextBox();
            enter_button = new Button();
            data_panel = new Panel();
            data_panel.SuspendLayout();
            SuspendLayout();
            // 
            // kinder_lebel
            // 
            kinder_lebel.AutoSize = true;
            kinder_lebel.Font = new Font("Segoe UI", 20F);
            kinder_lebel.Location = new Point(183, 21);
            kinder_lebel.Name = "kinder_lebel";
            kinder_lebel.Size = new Size(235, 46);
            kinder_lebel.TabIndex = 0;
            kinder_lebel.Text = "Електронний журнал";
            kinder_lebel.Click += kinder_lebel_Click;
            // 
            // loggging_label
            // 
            loggging_label.AutoSize = true;
            loggging_label.Font = new Font("Segoe UI", 15F);
            loggging_label.Location = new Point(46, 44);
            loggging_label.Name = "loggging_label";
            loggging_label.Size = new Size(82, 35);
            loggging_label.TabIndex = 1;
            loggging_label.Text = "Логін:";
            loggging_label.Click += loggging_label_Click;
            // 
            // password_label
            // 
            password_label.AutoSize = true;
            password_label.Font = new Font("Segoe UI", 15F);
            password_label.Location = new Point(46, 97);
            password_label.Name = "password_label";
            password_label.Size = new Size(107, 35);
            password_label.TabIndex = 2;
            password_label.Text = "Пароль:";
            password_label.Click += password_label_Click;
            // 
            // logging_textBox
            // 
            logging_textBox.Font = new Font("Segoe UI", 15F);
            logging_textBox.Location = new Point(163, 38);
            logging_textBox.Name = "logging_textBox";
            logging_textBox.Size = new Size(263, 41);
            logging_textBox.TabIndex = 3;
            logging_textBox.TextChanged += logging_textBox_TextChanged;
            // 
            // password_textBox
            // 
            password_textBox.Font = new Font("Segoe UI", 15F);
            password_textBox.Location = new Point(163, 91);
            password_textBox.Name = "password_textBox";
            password_textBox.Size = new Size(263, 41);
            password_textBox.TabIndex = 4;
            password_textBox.UseSystemPasswordChar = true;
            password_textBox.TextChanged += password_textBox_TextChanged;
            // 
            // enter_button
            // 
            enter_button.Font = new Font("Segoe UI", 15F);
            enter_button.Location = new Point(244, 414);
            enter_button.Name = "enter_button";
            enter_button.Size = new Size(113, 44);
            enter_button.TabIndex = 7;
            enter_button.Text = "Увійти";
            enter_button.UseVisualStyleBackColor = true;
            enter_button.Click += enter_button_Click;
            // 
            // data_panel
            // 
            data_panel.BackColor = SystemColors.ControlLightLight;
            data_panel.Controls.Add(loggging_label);
            data_panel.Controls.Add(logging_textBox);
            data_panel.Controls.Add(password_label);
            data_panel.Controls.Add(password_textBox);
            data_panel.Location = new Point(61, 142);
            data_panel.Name = "data_panel";
            data_panel.Size = new Size(511, 238);
            data_panel.TabIndex = 8;
            data_panel.Paint += data_panel_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 553);
            Controls.Add(data_panel);
            Controls.Add(enter_button);
            Controls.Add(kinder_lebel);
            Name = "Form1";
            Text = "Вхід";
            Load += Form1_Load;
            data_panel.ResumeLayout(false);
            data_panel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label kinder_lebel;
        private Label loggging_label;
        private Label password_label;
        private TextBox logging_textBox;
        private TextBox password_textBox;
        private Button enter_button;
        private Panel data_panel;
    }
}
