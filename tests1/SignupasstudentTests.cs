using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;

namespace tests1
{
    [TestClass]
    public class SignupasstudentTests
    {
        [TestMethod]
        public void InvalidUsernameAndPassword_ShouldFailValidation_UsingSignupFormLogic()
        {
            // Arrange
            var form = new signupasstudent(); // use your actual form
            string errorMessage;

            string badUsername = "usr!";         // Invalid: too short and contains '!'
            string badPassword = "password";     // Invalid: no digit or special character

            // Act
            bool result = form.ValidateCredentials(badUsername, badPassword, out errorMessage);

            // Assert
            Assert.IsFalse(result, "Expected validation to fail for invalid username/password.");
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage), "Error message should be provided.");
        }


        [TestMethod]
        public void IsValidGmail_ShouldReturnCorrectResult()
        {
            var form = new signupasstudent();

            // Valid Gmail
            Assert.IsTrue(form.IsValidGmail("user@gmail.com"), "Expected valid Gmail address to return true.");

            // Invalid Gmail (wrong domain)
            Assert.IsFalse(form.IsValidGmail("user@yahoo.com"), "Expected non-Gmail address to return false.");

            // Invalid Gmail (missing '@')
            Assert.IsFalse(form.IsValidGmail("usergmail.com"), "Expected malformed email to return false.");

            // Invalid Gmail (empty)
            Assert.IsFalse(form.IsValidGmail(""), "Expected empty email to return false.");

            // Invalid Gmail (null)
            Assert.IsFalse(form.IsValidGmail(null), "Expected null email to return false.");

            // Invalid Gmail (extra domain parts)
            Assert.IsFalse(form.IsValidGmail("user@gmail.co.uk"), "Expected subdomain Gmail to return false.");
        }

    }
}