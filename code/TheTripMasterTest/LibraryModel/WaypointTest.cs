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
                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MaxValue,
            };

            waypoint.Id = 2;
            waypoint.TripId = 2;
            waypoint.WaypointName = "Waypoint2";
            waypoint.TripName = "Trip2";
            waypoint.StartDate = DateTime.MinValue;
            waypoint.EndDate = DateTime.MinValue;

            Assert.AreEqual(2, waypoint.Id);
            Assert.AreEqual(2, waypoint.TripId);
            Assert.AreEqual("Waypoint2", waypoint.WaypointName);
            Assert.AreEqual("Trip2", waypoint.TripName);
            Assert.AreEqual(DateTime.MinValue, waypoint.StartDate);
            Assert.AreEqual(DateTime.MinValue, waypoint.EndDate);
        }
    }
}
