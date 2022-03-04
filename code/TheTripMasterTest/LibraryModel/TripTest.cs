using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class TripTest
    {
        [TestMethod]
        public void TestTrip()
        {
            Waypoint waypoint = new Waypoint();
            List<Waypoint> waypoints = new List<Waypoint>();
            waypoints.Add(waypoint);

            Trip trip = new Trip
            {
                TripId = 1,
                UserId = 1,
                Name = "Trip1",
                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MaxValue,
                Waypoints = new List<Waypoint>()
            };

            trip.TripId = 2;
            trip.UserId = 2;
            trip.Name = "trip2";
            trip.StartDate = DateTime.MinValue;
            trip.EndDate = DateTime.MinValue;
            trip.Waypoints = waypoints;

            Assert.AreEqual(2, trip.TripId);
            Assert.AreEqual(2, trip.UserId);
            Assert.AreEqual("trip2", trip.Name);
            Assert.AreEqual(DateTime.MinValue, trip.StartDate);
            Assert.AreEqual(DateTime.MinValue, trip.EndDate);
            Assert.AreEqual(waypoints, trip.Waypoints);
        }
    }
}
