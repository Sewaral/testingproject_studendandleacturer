using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class StatsForm : Form
    {
        private string historyFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data");
        private string lecturerId;

        public StatsForm(string lecturerId)
        {
            InitializeComponent();
            this.lecturerId = lecturerId;

            if (!LoadStatsData())
            {
                this.Dispose(); // ❗ Closes the form
            }
        }
        public bool LoadStatsData()
        {
            var table = new DataTable();
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Exam");
            table.Columns.Add("Score", typeof(int));
            table.Columns.Add("Student");

            int low = 0, mid = 0, high = 0;
            double total = 0;
            int count = 0;

            if (!Directory.Exists(historyFolderPath))
            {
                MessageBox.Show("Required folder not found.");
                return false;
            }

            var files = Directory.GetFiles(historyFolderPath, "*_history.xlsx");
            foreach (var filePath in files)
            {
                string studentName = Path.GetFileNameWithoutExtension(filePath).Replace("_history", "");

                using (var workbook = new XLWorkbook(filePath))
                {
                    var ws = workbook.Worksheet(1);
                    foreach (var row in ws.RangeUsed().RowsUsed().Skip(1))
                    {
                        try
                        {
                            string rowLecturerId = row.Cell(6).GetValue<string>().Trim();
                            if (rowLecturerId != lecturerId) continue;

                            string dateStr = row.Cell(1).GetString().Trim();
                            if (!DateTime.TryParse(dateStr, out DateTime date))
                                continue;

                            string examId = row.Cell(2).GetString().Trim();
                            if (string.IsNullOrEmpty(examId))
                                continue;

                            string scoreStr = row.Cell(5).GetString().Trim();
                            if (!double.TryParse(scoreStr, out double score))
                                continue;

                            total += score;
                            count++;

                            table.Rows.Add(date, $"Exam {examId}", (int)score, studentName);

                            if (score < 60) low++;
                            else if (score < 80) mid++;
                            else high++;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }

            if (count == 0)
                return false;

            dgvScores.DataSource = table;
            lblAverage.Text = $"Overall Average: {Math.Round(total / count, 2)}";

            chartStats.Series.Clear();
            var series = new Series("Score Distribution")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Label = "#PERCENT",
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            series.Points.AddXY("Low", low);
            series.Points.AddXY("Medium", mid);
            series.Points.AddXY("High", high);
            series.Points[0].Color = Color.SteelBlue;
            series.Points[1].Color = Color.Orange;
            series.Points[2].Color = Color.Firebrick;

            chartStats.Series.Add(series);

            comboBoxStudents.Items.Clear();
            comboBoxStudents.Items.AddRange(
                table.AsEnumerable()
                      .Select(r => r.Field<string>("Student"))
                      .Where(s => !string.IsNullOrWhiteSpace(s))
                      .Distinct()
                      .OrderBy(s => s)
                      .ToArray()
            );

            txtStatsSummary.Clear();
            var highestRow = table.AsEnumerable().OrderByDescending(r => r.Field<int>("Score")).FirstOrDefault();
            var lowestRow = table.AsEnumerable().OrderBy(r => r.Field<int>("Score")).FirstOrDefault();

            if (highestRow != null)
                txtStatsSummary.AppendText($"Highest score: {highestRow["Score"]} ({highestRow["Student"]})\n");
            if (lowestRow != null)
                txtStatsSummary.AppendText($"Lowest score: {lowestRow["Score"]} ({lowestRow["Student"]})\n");

            return true;
        }


        public void StatsForm_Load(object sender, EventArgs e)
        {
            var table = new DataTable();
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Exam");
            table.Columns.Add("Score", typeof(int));
            table.Columns.Add("Student");

            int low = 0, mid = 0, high = 0;
            double total = 0;
            int count = 0;

            if (!Directory.Exists(historyFolderPath))
            {
                MessageBox.Show("Required folder not found.");
                return;
            }

            var files = Directory.GetFiles(historyFolderPath, "*_history.xlsx");
            foreach (var filePath in files)
            {
                string studentName = Path.GetFileNameWithoutExtension(filePath).Replace("_history", "");

                using (var workbook = new XLWorkbook(filePath))
                {
                    var ws = workbook.Worksheet(1);
                    foreach (var row in ws.RangeUsed().RowsUsed().Skip(1))
                    {
                        try
                        {
                            string rowLecturerId = row.Cell(6).GetValue<string>().Trim();

                            if (rowLecturerId != lecturerId) continue;


                            // ✅ Date (safe parse with fallback)
                            string dateStr = row.Cell(1).GetString().Trim();
                            if (!DateTime.TryParse(dateStr, out DateTime date))
                            {
                                MessageBox.Show($"⚠️ Invalid date: '{dateStr}' — skipping this row.");
                                continue;
                            }

                            // ✅ Exam ID
                            string examId = row.Cell(2).GetString().Trim();
                            if (string.IsNullOrEmpty(examId))
                            {
                                MessageBox.Show("⚠️ Exam ID is empty — skipping this row.");
                                continue;
                            }

                            // ✅ Score (safe parse)
                            string scoreStr = row.Cell(5).GetString().Trim();
                            if (!double.TryParse(scoreStr, out double score))
                            {
                                MessageBox.Show($"⚠️ Invalid score: '{scoreStr}' — skipping this row.");
                                continue;
                            }

                            total += score;
                            count++;


                            table.Rows.Add(date, $"Exam {examId}", (int)score, studentName);

                            


                            if (score < 60) low++;
                            else if (score < 80) mid++;
                            else high++;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }

            if (count == 0)
            {
                using (var cmb = new CustomMessageBox("No grades found for your exams"))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            dgvScores.DataSource = table;
            lblAverage.Text = $"Overall Average: {Math.Round(total / count, 2)}";

            chartStats.Series.Clear();
            var series = new Series("Score Distribution")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Label = "#PERCENT",
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            series.Points.AddXY("Low", low);
            series.Points.AddXY("Medium", mid);
            series.Points.AddXY("High", high);
            series.Points[0].Color = Color.SteelBlue;
            series.Points[1].Color = Color.Orange;
            series.Points[2].Color = Color.Firebrick;

            chartStats.Series.Add(series);

            comboBoxStudents.Items.Clear();
            comboBoxStudents.Items.AddRange(
                table.AsEnumerable()
                      .Select(r => r.Field<string>("Student"))
                      .Where(s => !string.IsNullOrWhiteSpace(s))
                      .Distinct()
                      .OrderBy(s => s)
                      .ToArray()
            );

            txtStatsSummary.Clear();
            var highestRow = table.AsEnumerable().OrderByDescending(r => r.Field<int>("Score")).FirstOrDefault();
            var lowestRow = table.AsEnumerable().OrderBy(r => r.Field<int>("Score")).FirstOrDefault();

            if (highestRow != null)
                txtStatsSummary.AppendText($"Highest score: {highestRow["Score"]} ({highestRow["Student"]})\n");
            if (lowestRow != null)
                txtStatsSummary.AppendText($"Lowest score: {lowestRow["Score"]} ({lowestRow["Student"]})\n");
        }



        private void BtnShowProgress_Click(object sender, EventArgs e)
        {
            string selectedStudent = comboBoxStudents.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedStudent))
            {
                MessageBox.Show("Please select a student.");
                return;
            }

            string fileName = $"{selectedStudent}_history.xlsx";
            string path = Path.Combine(historyFolderPath, fileName);
            if (!File.Exists(path))
            {
                MessageBox.Show("No history file found for this student.");
                return;
            }

            var scores = new List<double>();
            var dates = new List<string>();

            using (var workbook = new XLWorkbook(path))
            {
                var ws = workbook.Worksheet(1);
                foreach (var row in ws.RangeUsed().RowsUsed().Skip(1))
                {
                    string rowLecturerId = row.Cell(6).GetValue<string>().Trim();
                    if (!rowLecturerId.Equals(lecturerId, StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (double.TryParse(row.Cell(5).GetValue<string>(), out double score))
                    {
                        scores.Add(score);

                        string rawDate = row.Cell(1).GetValue<string>().Trim();
                        if (DateTime.TryParse(rawDate, out DateTime date))
                            dates.Add(date.ToShortDateString());
                        else
                            dates.Add($"Unknown {scores.Count}");
                    }
                }
            }

            if (scores.Count == 0)
            {
                MessageBox.Show("This student has no scores from your exams.");
                return;
            }

            double avg = scores.Average();
            lblStudentAverage.Text = $"Student Average: {avg:F2}";

            chartProgress.Series.Clear();
            chartProgress.ChartAreas.Clear();
            chartProgress.ChartAreas.Add(new ChartArea("MainArea"));

            var series = new Series("Progress")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3
            };

            for (int i = 0; i < scores.Count; i++)
            {
                string label = i < dates.Count ? dates[i] : $"Exam {i + 1}";
                series.Points.AddXY(label, scores[i]);
            }

            chartProgress.Series.Add(series);
        }



    }
}
