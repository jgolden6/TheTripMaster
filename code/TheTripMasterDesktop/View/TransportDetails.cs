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

        public TransportDetails()
        {
            InitializeComponent();
        }

        /**
         * Deletes the selected event and navigates to the Trip Details page.
         */
        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.dataLayer.DeleteTransportation(SelectedEvent.Event.Id);
            DeleteButtonClick?.Invoke();
        }

        /**
         * Navigates to the Trip Details page.
         */
        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelButtonClick?.Invoke();
        }

        /**
         * Loads the relevant transportation data into the fields on the page.
         */
        public void LoadTransportDataIntoInputFields()
        {
            this.transportNameTextBox.Text = SelectedEvent.Event.ToString().Trim();
            this.startDatePicker.Value = SelectedEvent.Event.StartDate;
            this.endDatePicker.Value = SelectedEvent.Event.EndDate;
        }
    }
}
