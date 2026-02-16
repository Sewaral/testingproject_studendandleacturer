namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    partial class CreateExamForm
    {
        public System.ComponentModel.IContainer components = null;
        public Label lblTitle, lblMCQ, lblTrueFalse, lblComplete, lblCategory, lblDifficulty, lblExisting;
        public NumericUpDown numMCQ, numTrueFalse, numComplete;
        public ComboBox cmbCategory, cmbDifficulty;
        public Button btnCreateExam;
        public Panel panelExamCreation, panelExamList;
        public FlowLayoutPanel flowExams;

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {

            this.Text = "Create Exam";
            this.Size = new Size(1000, 720); // Slightly increased height
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#EAF0FF");

            // ===== Exam Creation Panel =====
            panelExamCreation = new Panel()
            {
                Location = new Point(40, 40),
                Size = new Size(420, 520),
                BackColor = Color.White,
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelExamCreation);

            lblTitle = new Label()
            {
                Text = "\uD83D\uDCCB  Create Your Exam",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#303F9F"),
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(10, 10),
                AutoSize = true
            };
            panelExamCreation.Controls.Add(lblTitle);

            Font labelFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            int labelX = 10, inputX = 180, currentY = 50, spacingY = 40;

            lblMCQ = new Label() { Text = "MCQ Questions:", Location = new Point(labelX, currentY), Size = new Size(150, 25), Font = labelFont };
            numMCQ = new NumericUpDown() { Location = new Point(inputX, currentY), Width = 130, Minimum = 0, Maximum = 50 };

            currentY += spacingY;
            lblTrueFalse = new Label() { Text = "True/False Questions:", Location = new Point(labelX, currentY), Size = new Size(150, 25), Font = labelFont };
            numTrueFalse = new NumericUpDown() { Location = new Point(inputX, currentY), Width = 130, Minimum = 0, Maximum = 50 };

            currentY += spacingY;
            lblComplete = new Label() { Text = "Complete Questions:", Location = new Point(labelX, currentY), Size = new Size(150, 25), Font = labelFont };
            numComplete = new NumericUpDown() { Location = new Point(inputX, currentY), Width = 130, Minimum = 0, Maximum = 50 };

            currentY += spacingY;
            lblCategory = new Label() { Text = "Select Category:", Location = new Point(labelX, currentY), Size = new Size(150, 25), Font = labelFont };
            cmbCategory = new ComboBox() { Location = new Point(inputX, currentY), Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };

            currentY += spacingY;
            lblDifficulty = new Label() { Text = "Select Difficulty:", Location = new Point(labelX, currentY), Size = new Size(150, 25), Font = labelFont };
            cmbDifficulty = new ComboBox() { Location = new Point(inputX, currentY), Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbDifficulty.Items.AddRange(new object[] { "Low", "Medium", "Hard", "Random" });


            currentY += spacingY + 15;
            btnCreateExam = new Button()
            {
                Text = "Create Exam",
                Location = new Point(110, currentY),
                Width = 200,
                Height = 40,
                BackColor = ColorTranslator.FromHtml("#3F51B5"),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnCreateExam.FlatAppearance.BorderSize = 0;

            panelExamCreation.Controls.AddRange(new Control[] {
        lblMCQ, numMCQ,
        lblTrueFalse, numTrueFalse,
        lblComplete, numComplete,
        lblCategory, cmbCategory,
        lblDifficulty, cmbDifficulty,
        btnCreateExam
    });

            // ===== Existing Exams Panel (bigger now) =====
            panelExamList = new Panel()
            {
                Location = new Point(480, 40),
                Size = new Size(460, 580), // Increased height to reduce scroll
                BackColor = Color.White,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = false // Disabled to allow full visibility
            };
            this.Controls.Add(panelExamList);

            lblExisting = new Label()
            {
                Text = "\uD83D\uDCC4  Existing Exams",
                Location = new Point(10, 10),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#303F9F"),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelExamList.Controls.Add(lblExisting);

            flowExams = new FlowLayoutPanel()
            {
                Location = new Point(10, 50),
                Size = new Size(430, 500), // Extended to fit more exams
                BorderStyle = BorderStyle.None,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true, // ✅ ENABLE SCROLLING
                BackColor = Color.White
            };
            panelExamList.Controls.Add(flowExams);
        }

    }
}
