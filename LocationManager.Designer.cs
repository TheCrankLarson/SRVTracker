namespace SRVTracker
{
    partial class LocationManager
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonAddEditLocation = new System.Windows.Forms.Button();
            this.buttonDeleteLocation = new System.Windows.Forms.Button();
            this.buttonLoadLocations = new System.Windows.Forms.Button();
            this.buttonSaveLocations = new System.Windows.Forms.Button();
            this.listBoxKnownLocations = new System.Windows.Forms.ListBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonAddEditLocation);
            this.groupBox3.Controls.Add(this.buttonDeleteLocation);
            this.groupBox3.Controls.Add(this.buttonLoadLocations);
            this.groupBox3.Controls.Add(this.buttonSaveLocations);
            this.groupBox3.Controls.Add(this.listBoxKnownLocations);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(274, 278);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Known Locations";
            // 
            // buttonAddEditLocation
            // 
            this.buttonAddEditLocation.Location = new System.Drawing.Point(6, 247);
            this.buttonAddEditLocation.Name = "buttonAddEditLocation";
            this.buttonAddEditLocation.Size = new System.Drawing.Size(53, 23);
            this.buttonAddEditLocation.TabIndex = 4;
            this.buttonAddEditLocation.Text = "Add...";
            this.buttonAddEditLocation.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteLocation
            // 
            this.buttonDeleteLocation.Location = new System.Drawing.Point(65, 247);
            this.buttonDeleteLocation.Name = "buttonDeleteLocation";
            this.buttonDeleteLocation.Size = new System.Drawing.Size(53, 23);
            this.buttonDeleteLocation.TabIndex = 3;
            this.buttonDeleteLocation.Text = "Delete";
            this.buttonDeleteLocation.UseVisualStyleBackColor = true;
            // 
            // buttonLoadLocations
            // 
            this.buttonLoadLocations.Location = new System.Drawing.Point(156, 247);
            this.buttonLoadLocations.Name = "buttonLoadLocations";
            this.buttonLoadLocations.Size = new System.Drawing.Size(53, 23);
            this.buttonLoadLocations.TabIndex = 2;
            this.buttonLoadLocations.Text = "Load...";
            this.buttonLoadLocations.UseVisualStyleBackColor = true;
            // 
            // buttonSaveLocations
            // 
            this.buttonSaveLocations.Location = new System.Drawing.Point(215, 247);
            this.buttonSaveLocations.Name = "buttonSaveLocations";
            this.buttonSaveLocations.Size = new System.Drawing.Size(53, 23);
            this.buttonSaveLocations.TabIndex = 1;
            this.buttonSaveLocations.Text = "Save...";
            this.buttonSaveLocations.UseVisualStyleBackColor = true;
            // 
            // listBoxKnownLocations
            // 
            this.listBoxKnownLocations.FormattingEnabled = true;
            this.listBoxKnownLocations.Location = new System.Drawing.Point(6, 16);
            this.listBoxKnownLocations.Name = "listBoxKnownLocations";
            this.listBoxKnownLocations.Size = new System.Drawing.Size(262, 225);
            this.listBoxKnownLocations.TabIndex = 0;
            // 
            // LocationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Name = "LocationManager";
            this.Size = new System.Drawing.Size(281, 287);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonAddEditLocation;
        private System.Windows.Forms.Button buttonDeleteLocation;
        private System.Windows.Forms.Button buttonLoadLocations;
        private System.Windows.Forms.Button buttonSaveLocations;
        private System.Windows.Forms.ListBox listBoxKnownLocations;
    }
}
