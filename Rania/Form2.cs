using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ClosedXML.Excel;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class Form2 : Form
    {
        private string studentName;
        private string historyFilePath;
        private DataTable historyData;

        private Label lblAverageOverall;
        private Label lblLatestOverall;
        private Label lblProgressOverall;
        private Chart scoreChart;
        private DataGridView categoryGrid;

        public Form2(string name)
        {
            studentName = name.Trim();
            string fileName = $"{studentName}_history.xlsx";
            historyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data", fileName);
            historyFilePath = Path.GetFullPath(historyFilePath);

            InitializeComponent();
            InitializeCustomComponents();

            LoadStudentHistory();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        private void InitializeCustomComponents()
        {
            this.Text = $" Exam Progress for {studentName}";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 250);
            this.Font = new Font("Segoe UI", 10);

            lblAverageOverall = new Label()
            {
                Location = new Point(20, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
            };
            this.Controls.Add(lblAverageOverall);

            lblLatestOverall = new Label()
            {
                Location = new Point(20, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
            };
            this.Controls.Add(lblLatestOverall);

            lblProgressOverall = new Label()
            {
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 90, 150),
            };
            this.Controls.Add(lblProgressOverall);

            Label title = new Label
            {
                Text = " Category Progress Summary",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 90),
                AutoSize = true
            };
            this.Controls.Add(title);

            categoryGrid = new DataGridView
            {
                Location = new Point(20, 120),
                Size = new Size(840, 180),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = ColorTranslator.FromHtml("#9FB3DF"),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.Black
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                },
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(categoryGrid);

            scoreChart = new Chart()
            {
                Location = new Point(20, 320),
                Size = new Size(840, 300),
                BackColor = Color.White,
                ForeColor = ColorTranslator.FromHtml("#9FB3DF"), // ✅ your color here
                BorderlineDashStyle = ChartDashStyle.Solid,
                BorderlineWidth = 1,
            };

            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "Exam Number";
            chartArea.AxisX.TitleForeColor = Color.Black; // ✅ X-axis title in black

            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;

            chartArea.AxisY.Title = "Score (%)";
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 100;
            chartArea.AxisX.TitleForeColor = Color.Black; // ✅ X-axis title in black

            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;

            scoreChart.ChartAreas.Add(chartArea);

            Series series = new Series("Scores")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.SteelBlue,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8,
                XValueType = ChartValueType.Int32,
            };
            scoreChart.Series.Add(series);

            this.Controls.Add(scoreChart);
        }

        protected virtual void LoadStudentHistory()
        {
            if (!File.Exists(historyFilePath))
            {
               
                return;
            }

            historyData = LoadExcelToDataTable(historyFilePath);

            if (historyData.Rows.Count == 0)
            {
                MessageBox.Show($"No exam records found in history for '{studentName}'.", "No Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AnalyzeOverallProgress(historyData);
            AnalyzeCategoryProgress(historyData);
            DrawChart(historyData);
        }

        public DataTable LoadExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                bool firstRow = true;

                foreach (var row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        foreach (var cell in row.Cells())
                            dt.Columns.Add(cell.Value.ToString().Trim());
                        firstRow = false;
                    }
                    else
                    {
                        var dataRow = dt.NewRow();
                        int colIndex = 0;
                        foreach (var cell in row.Cells())
                        {
                            if (cell.DataType == XLDataType.DateTime)
                                dataRow[colIndex++] = cell.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                            else if (cell.DataType == XLDataType.Number)
                                dataRow[colIndex++] = cell.GetDouble();
                            else
                                dataRow[colIndex++] = cell.Value.ToString().Trim();
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
            }

            return dt;
        }


        // 🔹 Helper methods for analyzing scores programmatically (used in automated environments)

        public void SetScoreLabels(Label average, Label latest, Label progress)
        {
            lblAverageOverall = average;
            lblLatestOverall = latest;
            lblProgressOverall = progress;
        }

        public void AnalyzeScoresManually(double[] scores)
        {
            var table = new DataTable();
            table.Columns.Add("Score (%)", typeof(string));

            foreach (var score in scores)
                table.Rows.Add(score.ToString("F2"));

            AnalyzeOverallProgress(table);
        }

        public void AnalyzeOverallProgress(DataTable table)
        {
            string scoreCol = table.Columns.Contains("Score (%)") ? "Score (%)" : null;

            if (scoreCol == null)
            {
                lblAverageOverall.Text = "No score data available.";
                lblLatestOverall.Text = "";
                lblProgressOverall.Text = "";
                return;
            }

            var scores = table.AsEnumerable()
                .Select(r => double.TryParse(r[scoreCol].ToString(), out double v) ? v : (double?)null)
                .Where(v => v.HasValue)
                .Select(v => v.Value)
                .ToList();

            if (scores.Count == 0)
            {
                lblAverageOverall.Text = "No valid scores found.";
                lblLatestOverall.Text = "";
                lblProgressOverall.Text = "";
                return;
            }

            double latest = scores.Last();
            double previousAvg = scores.Count > 1 ? scores.Take(scores.Count - 1).Average() : 0;
            double overallAvg = scores.Average();

            // ✅ Custom logic: If latest is 100, show +100% progress always
            double progress = latest == 100 ? 100 : latest - previousAvg;

            string arrow = progress > 0 ? "⬆" : progress < 0 ? "⬇" : "⭕";
            string progressText = progress >= 0 ? $"+{progress:F2}%" : $"{progress:F2}%";

            lblAverageOverall.Text = $"Average: {overallAvg:F2}%";
            lblLatestOverall.Text = $"Latest: {latest:F2}%";
            lblProgressOverall.Text = $"Progress: {arrow} {progressText}";
        }


        private void AnalyzeCategoryProgress(DataTable table)
        {
            var resultTable = new DataTable();
            resultTable.Columns.Add("Category");
            resultTable.Columns.Add("Average (%)");
            resultTable.Columns.Add("First Score (%)");
            resultTable.Columns.Add("Last Score (%)");
            resultTable.Columns.Add("Trend");

            var categoryGroups = table.AsEnumerable()
                .Where(r => !string.IsNullOrWhiteSpace(r["Category"].ToString()))
                .GroupBy(r => r["Category"].ToString().Trim().ToLowerInvariant());

            foreach (var group in categoryGroups)
            {
                var scores = group
                    .Select(r => double.TryParse(r["Score (%)"].ToString(), out double val) ? val : (double?)null)
                    .Where(v => v.HasValue)
                    .Select(v => v.Value)
                    .ToList();

                if (scores.Count == 0) continue;

                double avg = scores.Average();
                double first = scores.First();
                double last = scores.Last();
                double progress = last - avg;

                string trend = progress > 0 ? "Improving 😊" :
                               progress < 0 ? "Declining 😕" :
                               "Stable 😐";

                string displayCategory = char.ToUpper(group.Key[0]) + group.Key.Substring(1);
                resultTable.Rows.Add(displayCategory, avg.ToString("F2"), first.ToString("F2"), last.ToString("F2"), trend);
            }

            categoryGrid.DataSource = resultTable;
        }
        public bool LoadHistoryData()
        {
            if (!File.Exists(historyFilePath))
                return false;

            historyData = LoadExcelToDataTable(historyFilePath);

            if (historyData.Rows.Count == 0)
                return false;

            AnalyzeOverallProgress(historyData);
            AnalyzeCategoryProgress(historyData);
            DrawChart(historyData);
            return true;
        }

        private void DrawChart(DataTable table)
        {
            var series = scoreChart.Series["Scores"];
            series.Points.Clear();

            string scoreCol = table.Columns.Contains("Score (%)") ? "Score (%)" : null;
            if (scoreCol == null) return;

            int examNum = 1;
            foreach (DataRow row in table.Rows)
            {
                if (double.TryParse(row[scoreCol].ToString(), out double scoreVal))
                {
                    series.Points.AddXY(examNum, scoreVal);
                    examNum++;
                }
            }

            scoreChart.ChartAreas[0].RecalculateAxesScale();
        }
    }
}
