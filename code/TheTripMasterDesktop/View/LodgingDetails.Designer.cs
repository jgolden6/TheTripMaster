
namespace TheTripMasterDesktop.View
{
    partial class LodgingDetails
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
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.zipcodeTextBox = new System.Windows.Forms.TextBox();
            this.stateTextBox = new System.Windows.Forms.TextBox();
            this.cityTextBox = new System.Windows.Forms.TextBox();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.webView1 = new EO.WebBrowser.WebView();
            this.editButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.zipcodeErrorLabel = new System.Windows.Forms.Label();
            this.stateErrorLabel = new System.Windows.Forms.Label();
            this.cityErrorLabel = new System.Windows.Forms.Label();
            this.addressErrorLabel = new System.Windows.Forms.Label();
            this.dateTimeErrorLabel = new System.Windows.Forms.Label();
            this.descriptionErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(121, 282);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(200, 96);
            this.descriptionTextBox.TabIndex = 61;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 15);
            this.label9.TabIndex = 60;
            this.label9.Text = "Description:";
            // 
            // endDatePicker
            // 
            this.endDatePicker.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDatePicker.Location = new System.Drawing.Point(121, 240);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(200, 23);
            this.endDatePicker.TabIndex = 55;
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDatePicker.Location = new System.Drawing.Point(121, 198);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 23);
            this.startDatePicker.TabIndex = 54;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 246);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 15);
            this.label7.TabIndex = 53;
            this.label7.Text = "End Date:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 15);
            this.label8.TabIndex = 52;
            this.label8.Text = "Start Date:";
            // 
            // zipcodeTextBox
            // 
            this.zipcodeTextBox.Location = new System.Drawing.Point(121, 154);
            this.zipcodeTextBox.Name = "zipcodeTextBox";
            this.zipcodeTextBox.Size = new System.Drawing.Size(200, 23);
            this.zipcodeTextBox.TabIndex = 51;
            // 
            // stateTextBox
            // 
            this.stateTextBox.Location = new System.Drawing.Point(121, 113);
            this.stateTextBox.Name = "stateTextBox";
            this.stateTextBox.Size = new System.Drawing.Size(200, 23);
            this.stateTextBox.TabIndex = 50;
            // 
            // cityTextBox
            // 
            this.cityTextBox.Location = new System.Drawing.Point(121, 70);
            this.cityTextBox.Name = "cityTextBox";
            this.cityTextBox.Size = new System.Drawing.Size(200, 23);
            this.cityTextBox.TabIndex = 49;
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(121, 29);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(200, 23);
            this.addressTextBox.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 47;
            this.label4.Text = "Zip Code:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 46;
            this.label3.Text = "State:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 45;
            this.label2.Text = "City:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 44;
            this.label1.Text = "Street Address:";
            // 
            // webView1
            // 
            this.webView1.InputMsgFilter = null;
            this.webView1.ObjectForScripting = null;
            this.webView1.Title = null;
            this.webView1.Url = "";
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(30, 401);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(76, 23);
            this.editButton.TabIndex = 67;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 23);
            this.button1.TabIndex = 66;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(135, 401);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 23);
            this.button2.TabIndex = 65;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // zipcodeErrorLabel
            // 
            this.zipcodeErrorLabel.AutoSize = true;
            this.zipcodeErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.zipcodeErrorLabel.Location = new System.Drawing.Point(121, 180);
            this.zipcodeErrorLabel.Name = "zipcodeErrorLabel";
            this.zipcodeErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.zipcodeErrorLabel.TabIndex = 73;
            // 
            // stateErrorLabel
            // 
            this.stateErrorLabel.AutoSize = true;
            this.stateErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.stateErrorLabel.Location = new System.Drawing.Point(121, 136);
            this.stateErrorLabel.Name = "stateErrorLabel";
            this.stateErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.stateErrorLabel.TabIndex = 72;
            // 
            // cityErrorLabel
            // 
            this.cityErrorLabel.AutoSize = true;
            this.cityErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.cityErrorLabel.Location = new System.Drawing.Point(121, 95);
            this.cityErrorLabel.Name = "cityErrorLabel";
            this.cityErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.cityErrorLabel.TabIndex = 71;
            // 
            // addressErrorLabel
            // 
            this.addressErrorLabel.AutoSize = true;
            this.addressErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.addressErrorLabel.Location = new System.Drawing.Point(121, 52);
            this.addressErrorLabel.Name = "addressErrorLabel";
            this.addressErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.addressErrorLabel.TabIndex = 70;
            // 
            // dateTimeErrorLabel
            // 
            this.dateTimeErrorLabel.AutoSize = true;
            this.dateTimeErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.dateTimeErrorLabel.Location = new System.Drawing.Point(121, 264);
            this.dateTimeErrorLabel.Name = "dateTimeErrorLabel";
            this.dateTimeErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.dateTimeErrorLabel.TabIndex = 69;
            // 
            // descriptionErrorLabel
            // 
            this.descriptionErrorLabel.AutoSize = true;
            this.descriptionErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.descriptionErrorLabel.Location = new System.Drawing.Point(121, 381);
            this.descriptionErrorLabel.Name = "descriptionErrorLabel";
            this.descriptionErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.descriptionErrorLabel.TabIndex = 74;
            // 
            // LodgingDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.descriptionErrorLabel);
            this.Controls.Add(this.zipcodeErrorLabel);
            this.Controls.Add(this.stateErrorLabel);
            this.Controls.Add(this.cityErrorLabel);
            this.Controls.Add(this.addressErrorLabel);
            this.Controls.Add(this.dateTimeErrorLabel);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.zipcodeTextBox);
            this.Controls.Add(this.stateTextBox);
            this.Controls.Add(this.cityTextBox);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LodgingDetails";
            this.Size = new System.Drawing.Size(679, 447);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox zipcodeTextBox;
        private System.Windows.Forms.TextBox stateTextBox;
        private System.Windows.Forms.TextBox cityTextBox;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private EO.WebBrowser.WebView webView1;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label zipcodeErrorLabel;
        private System.Windows.Forms.Label stateErrorLabel;
        private System.Windows.Forms.Label cityErrorLabel;
        private System.Windows.Forms.Label addressErrorLabel;
        private System.Windows.Forms.Label dateTimeErrorLabel;
        private System.Windows.Forms.Label descriptionErrorLabel;
    }
}
