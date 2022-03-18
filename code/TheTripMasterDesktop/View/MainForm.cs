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

            this.tripDetailsPage.UpdateButtonClick += OpenOverviewPage;
            this.tripDetailsPage.AddWaypointButtonClick += OpenAddWaypointPage;
            this.tripDetailsPage.CancelButtonClick += OpenOverviewPage;

            this.addWaypointPage.ConfirmButtonClick += OpenTripDetailsPage;
            this.addWaypointPage.CancelButtonClick += OpenTripDetailsPage;
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
            this.tripDetailsPage.LoadWaypointDataIntoGridView();
        }

        private void OpenAddWaypointPage()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.addWaypointPage);
        }
    }
}
