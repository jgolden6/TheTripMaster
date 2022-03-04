using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestUser()
        {
            User user = new User
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "JSmith@gmail.com",
                Username = "JSmith",
                Password = "Password"
            };

            user.UserId = 2;
            user.FirstName = "Jim";
            user.LastName = "Jones";
            user.Email = "JJ@gmail.com";
            user.Username = "JJones";
            user.Password = "Wordpass";

            Assert.AreEqual(2, user.UserId);
            Assert.AreEqual("Jim", user.FirstName);
            Assert.AreEqual("Jones", user.LastName);
            Assert.AreEqual("JJ@gmail.com", user.Email);
            Assert.AreEqual("JJones", user.Username);
            Assert.AreEqual("Wordpass", user.Password);
        }
    }
}
