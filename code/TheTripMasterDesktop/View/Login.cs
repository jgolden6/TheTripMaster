using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginButtonClick?.Invoke();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterButtonClick?.Invoke();
        }
    }
}
