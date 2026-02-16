using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class done : Form
    {
        public done(string message)
        {
            InitializeComponent();

            // ✅ Set background image
            string imagePath = Path.Combine(Application.StartupPath, "Resources", "done.jpg");
            if (File.Exists(imagePath))
            {
                this.BackgroundImage = Image.FromFile(imagePath);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }

            // ✅ Form properties
            this.Text = "done";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Width = 240;
            this.Height = 280;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;

            Label lbl = new Label()
            {
                Text = message,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                AutoSize = false,
                Size = new Size(this.Width - 40, 70),
                Location = new Point(20, 100),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                BackColor = Color.Transparent
            };

            Button btnOk = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(90, 30),
                Location = new Point((this.Width - 100) / 2, this.Height - 90)  // 70 px from top for margin + button height
            };



            this.Controls.Add(lbl);
            this.Controls.Add(btnOk);
            this.AcceptButton = btnOk;
        }
    }
}
