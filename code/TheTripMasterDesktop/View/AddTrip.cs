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
            if (ValidateData())
            {
                Trip trip = new Trip
                {
                    UserId = ActiveUser.User.UserId, Name = this.tripNameTextBox.Text,
                    StartDate = this.startDatePicker.Value, EndDate = this.endDatePicker.Value
                };

                TripDataLayer.AddTrip(trip);
                ConfirmButtonClick?.Invoke();
            }
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
    }
}
