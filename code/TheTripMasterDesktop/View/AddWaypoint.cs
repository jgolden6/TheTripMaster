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
                WaypointName = this.waypointNameTextBox.Text,
                StreetAddress = this.addressTextBox.Text,
                City = this.cityTextBox.Text,
                State = this.stateTextBox.Text,
                ZipCode = this.zipcodeTextBox.Text,
                StartDate = this.startDatePicker.Value,
                EndDate = this.endDatePicker.Value
            };

            int id = this.dataLayer.AddWaypoint(waypoint);
            waypoint.Id = id;
            SelectedEvent.Event = waypoint;

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

            if (!AddressValidation.ValidateAddressField(this.addressTextBox.Text))
            {
                isValid = false;
                this.addressErrorLabel.Text = "Invalid address.";
            }

            if (!AddressValidation.ValidateAddressField(this.cityTextBox.Text))
            {
                isValid = false;
                this.cityErrorLabel.Text = "Invalid city.";
            }

            if (!AddressValidation.ValidateAddressField(this.stateTextBox.Text))
            {
                isValid = false;
                this.stateErrorLabel.Text = "Invalid state.";
            }

            if (!AddressValidation.ValidateZipCode(this.zipcodeTextBox.Text))
            {
                isValid = false;
                this.zipcodeErrorLabel.Text = "Invalid zip code.";
            }

            if (!TripValidation.ValidateName(this.waypointNameTextBox.Text))
            {
                isValid = false;
                this.nameErrorLabel.Text = "Invalid waypoint name.";
            }

            if (!TripValidation.ValidateDateTimesAfterNow(this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay))
            {
                isValid = false;
                this.dateTimeErrorLabel.Text = "Start date must be after current date.";
            }

            if (!TripValidation.ValidateStartBeforeEnd(this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay,
                this.endDatePicker.Value.Date + this.endTimePicker.Value.TimeOfDay))
            {
                isValid = false;
                this.dateTimeErrorLabel.Text = "End date must be after start date.";
            }

            foreach (Waypoint waypoint in this.dataLayer.GetTripWaypoints(SelectedTrip.Trip.TripId))
            {
                bool createdWaypointBeforeExistingWaypoint =
                    this.endDatePicker.Value.Date + this.endTimePicker.Value.TimeOfDay < waypoint.StartDate;

                bool createdWaypointAfterExistingWaypoint =
                    this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay > waypoint.EndDate;

                if (!createdWaypointBeforeExistingWaypoint && !createdWaypointAfterExistingWaypoint)
                {
                    isValid = false;
                    this.dateTimeErrorLabel.Text = "Waypoint is overlapping with " + waypoint.WaypointName + ": " +
                                                   waypoint.StartDate + " to " + waypoint.EndDate;
                }
            }

            return isValid;
        }

        private void ClearErrorMessages()
        {
            this.addressErrorLabel.Text = "";
            this.cityErrorLabel.Text = "";
            this.stateErrorLabel.Text = "";
            this.zipcodeErrorLabel.Text = "";
            this.nameErrorLabel.Text = "";
            this.dateTimeErrorLabel.Text = "";
        }
    }
}
