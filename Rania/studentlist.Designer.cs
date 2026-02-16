using System.Drawing;
using System.Windows.Forms;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    partial class studentlist
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnTakeExam;
        private Button btnHistory;
        private Button btnProgress;
        private Button btnLogout;
        private Label lblWelcome;
        private Panel panel1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true; // optional — keep minimize enabled

            this.panel1 = new Panel();
            this.panel1.Dock = DockStyle.Fill;

            string imagePath = System.IO.Path.Combine(Application.StartupPath, "Resources", "student.png");
            if (System.IO.File.Exists(imagePath))
            {
                this.panel1.BackgroundImage = Image.FromFile(imagePath);
                this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
            }

            this.Text = "Student Dashboard";
            this.Size = new Size(815, 512);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.panel1);

            // Welcome label (top-right, safe margin to avoid cutoff)
            lblWelcome = new Label()
            {
                Text = "Welcome, Student!",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(580, 20),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent // ensures no background is drawn

            };
            this.panel1.Controls.Add(lblWelcome);

            // Common button style
            Color buttonColor = ColorTranslator.FromHtml("#9FB3DF");
            Size reducedButtonSize = new Size(220, 45);
            Font buttonFont = new Font("Segoe UI", 11.5F, FontStyle.Bold);

            // Slightly more centered under "SmartExam"
            Point examBtnLoc = new Point(420, 230);
            Point historyBtnLoc = new Point(420, 290);
            Point progressBtnLoc = new Point(420, 350);

            // Take Exam
            btnTakeExam = CreateFixedButton("Take Exam", buttonColor, buttonFont, examBtnLoc, reducedButtonSize);
            btnTakeExam.Click += new System.EventHandler(this.BtnTakeExam_Click);
            this.panel1.Controls.Add(btnTakeExam);

            // Exam History
            btnHistory = CreateFixedButton("Exam History", buttonColor, buttonFont, historyBtnLoc, reducedButtonSize);
            btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            this.panel1.Controls.Add(btnHistory);

            // Progress Tracking
            btnProgress = CreateFixedButton("Progress Tracking", buttonColor, buttonFont, progressBtnLoc, reducedButtonSize);
            btnProgress.Click += new System.EventHandler(this.BtnProgress_Click);
            this.panel1.Controls.Add(btnProgress);

            // Log Out (bottom-right, small red button)
            btnLogout = new Button()
            {
                Text = "Log Out",
                Size = new Size(90, 30),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 40)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += new System.EventHandler(this.BtnExit_Click);
            this.panel1.Controls.Add(btnLogout);
        }

        private Button CreateFixedButton(string text, Color backColor, Font font, Point location, Size size)
        {
            var button = new Button()
            {
                Text = text,
                Size = size,
                Font = font,
                BackColor = backColor,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Location = location
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }
    }
}
