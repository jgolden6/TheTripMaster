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

        public MainForm()
        {
            InitializeComponent();
            this.mainPanel.Controls.Add(this.loginPage);

            this.loginPage.LoginButtonClick += Login_LoginButton;
            this.loginPage.RegisterButtonClick += Login_RegisterButton;

            this.registerPage.RegisterButtonClick += Register_RegisterButton;
            this.registerPage.CancelButtonClick += Register_CancelButton;

            this.overviewPage.AddTripButtonClick += Overview_AddTripButton;
            this.overviewPage.AccountButtonClick += Overview_AccountButton;
            this.overviewPage.LogoutButtonClick += Overview_LogoutButton;

            this.addTripPage.ConfirmButtonClick += AddTrip_ConfirmButton;
            this.addTripPage.CancelButtonClick += AddTrip_CancelButton;
        }

        private void Login_LoginButton()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.overviewPage);
        }

        private void Login_RegisterButton()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.registerPage);
        }

        private void Register_RegisterButton()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.loginPage);
        }

        private void Register_CancelButton()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.loginPage);
        }

        private void Overview_AddTripButton()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.addTripPage);
        }

        private void Overview_AccountButton()
        {

        }

        private void Overview_LogoutButton()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.loginPage);
        }

        private void AddTrip_ConfirmButton()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.overviewPage);
        }

        private void AddTrip_CancelButton()
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(this.overviewPage);
        }
    }
}
