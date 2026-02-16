using System.Drawing;
using System.Windows.Forms;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    partial class leacturelist
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnQuestions;
        private Button btnCreateExam;
        private Button btnStats;
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

            string imagePath = System.IO.Path.Combine(Application.StartupPath, "Resources", "lec.png");
            if (System.IO.File.Exists(imagePath))
            {
                this.panel1.BackgroundImage = Image.FromFile(imagePath);
                this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
            }

            this.Text = "Lecturer Dashboard";
            this.Size = new Size(815, 512);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.panel1);

            // Welcome Label (top-right, not cut)
            lblWelcome = new Label()
            {
                Text = "Welcome, Lecturer!",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(580, 20),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent // ensures no background is drawn

            };
            this.panel1.Controls.Add(lblWelcome);

            // Common styles
            Color buttonColor = ColorTranslator.FromHtml("#9FB3DF");
            Size buttonSize = new Size(220, 45);
            Font buttonFont = new Font("Segoe UI", 11.5F, FontStyle.Bold);

            // Aligned under "SmartExam"
            Point questionsBtnLoc = new Point(420, 230);
            Point createExamBtnLoc = new Point(420, 290);
            Point statsBtnLoc = new Point(420, 350);

            // Buttons
            btnQuestions = CreateFixedButton("Manage Questions", buttonColor, buttonFont, questionsBtnLoc, buttonSize);
            btnQuestions.Click += btnQuestions_Click;
            this.panel1.Controls.Add(btnQuestions);

            btnCreateExam = CreateFixedButton("Create Exam", buttonColor, buttonFont, createExamBtnLoc, buttonSize);
            btnCreateExam.Click += btnCreateExam_Click;
            this.panel1.Controls.Add(btnCreateExam);

            btnStats = CreateFixedButton("View Statistics", buttonColor, buttonFont, statsBtnLoc, buttonSize);
            btnStats.Click += btnStats_Click;
            this.panel1.Controls.Add(btnStats);

            // Log Out Button
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
            btnLogout.Click += (sender, e) => this.Close();
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
