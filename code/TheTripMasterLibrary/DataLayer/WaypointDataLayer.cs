using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using TheTripMasterLibrary.Model;

namespace TheTripMasterLibrary.DataLayer
{
    public class WaypointDataLayer
    {
        private const string ConnString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=TheTripMasterDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /**
         * Takes a Waypoint object, inserts the Trip ID, Waypoint Name, Start Date, and End Date into the Waypoint table.
         */
        public static void AddWaypoint(Waypoint waypoint)
        {
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

        /**
         * Gets the Events associated with a given trip ID.
         *
         * Return: A list of Events.
         */
        public static List<Waypoint> GetTripWaypoints(int tripId)
        {
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
                            Id = (int)reader["waypointId"],
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

        /**
         * Gets a specific Waypoint given an ID.
         *
         * Return: The Waypoint with the given ID.
         */
        public static Waypoint GetWaypoint(int waypointId)
        {
            string queryString =
                "SELECT * FROM TheTripMasterDatabase.dbo.[Waypoint] WHERE waypointId = @waypointId";

            Waypoint waypoint = new Waypoint();

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);

                cmd.Parameters.AddWithValue("@waypointId", waypointId);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        waypoint.Id = (int)reader["waypointId"];
                        waypoint.TripId = (int)reader["tripId"];
                        waypoint.WaypointName = reader["waypointName"].ToString();
                        waypoint.StartDate = (DateTime)reader["startDate"];
                        waypoint.EndDate = (DateTime)reader["endDate"];
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return waypoint;
        }

        /**
         * Deletes the Waypoint with the given ID.
         */
        public static void DeleteWaypoint(int waypointId)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM TheTripMasterDatabase.dbo.[Waypoint] WHERE waypointId = @waypointId";

                cmd.Parameters.AddWithValue("@waypointId", waypointId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
