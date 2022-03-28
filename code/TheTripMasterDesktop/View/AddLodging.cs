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

            this.dataLayer.AddLodging(lodging);

            ConfirmButtonClick?.Invoke();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelButtonClick?.Invoke();
        }

        private bool ValidateData()
        {
            bool isValid = true;

            if (!LodgingValidation.ValidateAddressField(this.addressTextBox.Text))
            {
                isValid = false;
                this.addressErrorLabel.Text = "Invalid address.";
            }

            if (!LodgingValidation.ValidateAddressField(this.cityTextBox.Text))
            {
                isValid = false;
                this.cityErrorLabel.Text = "Invalid city.";
            }

            if (!LodgingValidation.ValidateAddressField(this.stateTextBox.Text))
            {
                isValid = false;
                this.stateErrorLabel.Text = "Invalid state.";
            }

            if (!LodgingValidation.ValidateZipCode(this.zipcodeTextBox.Text))
            {
                isValid = false;
                this.zipcodeErrorLabel.Text = "Invalid zip code.";
            }

            if (!LodgingValidation.ValidateDateTimes(this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay,
                this.endDatePicker.Value.Date + this.endTimePicker.Value.TimeOfDay))
            {
                isValid = false;
                this.dateTimeErrorLabel.Text = "Dates are overlapping.";
            }

            if (!LodgingValidation.ValidateDescription(this.descriptionTextBox.Text))
            {
                isValid = false;
                this.descriptionErrorLabel.Text = "Invalid description.";
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
