using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class TransportationTest
    {
        [TestMethod]
        public void TestTransportation()
        {
            Transportation transport = new Transportation
            {
                Id = 1,
                TripId = 1,
                TripName = "Trip1",
                TransportationType = TransportationType.Car.ToString(),
                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MaxValue,
            };

            Assert.AreEqual(1, transport.Id);
            Assert.AreEqual(1, transport.TripId);
            Assert.AreEqual("Trip1", transport.TripName);
            Assert.AreEqual("Car", transport.TransportationType);
            Assert.AreEqual("Car", transport.ToString());
            Assert.AreEqual(DateTime.MaxValue, transport.StartDate);
            Assert.AreEqual(DateTime.MaxValue, transport.EndDate);
        }
    }
}
