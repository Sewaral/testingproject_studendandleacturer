namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    partial class Signupasleacture
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        public void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.formPanel = new Panel();
            this.titleLabel = new Label();
            this.subtitleLabel = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.textBox3 = new TextBox();
            this.textBox4 = new TextBox();
            this.button1 = new Button();
            this.checkBoxShow = new CheckBox();

            // Main Form Settings
            this.ClientSize = new Size(920, 520);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "Signupasleacture";
            this.Text = "Lecturer Sign Up";

            // panel1 (Background Image)
            this.panel1.Dock = DockStyle.Fill;
            string imagePath = System.IO.Path.Combine(Application.StartupPath, "Resources", "lectsignup.jpg");
            if (System.IO.File.Exists(imagePath))
            {
                this.panel1.BackgroundImage = Image.FromFile(imagePath);
                this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
            }
            this.panel1.BackColor = Color.Transparent;
            this.Controls.Add(this.panel1);

            // formPanel
            this.formPanel.Size = new Size(380, 420);
            this.formPanel.Location = new Point(510, 50);
            this.formPanel.BackColor = Color.White;
            this.panel1.Controls.Add(this.formPanel);

            Color titleColor = Color.Black;

            // titleLabel
            this.titleLabel.Text = "SIGN UP";
            this.titleLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.titleLabel.ForeColor = titleColor;
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = Color.Transparent;
            this.titleLabel.Location = new Point(90, 0); // moved higher

            // subtitleLabel
            this.subtitleLabel.Text = "Create your lecturer account";
            this.subtitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.subtitleLabel.ForeColor = titleColor;
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.BackColor = Color.Transparent;
            this.subtitleLabel.Location = new Point(85, 50);

            // textBox1 - Username
            this.textBox1.PlaceholderText = "Username";
            this.textBox1.Font = new Font("Segoe UI", 10F);
            this.textBox1.BackColor = Color.White;
            this.textBox1.BorderStyle = BorderStyle.FixedSingle;
            this.textBox1.Location = new Point(65, 95);
            this.textBox1.Size = new Size(250, 30);

            // textBox2 - Email
            this.textBox2.PlaceholderText = "Email";
            this.textBox2.Font = new Font("Segoe UI", 10F);
            this.textBox2.BackColor = Color.White;
            this.textBox2.BorderStyle = BorderStyle.FixedSingle;
            this.textBox2.Location = new Point(65, 140);
            this.textBox2.Size = new Size(250, 30);

            // textBox3 - Password
            this.textBox3.PlaceholderText = "Password";
            this.textBox3.Font = new Font("Segoe UI", 10F);
            this.textBox3.UseSystemPasswordChar = true;
            this.textBox3.BackColor = Color.White;
            this.textBox3.BorderStyle = BorderStyle.FixedSingle;
            this.textBox3.Location = new Point(65, 185);
            this.textBox3.Size = new Size(250, 30);

            // checkBoxShow
            this.checkBoxShow.Text = "Show Password";
            this.checkBoxShow.Font = new Font("Segoe UI", 9F);
            this.checkBoxShow.ForeColor = Color.Black;
            this.checkBoxShow.BackColor = Color.Transparent;
            this.checkBoxShow.Location = new Point(65, 220);

            // textBox4 - Lecturer ID
            this.textBox4.PlaceholderText = "Lecturer ID";
            this.textBox4.Font = new Font("Segoe UI", 10F);
            this.textBox4.BackColor = Color.White;
            this.textBox4.BorderStyle = BorderStyle.FixedSingle;
            this.textBox4.Location = new Point(65, 255);
            this.textBox4.Size = new Size(250, 30);

            // button1
            this.button1.Text = "Sign Up";
            this.button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.button1.BackColor = Color.FromArgb(33, 103, 208);
            this.button1.ForeColor = Color.White;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Size = new Size(250, 40);
            this.button1.Location = new Point(65, 355);
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // Add controls
            this.formPanel.Controls.Add(this.titleLabel);
            this.formPanel.Controls.Add(this.subtitleLabel);
            this.formPanel.Controls.Add(this.textBox1);
            this.formPanel.Controls.Add(this.textBox2);
            this.formPanel.Controls.Add(this.textBox3);
            this.formPanel.Controls.Add(this.checkBoxShow);
            this.formPanel.Controls.Add(this.textBox4);
            this.formPanel.Controls.Add(this.button1);
        }

        #endregion

        public Panel panel1;
        public Panel formPanel;
        public Label titleLabel;
        public Label subtitleLabel;
        public TextBox textBox1;
        public TextBox textBox2;
        public TextBox textBox3;
        public TextBox textBox4;
        public Button button1;
        public CheckBox checkBoxShow;
    }
}
