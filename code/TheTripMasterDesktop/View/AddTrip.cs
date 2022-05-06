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
    public partial class AddTrip : UserControl
    {
        TripDataLayer dataLayer = new TripDataLayer();

        public event Action ConfirmButtonClick;
        public event Action CancelButtonClick;

        public AddTrip()
        {
            InitializeComponent();
        }

        /**
         * Adds the Trip to the database if the Trip is valid and navigates to the Overview page.
         * Displays error messages for invalid data if the data isn't valid.
         */
        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();

            if (!ValidateData()) return;

            Trip trip = new Trip
            {
                UserId = ActiveUser.User.UserId, Name = this.tripNameTextBox.Text,
                StartDate = this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay,
                EndDate = this.endDatePicker.Value.Date + this.endTimePicker.Value.TimeOfDay
            };

            this.dataLayer.AddTrip(trip);
            ConfirmButtonClick?.Invoke();
        }

        /**
         * Navigates to the Overview page.
         */
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();
            CancelButtonClick?.Invoke();
        }

        /**
         * Validates all the information in the input fields.
         */
        private bool ValidateData()
        {
            bool isValid = true;

            if (!TripValidation.ValidateName(this.tripNameTextBox.Text))
            {
                isValid = false;
                this.tripNameErrorLabel.Text = "Invalid trip name.";
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

            foreach (Trip trip in this.dataLayer.GetAllTripsOfUser(ActiveUser.User.UserId))
            {
                bool createdTripBeforeExistingTrip =
                    this.endDatePicker.Value.Date + this.endTimePicker.Value.TimeOfDay < trip.StartDate;

                bool createdTripAfterExistingTrip =
                    this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay > trip.EndDate;

                if (!createdTripBeforeExistingTrip && !createdTripAfterExistingTrip)
                {
                    isValid = false;
                    this.dateTimeErrorLabel.Text = "Trip is overlapping with " + trip.Name.Trim() + ": " + 
                        trip.StartDate + " to " + trip.EndDate;
                }
            }

            return isValid;
        }

        private void ClearErrorMessages()
        {
            this.tripNameErrorLabel.Text = "";
            this.dateTimeErrorLabel.Text = "";
        }
    }
}
