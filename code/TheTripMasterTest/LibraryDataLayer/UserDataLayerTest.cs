using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterTest.LibraryDataLayer
{
    [TestClass]
    public class UserDataLayerTest
    {
        [TestMethod]
        public void TestValidAuthenticate()
        {
            UserDataLayer userDataLayer = new UserDataLayer();
            userDataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            
            User user = userDataLayer.Authenticate("JohnD", "password");

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestInvalidAuthenticate()
        {
            UserDataLayer userDataLayer = new UserDataLayer();
            userDataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            User user = userDataLayer.Authenticate("John", "password");

            Assert.IsNull(user);
        }

        [TestMethod]
        public void TestAddUser()
        {
            UserDataLayer userDataLayer = new UserDataLayer();
            userDataLayer.SetConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            User newUser = new User
            {
                FirstName = "Jane", 
                LastName = "Doe", 
                Email = "jane@gmail.com", 
                Username = "JaneD",
                Password = "password"
            };

            userDataLayer.AddUser(newUser);
            User user = userDataLayer.Authenticate("JaneD", "password");
            this.RemoveTestUser();

            Assert.IsNotNull(user);
        }

        private void RemoveTestUser()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM [User] WHERE username = @username";

                cmd.Parameters.AddWithValue("@username", "JaneD");

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
