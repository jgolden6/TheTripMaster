using System;
using System.Collections.Generic;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class SelectedTrip
    {
        public static Trip Trip { get; set; }

        public static void DeselectTrip()
        {
            Trip = null;
        }
    }
}
