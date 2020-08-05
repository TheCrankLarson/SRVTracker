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
            this.buttonTrackLocation = new System.Windows.Forms.Button();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.buttonEditLocation = new System.Windows.Forms.Button();
            this.buttonAddLocation = new System.Windows.Forms.Button();
            this.buttonDeleteLocation = new System.Windows.Forms.Button();
            this.buttonLoadLocations = new System.Windows.Forms.Button();
            this.buttonSaveLocations = new System.Windows.Forms.Button();
            this.listBoxLocations = new System.Windows.Forms.ListBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonTrackLocation);
            this.groupBox3.Controls.Add(this.buttonSaveAs);
            this.groupBox3.Controls.Add(this.buttonEditLocation);
            this.groupBox3.Controls.Add(this.buttonAddLocation);
            this.groupBox3.Controls.Add(this.buttonDeleteLocation);
            this.groupBox3.Controls.Add(this.buttonLoadLocations);
            this.groupBox3.Controls.Add(this.buttonSaveLocations);
            this.groupBox3.Controls.Add(this.listBoxLocations);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(281, 287);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Known Locations";
            // 
            // buttonTrackLocation
            // 
            this.buttonTrackLocation.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonTrackLocation.Location = new System.Drawing.Point(93, 258);
            this.buttonTrackLocation.Name = "buttonTrackLocation";
            this.buttonTrackLocation.Size = new System.Drawing.Size(23, 23);
            this.buttonTrackLocation.TabIndex = 7;
            this.buttonTrackLocation.UseVisualStyleBackColor = true;
            this.buttonTrackLocation.Click += new System.EventHandler(this.buttonTrackLocation_Click);
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveAs.Image = global::SRVTracker.Properties.Resources.SaveAs_16x;
            this.buttonSaveAs.Location = new System.Drawing.Point(235, 258);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(40, 23);
            this.buttonSaveAs.TabIndex = 6;
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // buttonEditLocation
            // 
            this.buttonEditLocation.Image = global::SRVTracker.Properties.Resources.Edit_16x;
            this.buttonEditLocation.Location = new System.Drawing.Point(64, 258);
            this.buttonEditLocation.Name = "buttonEditLocation";
            this.buttonEditLocation.Size = new System.Drawing.Size(23, 23);
            this.buttonEditLocation.TabIndex = 5;
            this.buttonEditLocation.UseVisualStyleBackColor = true;
            this.buttonEditLocation.Click += new System.EventHandler(this.buttonEditLocation_Click);
            // 
            // buttonAddLocation
            // 
            this.buttonAddLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddLocation.Image = global::SRVTracker.Properties.Resources.Add_16x;
            this.buttonAddLocation.Location = new System.Drawing.Point(6, 258);
            this.buttonAddLocation.Name = "buttonAddLocation";
            this.buttonAddLocation.Size = new System.Drawing.Size(23, 23);
            this.buttonAddLocation.TabIndex = 4;
            this.buttonAddLocation.UseVisualStyleBackColor = true;
            this.buttonAddLocation.Click += new System.EventHandler(this.buttonAddLocation_Click);
            // 
            // buttonDeleteLocation
            // 
            this.buttonDeleteLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteLocation.Image = global::SRVTracker.Properties.Resources.Remove_color_16x;
            this.buttonDeleteLocation.Location = new System.Drawing.Point(35, 258);
            this.buttonDeleteLocation.Name = "buttonDeleteLocation";
            this.buttonDeleteLocation.Size = new System.Drawing.Size(23, 23);
            this.buttonDeleteLocation.TabIndex = 3;
            this.buttonDeleteLocation.UseVisualStyleBackColor = true;
            this.buttonDeleteLocation.Click += new System.EventHandler(this.buttonDeleteLocation_Click);
            // 
            // buttonLoadLocations
            // 
            this.buttonLoadLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadLocations.Image = global::SRVTracker.Properties.Resources.OpenFile_16x;
            this.buttonLoadLocations.Location = new System.Drawing.Point(143, 258);
            this.buttonLoadLocations.Name = "buttonLoadLocations";
            this.buttonLoadLocations.Size = new System.Drawing.Size(40, 23);
            this.buttonLoadLocations.TabIndex = 2;
            this.buttonLoadLocations.UseVisualStyleBackColor = true;
            this.buttonLoadLocations.Click += new System.EventHandler(this.buttonLoadLocations_Click);
            // 
            // buttonSaveLocations
            // 
            this.buttonSaveLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveLocations.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSaveLocations.Location = new System.Drawing.Point(189, 258);
            this.buttonSaveLocations.Name = "buttonSaveLocations";
            this.buttonSaveLocations.Size = new System.Drawing.Size(40, 23);
            this.buttonSaveLocations.TabIndex = 1;
            this.buttonSaveLocations.UseVisualStyleBackColor = true;
            this.buttonSaveLocations.Click += new System.EventHandler(this.buttonSaveLocations_Click);
            // 
            // listBoxLocations
            // 
            this.listBoxLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLocations.DisplayMember = "Name";
            this.listBoxLocations.FormattingEnabled = true;
            this.listBoxLocations.Location = new System.Drawing.Point(6, 16);
            this.listBoxLocations.Name = "listBoxLocations";
            this.listBoxLocations.Size = new System.Drawing.Size(269, 238);
            this.listBoxLocations.TabIndex = 0;
            this.listBoxLocations.ValueMember = "Name";
            this.listBoxLocations.SelectedIndexChanged += new System.EventHandler(this.listBoxLocations_SelectedIndexChanged);
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
        private System.Windows.Forms.Button buttonAddLocation;
        private System.Windows.Forms.Button buttonDeleteLocation;
        private System.Windows.Forms.Button buttonLoadLocations;
        private System.Windows.Forms.Button buttonSaveLocations;
        private System.Windows.Forms.ListBox listBoxLocations;
        private System.Windows.Forms.Button buttonEditLocation;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.Button buttonTrackLocation;
    }
}
