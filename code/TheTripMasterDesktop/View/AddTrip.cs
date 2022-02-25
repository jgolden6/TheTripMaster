using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TheTripMasterDesktop.View
{
    public partial class AddTrip : UserControl
    {
        public event Action ConfirmButtonClick;
        public event Action CancelButtonClick;

        public AddTrip()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            ConfirmButtonClick?.Invoke();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelButtonClick?.Invoke();
        }
    }
}
