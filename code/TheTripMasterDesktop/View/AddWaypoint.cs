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
        WaypointDataLayer dataLayer = new WaypointDataLayer();

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
            this.ClearErrorMessages();

            if (!ValidateData()) return;

            Waypoint waypoint = new Waypoint
            {
                WaypointName = this.waypointNameTextBox.Text, StartDate = this.startDatePicker.Value,
                EndDate = this.endDatePicker.Value
            };

            this.dataLayer.AddWaypoint(waypoint);
            ConfirmButtonClick?.Invoke();
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

            bool isValid = true;

            if (!TripValidation.ValidateName(this.waypointNameTextBox.Text))
            {
                isValid = false;
                this.nameErrorLabel.Text = "Invalid waypoint name.";
            }

            if (!TripValidation.ValidateDateTimes(this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay,
                this.endDatePicker.Value.Date + this.endTimePicker.Value.TimeOfDay))
            {
                isValid = false;
                this.dateTimeErrorLabel.Text = "Dates are overlapping.";
            }

            return isValid;
        }

        private void ClearErrorMessages()
        {
            this.nameErrorLabel.Text = "";
            this.dateTimeErrorLabel.Text = "";
        }
    }
}
