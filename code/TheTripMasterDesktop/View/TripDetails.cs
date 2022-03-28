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
        WaypointDataLayer waypointDataLayer = new WaypointDataLayer();
        TransportationDataLayer transportDataLayer = new TransportationDataLayer();

        public event Action AddWaypointButtonClick;
        public event Action AddTransportButtonClick;
        public event Action AddLodgingButtonClick;
        public event Action CancelButtonClick;
        public event Action WaypointDataCellClick;
        public event Action TransportDataCellClick;

        public TripDetails()
        {
            InitializeComponent();
        }

        /**
         * Navigates to the Add Waypoint page.
         */
        private void addWaypointButton_Click(object sender, EventArgs e)
        {
            AddWaypointButtonClick?.Invoke();
        }


        private void addTransportButton_Click(object sender, EventArgs e)
        {
            AddTransportButtonClick?.Invoke();
        }


        private void addLodgingButton_Click(object sender, EventArgs e)
        {
            AddLodgingButtonClick?.Invoke();
        }

        /**
         * Navigates to the Overview page.
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
            return (TripValidation.ValidateName(this.tripNameTextBox.Text) &&
                    TripValidation.ValidateDateTimes(this.startDatePicker.Value, this.endDatePicker.Value));
        }

        public void LoadTripDataIntoInputFields()
        {
            this.tripNameTextBox.Text = SelectedTrip.Trip.Name;
            this.startDatePicker.Value = SelectedTrip.Trip.StartDate;
            this.endDatePicker.Value = SelectedTrip.Trip.EndDate;
        }

        public void LoadWaypointDataIntoGridView()
        {
            DataTable tripTable = new DataTable();
            tripTable.Columns.Add("Id");
            tripTable.Columns.Add("Type");
            tripTable.Columns.Add("Name");
            tripTable.Columns.Add("Start Date");
            tripTable.Columns.Add("End Date");

            foreach (Waypoint waypoint in this.waypointDataLayer.GetTripWaypoints(SelectedTrip.Trip.TripId))
            {
                tripTable.Rows.Add(new object[] { waypoint.Id, "Waypoint", waypoint.WaypointName, waypoint.StartDate.ToShortDateString(), waypoint.EndDate.ToShortDateString()});
            }

            foreach (Transportation transport in this.transportDataLayer.GetTripTransportations(SelectedTrip.Trip.TripId))
            {
                tripTable.Rows.Add(new object[] { transport.Id, "Transport", transport.TransportationType, transport.StartDate.ToShortDateString(), transport.EndDate.ToShortDateString() });
            }

            this.eventDataGridView.DataSource = tripTable;
        }

        private void eventDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.eventDataGridView.CurrentRow.Cells[1].Value.ToString().Equals("Waypoint"))
            {
                Waypoint waypoint =
                    this.waypointDataLayer.GetWaypoint(int.Parse(this.eventDataGridView.CurrentRow.Cells[0].Value.ToString()));

                SelectedEvent.Event = waypoint;

                WaypointDataCellClick?.Invoke();
            }
            else
            {
                Transportation transport =
                    this.transportDataLayer.GetTransportation(int.Parse(this.eventDataGridView.CurrentRow.Cells[0].Value.ToString()));

                SelectedEvent.Event = transport;

                TransportDataCellClick?.Invoke();
            }
        }
    }
}
