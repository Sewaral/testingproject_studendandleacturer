namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    partial class TrueFalseForm
    {
        public System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.ClientSize = new Size(720, 550);

            labelTitle = new Label();
            panelCorrect = new Panel();
            labelAnswer = new Label();
            radioTrue = new RadioButton();
            radioFalse = new RadioButton();
            panelDifficulty = new Panel();
            labelDifficulty = new Label();
            radioHard = new RadioButton();
            radioMedium = new RadioButton();
            radioLow = new RadioButton();
            labelOptions = new Label();
            labelOptionA = new Label();
            labelOptionB = new Label();
            txtOptionA = new TextBox();
            txtOptionB = new TextBox();
            btnSave = new Button();
            panelCorrect.SuspendLayout();
            panelDifficulty.SuspendLayout();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            labelTitle.Location = new Point(320, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(430, 46);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Add True / False Question";
            // 
            // panelCorrect
            // 
            panelCorrect.Controls.Add(labelAnswer);
            panelCorrect.Controls.Add(radioTrue);
            panelCorrect.Controls.Add(radioFalse);
            panelCorrect.Location = new Point(200, 250);
            panelCorrect.Name = "panelCorrect";
            panelCorrect.Size = new Size(600, 60);
            panelCorrect.TabIndex = 1;
            // 
            // labelAnswer
            // 
            labelAnswer.AutoSize = true;
            labelAnswer.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelAnswer.Location = new Point(10, 15);
            labelAnswer.Name = "labelAnswer";
            labelAnswer.Size = new Size(163, 28);
            labelAnswer.TabIndex = 0;
            labelAnswer.Text = "Correct Answer:";
            // 
            // radioTrue
            // 
            radioTrue.AutoSize = true;
            radioTrue.Font = new Font("Segoe UI", 11F);
            radioTrue.Location = new Point(180, 13);
            radioTrue.Name = "radioTrue";
            radioTrue.Size = new Size(69, 29);
            radioTrue.TabIndex = 1;
            radioTrue.Text = "True";
            // 
            // radioFalse
            // 
            radioFalse.AutoSize = true;
            radioFalse.Font = new Font("Segoe UI", 11F);
            radioFalse.Location = new Point(280, 13);
            radioFalse.Name = "radioFalse";
            radioFalse.Size = new Size(74, 29);
            radioFalse.TabIndex = 2;
            radioFalse.Text = "False";
            // 
            // panelDifficulty
            // 
            panelDifficulty.Controls.Add(labelDifficulty);
            panelDifficulty.Controls.Add(radioHard);
            panelDifficulty.Controls.Add(radioMedium);
            panelDifficulty.Controls.Add(radioLow);
            panelDifficulty.Location = new Point(200, 320);
            panelDifficulty.Name = "panelDifficulty";
            panelDifficulty.Size = new Size(600, 60);
            panelDifficulty.TabIndex = 2;
            // 
            // labelDifficulty
            // 
            labelDifficulty.AutoSize = true;
            labelDifficulty.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelDifficulty.Location = new Point(10, 15);
            labelDifficulty.Name = "labelDifficulty";
            labelDifficulty.Size = new Size(162, 28);
            labelDifficulty.TabIndex = 0;
            labelDifficulty.Text = "Difficulty Level:";
            // 
            // radioHard
            // 
            radioHard.AutoSize = true;
            radioHard.Font = new Font("Segoe UI", 11F);
            radioHard.Location = new Point(180, 13);
            radioHard.Name = "radioHard";
            radioHard.Size = new Size(74, 29);
            radioHard.TabIndex = 1;
            radioHard.Text = "Hard";
            // 
            // radioMedium
            // 
            radioMedium.AutoSize = true;
            radioMedium.Font = new Font("Segoe UI", 11F);
            radioMedium.Location = new Point(280, 13);
            radioMedium.Name = "radioMedium";
            radioMedium.Size = new Size(103, 29);
            radioMedium.TabIndex = 2;
            radioMedium.Text = "Medium";
            // 
            // radioLow
            // 
            radioLow.AutoSize = true;
            radioLow.Font = new Font("Segoe UI", 11F);
            radioLow.Location = new Point(400, 13);
            radioLow.Name = "radioLow";
            radioLow.Size = new Size(67, 29);
            radioLow.TabIndex = 3;
            radioLow.Text = "Low";
            // 
            // labelOptions
            // 
            labelOptions.AutoSize = true;
            labelOptions.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelOptions.Location = new Point(210, 400);
            labelOptions.Name = "labelOptions";
            labelOptions.Size = new Size(105, 31);
            labelOptions.TabIndex = 3;
            labelOptions.Text = "Options:";
            // 
            // labelOptionA
            // 
            labelOptionA.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelOptionA.Location = new Point(210, 446);
            labelOptionA.Name = "labelOptionA";
            labelOptionA.Size = new Size(38, 23);
            labelOptionA.TabIndex = 4;
            labelOptionA.Text = "A";
            // 
            // labelOptionB
            // 
            labelOptionB.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelOptionB.Location = new Point(444, 446);
            labelOptionB.Name = "labelOptionB";
            labelOptionB.Size = new Size(33, 32);
            labelOptionB.TabIndex = 6;
            labelOptionB.Text = "B";
            // 
            // txtOptionA
            // 
            txtOptionA.BackColor = Color.LightSalmon;
            txtOptionA.Font = new Font("Segoe UI", 11F);
            txtOptionA.Location = new Point(254, 443);
            txtOptionA.Name = "txtOptionA";
            txtOptionA.ReadOnly = true;
            txtOptionA.Size = new Size(150, 32);
            txtOptionA.TabIndex = 5;
            txtOptionA.Text = "True";
            txtOptionA.TabStop = false;
            txtOptionB.TabStop = false;

            // 
            // txtOptionB
            // 
            txtOptionB.BackColor = Color.LightSkyBlue;
            txtOptionB.Font = new Font("Segoe UI", 11F);
            txtOptionB.Location = new Point(483, 446);
            txtOptionB.Name = "txtOptionB";
            txtOptionB.ReadOnly = true;
            txtOptionB.Size = new Size(150, 32);
            txtOptionB.TabIndex = 7;
            txtOptionB.Text = "False";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(76, 175, 80);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(846, 770);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 8;
            btnSave.Text = "SAVE";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // TrueFalseForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 245, 243);
            Controls.Add(labelTitle);
            Controls.Add(panelCorrect);
            Controls.Add(panelDifficulty);
            Controls.Add(labelOptions);
            Controls.Add(labelOptionA);
            Controls.Add(txtOptionA);
            Controls.Add(labelOptionB);
            Controls.Add(txtOptionB);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "TrueFalseForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add True / False Question";
            Load += TrueFalseForm_Load;
            panelCorrect.ResumeLayout(false);
            panelCorrect.PerformLayout();
            panelDifficulty.ResumeLayout(false);
            panelDifficulty.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label labelTitle;
        public Label labelAnswer, labelDifficulty, labelOptions, labelOptionA, labelOptionB;
        public RadioButton radioTrue, radioFalse, radioHard, radioMedium, radioLow;
        public Panel panelCorrect, panelDifficulty;
        public TextBox txtOptionA, txtOptionB;
    }
}