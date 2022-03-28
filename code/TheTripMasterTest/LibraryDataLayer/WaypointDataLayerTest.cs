using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryDataLayer
{
    [TestClass]
    public class WaypointDataLayerTest
    {
        [TestMethod]
        public void TestAddWaypoint()
        {
            WaypointDataLayer dataLayer = new WaypointDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Waypoint newWaypoint = new Waypoint
            {
                TripId = 18,
                TripName = "Belgium",
                WaypointName = "Tower",
                StartDate = DateTime.Parse("7/2/2022 12:00:00 AM"),
                EndDate = DateTime.Parse("7/3/2022 12:00:00 AM")
            };

            SelectedTrip.Trip = new Trip {TripId = 18};
            dataLayer.AddWaypoint(newWaypoint);
            Waypoint waypoint = dataLayer.GetTripWaypoints(18)[0];
            dataLayer.DeleteWaypoint(waypoint.Id);

            Assert.AreEqual("Tower", waypoint.WaypointName.Trim());
        }

        [TestMethod]
        public void TestGetTripWaypoints()
        {
            WaypointDataLayer dataLayer = new WaypointDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            List<Waypoint> waypoints = dataLayer.GetTripWaypoints(6);

            Assert.AreEqual(2, waypoints.Count);
        }

        [TestMethod]
        public void TestGetWaypoint()
        {
            WaypointDataLayer dataLayer = new WaypointDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Waypoint waypoint = dataLayer.GetWaypoint(10);

            Assert.AreEqual("Lake", waypoint.WaypointName.Trim());
        }

        [TestMethod]
        public void TestDeleteWaypoint()
        {
            WaypointDataLayer dataLayer = new WaypointDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Waypoint newWaypoint = new Waypoint
            {
                TripId = 25,
                TripName = "Boston",
                WaypointName = "River",
                StartDate = DateTime.Parse("1/2/2024 12:00:00 AM"),
                EndDate = DateTime.Parse("1/3/2024 12:00:00 AM")
            };

            SelectedTrip.Trip = new Trip { TripId = 25 };
            dataLayer.AddWaypoint(newWaypoint);
            Waypoint waypoint = dataLayer.GetTripWaypoints(25)[0];
            dataLayer.DeleteWaypoint(waypoint.Id);
            List<Waypoint> waypoints = dataLayer.GetTripWaypoints(25);

            Assert.AreEqual(0, waypoints.Count);
        }
    }
}
