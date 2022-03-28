using System;
using System.Collections.Generic;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class SelectedLodging
    {
        public static Lodging Lodging { get; set; }

        public static void DeselectLodging()
        {
            Lodging = null;
        }
    }
}
