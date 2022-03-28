using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TheTripMasterLibrary.DataLayer
{
    public class DataLayer
    {
        public string ConnString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=TheTripMasterDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void SetConnection(string conn)
        {
            this.ConnString = conn;
        }
    }
}
