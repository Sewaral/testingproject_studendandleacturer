using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            signupasstudent signupForm = new signupasstudent();
            signupForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Signupasleacture signupForm = new Signupasleacture();
            signupForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginstudent login = new loginstudent();
            login.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Loginleacture login = new Loginleacture();
            login.Show();
        }
    }
}
