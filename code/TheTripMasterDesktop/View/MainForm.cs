using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheTripMasterDesktop.View;

namespace TheTripMasterDesktop
{
    public partial class MainForm : Form
    {
        Login loginPage = new Login();
        Register registerPage = new Register();
        Overview overviewPage = new Overview();
        AddTrip addTripPage = new AddTrip();
        TripDetails tripDetailsPage = new TripDetails();
        AddWaypoint addWaypointPage = new AddWaypoint();
        AddTransport addTransportPage = new AddTransport();
        WaypointDetails waypointDetailsPage = new WaypointDetails();
        TransportDetails transportDetailsPage = new TransportDetails();
        AddLodging addLodgingPage = new AddLodging();
        LodgingDetails lodgingDetailsPage = new LodgingDetails();

        /**
         * Adds the login page to the main panel and registers all the button events.
         */
        public MainForm()
        {
            InitializeComponent();
            this.mainPanel.Controls.Add(this.loginPage);

            this.loginPage.LoginButtonClick += OpenOverviewPage;
            this.loginPage.RegisterButtonClick += OpenRegisterPage;

            this.registerPage.RegisterButtonClick += OpenLoginPage;
            this.registerPage.CancelButtonClick += OpenLoginPage;

            this.overviewPage.AddTripButtonClick += OpenAddTripPage;
            this.overviewPage.LogoutButtonClick += OpenLoginPage;
            this.overviewPage.DataCellClick += OpenTripDetailsPage;

            this.addTripPage.ConfirmButtonClick += OpenOverviewPage;
            this.addTripPage.CancelButtonClick += OpenOverviewPage;

            this.tripDetailsPage.AddTransportButtonClick += OpenAddTransportPage;
            this.tripDetailsPage.AddWaypointButtonClick += OpenAddWaypointPage;
            this.tripDetailsPage.AddLodgingButtonClick += OpenAddLodgingPage;
            this.tripDetailsPage.CancelButtonClick += OpenOverviewPage;
            this.tripDetailsPage.WaypointDataCellClick += OpenWaypointDetailsPage;
            this.tripDetailsPage.TransportDataCellClick += OpenTransportDetailsPage;
            this.tripDetailsPage.LodgingDataCellClick += OpenLodgingDetailsPage;

            this.addWaypointPage.ConfirmButtonClick += OpenTripDetailsPage;
            this.addWaypointPage.CancelButtonClick += OpenTripDetailsPage;

            this.addTransportPage.ConfirmButtonClick += OpenTripDetailsPage;
            this.addTransportPage.CancelButtonClick += OpenTripDetailsPage;

            this.waypointDetailsPage.DeleteButtonClick += OpenTripDetailsPage;
            this.waypointDetailsPage.CancelButtonClick += OpenTripDetailsPage;
            this.waypointDetailsPage.EditButtonClick += OpenTripDetailsPage;

            this.transportDetailsPage.DeleteButtonClick += OpenTripDetailsPage;
            this.transportDetailsPage.CancelButtonClick += OpenTripDetailsPage;

            this.addLodgingPage.ConfirmButtonClick += OpenTripDetailsPage;
            this.addLodgingPage.CancelButtonClick += OpenTripDetailsPage;

            this.lodgingDetailsPage.DeleteButtonClick += OpenTripDetailsPage;
            this.lodgingDetailsPage.CancelButtonClick += OpenTripDetailsPage;
        }

        private void OpenLoginPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.loginPage);
        }

        private void OpenRegisterPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.registerPage);
        }

        private void OpenOverviewPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.overviewPage);
            this.overviewPage.LoadTripDataIntoGridView();
        }

        private void OpenAddTripPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.addTripPage);
        }

        private void OpenTripDetailsPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.tripDetailsPage);
            this.tripDetailsPage.LoadTripDataIntoInputFields();
            this.tripDetailsPage.LoadEventDataIntoGridView();
            this.tripDetailsPage.LoadLodgingDataIntoGridView();
        }

        private void OpenAddWaypointPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.addWaypointPage);
        }

        private void OpenAddTransportPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.addTransportPage);
        }

        private void OpenWaypointDetailsPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.waypointDetailsPage);
            this.waypointDetailsPage.LoadWaypointDataIntoInputFields();
        }

        private void OpenTransportDetailsPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.transportDetailsPage);
            this.transportDetailsPage.LoadTransportDataIntoInputFields();
        }

        private void OpenAddLodgingPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.addLodgingPage);
        }

        private void OpenLodgingDetailsPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.lodgingDetailsPage);
            this.lodgingDetailsPage.LoadLodgingDataIntoInputFields();
        }
    }
}
