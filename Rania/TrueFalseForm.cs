using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
                      

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class TrueFalseForm : Form
    {
        private int leftX = 200, inputX = 200, width = 800;
        private int startY = 100, gap = 100;
        private AddQuestion addQuestionForm;  // Declare at class level


        // ✅ Centralized Excel Path for Project
        private string path = Path.Combine(
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")),
            "Data",
            "Questions.xlsx"
        );

        private AddQuestion parentForm;  // Field at class level

        public TrueFalseForm(AddQuestion parent)  // Parameter name is DIFFERENT
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            this.parentForm = parent;  // ✅ Correct assignment now
            addQuestionForm = parent;  // ✅ Works as expected

            CreateField(ref labelQuestion, ref txtQuestion, ref lineQuestion, "Write Your Question", startY);
            CreateField(ref labelCategory, ref txtCategory, ref lineCategory, "Category", startY + 3 * gap);
            CreateField(ref labelHint, ref txtHint, ref lineHint, "Hint (Optional)", startY + 4 * gap);
        }





        private void CreateField(ref Label label, ref TextBox txt, ref Label line, string text, int y)
        {
            label = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(leftX, y),
                AutoSize = true
            };

            txt = new TextBox
            {
                Location = new Point(inputX, y + 35),
                Size = new Size(width, 30),
                Font = new Font("Segoe UI", 12F),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(246, 245, 243)
            };

            line = new Label
            {
                BorderStyle = BorderStyle.Fixed3D,
                Height = 2,
                Width = width,
                BackColor = Color.Black,
                Location = new Point(inputX, y + 65)
            };

            Controls.Add(label);
            Controls.Add(txt);
            Controls.Add(line);
        }

        public Label labelQuestion, labelCategory, labelHint;
        public TextBox txtQuestion, txtCategory, txtHint;
        public Label lineQuestion, lineCategory, lineHint;





        private void AddQuestionToExcel()
        {
            try
            {
                if (!File.Exists(path))
                {
                    using (var newWorkbook = new XLWorkbook())
                    {
                        var ws = newWorkbook.Worksheets.Add("Sheet1");
                        ws.Cell(1, 1).Value = "Number";
                        ws.Cell(1, 2).Value = "Question";
                        ws.Cell(1, 3).Value = "Type";
                        ws.Cell(1, 4).Value = "Options";
                        ws.Cell(1, 5).Value = "Correct Answer";
                        ws.Cell(1, 6).Value = "Category";
                        ws.Cell(1, 7).Value = "Hint";
                        ws.Cell(1, 8).Value = "Difficulty Level";
                        newWorkbook.SaveAs(path);
                    }
                }

                using (var workbook = new XLWorkbook(path))
                {
                    var worksheet = workbook.Worksheet("Questions");
                    var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;
                    lastRow++;

                    int questionNumber = lastRow - 1;
                    worksheet.Cell(lastRow, 9).Value = parentForm.GetLecturerId(); // ✅ save LecturerID

                    // 🟢 שמירת שורת השאלה
                    worksheet.Cell(lastRow, 1).Value = questionNumber;
                    worksheet.Cell(lastRow, 2).Value = txtQuestion.Text;
                    worksheet.Cell(lastRow, 3).Value = "True / False";
                    worksheet.Cell(lastRow, 5).Value = radioTrue.Checked ? "True" : "False";
                    worksheet.Cell(lastRow, 6).Value = txtCategory.Text;
                    worksheet.Cell(lastRow, 7).Value = txtHint.Text;
                    worksheet.Cell(lastRow, 8).Value = radioHard.Checked ? "Hard" :
                                                       radioMedium.Checked ? "Medium" : "Low";

                    // 🟢 שורות אפשרויות נפרדות
                    worksheet.Cell(lastRow + 1, 4).Value = "True";
                    worksheet.Cell(lastRow + 2, 4).Value = "False";

                    workbook.Save();
                }

                using (SuccessMessageBox successBox = new SuccessMessageBox("Question saved"))
                {
                    successBox.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error while saving " + ex.Message);
            }

            // 🧹 ניקוי
            txtQuestion.Text = "";
            txtCategory.Text = "";
            txtHint.Text = "";
            radioTrue.Checked = false;       
            radioFalse.Checked = false;
            radioHard.Checked = false;
            radioMedium.Checked = false;
            radioLow.Checked = false;
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text) ||
                string.IsNullOrWhiteSpace(txtCategory.Text) ||
                (!radioTrue.Checked && !radioFalse.Checked) ||
                (!radioHard.Checked && !radioMedium.Checked && !radioLow.Checked))
            {
                using (CustomMessageBox cmb = new CustomMessageBox("Please fill all fields properly.\nHint is optional."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            AddQuestionToExcel();
            parentForm.RefreshTotalQuestions();
            parentForm.LoadQuestionsFromExcel();

        }
        public string ValidateInputLogic()
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text) ||
                string.IsNullOrWhiteSpace(txtCategory.Text) ||
                (!radioTrue.Checked && !radioFalse.Checked) ||
                (!radioHard.Checked && !radioMedium.Checked && !radioLow.Checked))
            {
                return "Please fill all fields properly. Hint is optional.";
            }

            return "Valid";
        }

        private void TrueFalseForm_Load(object sender, EventArgs e)
        {

        }

        private void labelOptionA_Click(object sender, EventArgs e)
        {

        }
    }
}