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
    public partial class AddTransport : UserControl
    {
        TransportationDataLayer dataLayer = new TransportationDataLayer();

        public event Action ConfirmButtonClick;
        public event Action CancelButtonClick;

        public AddTransport()
        {
            InitializeComponent();
            this.typeComboBox.DataSource = Enum.GetNames(typeof(TransportationType));
        }

        /**
         * Adds the transportation if the data is valid and navigates to the Trip Details page.
         * Displays error messages for invalid data if the data isn't valid.
         */
        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();

            if (!ValidateData()) return;

            Transportation transportation = new Transportation
            {
                TransportationType = this.typeComboBox.Text,
                StartDate = this.startDatePicker.Value,
                EndDate = this.endDatePicker.Value
            };

            int id = this.dataLayer.AddTransportation(transportation);
            transportation.Id = id;
            SelectedEvent.Event = transportation;

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

            foreach (Transportation transportation in this.dataLayer.GetTripTransportations(SelectedTrip.Trip.TripId))
            {
                bool createdTransportBeforeExistingTransport =
                    this.endDatePicker.Value.Date + this.endTimePicker.Value.TimeOfDay < transportation.StartDate;

                bool createdTransportAfterExistingTransport =
                    this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay > transportation.EndDate;

                if (!createdTransportBeforeExistingTransport && !createdTransportAfterExistingTransport)
                {
                    isValid = false;
                    this.dateTimeErrorLabel.Text = "Transport is overlapping with " + transportation.TransportationType + ": " +
                                                   transportation.StartDate + " to " + transportation.EndDate;
                }
            }

            return isValid;
        }

        private void ClearErrorMessages()
        {
            this.dateTimeErrorLabel.Text = "";
        }
    }
}
