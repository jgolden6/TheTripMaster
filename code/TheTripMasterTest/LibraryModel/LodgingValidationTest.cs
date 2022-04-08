using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class LodgingValidationTest
    {
        [TestMethod]
        public void TestValidateDescription()
        {
            bool result = LodgingValidation.ValidateDescription("Description");
            Assert.AreEqual(true, result);

            result = LodgingValidation.ValidateDescription(null);
            Assert.AreEqual(true, result);

            result = LodgingValidation.ValidateDescription("TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777TooLong777");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestValidateDateTimes()
        {
            DateTime time1 = DateTime.MinValue;
            DateTime time2 = time1.AddDays(4);
            
            bool result = LodgingValidation.ValidateDateTimes(time1, time2);
            Assert.AreEqual(false, result);

            time1.AddDays(5);
            result = LodgingValidation.ValidateDateTimes(time1, time2);
            Assert.AreEqual(false, result);

            time1 = DateTime.Now.AddDays(1);
            time2 = time1.AddDays(2);
            result = LodgingValidation.ValidateDateTimes(time1, time2);
            Assert.AreEqual(true, result);
        }
    }
}
