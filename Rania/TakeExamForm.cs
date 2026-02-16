using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public class TakeExamForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public ComboBox cmbCategory, cmbDifficulty, cmbAvailableExams;
        public Button btnStartExam, btnPrevious, btnNext;
        public Label lblQuestion, lblProgress, lblHint, lblCharCount, lblInstructions;
        public TextBox txtAnswer, txtOpenEndedAnswer;
        public Panel pnlExamSelection, pnlExamTaking, pnlOptions;

        public string examsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Exams");
        public string historyFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data");

        public List<ExamInfo> availableExams = new List<ExamInfo>();
        public List<Question> currentExamQuestions = new List<Question>();
        public Dictionary<int, string> studentAnswers = new Dictionary<int, string>();
        public int currentQuestionIndex = 0;
        public string studentName;
        public string currentExamId;
        public ExamInfo currentExam; // ✅ Stores the selected exam object directly


        public TakeExamForm(string student)
        {
            studentName = student;
            SetupUI();
            LoadExamCategories();
        }

        public void SetupUI()
        {
            this.Text = "Take Exam";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10);
            this.BackColor = ColorTranslator.FromHtml("#F2EFE7");

            pnlExamSelection = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            pnlExamTaking = new Panel { Dock = DockStyle.Fill, Visible = false, BackColor = Color.White };
            this.Controls.Add(pnlExamSelection);
            this.Controls.Add(pnlExamTaking);

            lblInstructions = new Label
            {
                Text = "To start your exam, please enter the following details:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            pnlExamSelection.Controls.Add(lblInstructions);

            Label lblCategory = new Label { Text = "Category:", Location = new Point(20, 70), AutoSize = true };
            cmbCategory = new ComboBox { Location = new Point(100, 70), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCategory.SelectedIndexChanged += (s, e) => FilterExams();

            Label lblDifficulty = new Label { Text = "Difficulty:", Location = new Point(320, 70), AutoSize = true };
            cmbDifficulty = new ComboBox { Location = new Point(400, 70), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbDifficulty.SelectedIndexChanged += (s, e) => FilterExams();

            Label lblAvailable = new Label { Text = "Available Exams:", Location = new Point(20, 110), AutoSize = true };
            cmbAvailableExams = new ComboBox { Location = new Point(150, 110), Width = 450, DropDownStyle = ComboBoxStyle.DropDownList };

            btnStartExam = new Button
            {
                Text = "Start Exam",
                Location = new Point(620, 110),
                Width = 100,
                BackColor = ColorTranslator.FromHtml("#4CAF50"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnStartExam.FlatAppearance.BorderSize = 0;
            btnStartExam.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnStartExam.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnStartExam.Width, btnStartExam.Height, 15, 15));
            btnStartExam.Click += btnStartExam_Click;

            pnlExamSelection.Controls.AddRange(new Control[] { lblCategory, cmbCategory, lblDifficulty, cmbDifficulty, lblAvailable, cmbAvailableExams, btnStartExam });

            string imagePath = Path.Combine(Application.StartupPath, "Resources", "do.png");
            if (File.Exists(imagePath))
            {
                pnlExamSelection.BackgroundImage = Image.FromFile(imagePath);
                pnlExamSelection.BackgroundImageLayout = ImageLayout.Stretch;
            }

            lblQuestion = new Label
            {
                Location = new Point(20, 20),
                Size = new Size(840, 60),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#2A4759")
            };

            lblProgress = new Label { Location = new Point(20, 80), AutoSize = true, ForeColor = Color.Gray };
            pnlOptions = new Panel { Location = new Point(20, 110), Size = new Size(900, 400), AutoScroll = true };

            txtAnswer = new TextBox { Width = 600, Height = 30, Visible = false };
            txtOpenEndedAnswer = new TextBox { Width = 600, Height = 30, Visible = false };
            lblCharCount = new Label { AutoSize = true, ForeColor = Color.Gray, Visible = false };
            lblHint = new Label { AutoSize = true, ForeColor = Color.Gray, Visible = false, Font = new Font("Segoe UI", 9, FontStyle.Italic) };

            btnPrevious = new Button
            {
                Text = "Previous",
                Location = new Point(650, 530),
                BackColor = ColorTranslator.FromHtml("#9FB3DF"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnPrevious.FlatAppearance.BorderSize = 0;
            btnPrevious.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnPrevious.Size = new Size(90, 35);
            btnPrevious.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnPrevious.Width, btnPrevious.Height, 15, 15));
            btnPrevious.Click += btnPrevious_Click;

            btnNext = new Button
            {
                Text = "Next",
                Location = new Point(760, 530),
                BackColor = ColorTranslator.FromHtml("#9FB3DF"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnNext.Size = new Size(90, 35);
            btnNext.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnNext.Width, btnNext.Height, 15, 15));
            btnNext.Click += btnNext_Click;

            pnlExamTaking.Controls.AddRange(new Control[] {
                lblQuestion, lblProgress, pnlOptions,
                txtAnswer, txtOpenEndedAnswer,
                lblCharCount, lblHint,
                btnPrevious, btnNext
            });

            txtOpenEndedAnswer.TextChanged += (s, e) => {
                lblCharCount.Text = $"{txtOpenEndedAnswer.Text.Length}/500";
            };
        }


        public void LoadExamCategories()
        {
            cmbCategory.Items.Clear();
            cmbDifficulty.Items.Clear();
            availableExams.Clear();

            if (!Directory.Exists(examsFolder))
                Directory.CreateDirectory(examsFolder);

            var examFiles = Directory.GetFiles(examsFolder, "Exam_*.xlsx");
            var validDifficulties = new[] { "Low", "Medium", "Hard", "Random" };

            foreach (var file in examFiles)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                var parts = name.Split('_');

                if (parts.Length < 4)
                    continue;

                string examId = parts[^1]; // last part
                string difficulty = parts[^2]; // second last
                string category = string.Join("_", parts.Skip(1).Take(parts.Length - 3)); // everything between "Exam" and difficulty

                if (!validDifficulties.Contains(difficulty, StringComparer.OrdinalIgnoreCase))
                    continue;

                string lecturerId = "Unknown";
                try
                {
                    using (var wb = new XLWorkbook(file))
                    {
                        var ws = wb.Worksheet(1);
                        lecturerId = ws.Cell(2, 9).GetString().Trim();
                    }
                }
                catch
                {
                    // Ignore read errors
                }

                availableExams.Add(new ExamInfo
                {
                    FilePath = file,
                    Category = category.Replace('_', ' '), // ✅ Clean category for display
                    Difficulty = difficulty,
                    ExamId = examId,
                    LecturerId = lecturerId
                });
            }

            // Populate category dropdown (dynamic)
            var categories = availableExams.Select(e => e.Category).Distinct();
            cmbCategory.Items.Add("All");
            cmbCategory.Items.AddRange(categories.ToArray());

            // Populate difficulty dropdown (fixed)
            cmbDifficulty.Items.Add("All");
            cmbDifficulty.Items.AddRange(validDifficulties);

            cmbCategory.SelectedIndex = 0;
            cmbDifficulty.SelectedIndex = 0;

            FilterExams();
        }




        public void FilterExams()
        {
            cmbAvailableExams.Items.Clear();
            string selectedCat = cmbCategory.SelectedItem?.ToString();
            string selectedDiff = cmbDifficulty.SelectedItem?.ToString();

            var filtered = availableExams.AsEnumerable();
            if (selectedCat != "All") filtered = filtered.Where(e => e.Category == selectedCat);
            if (selectedDiff != "All") filtered = filtered.Where(e => e.Difficulty == selectedDiff);

            foreach (var exam in filtered)
            {
                cmbAvailableExams.Items.Add($"{exam.Category} - {exam.Difficulty} (ID: {exam.ExamId})");
            }

            if (cmbAvailableExams.Items.Count > 0)
                cmbAvailableExams.SelectedIndex = 0;
        }

        public void btnStartExam_Click(object sender, EventArgs e)
        {

            if (cmbAvailableExams.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an exam.");
                return;
            }

            string selectedText = cmbAvailableExams.SelectedItem.ToString();
            var selected = availableExams.FirstOrDefault(exam =>
              $"{exam.Category} - {exam.Difficulty} (ID: {exam.ExamId})" == selectedText);

            if (selected == null)
            {
                MessageBox.Show("Exam not found.");
                return;
            }

            currentExamId = selected.ExamId;
            currentExam = selected; // ✅ store it

            LoadExamQuestions(selected.FilePath);
            ShowQuestion(0);

            pnlExamSelection.Visible = false;
            pnlExamTaking.Visible = true;
        }

        public void LoadExamQuestions(string path)
        {
            currentExamQuestions.Clear();
            studentAnswers.Clear();
            currentQuestionIndex = 0;

            using var wb = new XLWorkbook(path);
            var ws = wb.Worksheet(1);

            foreach (var row in ws.RangeUsed().RowsUsed().Skip(1))
            {
                currentExamQuestions.Add(new Question
                {
                    Text = row.Cell(2).GetString(),
                    Type = row.Cell(3).GetString(),
                    Options = row.Cell(4).GetString()?.Split('\n') ?? Array.Empty<string>(),
                    Correct = row.Cell(5).GetString(),
                    Hint = row.Cell(8).GetString()
                });
            }
        }

        public void ShowQuestion(int index)
        {
            if (index < 0 || index >= currentExamQuestions.Count) return;

            var q = currentExamQuestions[index];

            lblQuestion.Text = $"QUESTION {index + 1}";
            lblQuestion.ForeColor = ColorTranslator.FromHtml("#2A4759");
            lblQuestion.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            pnlOptions.Controls.Clear();
            txtAnswer.Visible = false;
            txtOpenEndedAnswer.Visible = false;
            lblHint.Visible = false;
            lblCharCount.Visible = false;

            string displayText = q.Text.Contains("{") ? q.Text.Replace("{", "_").Replace("}", "") : q.Text;
            lblProgress.Text = $"{index + 1}/{currentExamQuestions.Count}";

            bool isOpenEnded = q.Text.Contains("{") && q.Text.Contains("}");
            bool isComplete = q.Type.ToLower().Contains("complete");
            bool isTrueFalse = q.Type.ToLower().Contains("true");
            bool isMCQ = q.Type.ToLower().Contains("mcq");

            Label lblText = new Label
            {
                Text = displayText,
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#123458"),
                Location = new Point(10, 10),
                AutoSize = true,
                MaximumSize = new Size(850, 0)
            };
            pnlOptions.Controls.Add(lblText);

            int startY = lblText.Bottom + 15;
            int controlBottom = startY;

            if (isOpenEnded || isComplete)
            {
                Label lblInstruction = new Label
                {
                    Text = " Complete the sentence:",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(10, startY),
                    AutoSize = true
                };
                pnlOptions.Controls.Add(lblInstruction);

                Label lblYourAnswer = new Label
                {
                    Text = " Your Answer:",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(10, lblInstruction.Bottom + 15),
                    AutoSize = true
                };
                pnlOptions.Controls.Add(lblYourAnswer);

                var box = isOpenEnded ? txtOpenEndedAnswer : txtAnswer;
                box.Visible = true;
                box.Text = studentAnswers.TryGetValue(index, out var answer) ? answer : "";
                box.Location = new Point(10, lblYourAnswer.Bottom + 6);
                box.Width = 600;
                box.Height = 32;
                box.BorderStyle = BorderStyle.FixedSingle;
                pnlOptions.Controls.Add(box);

                lblCharCount.Visible = true;
                lblCharCount.Text = $"{box.Text.Length}/500";
                lblCharCount.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                lblCharCount.ForeColor = Color.Gray;
                lblCharCount.Location = new Point(box.Right + 10, box.Top + 8);
                pnlOptions.Controls.Add(lblCharCount);

                controlBottom = box.Bottom + 10;
            }
            else if (isTrueFalse)
            {
                string[] options = { "True", "False" };
                for (int i = 0; i < options.Length; i++)
                {
                    var rb = new RadioButton
                    {
                        Text = options[i],
                        Tag = options[i],
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11, FontStyle.Bold),
                        ForeColor = Color.Black,
                        Location = new Point(10, startY + i * 40),
                        Checked = studentAnswers.TryGetValue(index, out var selected) && selected == options[i]
                    };
                    pnlOptions.Controls.Add(rb);
                }

                controlBottom = startY + options.Length * 40;
            }
            else if (isMCQ)
            {
                Color[] optionColors = {
            ColorTranslator.FromHtml("#BBD8A3"),
            ColorTranslator.FromHtml("#BE5B50"),
            ColorTranslator.FromHtml("#B2C6D5"),
            ColorTranslator.FromHtml("#FAD59A")
        };

                for (int i = 0; i < q.Options.Length; i++)
                {
                    string optionText = q.Options[i].Trim();

                    var rb = new RadioButton
                    {
                        Text = optionText,
                        Tag = optionText,
                        AutoSize = false,
                        Width = 400,
                        Height = 35,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = Color.Black,
                        BackColor = optionColors[i % optionColors.Length],
                        Checked = studentAnswers.TryGetValue(index, out var selected) && selected == optionText,
                        Location = new Point(10 + (i % 2) * 430, startY + (i / 2) * 45)
                    };
                    pnlOptions.Controls.Add(rb);
                }

                controlBottom = startY + (q.Options.Length + 1) / 2 * 45;
            }

            // 🔥 Move this OUTSIDE - show hint for all question types
            if (!string.IsNullOrWhiteSpace(q.Hint))
            {
                Label lblHintDynamic = new Label
                {
                    Text = $"💡 Hint: {q.Hint}",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Location = new Point(10, controlBottom + 15),
                    AutoSize = true
                };
                pnlOptions.Controls.Add(lblHintDynamic);
            }

            btnPrevious.Enabled = index > 0;
            btnNext.Text = index < currentExamQuestions.Count - 1 ? "Next" : "Finish";
            btnNext.BackColor = btnNext.Text == "Finish" ? Color.Red : ColorTranslator.FromHtml("#9FB3DF");
        }









        public void btnPrevious_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
            ShowQuestion(--currentQuestionIndex);
        }

        public void btnNext_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
            if (currentQuestionIndex < currentExamQuestions.Count - 1)
                ShowQuestion(++currentQuestionIndex);
            else
                FinishExam();
        }

        public void SaveCurrentAnswer()
        {
            var q = currentExamQuestions[currentQuestionIndex];
            bool isOpenEnded = q.Text.Contains("{") && q.Text.Contains("}");

            if (isOpenEnded)
                studentAnswers[currentQuestionIndex] = txtOpenEndedAnswer.Text.Trim();
            else if (q.Type.ToLower().Contains("complete"))
                studentAnswers[currentQuestionIndex] = txtAnswer.Text.Trim();
            else
            {
                foreach (RadioButton rb in pnlOptions.Controls.OfType<RadioButton>())
                {
                    if (rb.Checked)
                    {
                        studentAnswers[currentQuestionIndex] = rb.Tag.ToString();
                        break;
                    }
                }
            }
        }

        public void FinishExam()
        {
            SaveCurrentAnswer();

            int correctAnswers = 0;
            for (int i = 0; i < currentExamQuestions.Count; i++)
            {
                string correct = currentExamQuestions[i].Correct?.Trim();
                string answer = studentAnswers.ContainsKey(i) ? studentAnswers[i]?.Trim() : "";

                if (string.Equals(correct, answer, StringComparison.OrdinalIgnoreCase))
                    correctAnswers++;
            }

            double percentageScore = (double)correctAnswers / currentExamQuestions.Count * 100;
            string letterGrade = GetLetterGrade(percentageScore);
            string scoreDisplay = $"{correctAnswers}/{currentExamQuestions.Count} ({percentageScore:F1}%, Grade: {letterGrade})";

            string message = $"Exam finished!\nScore: {scoreDisplay}";
            using (var doneForm = new Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania.done(message))
            {
                doneForm.ShowDialog();
            }
            SaveExamHistory(percentageScore);

            pnlExamSelection.Visible = true;
            pnlExamTaking.Visible = false;
        }

        public string GetLetterGrade(double percentage)
        {
            if (percentage >= 90) return "A";
            if (percentage >= 80) return "B";
            if (percentage >= 70) return "C";
            if (percentage >= 60) return "D";
            return "F";
        }

        public void SaveExamHistory(double percentageScore)
        {
            if (!Directory.Exists(historyFolder))
                Directory.CreateDirectory(historyFolder);

            string filePath = Path.Combine(historyFolder, $"{studentName}_history.xlsx");
            bool fileExists = File.Exists(filePath);

            using var wb = fileExists ? new XLWorkbook(filePath) : new XLWorkbook();
            var ws = fileExists ? wb.Worksheet(1) : wb.AddWorksheet("History");

            int row = ws.LastRowUsed()?.RowNumber() + 1 ?? 2;

            if (!fileExists)
            {
                ws.Cell(1, 1).Value = "Date";
                ws.Cell(1, 2).Value = "Exam ID";
                ws.Cell(1, 3).Value = "Category";
                ws.Cell(1, 4).Value = "Difficulty";
                ws.Cell(1, 5).Value = "Score (%)";
                ws.Cell(1, 6).Value = "Lecturer ID";
            }

            var exam = currentExam; // ✅ use direct reference

            if (exam == null)
            {
                MessageBox.Show("Error: Could not locate current exam to save history.", "Save Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ws.Cell(row, 1).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ws.Cell(row, 2).Value = exam.ExamId;
            ws.Cell(row, 3).Value = exam.Category;
            ws.Cell(row, 4).Value = exam.Difficulty;
            ws.Cell(row, 5).Value = percentageScore;
            ws.Cell(row, 6).Value = exam.LecturerId;

            wb.SaveAs(filePath);
        }





        public class ExamInfo
        {
            public string ExamId { get; set; }
            public string Category { get; set; }
            public string Difficulty { get; set; }
            public string FilePath { get; set; }

            public string LecturerId { get; set; } // ✅ Add this line
        }


        public class Question
        {
            public string Text { get; set; }
            public string Type { get; set; }
            public string[] Options { get; set; }
            public string Correct { get; set; }
            public string Hint { get; set; }
        }
    }
}
