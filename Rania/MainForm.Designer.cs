namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnTakeExam;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Text = "מסך ראשי - סטודנט";
            this.Size = new System.Drawing.Size(500, 300);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            btnTakeExam = new System.Windows.Forms.Button()
            {
                Text = "📝 ביצוע מבחן",
                Location = new System.Drawing.Point(150, 50),
                Size = new System.Drawing.Size(200, 50),
                BackColor = System.Drawing.Color.LightSkyBlue
            };
            btnTakeExam.Click += new System.EventHandler(this.BtnTakeExam_Click);
            this.Controls.Add(btnTakeExam);

            btnHistory = new System.Windows.Forms.Button()
            {
                Text = "📊 היסטוריית ציונים",
                Location = new System.Drawing.Point(150, 120),
                Size = new System.Drawing.Size(200, 50),
                BackColor = System.Drawing.Color.LightGreen
            };
            btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            this.Controls.Add(btnHistory);

            btnExit = new System.Windows.Forms.Button()
            {
                Text = "🚪 יציאה",
                Location = new System.Drawing.Point(150, 190),
                Size = new System.Drawing.Size(200, 40),
                BackColor = System.Drawing.Color.LightCoral
            };
            btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            this.Controls.Add(btnExit);
        }
    }
}
