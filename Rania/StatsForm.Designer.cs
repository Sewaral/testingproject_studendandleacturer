using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    partial class StatsForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvScores;
        private Label lblAverage;
        private Chart chartStats;
        private ComboBox comboBoxStudents;
        private Button btnShowProgress;
        private Chart chartProgress;
        private Label lblStudentAverage;
        private RichTextBox txtStatsSummary;
        private Label lblLegend;
        private Label lblTableTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvScores = new DataGridView();
            this.lblAverage = new Label();
            this.chartStats = new Chart();
            this.comboBoxStudents = new ComboBox();
            this.btnShowProgress = new Button();
            this.chartProgress = new Chart();
            this.lblStudentAverage = new Label();
            this.txtStatsSummary = new RichTextBox();
            this.lblLegend = new Label();
            this.lblTableTitle = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvScores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProgress)).BeginInit();
            this.SuspendLayout();

            // StatsForm
            this.ClientSize = new Size(1000, 800);
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F);
            this.Text = "Score Statistics";
            this.Load += new System.EventHandler(this.StatsForm_Load);

            // Table Title
            this.lblTableTitle.Text = "Student Score Summary";
            this.lblTableTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTableTitle.Location = new Point(10, 10);
            this.lblTableTitle.Size = new Size(400, 25);

            // DataGridView
            this.dgvScores.Location = new Point(10, 40);
            this.dgvScores.Size = new Size(600, 250);
            this.dgvScores.ReadOnly = true;

            this.dgvScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScores.EnableHeadersVisualStyles = false;
            this.dgvScores.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ColorTranslator.FromHtml("#9FB3DF"), // ✅ Header background
                ForeColor = Color.Black,                         // Header text
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };


            // Pie Chart
            this.chartStats.Location = new Point(620, 40);
            this.chartStats.Size = new Size(350, 250);
            this.chartStats.ChartAreas.Add(new ChartArea("PieArea"));
            this.chartStats.Series.Add(new Series("Distribution")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                IsVisibleInLegend = false
            });
            this.chartStats.Titles.Add("Scores on Your Exams");


            // Legend Label
            this.lblLegend.Text = "● Blue = Low (<60)   ● Orange = Medium (60–79)   ● Red = High (80+)";
            this.lblLegend.Location = new Point(620, 295);
            this.lblLegend.Size = new Size(350, 40);
            this.lblLegend.ForeColor = Color.Black;
            this.lblLegend.Font = new Font("Segoe UI", 9F);

            // ComboBox for Students
            this.comboBoxStudents.Location = new Point(10, 350);
            this.comboBoxStudents.Size = new Size(300, 30);
            this.comboBoxStudents.DropDownStyle = ComboBoxStyle.DropDownList;

            // Button Show Progress
            this.btnShowProgress.Text = "Show Progress Chart";
            this.btnShowProgress.Location = new Point(320, 350);
            this.btnShowProgress.Size = new Size(300, 30);
            this.btnShowProgress.BackColor = ColorTranslator.FromHtml("#9FB3DF");
            this.btnShowProgress.ForeColor = Color.White;
            this.btnShowProgress.Click += new System.EventHandler(this.BtnShowProgress_Click);

            // Line Chart for Progress
            this.chartProgress.Location = new Point(10, 390);
            this.chartProgress.Size = new Size(960, 250);
            this.chartProgress.ChartAreas.Add(new ChartArea("ProgressArea"));
            this.chartProgress.Titles.Add("Student Score Progress Over Time");

            // Student Average Label
            this.lblStudentAverage.Location = new Point(10, 650);
            this.lblStudentAverage.Size = new Size(300, 30);
            this.lblStudentAverage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Overall Average Label
            this.lblAverage.Text = "Overall Average:";
            this.lblAverage.Location = new Point(10, 690);
            this.lblAverage.Size = new Size(960, 30);
            this.lblAverage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblAverage.BackColor = Color.Gainsboro;
            this.lblAverage.Padding = new Padding(5);

            // Summary RichTextBox
            this.txtStatsSummary.Location = new Point(10, 730);
            this.txtStatsSummary.Size = new Size(960, 60);
            this.txtStatsSummary.ReadOnly = true;
            this.txtStatsSummary.BorderStyle = BorderStyle.None;
            this.txtStatsSummary.BackColor = Color.FromArgb(240, 240, 240);

            // Controls
            this.Controls.Add(this.lblTableTitle);
            this.Controls.Add(this.dgvScores);
            this.Controls.Add(this.chartStats);
            this.Controls.Add(this.lblLegend);
            this.Controls.Add(this.comboBoxStudents);
            this.Controls.Add(this.btnShowProgress);
            this.Controls.Add(this.chartProgress);
            this.Controls.Add(this.lblStudentAverage);
            this.Controls.Add(this.lblAverage);
            this.Controls.Add(this.txtStatsSummary);

            ((System.ComponentModel.ISupportInitialize)(this.dgvScores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProgress)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
