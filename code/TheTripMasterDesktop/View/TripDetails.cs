using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterDesktop.View
{
    public partial class TripDetails : UserControl
    {
        public event Action UpdateButtonClick;
        public event Action AddWaypointButtonClick;
        public event Action CancelButtonClick;

        public TripDetails()
        {
            InitializeComponent();
        }

        /**
         * Updates the selected trip if the data is valid and navigates to the Overview page.
         */
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                TripDataLayer.UpdateTrip(this.tripNameTextBox.Text, this.startDatePicker.Value, this.endDatePicker.Value);
                UpdateButtonClick?.Invoke();
            }
        }

        /**
         * Navigates to the Add Waypoint page.
         */
        private void addWaypointButton_Click(object sender, EventArgs e)
        {
            AddWaypointButtonClick?.Invoke();
        }

        /**
         * Navigates to the Overview page.
         */
        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelButtonClick?.Invoke();
        }

        /**
        * Validates all the information in the input fields.
        */
        private bool ValidateData()
        {
            return (TripValidation.ValidateName(this.tripNameTextBox.Text) &&
                    TripValidation.ValidateDateTimes(this.startDatePicker.Value, this.endDatePicker.Value));
        }

        public void LoadTripDataIntoInputFields()
        {
            this.tripNameTextBox.Text = SelectedTrip.Trip.Name;
            this.startDatePicker.Value = SelectedTrip.Trip.StartDate;
            this.endDatePicker.Value = SelectedTrip.Trip.EndDate;
        }

        public void LoadWaypointDataIntoGridView()
        {
            DataTable tripTable = new DataTable();
            tripTable.Columns.Add("Name");
            tripTable.Columns.Add("Start Date");
            tripTable.Columns.Add("End Date");

            foreach (Waypoint waypoint in WaypointDataLayer.GetTripWaypoints(SelectedTrip.Trip.TripId))
            {
                tripTable.Rows.Add(new object[] { waypoint.WaypointName, waypoint.StartDate.ToShortDateString(), waypoint.EndDate.ToShortDateString() });
            }

            this.waypointDataGridView.DataSource = tripTable;
        }
    }
}
