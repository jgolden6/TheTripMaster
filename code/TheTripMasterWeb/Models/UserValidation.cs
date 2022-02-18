using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheTripMasterWeb.Models
{
    public static class UserValidation
    {
        public static bool ValidateName(string name)
        {
            name ??= "";
            return Regex.Match(name, "^[a-zA-Z]{1,16}$").Success;
        }

        public static bool ValidateEmail(string email)
        {
            email ??= "";
            return Regex.Match(email, "^[\\w]+@[\\w]+.[\\w.]+$").Success && email.Length <= 64;
        }

        public static bool ValidateUsername(string username)
        {
            username ??= "";
            return Regex.Match(username, "^\\w{1,16}$").Success;
        }

        public static bool ValidatePassword(string password)
        {
            password ??= "";
            return Regex.Match(password, "^.{1,16}$").Success;
        }
    }
}
