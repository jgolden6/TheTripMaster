using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTripMasterWeb.Models
{
    public static class ActiveUser
    {
        public static User User { get; set; }

        public static bool HasActiveUser() => (ActiveUser.User != null);
    }
}
