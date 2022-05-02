using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TheTripMasterLibrary.Model;

namespace TheTripMasterLibrary.DataLayer
{
    public class TransportationDataLayer : DataLayer
    {
        /**
         * Gets a specific Transportation given an ID.
         *
         * Return: The Transportation with the given ID.
         */
        public Transportation GetTransportation(int transportationId)
        {
            string queryString =
                "SELECT * FROM [Transportation] WHERE transportationId = @transportationId";

            Transportation transportation = new Transportation();

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);

                cmd.Parameters.AddWithValue("@transportationId", transportationId);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        transportation.Id = (int)reader["transportationId"];
                        transportation.TripId = (int)reader["tripId"];
                        transportation.TransportationType = reader["TransportationType"].ToString().Trim();
                        transportation.StartDate = (DateTime)reader["startDate"];
                        transportation.EndDate = (DateTime)reader["endDate"];
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return transportation;
        }

        /**
         * Gets the Events associated with a given trip ID.
         *
         * Return: A list of Events.
         */
        public List<Transportation> GetTripTransportations(int tripId)
        {
            string queryString =
                "SELECT * FROM [Transportation] WHERE tripId = @tripId";


            List<Transportation> transportations = new List<Transportation>();
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
                        Transportation transportation = new Transportation
                        {
                            Id = (int)reader["transportationId"],
                            TransportationType = reader["transportationType"].ToString().Trim(),
                            StartDate = (DateTime)reader["startDate"],
                            EndDate = (DateTime)reader["endDate"]
                        };
                        transportations.Add(transportation);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return transportations;
        }

        /**
         * Takes a Transportation object, inserts the Trip ID, Transportation Type, Start Date, and End Date into the Transportation table.
         */
        public int AddTransportation(Transportation transportation)
        {
            int index = 0;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO [Transportation] (tripId, transportationType, startDate, endDate) output INSERTED.transportationId " +
                                  "VALUES (@tripId, @transportationType, @startDate, @endDate)";

                cmd.Parameters.AddWithValue("@tripId", SelectedTrip.Trip.TripId);
                cmd.Parameters.AddWithValue("@transportationType", transportation.TransportationType);
                cmd.Parameters.AddWithValue("@startDate", transportation.StartDate);
                cmd.Parameters.AddWithValue("@endDate", transportation.EndDate);

                conn.Open();
                index = (int)cmd.ExecuteScalar();
                conn.Close();
            }

            return index;
        }

        /**
         * Edits a transportation on the database.
         */
        public void EditTransportation(Transportation transportation)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    "UPDATE [Transportation] SET transportationType = @transportationType, startDate = @startDate, endDate = @endDate " +
                    "WHERE transportationId = @transportationId ";

                cmd.Parameters.AddWithValue("@transportationId", transportation.Id);
                cmd.Parameters.AddWithValue("@transportationType", transportation.TransportationType);
                cmd.Parameters.AddWithValue("@startDate", transportation.StartDate);
                cmd.Parameters.AddWithValue("@endDate", transportation.EndDate);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        /**
        * Deletes the Transportation with the given ID.
        */
        public void DeleteTransportation(int transportationId)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM [Transportation] WHERE transportationId = @transportationId";

                cmd.Parameters.AddWithValue("@transportationId", transportationId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
