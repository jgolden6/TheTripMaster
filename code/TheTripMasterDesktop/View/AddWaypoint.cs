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
    public partial class AddWaypoint : UserControl
    {
        public event Action ConfirmButtonClick;
        public event Action CancelButtonClick;

        public AddWaypoint()
        {
            InitializeComponent();
        }

        /**
         * Adds the Waypoint if the data is valid and navigates to the Trip Details page.
         */
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                Waypoint waypoint = new Waypoint
                {
                    WaypointName = this.waypointNameTextBox.Text, StartDate = this.startDatePicker.Value,
                    EndDate = this.endDatePicker.Value
                };

                WaypointDataLayer.AddWaypoint(waypoint);
                ConfirmButtonClick?.Invoke();
            }
        }

        /**
         * Navigates to the Trip Details page.
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
            return (TripValidation.ValidateName(this.waypointNameTextBox.Text) &&
                    TripValidation.ValidateDateTimes(this.startDatePicker.Value, this.endDatePicker.Value));
        }
    }
}
