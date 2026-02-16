using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class signupasstudent : Form
    {
        public signupasstudent()
        {
            InitializeComponent();
            textBox3.UseSystemPasswordChar = true;
        }

        private void showPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();
            string password = textBox3.Text.Trim();
            string idStudent = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(idStudent))
            {
                using (var cmb = new CustomMessageBox("Please fill all fields."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            if (name.Length < 6 || name.Length > 8 || !Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
            {
                using (var cmb = new CustomMessageBox("Username must be 6–8 chars, letters & digits only."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            if (password.Length < 8 || password.Length > 10 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter) ||
                !password.Any(ch => "!@#$%^&*()_+-=[]{}|;:',.<>?/".Contains(ch)))
            {
                using (var cmb = new CustomMessageBox("Password must be 8–10 chars with letter, digit & special char."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@gmail\.com$"))
            {
                using (var cmb = new CustomMessageBox("Please enter a valid Gmail address ending with @gmail.com."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
            string folderPath = Path.Combine(basePath, "data2");
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, "signup1.xlsx");

            try
            {
                using (var workbook = File.Exists(filePath) ? new XLWorkbook(filePath) : new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault() ?? workbook.AddWorksheet("Students");

                    if (worksheet.Cell(1, 1).IsEmpty())
                    {
                        worksheet.Cell(1, 1).Value = "Name";
                        worksheet.Cell(1, 2).Value = "Email";
                        worksheet.Cell(1, 3).Value = "Password";
                        worksheet.Cell(1, 4).Value = "IDStudent";
                    }

                    int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;

                    for (int i = 2; i <= lastRow; i++)
                    {
                        string existingName = worksheet.Cell(i, 1).GetString();
                        string existingEmail = worksheet.Cell(i, 2).GetString();
                        string existingID = worksheet.Cell(i, 4).GetString();

                        if (!string.IsNullOrEmpty(existingName) && name.Equals(existingName, StringComparison.OrdinalIgnoreCase))
                        {
                            using (var cmb = new CustomMessageBox("This username is already taken."))
                            {
                                cmb.ShowDialog();
                            }
                            return;
                        }

                        if (!string.IsNullOrEmpty(existingEmail) && email.Equals(existingEmail, StringComparison.OrdinalIgnoreCase))
                        {
                            using (var cmb = new CustomMessageBox("This email is already registered."))
                            {
                                cmb.ShowDialog();
                            }
                            return;
                        }

                        if (!string.IsNullOrEmpty(existingID) && idStudent.Equals(existingID))
                        {
                            using (var cmb = new CustomMessageBox("This student ID is already registered."))
                            {
                                cmb.ShowDialog();
                            }
                            return;
                        }
                    }

                    int newRow = lastRow + 1;
                    worksheet.Cell(newRow, 1).Value = name;
                    worksheet.Cell(newRow, 2).Value = email;
                    worksheet.Cell(newRow, 3).Value = password;
                    worksheet.Cell(newRow, 4).Value = idStudent;

                    workbook.SaveAs(filePath);
                }

                using (var successMsg = new SuccessMessageBox("Registration successful!"))
                {
                    successMsg.ShowDialog();
                }

                loginstudent loginForm = new loginstudent();
                loginForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                using (var cmb = new CustomMessageBox("Error: " + ex.Message))
                {
                    cmb.ShowDialog();
                }
            }
        }

        public bool ValidateCredentials(string username, string password, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrEmpty(username) || username.Length < 6 || username.Length > 8 || !username.All(char.IsLetterOrDigit))
            {
                errorMessage = "Username must be 6–8 characters long and contain only letters and digits.";
                return false;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 8 || password.Length > 10 ||
                !password.Any(char.IsLetter) ||
                !password.Any(char.IsDigit) ||
                !password.Any(ch => "!@#$%^&*()_+-=[]{}|;:',.<>?/".Contains(ch)))
            {
                errorMessage = "Password must be 8–10 characters long and contain at least one letter, one digit, and one special character.";
                return false;
            }

            return true;
        }

        public bool IsValidGmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   Regex.IsMatch(email, @"^[^@\s]+@gmail\.com$");
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}
