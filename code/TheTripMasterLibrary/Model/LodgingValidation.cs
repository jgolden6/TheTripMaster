using System;
using System.Collections.Generic;
using System.Text;

namespace TheTripMasterLibrary.Model
{
    public class LodgingValidation
    {
        public static bool ValidateAddressField(string field)
        {
            field ??= "";
            return field.Length > 0 && field.Length <= 128;
        }

        public static bool ValidateZipCode(string zip)
        {
            zip ??= "";
            int n;
            return zip.Length > 0 && zip.Length <= 5 && int.TryParse(zip, out n);
        }

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
