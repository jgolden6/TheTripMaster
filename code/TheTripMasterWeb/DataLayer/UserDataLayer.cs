using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TheTripMasterWeb.Models
{
    public class UserDataLayer
    {
        private const string ConnString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=TheTripMasterDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        class DbUser : User
        {
            public DbUser(string username, string password, string firstName)
            {
                this.Username = username;
                this.Password = password;
                this.FirstName = firstName;
            }
        }

        public static User Authenticate(string username, string password)
        {
            User authenticatedUser = null;

            string queryString =
                "SELECT firstName FROM TheTripMasterDatabase.dbo.[User] WHERE username = @username AND password = @password";


            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        authenticatedUser = new DbUser(username, password, reader["firstName"].ToString());
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return authenticatedUser;
        }

        public static void AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO TheTripMasterDatabase.dbo.[User] (firstName, lastName, email, username, password) " +
                                  "VALUES (@firstName, @lastName, @email, @username, @password)";

                cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                cmd.Parameters.AddWithValue("@lastName", user.LastName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);

                conn.Open();

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
