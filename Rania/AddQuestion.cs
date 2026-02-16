namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    using Excel = Microsoft.Office.Interop.Excel;
    using System.IO;
    using System.Windows.Forms;
    using System.Drawing;
    using ClosedXML.Excel;
    using DocumentFormat.OpenXml.Office.SpreadSheetML.Y2023.MsForms;
    using System.Drawing.Drawing2D;



    public partial class AddQuestion : Form
    {
        private TableLayoutPanel table;
        private Label lblTotalQ;
        private string lecturerId;


        private string path = Path.Combine(
     Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")),
     "Data",
     "Questions.xlsx"
 );


        public AddQuestion(string lecturerName)
        {
            InitializeComponent();
            this.Bounds = Screen.PrimaryScreen.Bounds; // 🔵 Make full screen

            this.lecturerId = lecturerName; // ✅ Used in LoadQuestionsFromExcel
            CreateExcelFile();
            LoadQuestionsFromExcel();
        }
        public string GetLecturerId()
        {
            return lecturerId;
        }


        private Button CreateSideButton(string text, Point loc)
        {
            return new Button()
            {
                Text = text,
                Location = loc,
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(45, 160, 220),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
        }

        public void RefreshQuestions()
        {
            lblTotalQ.Text = GetTotalQuestionCountFromExcel().ToString();
            dataGridView.Rows.Clear(); // Clear existing rows from the grid
            LoadQuestionsFromExcel();  // Reload from Excel
        }


        public void btnTF_Click(object sender, EventArgs e)
        {
            TrueFalseForm tfForm = new TrueFalseForm(this);  // Pass THIS
            tfForm.Show();
        }


        public void btnOptions_Click(object sender, EventArgs e)
        {
            Options optionsForm = new Options(this);  // Pass this form as parent
            optionsForm.Show();
            
        }


        public void btnComplete_Click(object sender, EventArgs e)
        {
            complete completeForm = new complete(this);  // Pass this form to constructor
            completeForm.Show();
            
        }
        public void RefreshTotalQuestions()
        {
            dataGridView.Rows.Clear();  // ✅ Clear existing rows
            LoadQuestionsFromExcel();   // ✅ Reload updated data
        }




        public void LoadQuestionsFromExcel()
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Excel file not found.");
                return;
            }

            try
            {
                dataGridView.Rows.Clear();
                int questionNumber = 1;

                using (var workbook = new XLWorkbook(path))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed().Skip(1).ToList();

                    for (int i = 0; i < rows.Count; i++)
                    {
                        var row = rows[i];
                        var type = row.Cell(3).GetValue<string>()?.ToLower();
                        var colA = row.Cell(1).GetValue<string>();
                        var owner = row.Cell(9).GetValue<string>();

                        if (owner != lecturerId) continue;

                        if (string.IsNullOrWhiteSpace(colA) && (type != "mcq" && type != "true / false" && type != "complete sentence"))
                            continue;

                        var question = row.Cell(2).GetValue<string>();
                        var options = row.Cell(4).GetValue<string>();
                        var correctAnswer = row.Cell(5).GetValue<string>();
                        var category = row.Cell(6).GetValue<string>();
                        var hint = row.Cell(7).GetValue<string>();
                        var difficulty = row.Cell(8).GetValue<string>();

                        // Merge MCQ options
                        if (!string.IsNullOrEmpty(type) && type.Contains("mcq"))
                        {
                            var optionLines = new List<string>();
                            var optionRow = row;
                            int optionCount = 0;

                            while (optionRow != null && optionCount < 4)
                            {
                                var optColA = optionRow.Cell(1).GetValue<string>();
                                var optColD = optionRow.Cell(4).GetValue<string>();

                                // ✅ Fix: only break if it's a new real question row
                                if (optionRow != row && !string.IsNullOrWhiteSpace(optColA) && !string.IsNullOrWhiteSpace(optColD))
                                    break;

                                if (!string.IsNullOrWhiteSpace(optColD))
                                {
                                    optionLines.Add(optColD.Trim());
                                    optionCount++;
                                }

                                i++;
                                if (i >= rows.Count) break;
                                optionRow = rows[i];
                            }

                            i--; // rollback one index for outer loop
                            options = string.Join("\n", optionLines);
                        }

                        dataGridView.Rows.Add(
                            "#" + questionNumber,
                            question,
                            type,
                            options,
                            correctAnswer,
                            difficulty,
                            category,
                            hint,
                            "" // Action column
                        );

                        questionNumber++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading Excel:\n" + ex.Message);
            }
        }



        public int GetTotalQuestionCountFromExcel()
        {
            if (!File.Exists(path))
                return 0;

            int count = 0;

            try
            {
                using (var workbook = new XLWorkbook(path))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header

                    foreach (var row in rows)
                    {
                        var number = row.Cell(1).GetValue<string>();
                        var type = row.Cell(3).GetValue<string>().ToLower();
                        var owner = row.Cell(9).GetValue<string>(); // Column 9 = LecturerID

                        if (!string.IsNullOrWhiteSpace(number) &&
                            (type == "mcq" || type == "true / false" || type == "complete sentence") &&
                            owner == lecturerId)
                        {
                            count++;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed to count questions from Excel.");
            }

            return count;
        }













        public void AddQuestionToList(string number, string question, string type, string options, string correctAnswer, string difficulty, string category, string hint)
        {
            string showOptions = "-";

            if (!string.IsNullOrEmpty(type) && type.ToLower().Contains("mcq") && !string.IsNullOrWhiteSpace(options))
            {
                showOptions = "";
                string[] lines = options.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string opt in lines)
                {
                    showOptions += opt.Trim() + "\n";
                }
                showOptions = showOptions.TrimEnd();
            }

            dataGridView.Rows.Add(
                "#" + number,
                question,
                type,
                showOptions,
                correctAnswer,
                difficulty,
                category,
                string.IsNullOrWhiteSpace(hint) ? "-" : hint,
                ""
            );
        }

        // Put this inside your AddQuestion.cs logic file

        // Add this inside your class:
        // ShowEditPanel with all your requests applied
        public void ShowEditPanel(string number, string question, string type, string options, string correctAnswer, string difficulty, string category, string hint)
        {
            panelEdit.Controls.Clear();
            panelEdit.Visible = true;
            panelEdit.AutoScroll = true;

            Label title = new Label()
            {
                Text = $"Edit Question #{number}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            panelEdit.Controls.Add(title);

            Label lblQ = new Label() { Text = "Question:", Location = new Point(20, 70), AutoSize = true };
            TextBox txtQ = new TextBox() { Text = question, Location = new Point(20, 90), Width = 400, Multiline = true, Height = 60 };
            panelEdit.Controls.Add(lblQ);
            panelEdit.Controls.Add(txtQ);

            int nextY = 170;
            List<TextBox> optionBoxes = new List<TextBox>();
            ComboBox cmbCorrect = null;
            TextBox txtCorrectComplete = null;

            if (type.ToLower().Contains("mcq"))
            {
                string[] opts = options.Split('\n');
                char optChar = 'A';

                foreach (var opt in opts)
                {
                    Label lblOpt = new Label() { Text = $"{optChar})", Location = new Point(20, nextY), AutoSize = true };
                    TextBox txtOpt = new TextBox() { Text = opt.Length > 2 ? opt.Substring(2) : "", Location = new Point(50, nextY), Width = 370 };
                    panelEdit.Controls.Add(lblOpt);
                    panelEdit.Controls.Add(txtOpt);
                    optionBoxes.Add(txtOpt);
                    nextY += 40;
                    optChar++;
                }

                Label lblCorrect = new Label() { Text = "Correct Answer:", Location = new Point(20, nextY), AutoSize = true };
                cmbCorrect = new ComboBox() { Location = new Point(20, nextY + 20), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
                cmbCorrect.Items.AddRange(new[] { "A", "B", "C", "D" });
                cmbCorrect.SelectedItem = correctAnswer;
                panelEdit.Controls.Add(lblCorrect);
                panelEdit.Controls.Add(cmbCorrect);
                nextY += 70;
            }
            else if (type.ToLower().Contains("true"))
            {
                Label lblCorrect = new Label() { Text = "Correct Answer:", Location = new Point(20, nextY), AutoSize = true };
                cmbCorrect = new ComboBox() { Location = new Point(20, nextY + 20), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
                cmbCorrect.Items.AddRange(new[] { "True", "False" });
                cmbCorrect.SelectedItem = correctAnswer;
                panelEdit.Controls.Add(lblCorrect);
                panelEdit.Controls.Add(cmbCorrect);
                nextY += 70;
            }
            else if (type.ToLower().Contains("complete"))
            {
                Label lblNote = new Label()
                {
                    Text = "Use { } for missing words.",
                    Location = new Point(20, nextY),
                    AutoSize = true,
                    ForeColor = Color.Red
                };
                panelEdit.Controls.Add(lblNote);
                nextY += 30;

                Label lblCorrect = new Label() { Text = "Correct Answers (comma-separated):", Location = new Point(20, nextY), AutoSize = true };
                txtCorrectComplete = new TextBox() { Name = "txtEditCorrect", Text = correctAnswer, Location = new Point(20, nextY + 20), Width = 400 };
                panelEdit.Controls.Add(lblCorrect);
                panelEdit.Controls.Add(txtCorrectComplete);
                nextY += 60;
            }

            Label lblCat = new Label() { Text = "Category:", Location = new Point(20, nextY), AutoSize = true };
            TextBox txtCat = new TextBox() { Text = category, Location = new Point(20, nextY + 20), Width = 400 };
            panelEdit.Controls.Add(lblCat);
            panelEdit.Controls.Add(txtCat);
            nextY += 70;

            Label lblHint = new Label() { Text = "Hint (Optional):", Location = new Point(20, nextY), AutoSize = true };
            TextBox txtHint = new TextBox() { Text = hint, Location = new Point(20, nextY + 20), Width = 400 };
            panelEdit.Controls.Add(lblHint);
            panelEdit.Controls.Add(txtHint);
            nextY += 70;

            Label lblLevel = new Label() { Text = "Difficulty Level:", Location = new Point(20, nextY), AutoSize = true };
            ComboBox cmbLevel = new ComboBox()
            {
                Location = new Point(20, nextY + 20),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbLevel.Items.AddRange(new[] { "Low", "Medium", "Hard" });
            cmbLevel.SelectedItem = difficulty;
            panelEdit.Controls.Add(lblLevel);
            panelEdit.Controls.Add(cmbLevel);
            nextY += 80;

            Button btnSave = new Button()
            {
                BackColor = Color.FromArgb(76, 175, 80),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Location = new Point(20, nextY),
                Size = new Size(120, 40),
                Text = "SAVE"
            };

            Button btnCancel = new Button()
            {
                BackColor = Color.FromArgb(244, 67, 54),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Location = new Point(160, nextY),
                Size = new Size(120, 40),
                Text = "CANCEL"
            };

            btnSave.Click += (s, e) =>
            {
                string selectedCorrect;
                if (type.ToLower().Contains("complete"))
                {
                    selectedCorrect = txtCorrectComplete?.Text.Trim();
                }
                else
                {
                    selectedCorrect = cmbCorrect?.SelectedItem?.ToString();
                }

                UpdateQuestionInExcel(
                    number,
                    type,
                    txtQ.Text,
                    optionBoxes,
                    selectedCorrect,
                    txtCat.Text,
                    txtHint.Text,
                    cmbLevel.SelectedItem?.ToString()
                );
            };

            btnCancel.Click += (s, e) => panelEdit.Visible = false;

            panelEdit.Controls.Add(btnSave);
            panelEdit.Controls.Add(btnCancel);
        }






        public void UpdateQuestionInExcel(string number, string type, string newQuestion, List<TextBox> optionBoxes, string newCorrect, string newCategory, string newHint, string newLevel)
        {
            try
            {
                using (var workbook = new XLWorkbook(path))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed().ToList();
                    int uiIndex = int.Parse(number.Replace("#", "").Trim());
                    int realCount = 0;

                    for (int i = 1; i < rows.Count; i++)
                    {
                        var row = rows[i];
                        string typeCell = row.Cell(3).GetString().ToLower();
                        string owner = row.Cell(9).GetString().Trim();

                        if ((typeCell == "mcq" || typeCell == "true / false" || typeCell == "complete sentence") &&
                            owner == lecturerId)
                        {
                            realCount++;
                            if (realCount == uiIndex)
                            {
                                string originalLecturerId = owner;

                                if (string.IsNullOrWhiteSpace(newQuestion) || string.IsNullOrWhiteSpace(newCategory) || string.IsNullOrWhiteSpace(newLevel))
                                {
                                    MessageBox.Show("Please fill all fields properly. Hint is optional.");
                                    return;
                                }

                                string updatedCorrect = newCorrect;

                                if (type.ToLower().Contains("complete"))
                                {
                                    int placeholderCount = 0;
                                    int searchIndex = 0;
                                    while ((searchIndex = newQuestion.IndexOf("{}", searchIndex)) != -1)
                                    {
                                        placeholderCount++;
                                        searchIndex += 2;
                                    }

                                    var invalid = System.Text.RegularExpressions.Regex.Match(newQuestion, "\\{[^}]+\\}");
                                    if (invalid.Success)
                                    {
                                        MessageBox.Show("Invalid placeholder detected. Use {} with no content inside.");
                                        return;
                                    }

                                    if (placeholderCount == 0)
                                    {
                                        MessageBox.Show("Complete Sentence must include at least one {} placeholder.");
                                        return;
                                    }

                                    var answers = newCorrect.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                            .Select(s => s.Trim()).ToArray();

                                    if (answers.Length != placeholderCount)
                                    {
                                        MessageBox.Show($"You used {placeholderCount} {{}} but provided {answers.Length} answers.");
                                        return;
                                    }

                                    updatedCorrect = string.Join(", ", answers);
                                }

                                if (type.ToLower().Contains("mcq"))
                                {
                                    if (string.IsNullOrWhiteSpace(newCorrect))
                                    {
                                        MessageBox.Show("Please select a correct answer for MCQ.");
                                        return;
                                    }

                                    var options = optionBoxes.Select(b => b.Text.Trim()).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
                                    if (options.Count < 2)
                                    {
                                        MessageBox.Show("MCQ requires at least two options.");
                                        return;
                                    }

                                    for (int j = 0; j < 4; j++)
                                    {
                                        int rowIdx = i + j;
                                        if (j < options.Count)
                                        {
                                            if (j == 0)
                                            {
                                                worksheet.Cell(rowIdx + 1, 2).Value = newQuestion;
                                                worksheet.Cell(rowIdx + 1, 3).Value = "MCQ";
                                            }

                                            worksheet.Cell(rowIdx + 1, 4).Value = $"{(char)('A' + j)}) {options[j]}";
                                        }
                                        else
                                        {
                                            worksheet.Cell(rowIdx + 1, 4).Clear();
                                        }

                                        worksheet.Cell(rowIdx + 1, 1).Value = j == 0 ? uiIndex.ToString() : "";
                                        worksheet.Cell(rowIdx + 1, 5).Value = j == 0 ? newCorrect : "";
                                        worksheet.Cell(rowIdx + 1, 6).Value = j == 0 ? newCategory : "";
                                        worksheet.Cell(rowIdx + 1, 7).Value = j == 0 ? newHint : "";
                                        worksheet.Cell(rowIdx + 1, 8).Value = j == 0 ? newLevel : "";
                                        worksheet.Cell(rowIdx + 1, 9).Value = j == 0 ? originalLecturerId : "";
                                    }
                                }
                                else
                                {
                                    row.Cell(2).Value = newQuestion;
                                    row.Cell(3).Value = type;
                                    row.Cell(4).Clear();
                                    row.Cell(5).Value = updatedCorrect;
                                    row.Cell(6).Value = newCategory;
                                    row.Cell(7).Value = newHint;
                                    row.Cell(8).Value = newLevel;
                                    row.Cell(9).Value = originalLecturerId;
                                }

                                workbook.Save();
                                using (SuccessMessageBox successBox = new SuccessMessageBox("Updated Successfully!"))
                                {
                                    successBox.ShowDialog();
                                }

                                panelEdit.Visible = false;
                                dataGridView.Rows.Clear();
                                LoadQuestionsFromExcel();
                                dataGridView.Refresh();
                                return;
                            }
                        }
                    }

                    MessageBox.Show("Could not locate question to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }






        public async void DeleteQuestion(string uiNumber)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to delete question {uiNumber}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            int uiIndex = int.Parse(uiNumber.Replace("#", "").Trim());

            await Task.Run(() =>
            {
                try
                {
                    using (var workbook = new XLWorkbook(path))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RangeUsed().RowsUsed().ToList();
                        int realCount = 0;
                        int rowToDeleteIndex = -1;

                        for (int i = 1; i < rows.Count; i++)
                        {
                            var type = rows[i].Cell(3).GetValue<string>()?.ToLower();
                            var owner = rows[i].Cell(9).GetValue<string>()?.Trim();

                            if ((type == "mcq" || type == "true / false" || type == "complete sentence") &&
                                owner == lecturerId)
                            {
                                realCount++;
                                if (realCount == uiIndex)
                                {
                                    rowToDeleteIndex = i;
                                    break;
                                }
                            }
                        }

                        if (rowToDeleteIndex == -1)
                        {
                            Invoke(new Action(() => MessageBox.Show("Could not find the question to delete.")));
                            return;
                        }

                        var rowToDelete = rows[rowToDeleteIndex];
                        var typeDel = rowToDelete.Cell(3).GetValue<string>().ToLower();
                        int deleteCount = 1;

                        if (typeDel.Contains("mcq"))
                        {
                            for (int i = rowToDeleteIndex + 1; i < rows.Count && deleteCount < 4; i++)
                            {
                                var colA = rows[i].Cell(1).GetValue<string>();
                                var colD = rows[i].Cell(4).GetValue<string>();
                                if (string.IsNullOrWhiteSpace(colA) && !string.IsNullOrWhiteSpace(colD))
                                    deleteCount++;
                                else
                                    break;
                            }
                        }
                        else if (typeDel.Contains("complete"))
                        {
                            for (int i = rowToDeleteIndex + 1; i < rows.Count; i++)
                            {
                                var colA = rows[i].Cell(1).GetValue<string>();
                                var colD = rows[i].Cell(4).GetValue<string>();
                                if (string.IsNullOrWhiteSpace(colA) && string.IsNullOrWhiteSpace(colD))
                                    deleteCount++;
                                else
                                    break;
                            }
                        }

                        for (int i = 0; i < deleteCount; i++)
                        {
                            rows[rowToDeleteIndex].Delete();
                            rows.RemoveAt(rowToDeleteIndex);
                        }

                        rows = worksheet.RangeUsed().RowsUsed().ToList();
                        int questionNum = 1;
                        foreach (var row in rows.Skip(1))
                        {
                            var colType = row.Cell(3).GetValue<string>()?.ToLower();
                            if (colType == "mcq" || colType == "true / false" || colType == "complete sentence")
                            {
                                row.Cell(1).Value = questionNum++;
                            }
                            else
                            {
                                row.Cell(1).Value = "";
                            }
                        }

                        workbook.Save();

                        Invoke(new Action(() =>
                        {
                            using (SuccessMessageBox successBox = new SuccessMessageBox("Question deleted successfully!"))
                            {
                                successBox.ShowDialog();
                            }

                            LoadQuestionsFromExcel();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Invoke(new Action(() => MessageBox.Show("Something went wrong while deleting:\n" + ex.Message)));
                }
            });
        }





        private void CreateExcelFile()
        {
            string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string dataFolder = Path.Combine(projectPath, "Data");

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            string filePath = Path.Combine(dataFolder, "Questions.xlsx");

            if (!File.Exists(filePath))
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Questions");

                worksheet.Cell(1, 1).Value = "Number";
                worksheet.Cell(1, 2).Value = "Question";
                worksheet.Cell(1, 3).Value = "Type";
                worksheet.Cell(1, 4).Value = "Options";
                worksheet.Cell(1, 5).Value = "Correct Answer";
                worksheet.Cell(1, 6).Value = "Category";
                worksheet.Cell(1, 7).Value = "Hint";
                worksheet.Cell(1, 8).Value = "Difficulty Level";
                worksheet.Cell(1, 9).Value = "LecturerID";  // ✅ new column

                workbook.SaveAs(filePath);
            }
        }



    }


}
