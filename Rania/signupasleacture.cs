using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class Signupasleacture : Form
    {
        private CheckBox showPasswordCheckBox;

        public Signupasleacture()
        {
            InitializeComponent();
            SetupShowPasswordCheckbox();
            textBox3.UseSystemPasswordChar = true; // Hide password by default
        }

        public void SetupShowPasswordCheckbox()
        {
            showPasswordCheckBox = new CheckBox
            {
                Text = "Show Password",
                AutoSize = true,
                Location = new Point(textBox3.Right + 10, textBox3.Top),
                Parent = this
            };
            showPasswordCheckBox.CheckedChanged += (s, e) =>
            {
                textBox3.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
            };
            Controls.Add(showPasswordCheckBox);
        }

        public void label2_Click(object sender, EventArgs e) { }
        public void panel1_Paint(object sender, PaintEventArgs e) { }
        public void panel2_Paint(object sender, PaintEventArgs e) { }

        public void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();
            string password = textBox3.Text.Trim();
            string id = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(id))
            {
                using (var cmb = new CustomMessageBox("Please fill all fields."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            if (name.Length < 6 || name.Length > 8 || !Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
            {
                using (var cmb = new CustomMessageBox("Username must be 6–8 characters, letters & digits only."))
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
                using (var cmb = new CustomMessageBox("Please enter a valid Gmail ending with @gmail.com."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
            string folderPath = Path.Combine(basePath, "data2");
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, "signupasleacture.xlsx");

            if (!File.Exists(filePath))
            {
                using (var cmb = new CustomMessageBox("Data file does not exist:\n" + filePath))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;

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
                        if (!string.IsNullOrEmpty(existingID) && id.Equals(existingID))
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
                    worksheet.Cell(newRow, 4).Value = id;

                    workbook.Save();
                }

                // Keep success message as regular MessageBox
                using (var successMsg = new SuccessMessageBox("Registration successful!"))
                {
                    successMsg.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                using (var cmb = new CustomMessageBox("Error: " + ex.Message))
                {
                    cmb.ShowDialog();
                }
            }

            Loginleacture loginForm = new Loginleacture();
            loginForm.Show();
            this.Hide();
        }
    }
}
