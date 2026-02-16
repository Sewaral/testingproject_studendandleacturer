using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class Options : Form
    {
        public Label labelTitle, labelQuestion, labelInfo, labelCorrect, labelDifficulty, labelHint, labelCategory;
        public Label lineQuestion, lineA, lineB, lineC, lineD, lineHint, lineCategory;
        public TextBox txtQuestion, txtA, txtB, txtC, txtD, txtHint, txtCategory;
        public RadioButton radioA, radioB, radioC, radioD, radioHard, radioMedium, radioLow;
        public Button btnSave, btnBack;
        public Panel panelCorrect, panelDifficulty;
        public Label labelOptionA, labelOptionB, labelOptionC, labelOptionD;




        public void InitializeComponent()
        {
            labelTitle = new Label();
            labelQuestion = new Label();
            labelInfo = new Label();
            labelCorrect = new Label();
            labelDifficulty = new Label();
            labelHint = new Label();
            labelCategory = new Label();
            txtQuestion = new TextBox();
            txtA = new TextBox();
            txtB = new TextBox();
            txtC = new TextBox();
            txtD = new TextBox();
            txtHint = new TextBox();
            txtCategory = new TextBox();
            lineQuestion = new Label();
            lineA = new Label();
            lineB = new Label();
            lineC = new Label();
            lineD = new Label();
            lineHint = new Label();
            lineCategory = new Label();
            radioA = new RadioButton();
            radioB = new RadioButton();
            radioC = new RadioButton();
            radioD = new RadioButton();
            radioHard = new RadioButton();
            radioMedium = new RadioButton();
            radioLow = new RadioButton();
            panelCorrect = new Panel();
            panelDifficulty = new Panel();
            btnSave = new Button();
            btnBack = new Button();
            labelOptionA = new Label();
            labelOptionB = new Label();
            labelOptionC = new Label();
            labelOptionD = new Label();
            label1 = new Label();
            label2 = new Label();
            panelCorrect.SuspendLayout();
            panelDifficulty.SuspendLayout();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            labelTitle.Location = new Point(45, 23);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(586, 54);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Add Multiple Choice Question";
            // 
            // labelQuestion
            // 
            labelQuestion.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelQuestion.Location = new Point(50, 77);
            labelQuestion.Name = "labelQuestion";
            labelQuestion.Size = new Size(318, 37);
            labelQuestion.TabIndex = 1;
            labelQuestion.Text = "Write Your Question:";
            // 
            // labelInfo
            // 
            labelInfo.AutoSize = true;
            labelInfo.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            labelInfo.ForeColor = Color.Gray;
            labelInfo.Location = new Point(50, 187);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(221, 23);
            labelInfo.TabIndex = 4;
            labelInfo.Text = "Please write full options text ";
            // 
            // labelCorrect
            // 
            labelCorrect.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelCorrect.Location = new Point(10, 10);
            labelCorrect.Name = "labelCorrect";
            labelCorrect.Size = new Size(226, 37);
            labelCorrect.TabIndex = 0;
            labelCorrect.Text = "Correct Answer";
            // 
            // labelDifficulty
            // 
            labelDifficulty.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelDifficulty.Location = new Point(12, 10);
            labelDifficulty.Name = "labelDifficulty";
            labelDifficulty.Size = new Size(226, 37);
            labelDifficulty.TabIndex = 0;
            labelDifficulty.Text = "Difficulty Level";
            // 
            // labelHint
            // 
            labelHint.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelHint.Location = new Point(45, 623);
            labelHint.Name = "labelHint";
            labelHint.Size = new Size(147, 35);
            labelHint.TabIndex = 15;
            labelHint.Text = "Hint(optional)";
            // 
            // labelCategory
            // 
            labelCategory.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelCategory.Location = new Point(50, 528);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(100, 27);
            labelCategory.TabIndex = 18;
            labelCategory.Text = "Category:";
            // 
            // txtQuestion
            // 
            txtQuestion.BorderStyle = BorderStyle.None;
            txtQuestion.Location = new Point(50, 131);
            txtQuestion.Name = "txtQuestion";
            txtQuestion.Size = new Size(900, 20);
            txtQuestion.TabIndex = 2;
            // 
            // txtA
            // 
            txtA.BackColor = Color.FromArgb(221, 87, 70);
            txtA.Font = new Font("Segoe UI", 12F);
            txtA.Location = new Point(76, 225);
            txtA.Name = "txtA";
            txtA.Size = new Size(300, 34);
            txtA.TabIndex = 1;
            // 
            // txtB
            // 
            txtB.BackColor = Color.FromArgb(255, 196, 112);
            txtB.Font = new Font("Segoe UI", 12F);
            txtB.Location = new Point(76, 274);
            txtB.Name = "txtB";
            txtB.Size = new Size(300, 34);
            txtB.TabIndex = 3;
            // 
            // txtC
            // 
            txtC.BackColor = Color.FromArgb(71, 147, 175);
            txtC.Font = new Font("Segoe UI", 12F);
            txtC.Location = new Point(430, 225);
            txtC.Name = "txtC";
            txtC.Size = new Size(300, 34);
            txtC.TabIndex = 5;
            // 
            // txtD
            // 
            txtD.BackColor = Color.FromArgb(160, 200, 120);
            txtD.Font = new Font("Segoe UI", 12F);
            txtD.Location = new Point(426, 274);
            txtD.Name = "txtD";
            txtD.Size = new Size(300, 34);
            txtD.TabIndex = 7;
            // 
            // txtHint
            // 
            txtHint.Font = new Font("Segoe UI", 12F);
            txtHint.Location = new Point(50, 661);
            txtHint.Name = "txtHint";
            txtHint.Size = new Size(760, 34);
            txtHint.TabIndex = 16;
            // 
            // txtCategory
            // 
            txtCategory.Font = new Font("Segoe UI", 12F);
            txtCategory.Location = new Point(52, 558);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(722, 34);
            txtCategory.TabIndex = 19;
            // 
            // lineQuestion
            // 
            lineQuestion.BorderStyle = BorderStyle.Fixed3D;
            lineQuestion.Location = new Point(50, 170);
            lineQuestion.Name = "lineQuestion";
            lineQuestion.Size = new Size(900, 2);
            lineQuestion.TabIndex = 3;
            // 
            // lineA
            // 
            lineA.Location = new Point(320, 346);
            lineA.Name = "lineA";
            lineA.Size = new Size(100, 23);
            lineA.TabIndex = 6;
            // 
            // lineB
            // 
            lineB.Location = new Point(552, 311);
            lineB.Name = "lineB";
            lineB.Size = new Size(100, 23);
            lineB.TabIndex = 8;
            // 
            // lineC
            // 
            lineC.Location = new Point(0, 0);
            lineC.Name = "lineC";
            lineC.Size = new Size(100, 23);
            lineC.TabIndex = 10;
            // 
            // lineD
            // 
            lineD.Location = new Point(0, 0);
            lineD.Name = "lineD";
            lineD.Size = new Size(100, 23);
            lineD.TabIndex = 12;
            // 
            // lineHint
            // 
            lineHint.Location = new Point(0, 0);
            lineHint.Name = "lineHint";
            lineHint.Size = new Size(100, 23);
            lineHint.TabIndex = 17;
            // 
            // lineCategory
            // 
            lineCategory.Location = new Point(0, 0);
            lineCategory.Name = "lineCategory";
            lineCategory.Size = new Size(100, 23);
            lineCategory.TabIndex = 20;
            // 
            // radioA
            // 
            radioA.Location = new Point(30, 50);
            radioA.Name = "radioA";
            radioA.Size = new Size(104, 24);
            radioA.TabIndex = 1;
            radioA.Text = "A";
            // 
            // radioB
            // 
            radioB.Location = new Point(150, 50);
            radioB.Name = "radioB";
            radioB.Size = new Size(104, 24);
            radioB.TabIndex = 2;
            radioB.Text = "B";
            // 
            // radioC
            // 
            radioC.Location = new Point(270, 50);
            radioC.Name = "radioC";
            radioC.Size = new Size(104, 24);
            radioC.TabIndex = 3;
            radioC.Text = "C";
            // 
            // radioD
            // 
            radioD.Location = new Point(390, 50);
            radioD.Name = "radioD";
            radioD.Size = new Size(104, 24);
            radioD.TabIndex = 4;
            radioD.Text = "D";
            // 
            // radioHard
            // 
            radioHard.Location = new Point(30, 50);
            radioHard.Name = "radioHard";
            radioHard.Size = new Size(104, 24);
            radioHard.TabIndex = 1;
            radioHard.Text = "Hard";
            // 
            // radioMedium
            // 
            radioMedium.Location = new Point(200, 50);
            radioMedium.Name = "radioMedium";
            radioMedium.Size = new Size(104, 24);
            radioMedium.TabIndex = 2;
            radioMedium.Text = "Medium";
            // 
            // radioLow
            // 
            radioLow.Location = new Point(400, 50);
            radioLow.Name = "radioLow";
            radioLow.Size = new Size(104, 24);
            radioLow.TabIndex = 3;
            radioLow.Text = "Low";
            // 
            // panelCorrect
            // 
            panelCorrect.Controls.Add(labelCorrect);
            panelCorrect.Controls.Add(radioA);
            panelCorrect.Controls.Add(radioB);
            panelCorrect.Controls.Add(radioC);
            panelCorrect.Controls.Add(radioD);
            panelCorrect.Location = new Point(52, 337);
            panelCorrect.Name = "panelCorrect";
            panelCorrect.Size = new Size(900, 80);
            panelCorrect.TabIndex = 13;
            // 
            // panelDifficulty
            // 
            panelDifficulty.Controls.Add(labelDifficulty);
            panelDifficulty.Controls.Add(radioHard);
            panelDifficulty.Controls.Add(radioMedium);
            panelDifficulty.Controls.Add(radioLow);
            panelDifficulty.Location = new Point(50, 435);
            panelDifficulty.Name = "panelDifficulty";
            panelDifficulty.Size = new Size(900, 80);
            panelDifficulty.TabIndex = 14;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(76, 175, 80);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(716, 729);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 50);
            btnSave.TabIndex = 3;
            btnSave.Text = "SAVE";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(0, 0);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 27;
            // 
            // labelOptionA
            // 
            labelOptionA.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelOptionA.Location = new Point(50, 221);
            labelOptionA.Name = "labelOptionA";
            labelOptionA.Size = new Size(20, 27);
            labelOptionA.TabIndex = 0;
            labelOptionA.Text = "A";
            labelOptionA.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelOptionB
            // 
            labelOptionB.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelOptionB.Location = new Point(54, 280);
            labelOptionB.Name = "labelOptionB";
            labelOptionB.Size = new Size(20, 27);
            labelOptionB.TabIndex = 2;
            labelOptionB.Text = "B";
            labelOptionB.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelOptionC
            // 
            labelOptionC.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelOptionC.Location = new Point(404, 225);
            labelOptionC.Name = "labelOptionC";
            labelOptionC.Size = new Size(20, 27);
            labelOptionC.TabIndex = 4;
            labelOptionC.Text = "C";
            labelOptionC.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelOptionD
            // 
            labelOptionD.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelOptionD.Location = new Point(400, 274);
            labelOptionD.Name = "labelOptionD";
            labelOptionD.Size = new Size(20, 27);
            labelOptionD.TabIndex = 6;
            labelOptionD.Text = "D";
            labelOptionD.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Location = new Point(52, 606);
            label1.Name = "label1";
            label1.Size = new Size(900, 2);
            label1.TabIndex = 25;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.Location = new Point(52, 712);
            label2.Name = "label2";
            label2.Size = new Size(900, 2);
            label2.TabIndex = 26;
            // 
            // Options
            // 
            BackColor = Color.FromArgb(246, 245, 243);
            ClientSize = new Size(1200, 900);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(labelOptionA);
            Controls.Add(labelOptionB);
            Controls.Add(labelOptionC);
            Controls.Add(labelOptionD);
            Controls.Add(labelTitle);
            Controls.Add(labelQuestion);
            Controls.Add(txtQuestion);
            Controls.Add(lineQuestion);
            Controls.Add(labelInfo);
            Controls.Add(txtA);
            Controls.Add(lineA);
            Controls.Add(txtB);
            Controls.Add(lineB);
            Controls.Add(txtC);
            Controls.Add(lineC);
            Controls.Add(txtD);
            Controls.Add(lineD);
            Controls.Add(panelCorrect);
            Controls.Add(panelDifficulty);
            Controls.Add(labelHint);
            Controls.Add(txtHint);
            Controls.Add(lineHint);
            Controls.Add(labelCategory);
            Controls.Add(txtCategory);
            Controls.Add(lineCategory);
            Controls.Add(btnSave);
            Controls.Add(btnBack);
            Name = "Options";
            Text = "Add MCQ Question";
            Load += Options_Load;
            panelCorrect.ResumeLayout(false);
            panelDifficulty.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
    }
}