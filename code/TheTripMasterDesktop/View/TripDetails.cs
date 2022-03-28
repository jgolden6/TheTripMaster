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
        LodgingDataLayer lodgingDataLayer = new LodgingDataLayer();

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

        public void LoadTripDataIntoInputFields()
        {
            this.tripNameTextBox.Text = SelectedTrip.Trip.Name;
            this.startDatePicker.Value = SelectedTrip.Trip.StartDate;
            this.endDatePicker.Value = SelectedTrip.Trip.EndDate;
        }

        public void LoadEventDataIntoGridView()
        {
            DataTable eventTable = new DataTable();
            eventTable.Columns.Add("Id");
            eventTable.Columns.Add("Type");
            eventTable.Columns.Add("Name");
            eventTable.Columns.Add("Start Date");
            eventTable.Columns.Add("End Date");

            foreach (Waypoint waypoint in this.waypointDataLayer.GetTripWaypoints(SelectedTrip.Trip.TripId))
            {
                eventTable.Rows.Add(new object[] { 
                    waypoint.Id, "Waypoint", waypoint.WaypointName, 
                    waypoint.StartDate.ToShortDateString(), waypoint.EndDate.ToShortDateString()

                });
            }

            foreach (Transportation transport in this.transportDataLayer.GetTripTransportations(SelectedTrip.Trip.TripId))
            {
                eventTable.Rows.Add(new object[] { 
                    transport.Id, "Transport", transport.TransportationType, 
                    transport.StartDate.ToShortDateString(), transport.EndDate.ToShortDateString()
                });
            }

            this.eventDataGridView.DataSource = eventTable;
        }

        public void LoadLodgingDataIntoGridView()
        {
            DataTable lodgingTable = new DataTable();
            lodgingTable.Columns.Add("Id");
            lodgingTable.Columns.Add("Address");
            lodgingTable.Columns.Add("Start Date");
            lodgingTable.Columns.Add("End Date");

            foreach (Lodging lodging in this.lodgingDataLayer.GetTripLodgings(SelectedTrip.Trip.TripId))
            {
                lodgingTable.Rows.Add(new object[]
                {
                    lodging.LodgingId, lodging.StreetAddress, lodging.StartDate.ToShortDateString(),
                    lodging.EndDate.ToShortDateString()
                });
            }

            this.lodgingDataGridView.DataSource = lodgingTable;
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

        private void lodgingDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
