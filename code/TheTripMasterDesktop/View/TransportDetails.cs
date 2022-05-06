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
    public partial class TransportDetails : UserControl
    {
        TransportationDataLayer dataLayer = new TransportationDataLayer();

        public event Action DeleteButtonClick;
        public event Action CancelButtonClick;
        public event Action EditButtonClick;

        public TransportDetails()
        {
            InitializeComponent();
            this.transportTypeComboBox.DataSource = Enum.GetNames(typeof(TransportationType));
        }

        /**
         * Deletes the selected event and navigates to the Trip Details page.
         */
        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();
            this.dataLayer.DeleteTransportation(SelectedEvent.Event.Id);
            DeleteButtonClick?.Invoke();
        }

        /**
         * Navigates to the Trip Details page.
         */
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();
            CancelButtonClick?.Invoke();
        }

        /**
         * Loads the relevant transportation data into the fields on the page.
         */
        public void LoadTransportDataIntoInputFields()
        {
            this.transportTypeComboBox.SelectedItem = Enum.Parse(typeof(TransportationType), SelectedEvent.Event.ToString());
            this.startDatePicker.Value = SelectedEvent.Event.StartDate;
            this.endDatePicker.Value = SelectedEvent.Event.EndDate;
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();
            if (!ValidateData()) return;

            Transportation transport = new Transportation
            {
                Id = SelectedEvent.Event.Id,
                TransportationType = this.transportTypeComboBox.Text,
                StartDate = this.startDatePicker.Value,
                EndDate = this.endDatePicker.Value
            };

            this.dataLayer.EditTransportation(transport);

            EditButtonClick?.Invoke();
        }

        /**
         * Validates all the information in the input fields.
         */
        private bool ValidateData()
        {

            bool isValid = true;

            if (!TripValidation.ValidateDateTimesAfterNow(this.startDatePicker.Value))
            {
                isValid = false;
                this.dateTimeErrorLabel.Text = "Start date must be after current date.";
            }

            if (!TripValidation.ValidateStartBeforeEnd(this.startDatePicker.Value, this.endDatePicker.Value))
            {
                isValid = false;
                this.dateTimeErrorLabel.Text = "End date must be after start date.";
            }

            foreach (Transportation transportation in this.dataLayer.GetTripTransportations(SelectedTrip.Trip.TripId))
            {
                bool createdTransportBeforeExistingTransport = this.endDatePicker.Value < transportation.StartDate;

                bool createdTransportAfterExistingTransport = this.startDatePicker.Value > transportation.EndDate;

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
