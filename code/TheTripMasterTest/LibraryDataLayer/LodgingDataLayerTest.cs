using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryDataLayer
{
    [TestClass]
    public class LodgingDataLayerTest
    {
        [TestMethod]
        public void TestAddLodging()
        {
            LodgingDataLayer dataLayer = new LodgingDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Lodging newLodging = new Lodging
            {
                StreetAddress = "80 Street St",
                City = "City",
                State = "GA",
                ZipCode = "30110",
                Description = "",
                TripName = "Belgium",
                StartDate = DateTime.Parse("7/2/2022 12:00:00 AM"),
                EndDate = DateTime.Parse("7/3/2022 12:00:00 AM")
            };

            SelectedTrip.Trip = new Trip { TripId = 18 };
            dataLayer.AddLodging(newLodging);
            Lodging lodging = dataLayer.GetTripLodgings(18)[0];
            dataLayer.DeleteLodging(lodging.LodgingId);

            Assert.AreEqual("80 Street St", lodging.StreetAddress.Trim());
            Assert.AreEqual("City", lodging.City.Trim());
            Assert.AreEqual("GA", lodging.State.Trim());
            Assert.AreEqual("30110", lodging.ZipCode.Trim());
            Assert.AreEqual(DateTime.Parse("7/2/2022 12:00:00 AM"), lodging.StartDate);
            Assert.AreEqual(DateTime.Parse("7/3/2022 12:00:00 AM"), lodging.EndDate);
        }

        [TestMethod]
        public void TestGetTripLodgings()
        {
            LodgingDataLayer dataLayer = new LodgingDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            List<Lodging> lodgings = dataLayer.GetTripLodgings(25);

            Assert.AreEqual(2, lodgings.Count);
            Assert.AreEqual("60 Street St", lodgings[0].StreetAddress.Trim());
            Assert.AreEqual("70 Road Rd", lodgings[1].StreetAddress.Trim());
        }

        [TestMethod]
        public void TestGetLodging()
        {
            LodgingDataLayer dataLayer = new LodgingDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Lodging lodging = dataLayer.GetLodging(2);

            Assert.AreEqual("60 Street St", lodging.StreetAddress.Trim());
        }

        [TestMethod]
        public void TestDeleteLodging()
        {
            LodgingDataLayer dataLayer = new LodgingDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Lodging newLodging = new Lodging
            {
                StreetAddress = "90 Road St",
                City = "City",
                State = "GA",
                ZipCode = "30110",
                Description = "",
                TripName = "Belgium",
                StartDate = DateTime.Parse("7/5/2022 12:00:00 AM"),
                EndDate = DateTime.Parse("7/6/2022 12:00:00 AM")
            };

            SelectedTrip.Trip = new Trip { TripId = 6 };
            dataLayer.AddLodging(newLodging);
            Lodging lodging = dataLayer.GetTripLodgings(6)[0];
            dataLayer.DeleteLodging(lodging.LodgingId);
            List<Lodging> transports = dataLayer.GetTripLodgings(6);

            Assert.AreEqual(0, transports.Count);
        }
    }
}
