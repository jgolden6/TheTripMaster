using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using TheTripMasterLibrary.Model;

namespace TheTripMasterLibrary.DataLayer
{
    public class WaypointDataLayer : DataLayer
    {
        /**
         * Takes a Waypoint object, inserts the Trip ID, Waypoint Name, Start Date, and End Date into the Waypoint table.
         */
        public void AddWaypoint(Waypoint waypoint)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO [Waypoint] (tripId, waypointName, streetAddress, city, state, zipCode, startDate, endDate) " +
                                  "VALUES (@tripId, @waypointName, @streetAddress, @city, @state, @zipCode, @startDate, @endDate)";

                cmd.Parameters.AddWithValue("@tripId", SelectedTrip.Trip.TripId);
                cmd.Parameters.AddWithValue("@waypointName", waypoint.WaypointName);
                cmd.Parameters.AddWithValue("@streetAddress", waypoint.StreetAddress);
                cmd.Parameters.AddWithValue("@city", waypoint.City);
                cmd.Parameters.AddWithValue("@state", waypoint.State);
                cmd.Parameters.AddWithValue("@zipCode", waypoint.ZipCode);
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
        public List<Waypoint> GetTripWaypoints(int tripId)
        {
            string queryString =
                "SELECT * FROM [Waypoint] WHERE tripId = @tripId";


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
                            StreetAddress = reader["streetAddress"].ToString(),
                            City = reader["city"].ToString(),
                            State = reader["state"].ToString(),
                            ZipCode = reader["zipCode"].ToString(),
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
        public Waypoint GetWaypoint(int waypointId)
        {
            string queryString =
                "SELECT * FROM [Waypoint] WHERE waypointId = @waypointId";

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
                        waypoint.StreetAddress = reader["streetAddress"].ToString();
                        waypoint.City = reader["city"].ToString();
                        waypoint.State = reader["state"].ToString();
                        waypoint.ZipCode = reader["zipCode"].ToString();
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
        public void DeleteWaypoint(int waypointId)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM [Waypoint] WHERE waypointId = @waypointId";

                cmd.Parameters.AddWithValue("@waypointId", waypointId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void UpdateWaypoint(string name, string address, string city, string state, string zipcode, DateTime startDateTime, DateTime endDateTime)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE [Waypoint] SET waypointName = @name, streetAddress = @address, " +
                                  "city = @city, state = @state, zipCode = @zipcode, startDate = @startDate, endDate = @endDate " +
                                  "WHERE waypointId = @waypointId";

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@zipcode", zipcode);
                cmd.Parameters.AddWithValue("@startDate", startDateTime);
                cmd.Parameters.AddWithValue("@endDate", endDateTime);
                cmd.Parameters.AddWithValue("@waypointId", SelectedEvent.Event.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
