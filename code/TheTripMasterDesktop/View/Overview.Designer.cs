
namespace TheTripMasterDesktop.View
{
    partial class Overview
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tripDataGridView = new System.Windows.Forms.DataGridView();
            this.addTripButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tripDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tripDataGridView
            // 
            this.tripDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tripDataGridView.Location = new System.Drawing.Point(3, 64);
            this.tripDataGridView.Name = "tripDataGridView";
            this.tripDataGridView.RowTemplate.Height = 25;
            this.tripDataGridView.Size = new System.Drawing.Size(426, 261);
            this.tripDataGridView.TabIndex = 0;
            this.tripDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tripDataGridView_CellClick);
            // 
            // addTripButton
            // 
            this.addTripButton.Location = new System.Drawing.Point(3, 3);
            this.addTripButton.Name = "addTripButton";
            this.addTripButton.Size = new System.Drawing.Size(75, 23);
            this.addTripButton.TabIndex = 1;
            this.addTripButton.Text = "Add Trip";
            this.addTripButton.UseVisualStyleBackColor = true;
            this.addTripButton.Click += new System.EventHandler(this.addTripButton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(354, 3);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 2;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // Overview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.addTripButton);
            this.Controls.Add(this.tripDataGridView);
            this.Name = "Overview";
            this.Size = new System.Drawing.Size(432, 334);
            ((System.ComponentModel.ISupportInitialize)(this.tripDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tripDataGridView;
        private System.Windows.Forms.Button addTripButton;
        private System.Windows.Forms.Button logoutButton;
    }
}
