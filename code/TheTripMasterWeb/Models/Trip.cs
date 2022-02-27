using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTripMasterWeb.Models
{
    public class Trip
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<Waypoint> Waypoints { get; set; } 
    }
}
