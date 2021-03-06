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
        TripDataLayer dataLayer = new TripDataLayer();

        public event Action AddTripButtonClick;
        public event Action LogoutButtonClick;
        public event Action DataCellClick;

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
         * Sets the active user to null and navigates to the Login page.
         */
        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.tripDataGridView.DataSource = null;
            ActiveUser.Logout();
            LogoutButtonClick?.Invoke();
        }

        /**
         * Navigates to the Trip details page for the clicked Trip.
         */
        private void tripDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Trip trip = this.dataLayer.GetSelectedTrip(this.tripDataGridView.CurrentRow.Cells[0].Value.ToString());

            SelectedTrip.Trip = trip;

            DataCellClick?.Invoke();
        }

        /**
         * Populates the trip grid view with relevant data from the user's trips.
         */
        public void LoadTripDataIntoGridView()
        {
            DataTable tripTable = new DataTable();
            tripTable.Columns.Add("Trip Name");
            tripTable.Columns.Add("Start Date", typeof(DateTime));
            tripTable.Columns.Add("End Date", typeof(DateTime));

            foreach (Trip trip in this.dataLayer.GetAllTripsOfUser(ActiveUser.User.UserId))
            {
                tripTable.Rows.Add(new object[] { trip.Name.Trim(), trip.StartDate.ToShortDateString(), trip.EndDate.ToShortDateString() });
            }

            this.tripDataGridView.DataSource = tripTable;
            this.tripDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.tripDataGridView.RowHeadersVisible = false;
            this.tripDataGridView.Sort(this.tripDataGridView.Columns[1], ListSortDirection.Ascending);
        }
    }
}
