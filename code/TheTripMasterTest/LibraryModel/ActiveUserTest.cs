using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class ActiveUserTest
    {
        [TestMethod]
        public void TestActiveUser()
        {
            User user = new User {UserId = 1};
            ActiveUser.User = user;
            ActiveUser.TripName = "Trip";

            Assert.AreEqual(user, ActiveUser.User);
            Assert.AreEqual("Trip", ActiveUser.TripName);
            Assert.IsTrue(ActiveUser.HasActiveUser());

            ActiveUser.Logout();

            Assert.IsFalse(ActiveUser.HasActiveUser());
        }
    }
}
