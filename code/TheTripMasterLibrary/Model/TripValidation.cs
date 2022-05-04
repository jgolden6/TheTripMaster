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

        public static bool ValidateDateTimesAfterNow(DateTime startDateTime)
        {
            return startDateTime > DateTime.Now;
        }

        public static bool ValidateStartBeforeEnd(DateTime startDateTime, DateTime endDateTime)
        {
            return startDateTime < endDateTime;
        }
    }
}
