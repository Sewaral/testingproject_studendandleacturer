using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class ViewExamForm : Form
    {
        private FlowLayoutPanel panel;
        private string examPath;

        public ViewExamForm(string path)
        {
            examPath = path;
            InitializeComponent();
            LoadExamFromExcel(path);
        }

        private void InitializeComponent()
        {
            this.panel = new FlowLayoutPanel();
            this.SuspendLayout();

            // Panel settings
            this.panel.Dock = DockStyle.Fill;
            this.panel.AutoScroll = true;
            this.panel.Padding = new Padding(20);
            this.panel.BackColor = Color.White;
            this.panel.FlowDirection = FlowDirection.TopDown;
            this.panel.WrapContents = false;

            // Form settings
            this.ClientSize = new Size(850, 650);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Exam Viewer";
            this.ResumeLayout(false);
        }

        private void LoadExamFromExcel(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Exam file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string examId = Path.GetFileNameWithoutExtension(filePath).Split('_').Last();

            // Exam ID label at the top
            Label lblExamId = new Label
            {
                Text = $"Exam ID: {examId}",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Margin = new Padding(5, 5, 0, 15)
            };
            panel.Controls.Add(lblExamId);

            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1).ToList();

            int qNumber = 1;
            foreach (var row in rows)
            {
                string question = row.Cell(2).GetString();
                string type = row.Cell(3).GetString().ToLower();
                string[] options = row.Cell(4).GetString().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                string correct = row.Cell(5).GetString();
                string category = row.Cell(6).GetString();
                string difficulty = row.Cell(7).GetString();
                string hint = row.Cell(8).GetString();

                // Question container
                var container = new Panel
                {
                    Width = 780,
                    AutoSize = true,
                    BackColor = Color.White,
                    Margin = new Padding(0, 0, 0, 20)
                };

                // Question label
                var lblQ = new Label
                {
                    Text = $"Question {qNumber++}",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = ColorTranslator.FromHtml("#BE3D2A"),
                    AutoSize = true
                };
                container.Controls.Add(lblQ);

                // Question text
                var lblText = new Label
                {
                    Text = question,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.Black,
                    MaximumSize = new Size(760, 0),
                    AutoSize = true,
                    Location = new Point(0, lblQ.Bottom + 5)
                };
                container.Controls.Add(lblText);

                int startY = lblText.Bottom + 10;

                // Multiple Choice
                if (type.Contains("mcq"))
                {
                    Color[] mcqColors =
                    {
                        ColorTranslator.FromHtml("#BBD8A3"),
                        ColorTranslator.FromHtml("#BE5B50"),
                        ColorTranslator.FromHtml("#B2C6D5"),
                        ColorTranslator.FromHtml("#FAD59A")
                    };

                    for (int i = 0; i < options.Length; i++)
                    {
                        var lblOption = new Label
                        {
                            Text = options[i], // ✅ NO auto-prefix like A), B), etc.
                            AutoSize = false,
                            Width = 350,
                            Height = 30,
                            BackColor = mcqColors[i % mcqColors.Length],
                            ForeColor = Color.Black,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Location = new Point((i % 2) * 390, startY + (i / 2) * 40),
                            TextAlign = ContentAlignment.MiddleLeft,
                            Padding = new Padding(10, 3, 0, 0)
                        };
                        container.Controls.Add(lblOption);
                    }


                    startY += ((options.Length + 1) / 2) * 40 + 5;
                }
                else if (type.Contains("true"))
                {
                    string[] tf = { "True", "False" };
                    Color[] tfColors = { Color.Orange, Color.SkyBlue };

                    for (int i = 0; i < 2; i++)
                    {
                        var lbl = new Label
                        {
                            Text = tf[i],
                            AutoSize = false,
                            Width = 100,
                            Height = 30,
                            BackColor = tfColors[i],
                            Font = new Font("Segoe UI", 10),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Location = new Point(i * 110, startY)
                        };
                        container.Controls.Add(lbl);
                    }

                    startY += 40;
                }

                // Details
                AddLabelRow(container, "Type:", Culture(type), startY); startY += 25;
                AddLabelRow(container, "Correct Answer:", correct, startY); startY += 25;
                AddLabelRow(container, "Category:", category, startY); startY += 25;
                AddLabelRow(container, "Difficulty:", difficulty, startY); startY += 25;
                AddLabelRow(container, "Hint:", hint, startY); startY += 25;

                // Divider line
                var divider = new Panel
                {
                    Height = 2,
                    Width = 760,
                    BackColor = Color.MidnightBlue,
                    Location = new Point(0, startY + 5)
                };
                container.Controls.Add(divider);

                panel.Controls.Add(container);
            }
        }

        private void AddLabelRow(Control container, string title, string value, int y)
        {
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(0, y)
            };
            container.Controls.Add(lblTitle);

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(150, y)
            };
            container.Controls.Add(lblValue);
        }

        private string Culture(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return "";
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }
}
