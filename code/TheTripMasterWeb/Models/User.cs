using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTripMasterWeb.Models
{
    public class User
    {
        protected string username;

        protected string password;

        protected string firstName;

        public string GetUsername()
        {
            return this.username;
        }

        public string GetPassword()
        {
            return this.password;
        }

        public string GetFirstname()
        {
            return this.firstName;
        }
    }
}
