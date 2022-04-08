using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryModel
{
    [TestClass]
    public class SelectedEventTest
    {
        [TestMethod]
        public void TestSelectedEvent()
        {
            Event newEvent = new Event { Id = 1 };
            SelectedEvent.Event = newEvent;

            Assert.AreEqual(newEvent, SelectedEvent.Event);

            SelectedEvent.DeselectEvent();

            Assert.IsNull(SelectedEvent.Event);
        }
    }
}
