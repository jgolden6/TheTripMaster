using System;
using System.Collections.Generic;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class TripValidation
    {
        public static bool ValidateName(string name)
        {
            name ??= "";
            return name.Length > 0 && name.Length <= 128;
        }

        public static bool ValidateDateTimes(DateTime startDateTime, DateTime endDateTime)
        {
            return startDateTime > DateTime.Now && startDateTime < endDateTime;
        }
    }
}
