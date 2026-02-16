using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using ClosedXML.Excel;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class complete : Form
    {
        private string path = Path.Combine(
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..")),
            "Data",
            "Questions.xlsx"
        );

        private AddQuestion parentForm;

        public complete(AddQuestion parent)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.parentForm = parent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string questionText = txtQuestion.Text.Trim();
            string correctAnswer = txtCorrectAnswer.Text.Trim();
            string category = txtCategory.Text.Trim();

            // Check all required fields including category and difficulty
            if (string.IsNullOrWhiteSpace(questionText) ||
                string.IsNullOrWhiteSpace(category) ||
                string.IsNullOrWhiteSpace(correctAnswer) ||
                (!radioHard.Checked && !radioMedium.Checked && !radioLow.Checked))
            {
                using (CustomMessageBox cmb = new CustomMessageBox("Please fill all required fields properly. Hint is optional."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            // Count valid {} placeholders (no content inside)
            int placeholderCount = 0;
            int searchIndex = 0;
            while ((searchIndex = questionText.IndexOf("{}", searchIndex)) != -1)
            {
                placeholderCount++;
                searchIndex += 2;
            }

            // Detect any invalid {something} usage
            var invalidPlaceholder = System.Text.RegularExpressions.Regex.Match(questionText, "\\{[^}]+\\}");
            if (invalidPlaceholder.Success)
            {
                using (CustomMessageBox cmb = new CustomMessageBox("Invalid placeholder detected. Use {} with no content inside."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            if (placeholderCount == 0)
            {
                using (CustomMessageBox cmb = new CustomMessageBox("Please include {} to indicate missing word location."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            string[] answers = correctAnswer.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (answers.Length != placeholderCount)
            {
                using (CustomMessageBox cmb = new CustomMessageBox($"You have {placeholderCount} placeholders but provided {answers.Length} answers."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            AddQuestionToExcel();
            parentForm.RefreshTotalQuestions();
            parentForm.LoadQuestionsFromExcel();
        }



        private void AddQuestionToExcel()
        {
            if (!File.Exists(path))
            {
                using (var workbookCreate = new XLWorkbook())
                {
                    var worksheetCreate = workbookCreate.Worksheets.Add("Questions");
                    worksheetCreate.Cell(1, 1).Value = "Number";
                    worksheetCreate.Cell(1, 2).Value = "Question";
                    worksheetCreate.Cell(1, 3).Value = "Type";
                    worksheetCreate.Cell(1, 4).Value = "Options";
                    worksheetCreate.Cell(1, 5).Value = "Correct Answer";
                    worksheetCreate.Cell(1, 6).Value = "Category";
                    worksheetCreate.Cell(1, 7).Value = "Hint";
                    worksheetCreate.Cell(1, 8).Value = "Difficulty Level";
                    workbookCreate.SaveAs(path);
                }
            }

            try
            {
                using (var workbook = new XLWorkbook(path))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed().ToList();
                    int lastRow = rows.Count + 1;
                    int questionNumber = rows.Count;
                    if (questionNumber < 1) questionNumber = 1;

                    string questionText = txtQuestion.Text.Trim();
                    string correctAnswer = txtCorrectAnswer.Text.Trim();

                    worksheet.Cell(lastRow, 9).Value = parentForm.GetLecturerId();
                    worksheet.Cell(lastRow, 1).Value = questionNumber;
                    worksheet.Cell(lastRow, 2).Value = questionText;
                    worksheet.Cell(lastRow, 3).Value = "Complete Sentence";
                    worksheet.Cell(lastRow, 4).Value = "";
                    worksheet.Cell(lastRow, 5).Value = correctAnswer;
                    worksheet.Cell(lastRow, 6).Value = txtCategory.Text.Trim();
                    worksheet.Cell(lastRow, 7).Value = txtHint.Text.Trim();
                    worksheet.Cell(lastRow, 8).Value = radioHard.Checked ? "Hard" :
                                                       radioMedium.Checked ? "Medium" : "Low";

                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                using (CustomMessageBox cmb = new CustomMessageBox("Error writing to Excel: " + ex.Message))
                {
                    cmb.ShowDialog();
                }
            }

            using (SuccessMessageBox successBox = new SuccessMessageBox("Question saved"))
            {
                successBox.ShowDialog();
            }
            txtQuestion.Text = "";
            txtCategory.Text = "";
            txtCorrectAnswer.Text = "";
            txtHint.Text = "";
            radioHard.Checked = false;
            radioMedium.Checked = false;
            radioLow.Checked = false;
        }
        public bool AreRequiredFieldsFilled()
        {
            string questionText = txtQuestion.Text.Trim();
            string correctAnswer = txtCorrectAnswer.Text.Trim();
            string category = txtCategory.Text.Trim();

            // Count {} placeholders and check for invalid {text}
            int placeholderCount = 0;
            int searchIndex = 0;
            while ((searchIndex = questionText.IndexOf("{}", searchIndex)) != -1)
            {
                placeholderCount++;
                searchIndex += 2;
            }

            bool hasInvalidPlaceholder = System.Text.RegularExpressions.Regex.IsMatch(questionText, "\\{[^}]+\\}");

            string[] answers = correctAnswer.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return
                !string.IsNullOrWhiteSpace(questionText) &&
                !string.IsNullOrWhiteSpace(category) &&
                !string.IsNullOrWhiteSpace(correctAnswer) &&
                (radioHard.Checked || radioMedium.Checked || radioLow.Checked) &&
                placeholderCount > 0 &&
                !hasInvalidPlaceholder &&
                answers.Length == placeholderCount;
        }


        private void labelCategory_Click(object sender, EventArgs e)
        {

        }

        private void complete_Load(object sender, EventArgs e)
        {

        }
    }
}
