using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;

namespace tests1
{
    [TestClass]
    public class LoginstudentTests
    {
        private loginstudent loginForm;
        private MaskedTextBox maskedTextBox1;
        private MaskedTextBox maskedTextBox2;
        private Button button1;

        [TestInitialize]
        public void Setup()
        {
            loginForm = new loginstudent();
            loginForm.Show(); // Important to fully load controls

            maskedTextBox1 = GetPrivateField<MaskedTextBox>(loginForm, "maskedTextBox1");
            maskedTextBox2 = GetPrivateField<MaskedTextBox>(loginForm, "maskedTextBox2");
            button1 = GetPrivateField<Button>(loginForm, "button1");
        }

        [TestCleanup]
        public void Cleanup()
        {
            loginForm?.Close();
            loginForm?.Dispose();
        }

        private T GetPrivateField<T>(object instance, string fieldName)
        {
            var field = instance.GetType().GetField(fieldName,
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);
            return (T)field?.GetValue(instance);
        }

        [TestMethod]
        public void PasswordMaskingAndButtonEnableBehavior_ShouldBeCorrect()
        {
            // ✅ Test 1: Password field should be masked by default
            Assert.IsTrue(maskedTextBox2.UseSystemPasswordChar,
                "Password should be masked by default");

            // ✅ Test 2: Button should enable only when both fields are filled

            // Both fields filled
            maskedTextBox1.Text = "testuser";
            maskedTextBox2.Text = "testpass123!";
            loginForm.ValidateChildren();
            Assert.IsTrue(button1.Enabled, "Button should be enabled when both fields are filled");
        }
    }
}
