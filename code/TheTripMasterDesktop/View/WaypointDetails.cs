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
    public partial class WaypointDetails : UserControl
    {
        WaypointDataLayer dataLayer = new WaypointDataLayer();

        public event Action DeleteButtonClick;
        public event Action CancelButtonClick;
        public event Action EditButtonClick;

        public WaypointDetails()
        {
            InitializeComponent();
        }

        /**
         * Deletes the selected waypoint and navigates to the Trip Details page.
         */
        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.ClearErrorMessages();
            this.dataLayer.DeleteWaypoint(SelectedEvent.Event.Id);
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
         * Loads the relevant waypoint data into the fields on the page.
         */
        public void LoadWaypointDataIntoInputFields()
        {
            Waypoint waypoint = (Waypoint)SelectedEvent.Event;
            this.waypointNameTextBox.Text = SelectedEvent.Event.ToString().Trim();
            this.addressTextBox.Text = waypoint.StreetAddress.Trim();
            this.cityTextBox.Text = waypoint.City.Trim();
            this.stateTextBox.Text = waypoint.State.Trim();
            this.zipcodeTextBox.Text = waypoint.ZipCode;
            this.startDatePicker.Value = SelectedEvent.Event.StartDate;
            this.endDatePicker.Value = SelectedEvent.Event.EndDate;
            this.webControl1.WebView = this.webView1;
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

            Waypoint waypoint = new Waypoint
            {
                Id = SelectedEvent.Event.Id,
                WaypointName = this.waypointNameTextBox.Text,
                StreetAddress = this.addressTextBox.Text,
                City = this.cityTextBox.Text,
                State = this.stateTextBox.Text,
                ZipCode = this.zipcodeTextBox.Text,
                StartDate = this.startDatePicker.Value,
                EndDate = this.endDatePicker.Value
            };

            this.dataLayer.EditWaypoint(waypoint);

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

            if (!TripValidation.ValidateName(this.waypointNameTextBox.Text))
            {
                isValid = false;
                this.nameErrorLabel.Text = "Invalid waypoint name.";
            }

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

            foreach (Waypoint waypoint in this.dataLayer.GetTripWaypoints(SelectedTrip.Trip.TripId))
            {
                bool createdWaypointBeforeExistingWaypoint = this.endDatePicker.Value < waypoint.StartDate;

                bool createdWaypointAfterExistingWaypoint = this.startDatePicker.Value > waypoint.EndDate;

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
