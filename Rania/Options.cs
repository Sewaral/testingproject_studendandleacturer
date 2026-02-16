using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;


namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class Options : Form
    {
        private string path = Path.Combine(
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")),
            "Data",
            "Questions.xlsx"
        );
        private AddQuestion parentForm;  // Class level reference

        public Options(AddQuestion parent)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            this.parentForm = parent;  // ✅ Set the reference to AddQuestion form
        }




        public void CreateField(ref Label label, ref TextBox txt, ref Label line, string text, int x, int y, int width)
        {
            label = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(x, y),
                AutoSize = true
            };

            txt = new TextBox
            {
                Location = new Point(x, y + 35),
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
                Location = new Point(x, y + 65)
            };

            Controls.Add(label);
            Controls.Add(txt);
            Controls.Add(line);
        }

        public void AddQuestionToExcel()
        {
            try
            {
                if (!File.Exists(path))
                {
                    using (var newWorkbook = new XLWorkbook())
                    {
                        var ws = newWorkbook.Worksheets.Add("Questions");
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
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed().ToList();
                    int lastRow = rows.Count;

                    // Skip leftover MCQ option rows
                    while (lastRow > 1 &&
                           string.IsNullOrWhiteSpace(worksheet.Cell(lastRow + 1, 1).GetValue<string>()) &&
                           !string.IsNullOrWhiteSpace(worksheet.Cell(lastRow + 1, 4).GetValue<string>()))
                    {
                        lastRow++;
                    }

                    lastRow++; // Next empty row
                    int realQuestionCount = rows.Count - 1; // Skip header
                    if (realQuestionCount < 1) realQuestionCount = 1;

                    // 🟢 Write Main MCQ Row
                    worksheet.Cell(lastRow, 9).Value = parentForm.GetLecturerId(); // ✅ save LecturerID

                    worksheet.Cell(lastRow, 1).Value = realQuestionCount;
                    worksheet.Cell(lastRow, 2).Value = txtQuestion.Text;
                    worksheet.Cell(lastRow, 3).Value = "MCQ";
                    worksheet.Cell(lastRow, 4).Value = "A) " + txtA.Text;
                    worksheet.Cell(lastRow, 5).Value = radioA.Checked ? "A" :
                                                       radioB.Checked ? "B" :
                                                       radioC.Checked ? "C" : "D";
                    worksheet.Cell(lastRow, 6).Value = txtCategory.Text;
                    worksheet.Cell(lastRow, 7).Value = txtHint.Text;
                    worksheet.Cell(lastRow, 8).Value = radioHard.Checked ? "Hard" :
                                                       radioMedium.Checked ? "Medium" : "Low";

                    // 🟠 Write Options B, C, D
                    worksheet.Cell(lastRow + 1, 1).Value = "";
                    worksheet.Cell(lastRow + 1, 4).Value = "B) " + txtB.Text;

                    worksheet.Cell(lastRow + 2, 1).Value = "";
                    worksheet.Cell(lastRow + 2, 4).Value = "C) " + txtC.Text;

                    worksheet.Cell(lastRow + 3, 1).Value = "";
                    worksheet.Cell(lastRow + 3, 4).Value = "D) " + txtD.Text;

                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving MCQ to Excel: " + ex.Message);
            }

            using (SuccessMessageBox successBox = new SuccessMessageBox("Question saved"))
            {
                successBox.ShowDialog();
            }
            // 🧹 Clear input fields
            txtQuestion.Text = "";
            txtA.Text = "";
            txtB.Text = "";
            txtC.Text = "";
            txtD.Text = "";
            txtCategory.Text = "";
            txtHint.Text = "";
            radioHard.Checked = false;
            radioMedium.Checked = false;
            radioLow.Checked = false;
            radioA.Checked = radioB.Checked = radioC.Checked = radioD.Checked = false;
        }


        public void btnSave_Click(object sender, EventArgs e)
        {
            // Check required fields including category, question, options, correct answer, difficulty
            if (string.IsNullOrWhiteSpace(txtCategory.Text) ||  // added category check
                string.IsNullOrWhiteSpace(txtQuestion.Text) ||
                string.IsNullOrWhiteSpace(txtA.Text) ||
                string.IsNullOrWhiteSpace(txtB.Text) ||
                string.IsNullOrWhiteSpace(txtC.Text) ||
                string.IsNullOrWhiteSpace(txtD.Text) ||
                (!radioA.Checked && !radioB.Checked && !radioC.Checked && !radioD.Checked) ||
                (!radioHard.Checked && !radioMedium.Checked && !radioLow.Checked)) // combined difficulty check here
            {
                using (CustomMessageBox cmb = new CustomMessageBox("Please fill all fields properly. Hint is optional."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            // All validations passed — proceed with saving
            AddQuestionToExcel();
            parentForm.RefreshTotalQuestions();
            parentForm.LoadQuestionsFromExcel();
        }



        private void Options_Load(object sender, EventArgs e)
        {

        }
    }
}
