using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class UserValidationTest
    {
        [TestMethod]
        public void TestValidateName()
        {
            Assert.IsTrue(UserValidation.ValidateName("Jared"));
            Assert.IsFalse(UserValidation.ValidateName("abc123"));
            Assert.IsFalse(UserValidation.ValidateName(null));
        }

        [TestMethod]
        public void TestValidateEmail()
        {
            Assert.IsTrue(UserValidation.ValidateEmail("JSmith@gmail.com"));
            Assert.IsFalse(UserValidation.ValidateEmail("abc123"));
            Assert.IsFalse(UserValidation.ValidateEmail(null));
        }

        [TestMethod]
        public void TestValidateUsername()
        {
            Assert.IsTrue(UserValidation.ValidateUsername("JSmith"));
            Assert.IsFalse(UserValidation.ValidateUsername("12345678901234567890"));
            Assert.IsFalse(UserValidation.ValidateUsername(null));
        }

        [TestMethod]
        public void TestValidatePassword()
        {
            Assert.IsTrue(UserValidation.ValidatePassword("JSmith"));
            Assert.IsFalse(UserValidation.ValidatePassword("12345678901234567890"));
            Assert.IsFalse(UserValidation.ValidatePassword(null));
        }
    }
}
