using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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

        public static List<Trip> GetAllTripsOfUser(int userId)
        {
            string queryString =
                "SELECT * FROM TheTripMasterDatabase.dbo.[Trip] WHERE userId = @userId";


            List<Trip> trips = new List<Trip>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);

                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Trip trip = new Trip{ Name = reader["tripName"].ToString(), 
                                              StartDate = (DateTime)reader["startDate"],
                                              EndDate = (DateTime)reader["endDate"]
                        };
                        trips.Add(trip);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return trips;
        }

        public static void UpdateTrip(string name, DateTime startDateTime, DateTime endDateTime)
        {
            int userId = UserDataLayer.GetUserId(ActiveUser.User);
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE TheTripMasterDatabase.dbo.[Trip] SET startDate = @startDate, endDate = @endDate WHERE tripName = @tripName AND userId = @userId";


                cmd.Parameters.AddWithValue("@startDate", startDateTime);
                cmd.Parameters.AddWithValue("@endDate", endDateTime);
                cmd.Parameters.AddWithValue("@tripName", name);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static List<Waypoint> GetTripWaypoints(string tripName)
        {
            int userId = UserDataLayer.GetUserId(ActiveUser.User);
            int tripId = TripDataLayer.GetTripId(userId, tripName);

            string queryString =
                "SELECT * FROM TheTripMasterDatabase.dbo.[Waypoint] WHERE tripId = @tripId";


            List<Waypoint> waypoints = new List<Waypoint>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);
                
                cmd.Parameters.AddWithValue("@tripId", tripId);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Waypoint waypoint = new Waypoint
                        {
                            WaypointName = reader["waypointName"].ToString(),
                            StartDate = (DateTime)reader["startDate"],
                            EndDate = (DateTime)reader["endDate"]
                        };
                        waypoints.Add(waypoint);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return waypoints;
        }

        public static int GetTripId(int userId, string tripName)
        {
            Debug.WriteLine(tripName);
            int tripId = 0;
            string queryString =
                "SELECT tripId FROM TheTripMasterDatabase.dbo.[Trip] WHERE userId = @userId AND tripName = @tripName";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@tripName", tripName);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        tripId = reader.GetInt32(0);
                    }
                }
                finally
                {
                    reader.Close();

                }
            }

            return tripId;
        }

        public static void AddWaypoint(Waypoint waypoint, string tripName)
        {
            int userId = UserDataLayer.GetUserId(ActiveUser.User);
            int tripId = GetTripId(userId, tripName);
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO TheTripMasterDatabase.dbo.[Waypoint] (tripId, waypointName, startDate, endDate)" +
                                  "VALUES (@tripId, @waypointName, @startDate, @endDate)";

                cmd.Parameters.AddWithValue("@tripId", tripId);
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
