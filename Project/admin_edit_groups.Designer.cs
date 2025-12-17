namespace SchoolJournal
{
    partial class admin_edit_groups
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
            edit_button = new Button();
            panel1 = new Panel();
            current_textBox = new TextBox();
            current_label = new Label();
            room_textBox = new TextBox();
            room_label = new Label();
            teacher_comboBox1 = new ComboBox();
            teacher_label = new Label();
            count_comboBox = new ComboBox();
            count_label = new Label();
            age_comboBox = new ComboBox();
            age_label = new Label();
            name_textBox = new TextBox();
            name_label = new Label();
            back_button = new Button();
            groups_label = new Label();
            exit_button = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // edit_button
            // 
            edit_button.Font = new Font("Segoe UI", 15F);
            edit_button.Location = new Point(626, 224);
            edit_button.Name = "edit_button";
            edit_button.Size = new Size(152, 58);
            edit_button.TabIndex = 21;
            edit_button.Text = "Редагувати";
            edit_button.TextImageRelation = TextImageRelation.ImageBeforeText;
            edit_button.UseVisualStyleBackColor = true;
            edit_button.Click += edit_button_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonShadow;
            panel1.Controls.Add(current_textBox);
            panel1.Controls.Add(current_label);
            panel1.Controls.Add(room_textBox);
            panel1.Controls.Add(room_label);
            panel1.Controls.Add(teacher_comboBox1);
            panel1.Controls.Add(teacher_label);
            panel1.Controls.Add(count_comboBox);
            panel1.Controls.Add(count_label);
            panel1.Controls.Add(age_comboBox);
            panel1.Controls.Add(age_label);
            panel1.Controls.Add(name_textBox);
            panel1.Controls.Add(name_label);
            panel1.Location = new Point(12, 72);
            panel1.Name = "panel1";
            panel1.Size = new Size(608, 366);
            panel1.TabIndex = 20;
            panel1.Paint += panel1_Paint;
            // 
            // current_textBox
            // 
            current_textBox.Font = new Font("Segoe UI", 15F);
            current_textBox.Location = new Point(261, 190);
            current_textBox.Name = "current_textBox";
            current_textBox.Size = new Size(151, 41);
            current_textBox.TabIndex = 12;
            current_textBox.TextChanged += current_textBox_TextChanged;
            // 
            // current_label
            // 
            current_label.AutoSize = true;
            current_label.BackColor = SystemColors.ButtonHighlight;
            current_label.Font = new Font("Segoe UI", 15F);
            current_label.Location = new Point(30, 193);
            current_label.Name = "current_label";
            current_label.Size = new Size(226, 35);
            current_label.TabIndex = 11;
            current_label.Text = "Поточна Кількість:";
            // 
            // room_textBox
            // 
            room_textBox.Font = new Font("Segoe UI", 15F);
            room_textBox.Location = new Point(261, 297);
            room_textBox.Name = "room_textBox";
            room_textBox.Size = new Size(151, 41);
            room_textBox.TabIndex = 10;
            room_textBox.TextChanged += room_textBox_TextChanged;
            // 
            // room_label
            // 
            room_label.AutoSize = true;
            room_label.BackColor = SystemColors.ButtonHighlight;
            room_label.Font = new Font("Segoe UI", 15F);
            room_label.Location = new Point(30, 300);
            room_label.Name = "room_label";
            room_label.Size = new Size(109, 35);
            room_label.TabIndex = 9;
            room_label.Text = "Кімната:";
            // 
            // teacher_comboBox1
            // 
            teacher_comboBox1.Font = new Font("Segoe UI", 15F);
            teacher_comboBox1.FormattingEnabled = true;
            teacher_comboBox1.Location = new Point(261, 244);
            teacher_comboBox1.Name = "teacher_comboBox1";
            teacher_comboBox1.Size = new Size(151, 43);
            teacher_comboBox1.TabIndex = 7;
            teacher_comboBox1.SelectedIndexChanged += teacher_comboBox1_SelectedIndexChanged;
            // 
            // teacher_label
            // 
            teacher_label.AutoSize = true;
            teacher_label.BackColor = SystemColors.ButtonHighlight;
            teacher_label.Font = new Font("Segoe UI", 15F);
            teacher_label.Location = new Point(30, 247);
            teacher_label.Name = "teacher_label";
            teacher_label.Size = new Size(157, 35);
            teacher_label.TabIndex = 6;
            teacher_label.Text = "Вчитель: ";
            // 
            // count_comboBox
            // 
            count_comboBox.Font = new Font("Segoe UI", 15F);
            count_comboBox.FormattingEnabled = true;
            count_comboBox.Items.AddRange(new object[] { "25 учнів", "30 учнів", "35 учнів" });
            count_comboBox.Location = new Point(261, 137);
            count_comboBox.Name = "count_comboBox";
            count_comboBox.Size = new Size(151, 43);
            count_comboBox.TabIndex = 5;
            count_comboBox.SelectedIndexChanged += count_comboBox_SelectedIndexChanged;
            // 
            // count_label
            // 
            count_label.AutoSize = true;
            count_label.BackColor = SystemColors.ButtonHighlight;
            count_label.Font = new Font("Segoe UI", 15F);
            count_label.Location = new Point(30, 140);
            count_label.Name = "count_label";
            count_label.Size = new Size(173, 35);
            count_label.TabIndex = 4;
            count_label.Text = "Max Кількість:";
            // 
            // age_comboBox
            // 
            age_comboBox.Font = new Font("Segoe UI", 15F);
            age_comboBox.FormattingEnabled = true;
            age_comboBox.Items.AddRange(new object[] { "1 клас", "2 клас", "3 клас", "4 клас", "5 клас", "6 клас", "7 клас", "8 клас", "9 клас", "10 клас", "11 клас" });
            age_comboBox.Location = new Point(261, 79);
            age_comboBox.Name = "age_comboBox";
            age_comboBox.Size = new Size(151, 43);
            age_comboBox.TabIndex = 3;
            age_comboBox.SelectedIndexChanged += age_comboBox_SelectedIndexChanged;
            // 
            // age_label
            // 
            age_label.AutoSize = true;
            age_label.BackColor = SystemColors.ButtonHighlight;
            age_label.Font = new Font("Segoe UI", 15F);
            age_label.Location = new Point(30, 82);
            age_label.Name = "age_label";
            age_label.Size = new Size(207, 35);
            age_label.TabIndex = 2;
            age_label.Text = "Вікова категорія:";
            // 
            // name_textBox
            // 
            name_textBox.Font = new Font("Segoe UI", 15F);
            name_textBox.Location = new Point(261, 18);
            name_textBox.Name = "name_textBox";
            name_textBox.Size = new Size(151, 41);
            name_textBox.TabIndex = 1;
            name_textBox.TextChanged += name_textBox_TextChanged;
            // 
            // name_label
            // 
            name_label.AutoSize = true;
            name_label.BackColor = SystemColors.ButtonHighlight;
            name_label.Font = new Font("Segoe UI", 15F);
            name_label.Location = new Point(30, 21);
            name_label.Name = "name_label";
            name_label.Size = new Size(156, 35);
            name_label.TabIndex = 0;
            name_label.Text = "Назва класу";
            // 
            // back_button
            // 
            back_button.Location = new Point(12, 10);
            back_button.Name = "back_button";
            back_button.Size = new Size(94, 29);
            back_button.TabIndex = 19;
            back_button.Text = "Назад";
            back_button.UseVisualStyleBackColor = true;
            back_button.Click += back_button_Click;
            // 
            // groups_label
            // 
            groups_label.AutoSize = true;
            groups_label.Font = new Font("Segoe UI", 20F);
            groups_label.Location = new Point(231, 10);
            groups_label.Name = "groups_label";
            groups_label.Size = new Size(312, 46);
            groups_label.TabIndex = 18;
            groups_label.Text = "Редагування класу";
            groups_label.Click += groups_label_Click;
            // 
            // exit_button
            // 
            exit_button.Location = new Point(694, 10);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(94, 29);
            exit_button.TabIndex = 17;
            exit_button.Text = "Вийти ";
            exit_button.UseVisualStyleBackColor = true;
            exit_button.Click += exit_button_Click;
            // 
            // admin_edit_groups
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(edit_button);
            Controls.Add(panel1);
            Controls.Add(back_button);
            Controls.Add(groups_label);
            Controls.Add(exit_button);
            Name = "admin_edit_groups";
            Text = "Редагувати клас";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button edit_button;
        private Panel panel1;
        private TextBox current_textBox;
        private Label current_label;
        private TextBox room_textBox;
        private Label room_label;
        private ComboBox teacher_comboBox1;
        private Label teacher_label;
        private ComboBox count_comboBox;
        private Label count_label;
        private ComboBox age_comboBox;
        private Label age_label;
        private TextBox name_textBox;
        private Label name_label;
        private Button back_button;
        private Label groups_label;
        private Button exit_button;
    }
}