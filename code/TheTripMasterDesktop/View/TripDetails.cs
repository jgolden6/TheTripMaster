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
    public partial class TripDetails : UserControl
    {
        public event Action UpdateButtonClick;
        public event Action AddWaypointButtonClick;
        public event Action CancelButtonClick;

        public TripDetails()
        {
            InitializeComponent();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                TripDataLayer.UpdateTrip(this.tripNameTextBox.Text, this.startDatePicker.Value, this.endDatePicker.Value);
                UpdateButtonClick?.Invoke();
            }
        }

        private void addWaypointButton_Click(object sender, EventArgs e)
        {
            AddWaypointButtonClick?.Invoke();
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
            return (TripValidation.ValidateName(this.tripNameTextBox.Text) &&
                    TripValidation.ValidateDateTimes(this.startDatePicker.Value, this.endDatePicker.Value));
        }
    }
}
