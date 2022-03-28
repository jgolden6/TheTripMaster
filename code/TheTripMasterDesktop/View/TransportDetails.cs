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
        public event Action DeleteButtonClick;
        public event Action CancelButtonClick;

        public TransportDetails()
        {
            InitializeComponent();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            TransportationDataLayer.DeleteTransportation(SelectedEvent.Event.Id);
            DeleteButtonClick?.Invoke();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelButtonClick?.Invoke();
        }

        public void LoadTransportDataIntoInputFields()
        {
            this.transportNameTextBox.Text = SelectedEvent.Event.ToString();
            this.startDatePicker.Value = SelectedEvent.Event.StartDate;
            this.endDatePicker.Value = SelectedEvent.Event.EndDate;
        }
    }
}
