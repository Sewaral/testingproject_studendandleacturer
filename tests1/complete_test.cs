using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;
using System.Windows.Forms;
using System.Reflection;

namespace UnitTests
{
    [TestClass]
    public class complete_test
    {
        private complete form;

        [TestInitialize]
        public void Setup()
        {
            form = new complete(new AddQuestion("testLecturer"));

            // Inject real control instances into private fields
            SetPrivateField("txtQuestion", new TextBox());
            SetPrivateField("txtCorrectAnswer", new TextBox());
            SetPrivateField("txtCategory", new TextBox());
            SetPrivateField("radioHard", new RadioButton());
            SetPrivateField("radioMedium", new RadioButton());
            SetPrivateField("radioLow", new RadioButton());
        }

        private void SetPrivateField(string name, object control)
        {
            form.Controls.Add(control as Control); // add to form so it's not detached
            typeof(complete).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(form, control);
        }

        private T GetField<T>(string name) where T : Control
        {
            return typeof(complete).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(form) as T;
        }

        [TestMethod]
        public void AreRequiredFieldsFilled_Valid_ReturnsTrue()
        {
            GetField<TextBox>("txtQuestion").Text = "The {} runs.";
            GetField<TextBox>("txtCorrectAnswer").Text = "dog";
            GetField<TextBox>("txtCategory").Text = "Animals";
            GetField<RadioButton>("radioLow").Checked = true;

            Assert.IsTrue(form.AreRequiredFieldsFilled());
        }

        [TestMethod]
        public void AreRequiredFieldsFilled_Invalid_ReturnsFalse()
        {
            GetField<TextBox>("txtQuestion").Text = "The {} jumps.";
            GetField<TextBox>("txtCorrectAnswer").Text = ""; // missing answer
            GetField<TextBox>("txtCategory").Text = "Nature";
            GetField<RadioButton>("radioMedium").Checked = true;

            Assert.IsFalse(form.AreRequiredFieldsFilled());
        }
    }
}
