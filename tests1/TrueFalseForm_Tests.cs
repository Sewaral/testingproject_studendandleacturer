using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;

namespace Tests
{
    [TestClass]
    public class TrueFalseForm_Tests
    {
        private TrueFalseForm form;

        [TestInitialize]
        public void Init()
        {
            var parent = new AddQuestion("testLecturer");
            form = new TrueFalseForm(parent);
        }

        [TestMethod]
        public void Test_ValidInput_ReturnsValid()
        {
            form.txtQuestion.Text = "Is the sky blue?";
            form.txtCategory.Text = "Science";
            form.radioTrue.Checked = true;
            form.radioMedium.Checked = true;

            string result = form.ValidateInputLogic();

            Assert.AreEqual("Valid", result);
        }

        [TestMethod]
        public void Test_MissingCategory_ReturnsWarning()
        {
            form.txtQuestion.Text = "Is water dry?";
            form.txtCategory.Text = ""; // Missing category
            form.radioTrue.Checked = true;
            form.radioLow.Checked = true;

            string result = form.ValidateInputLogic();

            Assert.AreEqual("Please fill all fields properly. Hint is optional.", result);
        }
    }
}
