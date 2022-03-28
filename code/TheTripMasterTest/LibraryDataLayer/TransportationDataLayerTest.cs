using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryDataLayer
{
    [TestClass]
    public class TransportationDataLayerTest
    {
        [TestMethod]
        public void TestGetTransportation()
        {
            TransportationDataLayer dataLayer = new TransportationDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Transportation transport = dataLayer.GetTransportation(2);

            Assert.AreEqual("Boat", transport.TransportationType.Trim());
        }

        [TestMethod]
        public void TestGetTripTransportations()
        {
            TransportationDataLayer dataLayer = new TransportationDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            List<Transportation> transports = dataLayer.GetTripTransportations(25);

            Assert.AreEqual(2, transports.Count);
            Assert.AreEqual("Boat", transports[0].TransportationType.Trim());
            Assert.AreEqual("Plane", transports[1].TransportationType.Trim());
        }

        [TestMethod]
        public void TestAddTransportation()
        {
            TransportationDataLayer dataLayer = new TransportationDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Transportation newTransport = new Transportation
            {
                TripId = 18,
                TripName = "Belgium",
                TransportationType = "Car",
                StartDate = DateTime.Parse("7/2/2022 12:00:00 AM"),
                EndDate = DateTime.Parse("7/3/2022 12:00:00 AM")
            };

            SelectedTrip.Trip = new Trip { TripId = 18 };
            dataLayer.AddTransportation(newTransport);
            Transportation transport = dataLayer.GetTripTransportations(18)[0];
            dataLayer.DeleteTransportation(transport.Id);

            Assert.AreEqual("Car", transport.TransportationType.Trim());
        }

        [TestMethod]
        public void TestDeleteTransportation()
        {
            TransportationDataLayer dataLayer = new TransportationDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Transportation newTransport = new Transportation
            {
                TripId = 18,
                TripName = "Boston",
                TransportationType = "Car",
                StartDate = DateTime.Parse("1/2/2024 12:00:00 AM"),
                EndDate = DateTime.Parse("1/3/2024 12:00:00 AM")
            };

            SelectedTrip.Trip = new Trip { TripId = 18 };
            dataLayer.AddTransportation(newTransport);
            Transportation transport = dataLayer.GetTripTransportations(18)[0];
            dataLayer.DeleteTransportation(transport.Id);
            List<Transportation> transports = dataLayer.GetTripTransportations(18);

            Assert.AreEqual(0, transports.Count);
        }
    }
}
