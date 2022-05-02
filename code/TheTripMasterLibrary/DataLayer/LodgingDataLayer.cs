using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TheTripMasterLibrary.Model;

namespace TheTripMasterLibrary.DataLayer
{
    public class LodgingDataLayer : DataLayer
    {
        /**
         * Takes a Lodging object, inserts the Trip ID, Address fields, Start Date, and End Date into the Lodging table.
         */
        public int AddLodging(Lodging lodging)
        {
            int index = 0;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO [Lodging] (tripId, streetAddress, city, state, zipCode, description, startDate, endDate) output INSERTED.lodgingId " +
                                  "VALUES (@tripId, @streetAddress, @city, @state, @zipCode, @description, @startDate, @endDate)";

                cmd.Parameters.AddWithValue("@tripId", SelectedTrip.Trip.TripId);
                cmd.Parameters.AddWithValue("@streetAddress", lodging.StreetAddress);
                cmd.Parameters.AddWithValue("@city", lodging.City);
                cmd.Parameters.AddWithValue("@state", lodging.State);
                cmd.Parameters.AddWithValue("@zipCode", lodging.ZipCode);
                cmd.Parameters.AddWithValue("@description", lodging.Description);
                cmd.Parameters.AddWithValue("@startDate", lodging.StartDate);
                cmd.Parameters.AddWithValue("@endDate", lodging.EndDate);

                conn.Open();
                index = (int)cmd.ExecuteScalar();
                conn.Close();
            }

            return index;
        }

        /**
         * Takes a Lodging object, and edits it database entry.
         */
        public void EditLodging(Lodging lodging)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE [Lodging] SET streetAddress = @streetAddress, city = @city, state = @state, " +
                                  "zipCode = @zipCode, description = @description, startDate = @startDate, endDate = @endDate WHERE lodgingId = @lodgingId ";

                cmd.Parameters.AddWithValue("@lodgingId", lodging.LodgingId);
                cmd.Parameters.AddWithValue("@streetAddress", lodging.StreetAddress);
                cmd.Parameters.AddWithValue("@city", lodging.City);
                cmd.Parameters.AddWithValue("@state", lodging.State);
                cmd.Parameters.AddWithValue("@zipCode", lodging.ZipCode);
                cmd.Parameters.AddWithValue("@description", lodging.Description);
                cmd.Parameters.AddWithValue("@startDate", lodging.StartDate);
                cmd.Parameters.AddWithValue("@endDate", lodging.EndDate);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        /**
         * Gets the Lodgings associated with a given trip ID.
         *
         * Return: A list of Lodgings.
         */
        public List<Lodging> GetTripLodgings(int tripId)
        {
            string queryString =
                "SELECT * FROM [Lodging] WHERE tripId = @tripId";


            List<Lodging> lodgings = new List<Lodging>();
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
                        Lodging lodging = new Lodging
                        {
                            LodgingId = (int)reader["lodgingId"],
                            StreetAddress = reader["streetAddress"].ToString().Trim(),
                            City = reader["city"].ToString().Trim(),
                            State = reader["state"].ToString().Trim(),
                            ZipCode = reader["zipCode"].ToString().Trim(),
                            Description = reader["description"].ToString().Trim() ?? "",
                            StartDate = (DateTime)reader["startDate"],
                            EndDate = (DateTime)reader["endDate"]
                        };
                        lodgings.Add(lodging);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return lodgings;
        }

        /**
         * Gets a specific Lodging given an ID.
         *
         * Return: The Lodging with the given ID.
         */
        public Lodging GetLodging(int lodgingId)
        {
            string queryString =
                "SELECT * FROM [Lodging] WHERE lodgingId = @lodgingId";

            Lodging lodging = new Lodging();

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);

                cmd.Parameters.AddWithValue("@lodgingId", lodgingId);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        lodging.LodgingId = (int)reader["lodgingId"];
                        lodging.StreetAddress = reader["streetAddress"].ToString();
                        lodging.City = reader["city"].ToString();
                        lodging.State = reader["state"].ToString();
                        lodging.ZipCode = reader["zipCode"].ToString();
                        lodging.Description = reader["description"].ToString();
                        lodging.StartDate = (DateTime)reader["startDate"];
                        lodging.EndDate = (DateTime)reader["endDate"];
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return lodging;
        }

        /**
         * Deletes the Lodging with the given ID.
         */
        public void DeleteLodging(int lodgingId)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM [Lodging] WHERE lodgingId = @lodgingId";

                cmd.Parameters.AddWithValue("@lodgingId", lodgingId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
