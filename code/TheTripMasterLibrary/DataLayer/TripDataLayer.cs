using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using TheTripMasterLibrary.Model;

namespace TheTripMasterLibrary.DataLayer
{
    public class TripDataLayer
    {
        private const string ConnString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=TheTripMasterDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static void AddTrip(Trip trip)
        {
            int userId = ActiveUser.User.UserId;

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

        public static void UpdateTrip(string name, DateTime startDateTime, DateTime endDateTime)
        {
            int userId = ActiveUser.User.UserId;

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
    }
}
