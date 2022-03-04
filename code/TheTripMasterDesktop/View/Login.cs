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
    public partial class Login : UserControl
    {
        public event Action LoginButtonClick;
        public event Action RegisterButtonClick;

        public Login()
        {
            InitializeComponent();
        }

        /**
         * Sets the active user and navigates to the Overview page if the credentials are valid.
         */
        private void loginButton_Click(object sender, EventArgs e)
        {
            User user = UserDataLayer.Authenticate(this.usernameTextBox.Text, this.passwordTextBox.Text);

            this.usernameTextBox.Clear();
            this.passwordTextBox.Clear();

            if (user != null)
            {
                ActiveUser.User = user;
                LoginButtonClick?.Invoke();
            }
        }

        /**
         * Navigates to the Register page.
         */
        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterButtonClick?.Invoke();
        }
    }
}
