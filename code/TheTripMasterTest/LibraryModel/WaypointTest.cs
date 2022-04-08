using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class WaypointTest
    {
        [TestMethod]
        public void TestWaypoint()
        {
            Waypoint waypoint = new Waypoint
            {
                Id = 1,
                TripId = 1,
                WaypointName = "Waypoint1",
                TripName = "Trip1",
                StreetAddress = "4299 Express Ln",
                City = "Sarasota",
                State = "Florida",
                ZipCode = "34238",
                Latitude = 35.000,
                Longitude = 36.000,
                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MaxValue,
            };

            Assert.AreEqual(1, waypoint.Id);
            Assert.AreEqual(1, waypoint.TripId);
            Assert.AreEqual("Waypoint1", waypoint.WaypointName);
            Assert.AreEqual("Trip1", waypoint.TripName);
            Assert.AreEqual("4299 Express Ln", waypoint.StreetAddress);
            Assert.AreEqual("Sarasota", waypoint.City);
            Assert.AreEqual("Florida", waypoint.State);
            Assert.AreEqual("34238", waypoint.ZipCode);
            Assert.AreEqual(35.000, waypoint.Latitude);
            Assert.AreEqual(36.000, waypoint.Longitude);
            Assert.AreEqual(DateTime.MaxValue, waypoint.StartDate);
            Assert.AreEqual(DateTime.MaxValue, waypoint.EndDate);
            Assert.AreEqual("Waypoint1", waypoint.ToString());
        }
    }
}
