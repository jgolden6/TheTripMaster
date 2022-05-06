
namespace TheTripMasterDesktop.View
{
    partial class AddTrip
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.tripNameTextBox = new System.Windows.Forms.TextBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.endTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.tripNameErrorLabel = new System.Windows.Forms.Label();
            this.dateTimeErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trip Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Start Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "End Date:";
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "";
            this.startDatePicker.Location = new System.Drawing.Point(106, 75);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 23);
            this.startDatePicker.TabIndex = 3;
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(106, 133);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(200, 23);
            this.endDatePicker.TabIndex = 4;
            // 
            // tripNameTextBox
            // 
            this.tripNameTextBox.Location = new System.Drawing.Point(106, 29);
            this.tripNameTextBox.Name = "tripNameTextBox";
            this.tripNameTextBox.Size = new System.Drawing.Size(200, 23);
            this.tripNameTextBox.TabIndex = 5;
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(56, 247);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 6;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(198, 247);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // startTimePicker
            // 
            this.startTimePicker.CustomFormat = "";
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.startTimePicker.Location = new System.Drawing.Point(201, 104);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(105, 23);
            this.startTimePicker.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Time:";
            // 
            // endTimePicker
            // 
            this.endTimePicker.CustomFormat = "";
            this.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.endTimePicker.Location = new System.Drawing.Point(201, 162);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.Size = new System.Drawing.Size(105, 23);
            this.endTimePicker.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Time:";
            // 
            // tripNameErrorLabel
            // 
            this.tripNameErrorLabel.AutoSize = true;
            this.tripNameErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.tripNameErrorLabel.Location = new System.Drawing.Point(106, 54);
            this.tripNameErrorLabel.Name = "tripNameErrorLabel";
            this.tripNameErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.tripNameErrorLabel.TabIndex = 12;
            // 
            // dateTimeErrorLabel
            // 
            this.dateTimeErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.dateTimeErrorLabel.Location = new System.Drawing.Point(29, 193);
            this.dateTimeErrorLabel.Name = "dateTimeErrorLabel";
            this.dateTimeErrorLabel.Size = new System.Drawing.Size(277, 34);
            this.dateTimeErrorLabel.TabIndex = 14;
            // 
            // AddTrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateTimeErrorLabel);
            this.Controls.Add(this.tripNameErrorLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.endTimePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.startTimePicker);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.tripNameTextBox);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddTrip";
            this.Size = new System.Drawing.Size(340, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.TextBox tripNameTextBox;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker endTimePicker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label tripNameErrorLabel;
        private System.Windows.Forms.Label dateTimeErrorLabel;
    }
}
