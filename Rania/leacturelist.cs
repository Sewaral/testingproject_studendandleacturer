using System;
using System.Windows.Forms;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class leacturelist : Form
    {
        private string lectureName;

        public leacturelist(string name)
        {
            InitializeComponent();
            lectureName = name;
            lblWelcome.Text = $"Welcome {lectureName}";
        }

        private void leacturelist_Load(object sender, EventArgs e)
        {
            // Optional: load extra info here if needed
        }

        private void btnQuestions_Click(object sender, EventArgs e)
        {
            AddQuestion questionForm = new AddQuestion(lectureName); // ✅ Pass the lecturer ID
            questionForm.ShowDialog();
        }

        private void btnCreateExam_Click(object sender, EventArgs e)
        {
            CreateExamForm examForm = new CreateExamForm(lectureName); // ✅ Pass lecturer ID
            examForm.ShowDialog();
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            StatsForm statsForm = new StatsForm(lectureName); // ✅ Pass lecturer ID

            // 🚫 Prevent opening if no data found
            if (!statsForm.LoadStatsData())
            {
                using (var cmb = new CustomMessageBox("No grades found for your exams"))
                {
                    cmb.ShowDialog();
                }
                return;
            }

            statsForm.ShowDialog();
        }

    }
}
