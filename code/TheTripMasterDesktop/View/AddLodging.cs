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
    public partial class AddLodging : UserControl
    {
        LodgingDataLayer dataLayer = new LodgingDataLayer();

        public event Action ConfirmButtonClick;
        public event Action CancelButtonClick;

        public AddLodging()
        {
            InitializeComponent();
        }

        /**
         * Adds the lodging if the data is valid and navigates to the Trip Details page.
         * Displays error messages for invalid data if the data isn't valid.
         */
        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();

            if (!ValidateData()) return;

            Lodging lodging = new Lodging
            {
                StreetAddress = this.addressTextBox.Text,
                City = this.cityTextBox.Text,
                State = this.stateTextBox.Text,
                ZipCode = this.zipcodeTextBox.Text,
                StartDate = this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay,
                EndDate = this.endDatePicker.Value.Date + this.endTimePicker.Value.TimeOfDay,
                Description = this.descriptionTextBox.Text
            };

            int id = this.dataLayer.AddLodging(lodging);
            lodging.LodgingId = id;
            SelectedLodging.Lodging = lodging;

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

            if (!LodgingValidation.ValidateDescription(this.descriptionTextBox.Text))
            {
                isValid = false;
                this.descriptionErrorLabel.Text = "Invalid description.";
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

            foreach (Lodging lodging in this.dataLayer.GetTripLodgings(SelectedTrip.Trip.TripId))
            {
                bool createdLodgingBeforeExistingLodging =
                    this.endDatePicker.Value.Date + this.endTimePicker.Value.TimeOfDay < lodging.StartDate;

                bool createdLodgingAfterExistingLodging =
                    this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay > lodging.EndDate;

                if (!createdLodgingBeforeExistingLodging && !createdLodgingAfterExistingLodging)
                {
                    isValid = false;
                    this.dateTimeErrorLabel.Text = "Lodging is overlapping with: " +
                                                   lodging.StartDate + " to " + lodging.EndDate;
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
            this.dateTimeErrorLabel.Text = "";
            this.descriptionErrorLabel.Text = "";
        }
    }
}
