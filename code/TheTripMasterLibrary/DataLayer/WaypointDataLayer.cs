using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TheTripMasterLibrary.Model;

namespace TheTripMasterLibrary.DataLayer
{
    public class WaypointDataLayer
    {
        private const string ConnString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=TheTripMasterDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static void AddWaypoint(Waypoint waypoint)
        {
            int userId = ActiveUser.User.UserId;

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO TheTripMasterDatabase.dbo.[Waypoint] (tripId, waypointName, startDate, endDate) " +
                                  "VALUES (@tripId, @waypointName, @startDate, @endDate)";

                cmd.Parameters.AddWithValue("@tripId", SelectedTrip.Trip.TripId);
                cmd.Parameters.AddWithValue("@waypointName", waypoint.WaypointName);
                cmd.Parameters.AddWithValue("@startDate", waypoint.StartDate);
                cmd.Parameters.AddWithValue("@endDate", waypoint.EndDate);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
