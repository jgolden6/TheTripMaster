using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class Waypoint : Event
    {
        public string WaypointName { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public override string ToString()
        {
            return this.WaypointName;
        }
    }
}
