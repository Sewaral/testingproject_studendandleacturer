using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Windows.Forms;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;

namespace tests1
{
    [TestClass]
    public class HistoryForm_Tests
    {
        [TestClass]
        public class HistoryForm_LogicTests
        {
            [TestMethod]
            public void CalculateAverage_WithValidScores_ReturnsCorrectAverage()
            {
                // Arrange
                var form = new HistoryForm("fake");
                double[] scores = { 80, 90, 100 };

                // Act
                double result = form.CalculateAverage(scores);

                // Assert
                Assert.AreEqual(90, result, 0.01, "Expected average of 80, 90, 100 to be 90.");
            }

            [TestMethod]
            public void CalculateAverage_WhenNoExams_ReturnsMinusOne()
            {
                var form = new HistoryForm("fake");

                // Simulate student with no exam results
                Assert.AreEqual(-1, form.CalculateAverage(null), "Expected -1 when student has no exams (null input).");
                Assert.AreEqual(-1, form.CalculateAverage(new double[0]), "Expected -1 when student has no exams (empty input).");
            }

        }
    }
    }
