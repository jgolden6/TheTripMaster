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
    public partial class AddTrip : UserControl
    {
        public event Action ConfirmButtonClick;
        public event Action CancelButtonClick;

        public AddTrip()
        {
            InitializeComponent();
        }

        /**
         * Adds the Trip to the database if the Trip is valid and navigates to the Overview page.
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

            TripDataLayer.AddTrip(trip);
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
            this.tripNameErrorLabel.Text = "";
            this.dateTimeErrorLabel.Text = "";
        }
    }
}
