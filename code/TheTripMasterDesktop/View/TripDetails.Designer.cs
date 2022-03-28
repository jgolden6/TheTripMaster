
namespace TheTripMasterDesktop.View
{
    partial class TripDetails
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
            this.label1 = new System.Windows.Forms.Label();
            this.tripNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.eventDataGridView = new System.Windows.Forms.DataGridView();
            this.addWaypointButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addTransportButton = new System.Windows.Forms.Button();
            this.lodgingDataGridView = new System.Windows.Forms.DataGridView();
            this.addLodgingButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.eventDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lodgingDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // tripNameTextBox
            // 
            this.tripNameTextBox.Location = new System.Drawing.Point(88, 23);
            this.tripNameTextBox.Name = "tripNameTextBox";
            this.tripNameTextBox.Size = new System.Drawing.Size(100, 23);
            this.tripNameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "End Date:";
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDatePicker.Location = new System.Drawing.Point(88, 63);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 23);
            this.startDatePicker.TabIndex = 4;
            // 
            // endDatePicker
            // 
            this.endDatePicker.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDatePicker.Location = new System.Drawing.Point(88, 104);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(200, 23);
            this.endDatePicker.TabIndex = 5;
            // 
            // eventDataGridView
            // 
            this.eventDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventDataGridView.Location = new System.Drawing.Point(21, 146);
            this.eventDataGridView.Name = "eventDataGridView";
            this.eventDataGridView.RowTemplate.Height = 25;
            this.eventDataGridView.Size = new System.Drawing.Size(267, 101);
            this.eventDataGridView.TabIndex = 6;
            this.eventDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.eventDataGridView_CellClick);
            // 
            // addWaypointButton
            // 
            this.addWaypointButton.Location = new System.Drawing.Point(21, 253);
            this.addWaypointButton.Name = "addWaypointButton";
            this.addWaypointButton.Size = new System.Drawing.Size(98, 23);
            this.addWaypointButton.TabIndex = 8;
            this.addWaypointButton.Text = "Add Waypoint";
            this.addWaypointButton.UseVisualStyleBackColor = true;
            this.addWaypointButton.Click += new System.EventHandler(this.addWaypointButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(229, 23);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(59, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addTransportButton
            // 
            this.addTransportButton.Location = new System.Drawing.Point(190, 253);
            this.addTransportButton.Name = "addTransportButton";
            this.addTransportButton.Size = new System.Drawing.Size(98, 23);
            this.addTransportButton.TabIndex = 10;
            this.addTransportButton.Text = "Add Transport";
            this.addTransportButton.UseVisualStyleBackColor = true;
            this.addTransportButton.Click += new System.EventHandler(this.addTransportButton_Click);
            // 
            // lodgingDataGridView
            // 
            this.lodgingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lodgingDataGridView.Location = new System.Drawing.Point(21, 309);
            this.lodgingDataGridView.Name = "lodgingDataGridView";
            this.lodgingDataGridView.RowTemplate.Height = 25;
            this.lodgingDataGridView.Size = new System.Drawing.Size(267, 101);
            this.lodgingDataGridView.TabIndex = 11;
            // 
            // addLodgingButton
            // 
            this.addLodgingButton.Location = new System.Drawing.Point(21, 416);
            this.addLodgingButton.Name = "addLodgingButton";
            this.addLodgingButton.Size = new System.Drawing.Size(98, 23);
            this.addLodgingButton.TabIndex = 12;
            this.addLodgingButton.Text = "Add Lodging";
            this.addLodgingButton.UseVisualStyleBackColor = true;
            this.addLodgingButton.Click += new System.EventHandler(this.addLodgingButton_Click);
            // 
            // TripDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addLodgingButton);
            this.Controls.Add(this.lodgingDataGridView);
            this.Controls.Add(this.addTransportButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addWaypointButton);
            this.Controls.Add(this.eventDataGridView);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tripNameTextBox);
            this.Controls.Add(this.label1);
            this.Name = "TripDetails";
            this.Size = new System.Drawing.Size(310, 457);
            ((System.ComponentModel.ISupportInitialize)(this.eventDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lodgingDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tripNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.DataGridView eventDataGridView;
        private System.Windows.Forms.Button addWaypointButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addTransportButton;
        private System.Windows.Forms.DataGridView lodgingDataGridView;
        private System.Windows.Forms.Button addLodgingButton;
    }
}
