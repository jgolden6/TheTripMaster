using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class Trip
    {
        public int TripId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<Waypoint> Waypoints { get; set; }
    }
}
