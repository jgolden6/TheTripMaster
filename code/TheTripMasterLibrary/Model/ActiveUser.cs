using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TheTripMasterLibrary.DataLayer;

namespace TheTripMasterLibrary.Model
{
    public class ActiveUser
    {
        public static User User { get; set; }

        public static string TripName { get; set; }

        public static bool HasActiveUser() => (ActiveUser.User != null);

        public static void Logout()
        {
            User = null;
        }
    }
}
