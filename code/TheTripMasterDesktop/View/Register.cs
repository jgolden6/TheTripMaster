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
    public partial class Register : UserControl
    {
        public event Action RegisterButtonClick;
        public event Action CancelButtonClick;

        public Register()
        {
            InitializeComponent();
        }

        /**
         * Adds the user to the database if the information is valid and navigates to the Login page.
         */
        private void registerButton_Click(object sender, EventArgs e)
        {
            if (ValidateData() && this.passwordTextBox.Text == this.passwordCheckTextBox.Text)
            {
                User user = new User
                {
                    FirstName = this.firstNameTextBox.Text, LastName = this.lastNameTextBox.Text,
                    Email = this.emailTextBox.Text, Username = this.usernameTextBox.Text,
                    Password = this.passwordTextBox.Text
                };

                UserDataLayer.AddUser(user);
                RegisterButtonClick?.Invoke();
            }
        }

        /**
         * Navigates to the Login page.
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
            bool isValid = true;

            if (!UserValidation.ValidateName(this.firstNameTextBox.Text))
            {
                isValid = false;
            }

            if (!UserValidation.ValidateName(this.lastNameTextBox.Text))
            {
                isValid = false;
            }

            if (!UserValidation.ValidateEmail(this.emailTextBox.Text))
            {
                isValid = false;
            }

            if (!UserValidation.ValidateUsername(this.usernameTextBox.Text))
            {
                isValid = false;
            }

            if (!UserValidation.ValidatePassword(this.passwordTextBox.Text))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
