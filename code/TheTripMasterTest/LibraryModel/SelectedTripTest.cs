using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class SelectedTripTest
    {
        [TestMethod]
        public void TestSelectedTrip()
        {
            Trip trip = new Trip {TripId = 1};
            SelectedTrip.Trip = trip;

            Assert.AreEqual(trip, SelectedTrip.Trip);

            SelectedTrip.DeselectTrip();

            Assert.IsNull(SelectedTrip.Trip);
        }
    }
}
