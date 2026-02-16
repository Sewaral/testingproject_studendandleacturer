using System;
using System.Windows.Forms;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnTakeExam_Click(object sender, EventArgs e)
        {
            //new TakeExamForm().ShowDialog();
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            new HistoryForm(Environment.UserName).ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
