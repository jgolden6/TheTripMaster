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

        public WaypointDetails()
        {
            InitializeComponent();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.dataLayer.DeleteWaypoint(SelectedEvent.Event.Id);
            DeleteButtonClick?.Invoke();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelButtonClick?.Invoke();
        }

        public void LoadWaypointDataIntoInputFields()
        {
            this.waypointNameTextBox.Text = SelectedEvent.Event.ToString();
            this.startDatePicker.Value = SelectedEvent.Event.StartDate;
            this.endDatePicker.Value = SelectedEvent.Event.EndDate;
        }
    }
}
