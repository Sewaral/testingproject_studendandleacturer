using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class loginstudent : Form
    {
        public loginstudent()
        {
            InitializeComponent();
            maskedTextBox2.UseSystemPasswordChar = true;
            //showPasswordCheckBox.CheckedChanged += showPasswordCheckBox_CheckedChanged;
        }

        //private void showPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        //{
        //    maskedTextBox2.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
        //}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            signupasstudent signupForm = new signupasstudent();
            signupForm.Show();
        }

        private void loginstudent_Load(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = maskedTextBox1.Text.Trim();
            string password = maskedTextBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                using (var cmb = new CustomMessageBox("Please enter username and password."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
            string folderPath = Path.Combine(basePath, "data2");
            Directory.CreateDirectory(folderPath);

            string signupPath = Path.Combine(folderPath, "signup1.xlsx");
            string loginLogPath = Path.Combine(folderPath, "logasstudent.xlsx");

            if (!File.Exists(signupPath))
            {
                using (var cmb = new CustomMessageBox("No registered users found."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            try
            {
                bool loginSuccessful = false;

                using (var signupWorkbook = new XLWorkbook(signupPath))
                {
                    var sheet = signupWorkbook.Worksheet(1);
                    int lastRow = sheet.LastRowUsed()?.RowNumber() ?? 1;

                    for (int row = 2; row <= lastRow; row++)
                    {
                        string storedUsername = sheet.Cell(row, 1).GetString();
                        string storedPassword = sheet.Cell(row, 3).GetString();

                        if (storedUsername == username && storedPassword == password)
                        {
                            loginSuccessful = true;
                            break;
                        }
                    }
                }

                if (loginSuccessful)
                {
                    using (SuccessMessageBox successBox = new SuccessMessageBox("Login successful!"))
                    {
                        successBox.ShowDialog();
                    }
                    using (var loginWorkbook = File.Exists(loginLogPath)
                        ? new XLWorkbook(loginLogPath)
                        : new XLWorkbook())
                    {
                        var loginSheet = loginWorkbook.Worksheets.FirstOrDefault()
                            ?? loginWorkbook.AddWorksheet("Logins");

                        if (loginSheet.Cell(1, 1).IsEmpty())
                        {
                            loginSheet.Cell(1, 1).Value = "Username";
                            loginSheet.Cell(1, 2).Value = "Password";
                            loginSheet.Cell(1, 3).Value = "Login Time";
                        }

                        int newRow = loginSheet.LastRowUsed()?.RowNumber() + 1 ?? 2;
                        loginSheet.Cell(newRow, 1).Value = username;
                        loginSheet.Cell(newRow, 2).Value = password;
                        loginSheet.Cell(newRow, 3).Value = DateTime.Now.ToString("g");

                        loginWorkbook.SaveAs(loginLogPath);
                    }

                    studentlist studentForm = new studentlist(username);
                    studentForm.Show();
                    this.Hide();
                }
                else
                {
                    using (var cmb = new CustomMessageBox("Incorrect username or password."))
                    {
                        cmb.ShowDialog();
                    }
                }
            }
            catch (IOException ioEx)
            {
                using (var cmb = new CustomMessageBox("File is open or locked. Close Excel and try again.\n\n" + ioEx.Message))
                {
                    cmb.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                using (var cmb = new CustomMessageBox("Error: " + ex.Message))
                {
                    cmb.ShowDialog();
                }
            }
        }
    }
}
