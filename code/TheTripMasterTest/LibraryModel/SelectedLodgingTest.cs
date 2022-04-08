using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class SelectedLodgingTest
    {
        [TestMethod]
        public void TestSelectedLodging()
        {
            Lodging lodging = new Lodging { LodgingId = 1 };
            SelectedLodging.Lodging = lodging;

            Assert.AreEqual(lodging, SelectedLodging.Lodging);

            SelectedLodging.DeselectLodging();

            Assert.IsNull(SelectedLodging.Lodging);
        }
    }
}
