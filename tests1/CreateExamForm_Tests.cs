using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;

namespace tests1
{
    [TestClass]
    public class CreateExamForm_Tests
    {
        [TestMethod]
        public void Test_QuestionSelection_WithMatchingCategoryAndDifficulty()
        {
            // Arrange
            var form = new CreateExamForm("L123");
            var selectedCategory = "Math";
            var selectedDifficulty = "Medium";

            var questions = new List<(string, string, string, string, string, string, string)>
            {
                ("Q1", "MCQ", "A\nB\nC\nD", "A", "Math", "Medium", "Hint1"),
                ("Q2", "True / False", "True\nFalse", "True", "Math", "Medium", "Hint2"),
                ("Q3", "Complete", "", "Answer", "Math", "Medium", "Hint3")
            };

            // Act
            var selected = form.FilterQuestionsByCategoryAndDifficulty(questions, selectedCategory, selectedDifficulty);

            // Assert
            Assert.AreEqual(3, selected.Count);
        }

        [TestMethod]
        public void Test_QuestionSelection_NoMatch()
        {
            // Arrange
            var form = new CreateExamForm("L123");
            var selectedCategory = "Physics";
            var selectedDifficulty = "Hard";

            var questions = new List<(string, string, string, string, string, string, string)>
            {
                ("Q1", "MCQ", "A\nB\nC\nD", "A", "Math", "Medium", "Hint1"),
                ("Q2", "True / False", "True\nFalse", "True", "Math", "Medium", "Hint2"),
                ("Q3", "Complete", "", "Answer", "Math", "Medium", "Hint3")
            };

            // Act
            var selected = form.FilterQuestionsByCategoryAndDifficulty(questions, selectedCategory, selectedDifficulty);

            // Assert
            Assert.AreEqual(0, selected.Count);
        }
    }
}
