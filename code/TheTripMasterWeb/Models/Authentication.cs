using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TheTripMasterWeb.Models
{
    public class Authentication
    {
        class DbUser : User
        {
            public DbUser(string username, string password, string firstName)
            {
                this.username = username;
                this.password = password;
                this.firstName = firstName;
            }
        }

        public static User Authenticate(string username, string password)
        {
            User authenticatedUser = null;

            string queryString =
                "SELECT firstName FROM TheTripMasterDatabase.dbo.[User] WHERE username = @username AND password = @password";
            string connString = 
                "Data Source=(localdb)\\ProjectsV13;Initial Catalog=TheTripMasterDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection conn = new SqlConnection(connString))
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
    }
}
