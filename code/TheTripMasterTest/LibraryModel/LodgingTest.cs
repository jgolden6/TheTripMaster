using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class LodgingTest
    {
        [TestMethod]
        public void TestLodging()
        {
            Lodging lodging = new Lodging
            {
                LodgingId = 1,
                TripName = "Trip1",
                StreetAddress = "4299 Express Ln",
                City = "Sarasota",
                State = "Florida",
                ZipCode = "34238",
                Latitude = 34.009,
                Longitude = 35.000, 
                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MaxValue,
            };

            Assert.AreEqual(1, lodging.LodgingId);
            Assert.AreEqual("Trip1", lodging.TripName);
            Assert.AreEqual("4299 Express Ln", lodging.StreetAddress);
            Assert.AreEqual("Sarasota", lodging.City);
            Assert.AreEqual("Florida", lodging.State);
            Assert.AreEqual("34238", lodging.ZipCode);
            Assert.AreEqual(34.009, lodging.Latitude);
            Assert.AreEqual(35.000, lodging.Longitude);
            Assert.AreEqual(DateTime.MaxValue, lodging.StartDate);
            Assert.AreEqual(DateTime.MaxValue, lodging.EndDate);
        }
    }
}
