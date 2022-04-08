using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class CoordinatesTest
    {
        [TestMethod]
        public void TestCoordinates()
        {
            Coordinates coords = new Coordinates
            {
                Latitude = 35.000,
                Longitude = 35.000
            };

            Assert.AreEqual(35.000, coords.Latitude);
            Assert.AreEqual(35.000, coords.Longitude);
        }
    }
}
