using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterDesktop.View
{
    public partial class Overview : UserControl
    {
        public event Action AddTripButtonClick;
        public event Action AccountButtonClick;
        public event Action LogoutButtonClick;

        public Overview()
        {
            InitializeComponent();
        }

        /**
         * Navigates to the Add Trip page.
         */
        private void addTripButton_Click(object sender, EventArgs e)
        {
            AddTripButtonClick?.Invoke();
        }

        /**
         * Navigates to the Account page.
         */
        private void accountButton_Click(object sender, EventArgs e)
        {
            AccountButtonClick?.Invoke();
        }

        /**
         * Sets the active user to null and navigates to the Login page.
         */
        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.tripDataGridView.DataSource = null;
            ActiveUser.Logout();
            LogoutButtonClick?.Invoke();
        }

        /**
         * Refreshes the list of Trips for the current user.
         */
        private void refreshButton_Click(object sender, EventArgs e)
        {
            DataTable tripTable = new DataTable();
            tripTable.Columns.Add("Trip Name");
            tripTable.Columns.Add("Start Date");
            tripTable.Columns.Add("End Date");

            foreach (Trip trip in TripDataLayer.GetAllTripsOfUser(ActiveUser.User.UserId))
            {
                tripTable.Rows.Add(new object[] {trip.Name, trip.StartDate.ToShortDateString(), trip.EndDate.ToShortDateString()});
            }

            this.tripDataGridView.DataSource = tripTable;
        }

        /**
         * Navigates to the Trip details page for the clicked Trip.
         */
        private void tripDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Take to trip details
        }
    }
}
