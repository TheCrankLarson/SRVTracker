namespace SRVTracker
{
    partial class FormRoutePlanner
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPlanetName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRadius = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxKnownLocations = new System.Windows.Forms.ListBox();
            this.buttonSaveLocations = new System.Windows.Forms.Button();
            this.buttonLoadLocations = new System.Windows.Forms.Button();
            this.buttonDeleteLocation = new System.Windows.Forms.Button();
            this.buttonAddEditLocation = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxRadius);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxPlanetName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Route Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(12, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 133);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Waypoints";
            // 
            // textBoxRouteName
            // 
            this.textBoxRouteName.Location = new System.Drawing.Point(9, 32);
            this.textBoxRouteName.Name = "textBoxRouteName";
            this.textBoxRouteName.Size = new System.Drawing.Size(159, 20);
            this.textBoxRouteName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // textBoxPlanetName
            // 
            this.textBoxPlanetName.Location = new System.Drawing.Point(174, 32);
            this.textBoxPlanetName.Name = "textBoxPlanetName";
            this.textBoxPlanetName.Size = new System.Drawing.Size(141, 20);
            this.textBoxPlanetName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Planet:";
            // 
            // textBoxRadius
            // 
            this.textBoxRadius.Location = new System.Drawing.Point(321, 32);
            this.textBoxRadius.Name = "textBoxRadius";
            this.textBoxRadius.Size = new System.Drawing.Size(100, 20);
            this.textBoxRadius.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Radius:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonAddEditLocation);
            this.groupBox3.Controls.Add(this.buttonDeleteLocation);
            this.groupBox3.Controls.Add(this.buttonLoadLocations);
            this.groupBox3.Controls.Add(this.buttonSaveLocations);
            this.groupBox3.Controls.Add(this.listBoxKnownLocations);
            this.groupBox3.Location = new System.Drawing.Point(447, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(274, 278);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Known Locations";
            // 
            // listBoxKnownLocations
            // 
            this.listBoxKnownLocations.FormattingEnabled = true;
            this.listBoxKnownLocations.Location = new System.Drawing.Point(6, 16);
            this.listBoxKnownLocations.Name = "listBoxKnownLocations";
            this.listBoxKnownLocations.Size = new System.Drawing.Size(262, 225);
            this.listBoxKnownLocations.TabIndex = 0;
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
            // buttonLoadLocations
            // 
            this.buttonLoadLocations.Location = new System.Drawing.Point(156, 247);
            this.buttonLoadLocations.Name = "buttonLoadLocations";
            this.buttonLoadLocations.Size = new System.Drawing.Size(53, 23);
            this.buttonLoadLocations.TabIndex = 2;
            this.buttonLoadLocations.Text = "Load...";
            this.buttonLoadLocations.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteLocation
            // 
            this.buttonDeleteLocation.Location = new System.Drawing.Point(65, 247);
            this.buttonDeleteLocation.Name = "buttonDeleteLocation";
            this.buttonDeleteLocation.Size = new System.Drawing.Size(53, 23);
            this.buttonDeleteLocation.TabIndex = 3;
            this.buttonDeleteLocation.Text = "Delete";
            this.buttonDeleteLocation.UseVisualStyleBackColor = true;
            this.buttonDeleteLocation.Click += new System.EventHandler(this.buttonDeleteLocation_Click);
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
            // FormRoutePlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormRoutePlanner";
            this.Text = "FormRoutePlanner";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxRadius;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPlanetName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRouteName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonAddEditLocation;
        private System.Windows.Forms.Button buttonDeleteLocation;
        private System.Windows.Forms.Button buttonLoadLocations;
        private System.Windows.Forms.Button buttonSaveLocations;
        private System.Windows.Forms.ListBox listBoxKnownLocations;
    }
}