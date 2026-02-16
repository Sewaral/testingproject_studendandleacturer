namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    partial class Form1
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
            ClientSize = new Size(10, 840);

            panel1 = new Panel();
            topBar = new Panel();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            label1 = new Label();
            subLabel = new Label();

            panel1.SuspendLayout();
            topBar.SuspendLayout();
            SuspendLayout();

            // panel1
            panel1.Dock = DockStyle.Fill;
            panel1.Controls.Add(topBar);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(subLabel);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1280, 840);
            panel1.TabIndex = 1;

            string imagePath = Path.Combine(Application.StartupPath, "Resources", "main.jpg");
            if (File.Exists(imagePath))
           {
                panel1.BackgroundImage = Image.FromFile(imagePath);
                panel1.BackgroundImageLayout = ImageLayout.Stretch;
            }

            // topBar
            topBar.Dock = DockStyle.Top;
            topBar.Height = 70;
            topBar.BackColor = Color.Transparent;
            topBar.Location = new Point(0, 0);
            topBar.Controls.Add(CreateNavButton("Student Sign Up", 10, button1_Click));
            topBar.Controls.Add(CreateNavButton("Lecturer Sign Up", 170, button2_Click));
            topBar.Controls.Add(CreateNavButton("Student Login", 330, button3_Click));
            topBar.Controls.Add(CreateNavButton("Lecturer Login", 490, button4_Click));

            

            // Form1
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 840);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Controls.Add(panel1);
            Name = "Form1";
            Text = "SmartExam - Home";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            topBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Button CreateNavButton(string text, int x, EventHandler clickHandler)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.FromArgb(28, 49, 64);
            btn.BackColor = Color.Transparent;
            btn.Location = new Point(x, 20);
            btn.AutoSize = true;
            btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn.Click += (s, e) =>
            {
                HighlightButton(btn);
                clickHandler(s, e);
            };
            return btn;
        }

        private void HighlightButton(Button selected)
        {
            foreach (Control ctrl in topBar.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                    btn.FlatAppearance.BorderSize = 0;
                }
            }
            selected.Font = new Font("Segoe UI", 10F, FontStyle.Underline);
        }

        #endregion

        private Panel panel1;
        private Panel topBar;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label label1;
        private Label subLabel;
    }
}
