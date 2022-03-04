using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class TripValidationTest
    {
        [TestMethod]
        public void TestValidateName()
        {
            Assert.IsTrue(TripValidation.ValidateName("Trip"));
            Assert.IsFalse(TripValidation.ValidateName(null));
        }

        [TestMethod]
        public void TestValidateDateTimes()
        {
            Assert.IsTrue(TripValidation.ValidateDateTimes(DateTime.Now.AddDays(1), DateTime.MaxValue));
            Assert.IsFalse(TripValidation.ValidateDateTimes(DateTime.MinValue, DateTime.MaxValue));
            Assert.IsFalse(TripValidation.ValidateDateTimes(DateTime.MaxValue, DateTime.MinValue));
        }
    }
}
