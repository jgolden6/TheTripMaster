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
    public partial class LodgingDetails : UserControl
    {
        LodgingDataLayer dataLayer = new LodgingDataLayer();

        public event Action DeleteButtonClick;
        public event Action CancelButtonClick;
        public event Action EditButtonClick;

        public LodgingDetails()
        {
            InitializeComponent();
        }

        /**
         * Deletes the selected lodging and navigates to the Trip Details page.
         */
        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();
            this.dataLayer.DeleteLodging(SelectedLodging.Lodging.LodgingId);
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
         * Loads the lodging data into the fields on the page.
         */
        public void LoadLodgingDataIntoInputFields()
        {
            this.addressTextBox.Text = SelectedLodging.Lodging.StreetAddress.Trim();
            this.cityTextBox.Text = SelectedLodging.Lodging.City.Trim();
            this.stateTextBox.Text = SelectedLodging.Lodging.State.Trim();
            this.zipcodeTextBox.Text = SelectedLodging.Lodging.ZipCode;
            this.startDatePicker.Value = SelectedLodging.Lodging.StartDate;
            this.endDatePicker.Value = SelectedLodging.Lodging.EndDate;
            this.descriptionTextBox.Text = SelectedLodging.Lodging.Description.Trim();
            //this.webControl1.WebView = this.webView1;
            this.webView1.Url = "https://maps.googleapis.com/maps/api/staticmap?zoom=14&size=400x400&markers=" +
                                this.addressTextBox.Text + "," +
                                this.cityTextBox.Text + "," +
                                this.stateTextBox.Text +
                                "&key=AIzaSyDmIIfvmSD3Yd0Bb4Bl-LTvkkLC0MFnZ4E";
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();
            if (!ValidateData()) return;

            Lodging lodging = new Lodging
            {
                LodgingId = SelectedLodging.Lodging.LodgingId,
                StreetAddress = this.addressTextBox.Text,
                City = this.cityTextBox.Text,
                State = this.stateTextBox.Text,
                ZipCode = this.zipcodeTextBox.Text,
                StartDate = this.startDatePicker.Value,
                EndDate = this.endDatePicker.Value,
                Description = this.descriptionTextBox.Text
            };

            this.dataLayer.EditLodging(lodging);

            EditButtonClick?.Invoke();
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

            if (!LodgingValidation.ValidateDateTimes(this.startDatePicker.Value, this.endDatePicker.Value))
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
