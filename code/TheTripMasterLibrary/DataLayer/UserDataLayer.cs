using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TheTripMasterLibrary.Model;

namespace TheTripMasterLibrary.DataLayer
{
    public class UserDataLayer
    {
        private const string ConnString =
            "Data Source=(localdb)\\ProjectsV13;Initial Catalog=TheTripMasterDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /**
         * Private constructor for a local User object.
         */
        class DbUser : User
        {
            public DbUser(int id, string username, string password, string firstName, string lastName, string email)
            {
                this.UserId = id;
                this.Username = username;
                this.Password = password;
                this.FirstName = firstName;
                this.LastName = lastName;
                this.Email = email;
            }
        }

        /**
         * Verifies the given credentials, checks the username and password against the database.
         *
         * Return: The verified user, if the credentials are correct.
         */
        public static User Authenticate(string username, string password)
        {
            User authenticatedUser = null;

            string queryString =
                "SELECT * FROM TheTripMasterDatabase.dbo.[User] WHERE username = @username AND password = @password";


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
                        authenticatedUser = new DbUser((int) reader["userId"], username, password,
                            reader["firstName"].ToString(), reader["lastName"].ToString(), reader["email"].ToString());
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return authenticatedUser;
        }

        /**
         * Takes a user objects and inserts the First Name, Last Name, Email, Username, and Password into the User table.
         */
        public static void AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO TheTripMasterDatabase.dbo.[User] (firstName, lastName, email, username, password) " +
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
