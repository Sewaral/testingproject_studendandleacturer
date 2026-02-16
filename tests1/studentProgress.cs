using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;

namespace tests1
{
    [TestClass]
    public class Form2_Tests
    {
        private class RealForm2 : Form2
        {
            public Label avg = new Label();
            public Label latest = new Label();
            public Label progress = new Label();
            public DataGridView grid = new DataGridView();

            public RealForm2() : base("TestStudent")
            {
                SetScoreLabels(avg, latest, progress);

                typeof(Form2)
                    .GetField("categoryGrid", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.SetValue(this, grid);
            }

            public void RunAnalyzeOverallProgress(DataTable table)
            {
                AnalyzeOverallProgress(table);
            }

            public void RunAnalyzeCategoryProgress(DataTable table)
            {
                var method = typeof(Form2).GetMethod("AnalyzeCategoryProgress", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                method?.Invoke(this, new object[] { table });
            }
        }

        [TestMethod]
        public void Test_AnalyzeOverallProgress_WorksCorrectly()
        {
            var form = new RealForm2();
            var table = new DataTable();
            table.Columns.Add("Score (%)");
            table.Rows.Add("70.0");
            table.Rows.Add("80.0");
            table.Rows.Add("90.0");

            form.RunAnalyzeOverallProgress(table);

            Assert.AreEqual("Average: 80.00%", form.avg.Text);
            Assert.AreEqual("Latest: 90.00%", form.latest.Text);
            Assert.IsTrue(form.progress.Text.Contains("⬆"));
        }

        
    }
}
