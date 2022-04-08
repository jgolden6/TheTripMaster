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
        public event Action LodgingDataCellClick;

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

        /**
         * Navigates to the Add Transport page.
         */
        private void addTransportButton_Click(object sender, EventArgs e)
        {
            AddTransportButtonClick?.Invoke();
        }

        /**
         * Navigates to the Add Lodging page.
         */
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
         * Loads the relevant trip data into the fields on the page.
         */
        public void LoadTripDataIntoInputFields()
        {
            this.tripNameTextBox.Text = SelectedTrip.Trip.Name;
            this.startDatePicker.Value = SelectedTrip.Trip.StartDate;
            this.endDatePicker.Value = SelectedTrip.Trip.EndDate;
        }

        /**
         * Populates the event grid view with the data from the trip's waypoints and transportation.
         */
        public void LoadEventDataIntoGridView()
        {
            DataTable eventTable = new DataTable();
            eventTable.Columns.Add("Id");
            eventTable.Columns.Add("Type");
            eventTable.Columns.Add("Name");
            eventTable.Columns.Add("Start Date", typeof(DateTime));
            eventTable.Columns.Add("End Date", typeof(DateTime));

            foreach (Waypoint waypoint in this.waypointDataLayer.GetTripWaypoints(SelectedTrip.Trip.TripId))
            {
                eventTable.Rows.Add(new object[] { 
                    waypoint.Id, "Waypoint", waypoint.WaypointName.Trim(), 
                    waypoint.StartDate.ToShortDateString(), waypoint.EndDate.ToShortDateString()

                });
            }

            foreach (Transportation transport in this.transportDataLayer.GetTripTransportations(SelectedTrip.Trip.TripId))
            {
                eventTable.Rows.Add(new object[] { 
                    transport.Id, "Transport", transport.TransportationType.Trim(), 
                    transport.StartDate.ToShortDateString(), transport.EndDate.ToShortDateString()
                });
            }

            this.eventDataGridView.DataSource = eventTable;
            this.eventDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.eventDataGridView.RowHeadersVisible = false;
            this.eventDataGridView.Sort(this.eventDataGridView.Columns[3], ListSortDirection.Ascending);
        }

        /**
         * Populates the lodging grid view with the data from the trip's lodging.
         */
        public void LoadLodgingDataIntoGridView()
        {
            DataTable lodgingTable = new DataTable();
            lodgingTable.Columns.Add("Id");
            lodgingTable.Columns.Add("Address");
            lodgingTable.Columns.Add("Start Date", typeof(DateTime));
            lodgingTable.Columns.Add("End Date", typeof(DateTime));

            foreach (Lodging lodging in this.lodgingDataLayer.GetTripLodgings(SelectedTrip.Trip.TripId))
            {
                lodgingTable.Rows.Add(new object[]
                {
                    lodging.LodgingId, lodging.StreetAddress.Trim(), lodging.StartDate.ToShortDateString(),
                    lodging.EndDate.ToShortDateString()
                });
            }

            this.lodgingDataGridView.DataSource = lodgingTable;
            this.lodgingDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.lodgingDataGridView.RowHeadersVisible = false;
            this.lodgingDataGridView.Sort(this.lodgingDataGridView.Columns[2], ListSortDirection.Ascending);
        }

        /**
         * Sets the selected event to the waypoint or transportation selected,
         * and navigates to the Waypoint Details or the Transport Details page.
         */
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

        /**
         * Sets the selected lodging to the lodging selected and navigates to the Lodging Selected page.
         */
        private void lodgingDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Lodging lodging =
                this.lodgingDataLayer.GetLodging(int.Parse(this.lodgingDataGridView.CurrentRow.Cells[0].Value.ToString()));

            SelectedLodging.Lodging = lodging;

            LodgingDataCellClick?.Invoke();
        }
    }
}
