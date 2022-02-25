using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TheTripMasterDesktop.View
{
    public partial class Overview : UserControl
    {
        public event Action AddTripButtonClick;
        public event Action AccountButtonClick;
        public event Action LogoutButtonClick;

        public Overview()
        {
            InitializeComponent();
        }

        private void addTripButton_Click(object sender, EventArgs e)
        {
            AddTripButtonClick?.Invoke();
        }

        private void accountButton_Click(object sender, EventArgs e)
        {
            AccountButtonClick?.Invoke();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            LogoutButtonClick?.Invoke();
        }
    }
}
