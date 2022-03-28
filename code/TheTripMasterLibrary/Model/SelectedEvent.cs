using System;
using System.Collections.Generic;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class SelectedEvent
    {
        public static Event Event { get; set; }

        public static void DeselectEvent()
        {
            Event = null;
        }
    }
}
