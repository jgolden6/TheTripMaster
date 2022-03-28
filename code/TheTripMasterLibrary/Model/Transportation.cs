using System;
using System.Collections.Generic;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class Transportation : Event
    {
        public string TransportationType { get; set; }

        public override string ToString()
        {
            return this.TransportationType.ToString();
        }
    }
}
