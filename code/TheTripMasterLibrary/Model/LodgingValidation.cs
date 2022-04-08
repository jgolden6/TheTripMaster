using System;
using System.Collections.Generic;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class LodgingValidation
    {

        public static bool ValidateDescription(string description)
        {
            if (description == null)
            {
                return true;
            }

            return description.Length <= 256;
        }

        public static bool ValidateDateTimes(DateTime startDateTime, DateTime endDateTime)
        {
            return startDateTime > DateTime.Now && startDateTime < endDateTime;
        }
    }
}
