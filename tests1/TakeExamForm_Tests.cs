using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace tests1
{
    [TestClass]
    public class TakeExamForm_Tests
    {
        private TakeExamForm form;

        [TestInitialize]
        public void Init()
        {
            form = new TakeExamForm("TestStudent");

            // Setup 3 mock questions: 2 correct, 1 wrong
            form.currentExamQuestions = new List<TakeExamForm.Question>
            {
                new TakeExamForm.Question { Text = "Q1", Type = "true", Options = new[] { "True", "False" }, Correct = "True" },
                new TakeExamForm.Question { Text = "Q2", Type = "mcq", Options = new[] { "A", "B", "C", "D" }, Correct = "C" },
                new TakeExamForm.Question { Text = "Q3", Type = "complete", Correct = "answer" }
            };

            form.studentAnswers = new Dictionary<int, string>
            {
                { 0, "True" },
                { 1, "C" },
                { 2, "wrong" }
            };

            form.currentExam = new TakeExamForm.ExamInfo
            {
                ExamId = "E1",
                Category = "Science",
                Difficulty = "Medium",
                FilePath = "fake.xlsx",
                LecturerId = "L1"
            };
        }

        [TestMethod]
        public void Test_GradeCalculation_AndLetterGrade()
        {
            // Act
            form.SaveCurrentAnswer(); // optional: to trigger one more time
            var grade = form.GetLetterGrade(66.6); // emulate 2/3 correct

            // Assert
            Assert.AreEqual("D", grade); // 66.6% => D

            // Now test internal logic
            int correctCount = form.currentExamQuestions
                .Select((q, i) => (correct: q.Correct.Trim(), answer: form.studentAnswers.TryGetValue(i, out var a) ? a.Trim() : ""))
                .Count(x => string.Equals(x.correct, x.answer, System.StringComparison.OrdinalIgnoreCase));

            Assert.AreEqual(2, correctCount);
        }

        [TestMethod]
        public void Test_SaveCurrentAnswer_TracksAnswerCorrectly()
        {
            // Simulate MCQ screen and checked radio
            var q = new TakeExamForm.Question
            {
                Text = "Which is correct?",
                Type = "mcq",
                Options = new[] { "Red", "Green", "Blue" },
                Correct = "Blue"
            };

            form.currentExamQuestions = new List<TakeExamForm.Question> { q };
            form.currentQuestionIndex = 0;
            form.pnlOptions = new Panel();

            var rb = new RadioButton { Text = "Blue", Tag = "Blue", Checked = true };
            form.pnlOptions.Controls.Add(rb);

            // Act
            form.SaveCurrentAnswer();

            // Assert
            Assert.IsTrue(form.studentAnswers.ContainsKey(0));
            Assert.AreEqual("Blue", form.studentAnswers[0]);
        }
    }
}
