using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class CreateExamForm : Form
    {
        public string questionsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data", "Questions.xlsx");
        public string examsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Exams");
        public string lecturerId;

        public CreateExamForm(string lecturerId)
        {
            this.lecturerId = lecturerId;
            InitializeComponent();
            LoadCategories();
            LoadExams();
            btnCreateExam.Click += BtnCreateExam_Click;
        }

        public void LoadCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("Random");

            if (!File.Exists(questionsPath)) return;

            using (var workbook = new XLWorkbook(questionsPath))
            {
                var ws = workbook.Worksheet(1);
                var categories = ws.RangeUsed().RowsUsed().Skip(1)
                    .Where(r => r.Cell(9).GetValue<string>() == lecturerId) // Filter by lecturer
                    .Select(r => r.Cell(6).GetValue<string>().Trim())
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Distinct()
                    .ToList();

                foreach (var cat in categories)
                    cmbCategory.Items.Add(cat);
            }
        }

        public void LoadExams()
        {
            flowExams.Controls.Clear();

            if (!Directory.Exists(examsFolder))
                return;

            var examFiles = Directory.GetFiles(examsFolder, "Exam_*.xlsx")
                .Select(f => Path.GetFileName(f))
                .OrderBy(f => f)
                .ToList();

            bool hasAny = false;

            foreach (var file in examFiles)
            {
                string fullPath = Path.Combine(examsFolder, file);
                try
                {
                    using var wb = new XLWorkbook(fullPath);
                    var ws = wb.Worksheet(1);
                    string fileLecturerId = ws.Cell(2, 9).GetString().Trim();

                    // ✅ Only show exams created by current lecturer
                    if (fileLecturerId != lecturerId)
                        continue;
                }
                catch
                {
                    continue; // skip unreadable or invalid files
                }

                hasAny = true;
                string examId = Path.GetFileNameWithoutExtension(file).Replace("Exam_", "");

                Panel panel = new Panel
                {
                    Width = 500,
                    Height = 50,
                    BackColor = Color.White,
                    Margin = new Padding(3)
                };

                Label lbl = new Label
                {
                    Text = examId,
                    AutoSize = false,
                    Width = 180,
                    Location = new Point(10, 15),
                    Font = new Font("Segoe UI", 10F)
                };
                panel.Controls.Add(lbl);


                Button btnView = new Button
                {
                    Text = "View",
                    BackColor = Color.FromArgb(76, 175, 80), // Green
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Width = 80,
                    Height = 35, // Match Delete button
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), // Ensure it's visible
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(230, 10)
                };
                btnView.FlatAppearance.BorderSize = 0;
                btnView.Click += (s, e) =>
                {
                    if (File.Exists(fullPath))
                        new ViewExamForm(fullPath).ShowDialog();
                };
                panel.Controls.Add(btnView);


                Button btnDelete = new Button
                {
                    Text = "Delete",
                    BackColor = Color.FromArgb(244, 67, 54),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Width = 80,
                    Height = 35, // match View
                    Location = new Point(320, 10),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                btnDelete.FlatAppearance.BorderSize = 0;
                btnDelete.Click += (s, e) =>
                {
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                        LoadExams(); // Refresh
                    }
                };
                panel.Controls.Add(btnDelete);


                flowExams.Controls.Add(panel);
            }

            if (!hasAny)
            {
                Label noExamsLabel = new Label
                {
                    Text = "No exams found.",
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.DarkGray,
                    AutoSize = true,
                    Padding = new Padding(10)
                };
                flowExams.Controls.Add(noExamsLabel);
            }
        }



        public void BtnCreateExam_Click(object sender, EventArgs e)
        {
            if (!File.Exists(questionsPath))
            {
                using (CustomMessageBox cmb = new CustomMessageBox("Questions file not found."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            if (cmbCategory.SelectedIndex == -1 || cmbDifficulty.SelectedIndex == -1)
            {
                using (CustomMessageBox cmb = new CustomMessageBox("Please select both Category and Difficulty before creating the exam."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            int mcqCount = (int)numMCQ.Value;
            int trueFalseCount = (int)numTrueFalse.Value;
            int completeCount = (int)numComplete.Value;

            if (mcqCount + trueFalseCount + completeCount == 0)
            {
                using (CustomMessageBox cmb = new CustomMessageBox("Please select at least one question."))
                {
                    cmb.ShowDialog();
                }

                return;
            }

            string selectedCategory = cmbCategory.SelectedItem?.ToString();
            string selectedDifficulty = cmbDifficulty.SelectedItem?.ToString();

            var selectedQuestions = new List<(string Question, string Type, string Options, string Correct, string Category, string Difficulty, string Hint)>();

            using (var workbook = new XLWorkbook(questionsPath))
            {
                var ws = workbook.Worksheet(1);
                var rows = ws.RangeUsed().RowsUsed().Skip(1).ToList();

                for (int i = 0; i < rows.Count; i++)
                {
                    var row = rows[i];
                    string ownerId = row.Cell(9).GetString().Trim();
                    if (ownerId != lecturerId)
                        continue;

                    string type = row.Cell(3).GetString().Trim().ToLower();
                    string question = row.Cell(2).GetString();
                    string correct = row.Cell(5).GetString();
                    string category = row.Cell(6).GetString();
                    string hint = row.Cell(7).GetString();
                    string difficulty = row.Cell(8).GetString();

                    if ((selectedCategory != "Random" && !category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)) ||
                        (selectedDifficulty != "Random" && !difficulty.Equals(selectedDifficulty, StringComparison.OrdinalIgnoreCase)))
                        continue;

                    if (type == "mcq")
                    {
                        var options = new List<string>();
                        int startIndex = i;
                        while (i < rows.Count && (string.IsNullOrWhiteSpace(rows[i].Cell(1).GetString()) || i == startIndex))
                        {
                            var opt = rows[i].Cell(4).GetString();
                            if (!string.IsNullOrWhiteSpace(opt))
                                options.Add(opt.Trim());
                            i++;
                        }
                        i--; // rewind one because the loop ends after increment
                        selectedQuestions.Add((question, "MCQ", string.Join("\n", options), correct, category, difficulty, hint));
                    }
                    else if (type.Contains("true"))
                    {
                        selectedQuestions.Add((question, "True / False", "True\nFalse", correct, category, difficulty, hint));
                    }
                    else if (type.Contains("complete"))
                    {
                        selectedQuestions.Add((question, "Complete", "", correct, category, difficulty, hint));
                    }
                }
            }

            int availableMCQ = selectedQuestions.Count(q => q.Type == "MCQ");
            int availableTF = selectedQuestions.Count(q => q.Type == "True / False");
            int availableComplete = selectedQuestions.Count(q => q.Type == "Complete");

            if (mcqCount > availableMCQ || trueFalseCount > availableTF || completeCount > availableComplete)
            {
                string msg = "Not enough questions: ";
                if (mcqCount > availableMCQ)
                    msg += $"MCQ {mcqCount} requested, {availableMCQ} available; ";
                if (trueFalseCount > availableTF)
                    msg += $"True/False {trueFalseCount} requested, {availableTF} available; ";
                if (completeCount > availableComplete)
                    msg += $"Complete {completeCount} requested, {availableComplete} available; ";

                using (CustomMessageBox cmb = new CustomMessageBox(msg.TrimEnd(' ', ';')))
                {
                    cmb.ShowDialog();
                }
                return;
            }



            var final = selectedQuestions
                .Where(q => q.Type == "MCQ").OrderBy(x => Guid.NewGuid()).Take(mcqCount)
                .Concat(selectedQuestions.Where(q => q.Type == "True / False").OrderBy(x => Guid.NewGuid()).Take(trueFalseCount))
                .Concat(selectedQuestions.Where(q => q.Type == "Complete").OrderBy(x => Guid.NewGuid()).Take(completeCount))
                .ToList();

            if (final.Count == 0)
            {
                using (CustomMessageBox cmb = new CustomMessageBox("No questions were selected to be saved."))
                {
                    cmb.ShowDialog();
                }

                return;
            }

            string safeCategory = string.Join("_", selectedCategory.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");
            string safeDifficulty = string.Join("_", selectedDifficulty.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");

            var existingIds = Directory.GetFiles(examsFolder, $"Exam_{safeCategory}_{safeDifficulty}_*.xlsx")
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .Select(name =>
                {
                    var parts = name.Split('_');
                    if (parts.Length >= 4 && int.TryParse(parts[^1], out int id))
                        return id;
                    return 0;
                }).ToList();

            int nextExamId = (existingIds.Any() ? existingIds.Max() : 0) + 1;

            SaveExam(final, nextExamId, selectedCategory, selectedDifficulty);
            LoadExams();
            using (SuccessMessageBox successBox = new SuccessMessageBox("Exam created successfully!"))
            {
                successBox.ShowDialog();
            }
        }


        private void SaveExam(
    List<(string Question, string Type, string Options, string Correct, string Category, string Difficulty, string Hint)> questions,
    int examId,
    string category,
    string difficulty)
        {
            string safeCategory = string.Join("_", category.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");
            string safeDifficulty = string.Join("_", difficulty.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");

            string fileName = $"Exam_{safeCategory}_{safeDifficulty}_{examId:D3}.xlsx";
            string path = Path.Combine(examsFolder, fileName);

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Exam");
                ws.Cell(1, 1).Value = "Number";
                ws.Cell(1, 2).Value = "Question";
                ws.Cell(1, 3).Value = "Type";
                ws.Cell(1, 4).Value = "Options";
                ws.Cell(1, 5).Value = "Correct Answer";
                ws.Cell(1, 6).Value = "Category";
                ws.Cell(1, 7).Value = "Difficulty";
                ws.Cell(1, 8).Value = "Hint";
                ws.Cell(1, 9).Value = "LecturerId"; // ✅ NEW column

                int row = 2;
                int number = 1;

                foreach (var q in questions)
                {
                    ws.Cell(row, 1).Value = number++;
                    ws.Cell(row, 2).Value = q.Question;
                    ws.Cell(row, 3).Value = q.Type;
                    ws.Cell(row, 4).Value = q.Options;
                    ws.Cell(row, 5).Value = q.Correct;
                    ws.Cell(row, 6).Value = q.Category;
                    ws.Cell(row, 7).Value = q.Difficulty;
                    ws.Cell(row, 8).Value = q.Hint;
                    ws.Cell(row, 9).Value = lecturerId; // ✅ Save current user ID
                    row++;
                }

                workbook.SaveAs(path);
            }
        }
        public List<(string Question, string Type, string Options, string Correct, string Category, string Difficulty, string Hint)>
    FilterQuestionsByCategoryAndDifficulty(
        List<(string Question, string Type, string Options, string Correct, string Category, string Difficulty, string Hint)> allQuestions,
        string selectedCategory,
        string selectedDifficulty)
        {
            return allQuestions
                .Where(q =>
                    (selectedCategory == "Random" || q.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)) &&
                    (selectedDifficulty == "Random" || q.Difficulty.Equals(selectedDifficulty, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }



        private void ShowAvailableQuestionStats()
        {
            string selectedCategory = cmbCategory.SelectedItem?.ToString();
            string selectedDifficulty = cmbDifficulty.SelectedItem?.ToString();
            var stats = new Dictionary<string, int>
            {
                ["MCQ"] = 0,
                ["True / False"] = 0,
                ["Complete"] = 0
            };

            if (!File.Exists(questionsPath)) return;

            using (var workbook = new XLWorkbook(questionsPath))
            {
                var ws = workbook.Worksheet(1);
                var rows = ws.RangeUsed().RowsUsed().Skip(1).ToList();

                int i = 0;
                while (i < rows.Count)
                {
                    var row = rows[i];

                    string ownerId = row.Cell(9).GetString().Trim();
                    if (ownerId != lecturerId)
                    {
                        i++;
                        continue;
                    }

                    string type = row.Cell(3).GetString().Trim().ToLower();
                    string category = row.Cell(6).GetString();
                    string difficulty = row.Cell(7).GetString();

                    if ((selectedCategory != "Random" && !category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)) ||
                        (selectedDifficulty != "Random" && !difficulty.Equals(selectedDifficulty, StringComparison.OrdinalIgnoreCase)))
                    {
                        i++;
                        continue;
                    }

                    if (type == "mcq")
                    {
                        stats["MCQ"]++;
                        i += 4;
                    }
                    else if (type.Contains("true"))
                    {
                        stats["True / False"]++;
                        i++;
                    }
                    else if (type.Contains("complete"))
                    {
                        stats["Complete"]++;
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            string msg = $"Available questions:\n" +
                         $"MCQ: {stats["MCQ"]}\n" +
                         $"True / False: {stats["True / False"]}\n" +
                         $"Complete: {stats["Complete"]}";
            MessageBox.Show(msg, "Question Stats", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
