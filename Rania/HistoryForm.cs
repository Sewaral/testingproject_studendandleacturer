using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class HistoryForm : Form
    {
        private Panel statsPanel;
        private Label averageLabel;
        private Label progressLabel;
        private DataTable historyData;
        private DataGridView dataGridView1 = new DataGridView();

        public HistoryForm(string studentName)
        {
            InitializeComponent();
            InitializeCustomComponents();
            LoadStudentHistory(studentName);
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Student Exam History";
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Padding = new Padding(20);
            this.Font = new Font("Segoe UI", 10);
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Exam History Table",
                Location = new Point(20, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50)
            };
            this.Controls.Add(titleLabel);

            dataGridView1.Location = new Point(20, 50);
            dataGridView1.Size = new Size(840, 300);  // Wider size to avoid horizontal scroll
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.EnableHeadersVisualStyles = false;


            dataGridView1.BackgroundColor = Color.FromArgb(240, 240, 240);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(240, 240, 240),
                SelectionForeColor = Color.Black,
                Font = new Font("Segoe UI", 9.5f),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5, 2, 5, 2),
                WrapMode = DataGridViewTriState.False
            };
            dataGridView1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ColorTranslator.FromHtml("#9FB3DF"),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };
            dataGridView1.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(250, 250, 250)
            };
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.MultiSelect = false;

            dataGridView1.RowPostPaint += (sender, e) =>
            {
                var grid = sender as DataGridView;
                var rowIdx = (e.RowIndex + 1).ToString();
                var centerFormat = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                var headerBounds = new Rectangle(
                    e.RowBounds.Left, e.RowBounds.Top,
                    grid.RowHeadersWidth, e.RowBounds.Height);
                e.Graphics.DrawString(rowIdx, grid.Font, Brushes.Black, headerBounds, centerFormat);
            };

            this.Controls.Add(dataGridView1);

            statsPanel = new Panel
            {
                Location = new Point(20, dataGridView1.Bottom + 10),
                Size = new Size(600, 100),
                BackColor = Color.FromArgb(240, 240, 240)
            };

            averageLabel = new Label
            {
                Size = new Size(200, 60),
                Location = new Point(0, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = ColorTranslator.FromHtml("#9FB3DF"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Text = " Average: --"
            };

            progressLabel = new Label
            {
                Size = new Size(200, 60),
                Location = new Point(220, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = ColorTranslator.FromHtml("#9FB3DF"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Text = " Progress: --"
            };

            statsPanel.Controls.Add(averageLabel);
            statsPanel.Controls.Add(progressLabel);
            this.Controls.Add(statsPanel);
        }

        private void LoadStudentHistory(string studentName)
        {
            try
            {
                string historyFile = $"{studentName}_history.xlsx";
                string historyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data", historyFile);
                string fullPath = Path.GetFullPath(historyPath);

                if (!File.Exists(fullPath))
                {
                    
                    return;
                }

                historyData = LoadExcelToDataTable(fullPath);
                dataGridView1.DataSource = historyData;

                if (dataGridView1.Columns.Contains("Score (%)"))
                {
                    dataGridView1.Columns["Score (%)"].HeaderText = "Grade";
                    dataGridView1.Columns["Score (%)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns["Score (%)"].DefaultCellStyle.Format = "0.00";
                }

                dataGridView1.CellFormatting += (s, e) =>
                {
                    if (dataGridView1.Columns[e.ColumnIndex].HeaderText.Contains("Grade"))
                    {
                        if (e.Value != null && double.TryParse(e.Value.ToString(), out double score))
                        {
                            e.CellStyle.ForeColor = score < 56 ? Color.Red : Color.Green;
                            e.CellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                        }
                    }
                };

                CalculateAndDisplayStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading history: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable LoadExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheets.Worksheet(1);
                bool firstRow = true;

                foreach (var row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        foreach (var cell in row.Cells())
                            dt.Columns.Add(cell.Value.ToString());
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
                                dataRow[colIndex++] = cell.Value.ToString();
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
            }
            return dt;
        }

        private void CalculateAndDisplayStats()
        {
            if (!historyData.Columns.Contains("Score") && !historyData.Columns.Contains("Score (%)")) return;

            try
            {
                string scoreColumn = historyData.Columns.Contains("Score") ? "Score" : "Score (%)";

                var scores = historyData.AsEnumerable()
                    .Select(row => double.TryParse(row[scoreColumn]?.ToString(), out double val) ? (double?)val : null)
                    .Where(val => val.HasValue)
                    .Select(val => val.Value)
                    .ToList();

                if (scores.Count == 0)
                {
                    averageLabel.Text = " Average: --";
                    progressLabel.Text = " Progress: --";
                    return;
                }

                double average = scores.Average();
                averageLabel.Text = $" Average: {average:F2}%";

                double progress;
                if (scores.Count > 1)
                {
                    double previousAvg = scores.Take(scores.Count - 1).Average();
                    progress = scores.Last() == 100 ? 100 : scores.Last() - previousAvg;
                }
                else
                {
                    // ✅ Only one score, and it's 100 → report 100% progress
                    progress = scores[0] == 100 ? 100 : 0;
                }

                string emojiArrow = progress > 0 ? "🟢↑" : progress < 0 ? "🔴↓" : "⚪→";
                string sign = progress >= 0 ? "+" : "";
                progressLabel.Text = $" Progress: {emojiArrow} {sign}{progress:F2}%";
            }
            catch
            {
                averageLabel.Text = " Average: Error";
                progressLabel.Text = " Progress: Error";
            }
        }
        public double CalculateAverage(double[] scores)
        {
            if (scores == null || scores.Length == 0)
                return -1;

            return scores.Average();
        }



        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}