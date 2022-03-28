﻿using System;
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
        public event Action ConfirmButtonClick;
        public event Action CancelButtonClick;

        public AddTransport()
        {
            InitializeComponent();
        }

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

            TransportationDataLayer.AddTransportation(transportation);
            ConfirmButtonClick?.Invoke();
        }

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
            this.nameErrorLabel.Text = "";
            this.dateTimeErrorLabel.Text = "";
        }
    }
}
