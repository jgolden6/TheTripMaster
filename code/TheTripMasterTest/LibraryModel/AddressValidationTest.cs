using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class AddressValidationTest
    {
        [TestMethod]
        public void TestValidateAddressField()
        {
            bool result = AddressValidation.ValidateAddressField("65 Road Rd");

            Assert.AreEqual(true, result);

            result = AddressValidation.ValidateAddressField("");

            Assert.AreEqual(false, result);

            result = AddressValidation.ValidateAddressField(null);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestValidateZipCode()
        {
            bool result = AddressValidation.ValidateZipCode("30110");

            Assert.AreEqual(true, result);

            result = AddressValidation.ValidateZipCode("301110");

            Assert.AreEqual(false, result);

            result = AddressValidation.ValidateZipCode("3011b");

            Assert.AreEqual(false, result);

            result = AddressValidation.ValidateZipCode(null);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestValidateAddress()
        {
            bool result = AddressValidation.ValidateAddress("4299 Express Ln", "Sarasota", "Florida", "34238");

            Assert.AreEqual(true, result);

            result = AddressValidation.ValidateAddress("trash value", "trash", "trash", "342388");

            Assert.AreEqual(false, result);
        }
    }
}
