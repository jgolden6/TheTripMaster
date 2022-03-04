using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class Waypoint
    {
        public int WaypointId { get; set; }

        public int TripId { get; set; }

        public string WaypointName { get; set; }

        public string TripName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
