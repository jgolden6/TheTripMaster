using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TheTripMasterWeb.Models;

namespace TheTripMasterWeb.DataLayer
{
    public class TripDataLayer
    {
        private const string ConnString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=TheTripMasterDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static void AddTrip(Trip trip)
        {
            int userId = UserDataLayer.GetUserId(ActiveUser.User);
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO TheTripMasterDatabase.dbo.[Trip] (userId, tripName, startDate, endDate) " +
                                  "VALUES (@userId, @tripName, @startDate, @endDate)";

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@tripName", trip.Name);
                cmd.Parameters.AddWithValue("@startDate", trip.StartDate);
                cmd.Parameters.AddWithValue("@endDate", trip.EndDate);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
