
using System;
using System.Windows.Forms;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class studentlist : Form
    {
        private string studentName;

        public studentlist(string studentName)
        {
            InitializeComponent();
            this.studentName = studentName;

            // תווית שלום עם שם הסטודנט בצד ימין למעלה
            Label lblGreeting = new Label
            {
                Text = $"שלום {studentName}",
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(this.ClientSize.Width - 180, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            this.Controls.Add(lblGreeting);
        }

        private void BtnTakeExam_Click(object sender, EventArgs e)
        {
            new TakeExamForm(studentName).ShowDialog();
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            var progressForm = new Form2(studentName);

            var historyForm = new HistoryForm(studentName);
            if (!progressForm.LoadHistoryData())
            {
                using (var cmb = new CustomMessageBox("No grades found for your exams."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            historyForm.ShowDialog();
        }


        private void BtnProgress_Click(object sender, EventArgs e)
        {
            var progressForm = new Form2(studentName);
            if (!progressForm.LoadHistoryData())
            {
                using (var cmb = new CustomMessageBox("No grades found for your exams."))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            progressForm.ShowDialog();
        }




        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

