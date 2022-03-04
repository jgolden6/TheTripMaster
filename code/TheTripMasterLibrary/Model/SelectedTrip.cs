using System;
using System.Collections.Generic;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    class SelectedTrip
    {
        public static Trip trip { get; set; }

        public static void DeselectTrip()
        {
            trip = null;
        }
    }
}
