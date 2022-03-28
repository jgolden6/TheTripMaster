﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class Waypoint : Event
    {
        public string WaypointName { get; set; }

        public override string ToString()
        {
            return this.WaypointName;
        }
    }
}
