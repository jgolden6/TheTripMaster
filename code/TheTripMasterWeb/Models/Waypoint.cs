using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTripMasterWeb.Models
{
    public class Waypoint
    {
        public int TripId { get; set; }

        public string WaypointName { get; set; }

        public string TripName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
