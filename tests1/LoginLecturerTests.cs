using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;
using System.Windows.Forms;

namespace tests1
{
    [TestClass]
    public class LoginLecturerTests
    {
        [TestMethod]
        public void IsUsernameEmpty_ShouldReturnTrue_WhenUsernameIsEmpty()
        {
            // Arrange
            var form = new Loginleacture();

            // Act
            var result = form.IsUsernameEmpty();

            // Assert
            Assert.IsTrue(result, "Expected username to be empty.");

            form.Dispose();
        }
    }
}