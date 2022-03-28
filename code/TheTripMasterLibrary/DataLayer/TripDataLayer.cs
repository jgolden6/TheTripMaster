using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using TheTripMasterLibrary.Model;

namespace TheTripMasterLibrary.DataLayer
{
    public class TripDataLayer : DataLayer
    {
        /**
         * Takes a Trip object and inserts the User ID, Trip Name, Start Date, and End Date into the Trip table.
         */
        public void AddTrip(Trip trip)
        {
            int userId = ActiveUser.User.UserId;

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO [Trip] (userId, tripName, startDate, endDate) " +
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

        /**
         * Gets all the trips associated with the given User ID.
         *
         * Return: A list of trips belonging to the User.
         */
        public List<Trip> GetAllTripsOfUser(int userId)
        {
            string queryString =
                "SELECT * FROM [Trip] WHERE userId = @userId";


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
                        Trip trip = new Trip
                        {
                            TripId = (int) reader["tripId"],
                            UserId = (int) reader["userId"],
                            Name = reader["tripName"].ToString(),
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

        /**
         * Takes a Name, Start Time, and End Time, and updates the Trip being edited.
         */
        public void UpdateTrip(string name, DateTime startDateTime, DateTime endDateTime)
        {
            int userId = ActiveUser.User.UserId;

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE [Trip] SET startDate = @startDate, endDate = @endDate WHERE tripName = @tripName AND userId = @userId";


                cmd.Parameters.AddWithValue("@startDate", startDateTime);
                cmd.Parameters.AddWithValue("@endDate", endDateTime);
                cmd.Parameters.AddWithValue("@tripName", name);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        /**
         * Gets the Trip with the given Name.
         *
         * Return: A Trip.
         */
        public Trip GetSelectedTrip(string tripName)
        {
            int userId = ActiveUser.User.UserId;

            string queryString =
                "SELECT * FROM [Trip] WHERE tripName = @tripName AND userId = @userId";

            Trip selectedTrip = new Trip();

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);

                cmd.Parameters.AddWithValue("@tripName", tripName);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        selectedTrip.TripId = (int) reader["tripId"];
                        selectedTrip.UserId = (int) reader["userId"];
                        selectedTrip.Name = reader["tripName"].ToString();
                        selectedTrip.StartDate = (DateTime) reader["startDate"];
                        selectedTrip.EndDate = (DateTime) reader["endDate"];
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return selectedTrip;
        }
    }
}
