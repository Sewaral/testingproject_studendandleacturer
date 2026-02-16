namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    partial class complete
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label labelTitle, labelQuestion, labelInfo, labelDifficulty, labelHint, labelCategory, lblCorrectAnswer;
        private System.Windows.Forms.Label lineQuestion, lineHint, lineCategory;
        private System.Windows.Forms.TextBox txtQuestion, txtHint, txtCategory, txtCorrectAnswer;
        private System.Windows.Forms.RadioButton radioHard, radioMedium, radioLow;
        private System.Windows.Forms.Panel panelDifficulty;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelTitle = new Label();
            labelQuestion = new Label();
            txtQuestion = new TextBox();
            lineQuestion = new Label();
            labelInfo = new Label();
            labelDifficulty = new Label();
            radioHard = new RadioButton();
            radioMedium = new RadioButton();
            radioLow = new RadioButton();
            panelDifficulty = new Panel();
            labelHint = new Label();
            txtHint = new TextBox();
            lineHint = new Label();
            labelCategory = new Label();
            txtCategory = new TextBox();
            lineCategory = new Label();
            lblCorrectAnswer = new Label();
            txtCorrectAnswer = new TextBox();
            btnSave = new Button();
            panelDifficulty.SuspendLayout();
            SuspendLayout();

            // labelTitle
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            labelTitle.Location = new Point(200, -1);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(556, 54);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Add Complete The Sentence";

            // labelQuestion
            labelQuestion.AutoSize = true;
            labelQuestion.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelQuestion.Location = new Point(50, 59);
            labelQuestion.Name = "labelQuestion";
            labelQuestion.Text = "Write Your Question:";

            // txtQuestion
            txtQuestion.BorderStyle = BorderStyle.None;
            txtQuestion.Location = new Point(50, 110);
            txtQuestion.Name = "txtQuestion";
            txtQuestion.Size = new Size(900, 20);
            txtQuestion.TabIndex = 2;

            // lineQuestion
            lineQuestion.BorderStyle = BorderStyle.Fixed3D;
            lineQuestion.Location = new Point(50, 135);
            lineQuestion.Name = "lineQuestion";
            lineQuestion.Size = new Size(900, 2);
            lineQuestion.TabIndex = 3;

            // labelInfo
            labelInfo.AutoSize = true;
            labelInfo.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            labelInfo.ForeColor = Color.Gray;
            labelInfo.Location = new Point(50, 150);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(382, 23);
            labelInfo.TabIndex = 4;
            labelInfo.Text = "Use { } for missing words. Example: The {} is hot";

            // lblCorrectAnswer
            lblCorrectAnswer.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCorrectAnswer.Location = new Point(50, 190);
            lblCorrectAnswer.Name = "lblCorrectAnswer";
            lblCorrectAnswer.Size = new Size(300, 30);
            lblCorrectAnswer.TabIndex = 5;
            lblCorrectAnswer.Text = "Correct Answer";
            // labelInfoAnswer
            Label labelInfoAnswer = new Label();
            labelInfoAnswer.AutoSize = true;
            labelInfoAnswer.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            labelInfoAnswer.ForeColor = Color.Gray;
            labelInfoAnswer.Location = new Point(50, 222); // Adjust Y position as needed
            labelInfoAnswer.Name = "labelInfoAnswer";
            labelInfoAnswer.Size = new Size(400, 23);
            labelInfoAnswer.TabIndex = 6;
            labelInfoAnswer.Text = "Use , between correct words. Example: sun, hot, red";

            // Add to form controls
            this.Controls.Add(labelInfoAnswer);


            // txtCorrectAnswer
            txtCorrectAnswer.BorderStyle = BorderStyle.None;
            txtCorrectAnswer.Location = new Point(50, 240);
            txtCorrectAnswer.Name = "txtCorrectAnswer";
            txtCorrectAnswer.Size = new Size(900, 20);
            txtCorrectAnswer.TabIndex = 6;

            // panelDifficulty
            panelDifficulty.Controls.Add(labelDifficulty);
            panelDifficulty.Controls.Add(radioHard);
            panelDifficulty.Controls.Add(radioMedium);
            panelDifficulty.Controls.Add(radioLow);
            panelDifficulty.Location = new Point(50, 280);
            panelDifficulty.Name = "panelDifficulty";
            panelDifficulty.Size = new Size(900, 80);
            panelDifficulty.TabIndex = 7;

            // labelDifficulty
            labelDifficulty.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelDifficulty.Location = new Point(0, 5);
            labelDifficulty.Name = "labelDifficulty";
            labelDifficulty.Size = new Size(200, 30);
            labelDifficulty.TabIndex = 0;
            labelDifficulty.Text = "Difficulty";

            // radioHard
            radioHard.Location = new Point(30, 50);
            radioHard.Name = "radioHard";
            radioHard.Size = new Size(104, 24);
            radioHard.TabIndex = 1;
            radioHard.Text = "Hard";

            // radioMedium
            radioMedium.Location = new Point(200, 50);
            radioMedium.Name = "radioMedium";
            radioMedium.Size = new Size(104, 24);
            radioMedium.TabIndex = 2;
            radioMedium.Text = "Medium";

            // radioLow
            radioLow.Location = new Point(400, 50);
            radioLow.Name = "radioLow";
            radioLow.Size = new Size(104, 24);
            radioLow.TabIndex = 3;
            radioLow.Text = "Low";

            // labelHint
            labelHint.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelHint.Location = new Point(50, 375);
            labelHint.Name = "labelHint";
            labelHint.Size = new Size(200, 30);
            labelHint.TabIndex = 8;
            labelHint.Text = "Hint";

            // txtHint
            txtHint.BorderStyle = BorderStyle.None;
            txtHint.Location = new Point(50, 415);
            txtHint.Name = "txtHint";
            txtHint.Size = new Size(900, 20);
            txtHint.TabIndex = 9;

            // lineHint
            lineHint.BorderStyle = BorderStyle.Fixed3D;
            lineHint.Location = new Point(50, 445);
            lineHint.Name = "lineHint";
            lineHint.Size = new Size(900, 2);
            lineHint.TabIndex = 10;

            // labelCategory
            labelCategory.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelCategory.Location = new Point(50, 470);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(200, 30);
            labelCategory.TabIndex = 11;
            labelCategory.Text = "Category";

            // txtCategory
            txtCategory.BorderStyle = BorderStyle.None;
            txtCategory.Location = new Point(50, 510);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(900, 20);
            txtCategory.TabIndex = 12;

            // lineCategory
            lineCategory.BorderStyle = BorderStyle.Fixed3D;
            lineCategory.Location = new Point(50, 540);
            lineCategory.Name = "lineCategory";
            lineCategory.Size = new Size(900, 2);
            lineCategory.TabIndex = 13;

            // btnSave
            btnSave.BackColor = Color.FromArgb(76, 175, 80);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(800, 570);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 50);
            btnSave.TabIndex = 14;
            btnSave.Text = "SAVE";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            // complete
            BackColor = Color.FromArgb(246, 245, 243);
            ClientSize = new Size(1050, 650);
            Controls.Add(labelTitle);
            Controls.Add(labelQuestion);
            Controls.Add(txtQuestion);
            Controls.Add(lineQuestion);
            Controls.Add(labelInfo);
            Controls.Add(lblCorrectAnswer);
            Controls.Add(txtCorrectAnswer);
            Controls.Add(panelDifficulty);
            Controls.Add(labelHint);
            Controls.Add(txtHint);
            Controls.Add(lineHint);
            Controls.Add(labelCategory);
            Controls.Add(txtCategory);
            Controls.Add(lineCategory);
            Controls.Add(btnSave);
            Name = "complete";
            Text = "Add Complete The Sentence";
            Load += complete_Load;
            panelDifficulty.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
