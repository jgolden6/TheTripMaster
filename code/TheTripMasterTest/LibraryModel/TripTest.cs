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
            List<Lodging> lodgings = new List<Lodging>();
            waypoints.Add(waypoint);

            Trip trip = new Trip
            {
                TripId = 1,
                UserId = 1,
                Name = "Trip1",
                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MaxValue,
                Events = waypoints,
                Lodgings = lodgings
            };
            

            Assert.AreEqual(1, trip.TripId);
            Assert.AreEqual(1, trip.UserId);
            Assert.AreEqual("Trip1", trip.Name);
            Assert.AreEqual(DateTime.MaxValue, trip.StartDate);
            Assert.AreEqual(DateTime.MaxValue, trip.EndDate);
            Assert.AreEqual(waypoints, trip.Events);
            Assert.AreEqual(lodgings, trip.Lodgings);
        }
    }
}
