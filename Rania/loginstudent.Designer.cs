namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    partial class loginstudent
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.innerPanel = new Panel();
            string imagePath = System.IO.Path.Combine(Application.StartupPath, "Resources", "loginbackground.png");
            if (System.IO.File.Exists(imagePath))
            {
                this.panel1.BackgroundImage = Image.FromFile(imagePath);
                this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
            }
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.BackColor = Color.Transparent;
            this.Controls.Add(this.panel1);

            pictureBox1 = new PictureBox();
            button1 = new Button();
            maskedTextBox2 = new MaskedTextBox();
            maskedTextBox1 = new MaskedTextBox();
            label3 = new Label();
            label2 = new Label();
            linkLabel1 = new LinkLabel();
            label1 = new Label();

            SuspendLayout();

            int formWidth = 500;
            int formHeight = 520;
            int contentWidth = 250;
            int centerX = (formWidth + 100) / 2 - contentWidth / 2;

            // innerPanel - White content area with black border
            innerPanel.Size = new Size(320, 400);
            innerPanel.Location = new Point((formWidth - innerPanel.Width) / 2, 50);
            innerPanel.BackColor = Color.White;
            innerPanel.BorderStyle = BorderStyle.FixedSingle;

            // label1 - Student Login
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.Location = new Point((innerPanel.Width - 200) / 2, 10);
            label1.Text = "Student Login";

            // pictureBox1 - Icon under title (larger)
            pictureBox1.Size = new Size(70, 70);
            pictureBox1.Location = new Point((innerPanel.Width - 70) / 2, 55);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            string iconPath = System.IO.Path.Combine(Application.StartupPath, "Resources", "studenticon.png");
            if (System.IO.File.Exists(iconPath))
            {
                pictureBox1.Image = Image.FromFile(iconPath);
            }

            // label2 - Username
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(35, 135);
            label2.Text = "Username:";

            // maskedTextBox1
            maskedTextBox1.Font = new Font("Segoe UI", 10F);
            maskedTextBox1.Location = new Point(35, 160);
            maskedTextBox1.Size = new Size(250, 30);

            // label3 - Password
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(35, 195);
            label3.Text = "Password:";

            // maskedTextBox2
            maskedTextBox2.Font = new Font("Segoe UI", 10F);
            maskedTextBox2.Location = new Point(35, 220);
            maskedTextBox2.Size = new Size(250, 30);
            maskedTextBox2.UseSystemPasswordChar = true;

            // button1 - Login
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.BackColor = Color.FromArgb(33, 103, 208);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Location = new Point(35, 265);
            button1.Size = new Size(250, 40);
            button1.Text = "Log In";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;

            // linkLabel1 - Register
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9F);
            linkLabel1.LinkColor = Color.MediumBlue;
            linkLabel1.Location = new Point(35, 315);
            linkLabel1.Text = "Don't have an account? Sign up";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;

            // Form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(formWidth, formHeight);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(panel1);
            panel1.Controls.Add(innerPanel);
            innerPanel.Controls.Add(label1);
            innerPanel.Controls.Add(pictureBox1);
            innerPanel.Controls.Add(label2);
            innerPanel.Controls.Add(maskedTextBox1);
            innerPanel.Controls.Add(label3);
            innerPanel.Controls.Add(maskedTextBox2);
            innerPanel.Controls.Add(button1);
            innerPanel.Controls.Add(linkLabel1);
            Name = "loginstudent";
            Text = "loginstudent";
            Load += loginstudent_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel innerPanel;
        private PictureBox pictureBox1;
        private Button button1;
        private MaskedTextBox maskedTextBox2;
        private MaskedTextBox maskedTextBox1;
        private Label label3;
        private Label label2;
        private LinkLabel linkLabel1;
        private Label label1;
    }
}