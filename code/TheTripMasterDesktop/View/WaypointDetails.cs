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

        /**
         * Deletes the selected waypoint and navigates to the Trip Details page.
         */
        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.dataLayer.DeleteWaypoint(SelectedEvent.Event.Id);
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
    }
}
