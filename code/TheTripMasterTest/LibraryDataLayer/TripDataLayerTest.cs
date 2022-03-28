using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryDataLayer
{
    [TestClass]
    public class TripDataLayerTest
    {
        [TestMethod]
        public void TestAddTrip()
        {
            ActiveUser.User = new User {UserId = 1};
            TripDataLayer dataLayer = new TripDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Trip newTrip = new Trip
            {
                Name = "Vacation",
                StartDate = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0)),
                EndDate = DateTime.Now.Add(new TimeSpan(3, 0, 0, 0))
            };

            dataLayer.AddTrip(newTrip);
            Trip trip = dataLayer.GetSelectedTrip("Vacation");
            this.RemoveTestTrip();

            Assert.IsNotNull(trip.Name);
        }

        [TestMethod]
        public void TestGetAllTrips()
        {
            TripDataLayer dataLayer = new TripDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            List<Trip> trips = dataLayer.GetAllTripsOfUser(1005);

            Assert.AreEqual(4, trips.Count);
            Assert.IsNotNull(trips[0].Name);
        }

        [TestMethod]
        public void TestUpdateTrip()
        {
            ActiveUser.User = new User { UserId = 1005 };
            TripDataLayer dataLayer = new TripDataLayer();
            dataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            dataLayer.UpdateTrip("Paris", DateTime.Now.Add(new TimeSpan(500, 0, 0, 0)), DateTime.Now.Add(new TimeSpan(501, 0, 0, 0)));
            Trip trip = dataLayer.GetSelectedTrip("Paris");
            dataLayer.UpdateTrip("Paris", DateTime.Parse("10/1/2022 12:00:00 AM"), DateTime.Parse("11/1/2022 12:00:00 AM"));

            Assert.AreNotEqual(DateTime.Parse("5/1/2022 12:00:00 AM"), trip.StartDate);
        }

        private void RemoveTestTrip()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM [Trip] WHERE userId = @userId AND tripName = @tripName";

                cmd.Parameters.AddWithValue("@userId", 1);
                cmd.Parameters.AddWithValue("@tripName", "Vacation");

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
