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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSaveRoute = new System.Windows.Forms.Button();
            this.buttonLoadRoute = new System.Windows.Forms.Button();
            this.buttonSaveRouteAs = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownRadius = new System.Windows.Forms.NumericUpDown();
            this.checkBoxRadius = new System.Windows.Forms.CheckBox();
            this.buttonSetAsTarget = new System.Windows.Forms.Button();
            this.buttonMoveWaypointDown = new System.Windows.Forms.Button();
            this.buttonMoveWaypointUp = new System.Windows.Forms.Button();
            this.buttonDeleteWaypoint = new System.Windows.Forms.Button();
            this.buttonAddWaypoint = new System.Windows.Forms.Button();
            this.listBoxWaypoints = new System.Windows.Forms.ListBox();
            this.locationManager = new SRVTracker.LocationManager();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSaveRoute);
            this.groupBox1.Controls.Add(this.buttonLoadRoute);
            this.groupBox1.Controls.Add(this.buttonSaveRouteAs);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Route Information";
            // 
            // buttonSaveRoute
            // 
            this.buttonSaveRoute.Enabled = false;
            this.buttonSaveRoute.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSaveRoute.Location = new System.Drawing.Point(384, 30);
            this.buttonSaveRoute.Name = "buttonSaveRoute";
            this.buttonSaveRoute.Size = new System.Drawing.Size(39, 23);
            this.buttonSaveRoute.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonSaveRoute, "Save this route (overwrite existing file)");
            this.buttonSaveRoute.UseVisualStyleBackColor = true;
            this.buttonSaveRoute.Click += new System.EventHandler(this.buttonSaveRoute_Click);
            // 
            // buttonLoadRoute
            // 
            this.buttonLoadRoute.Image = global::SRVTracker.Properties.Resources.OpenFile_16x;
            this.buttonLoadRoute.Location = new System.Drawing.Point(301, 30);
            this.buttonLoadRoute.Name = "buttonLoadRoute";
            this.buttonLoadRoute.Size = new System.Drawing.Size(40, 23);
            this.buttonLoadRoute.TabIndex = 4;
            this.buttonLoadRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.buttonLoadRoute, "Load route file");
            this.buttonLoadRoute.UseVisualStyleBackColor = true;
            this.buttonLoadRoute.Click += new System.EventHandler(this.buttonLoadRoute_Click);
            // 
            // buttonSaveRouteAs
            // 
            this.buttonSaveRouteAs.Image = global::SRVTracker.Properties.Resources.SaveAs_16x;
            this.buttonSaveRouteAs.Location = new System.Drawing.Point(343, 30);
            this.buttonSaveRouteAs.Name = "buttonSaveRouteAs";
            this.buttonSaveRouteAs.Size = new System.Drawing.Size(39, 23);
            this.buttonSaveRouteAs.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonSaveRouteAs, "Save this route as a file");
            this.buttonSaveRouteAs.UseVisualStyleBackColor = true;
            this.buttonSaveRouteAs.Click += new System.EventHandler(this.buttonSaveRouteAs_Click);
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
            // textBoxRouteName
            // 
            this.textBoxRouteName.Location = new System.Drawing.Point(9, 32);
            this.textBoxRouteName.Name = "textBoxRouteName";
            this.textBoxRouteName.Size = new System.Drawing.Size(286, 20);
            this.textBoxRouteName.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxRouteName, "Name of the route");
            this.textBoxRouteName.TextChanged += new System.EventHandler(this.textBoxRouteName_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.buttonSetAsTarget);
            this.groupBox2.Controls.Add(this.buttonMoveWaypointDown);
            this.groupBox2.Controls.Add(this.buttonMoveWaypointUp);
            this.groupBox2.Controls.Add(this.buttonDeleteWaypoint);
            this.groupBox2.Controls.Add(this.buttonAddWaypoint);
            this.groupBox2.Controls.Add(this.listBoxWaypoints);
            this.groupBox2.Location = new System.Drawing.Point(12, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(429, 218);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Waypoints";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownRadius);
            this.groupBox3.Controls.Add(this.checkBoxRadius);
            this.groupBox3.Location = new System.Drawing.Point(9, 164);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(412, 48);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Waypoint match conditions";
            // 
            // numericUpDownRadius
            // 
            this.numericUpDownRadius.Enabled = false;
            this.numericUpDownRadius.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRadius.Location = new System.Drawing.Point(91, 18);
            this.numericUpDownRadius.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRadius.Name = "numericUpDownRadius";
            this.numericUpDownRadius.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownRadius.TabIndex = 1;
            this.toolTip1.SetToolTip(this.numericUpDownRadius, "Radius in meters of the circle that marks the waypoint boundary\r\n(the waypoint is" +
        " the centre)");
            this.numericUpDownRadius.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownRadius.ValueChanged += new System.EventHandler(this.numericUpDownRadius_ValueChanged);
            // 
            // checkBoxRadius
            // 
            this.checkBoxRadius.AutoSize = true;
            this.checkBoxRadius.Checked = true;
            this.checkBoxRadius.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRadius.Enabled = false;
            this.checkBoxRadius.Location = new System.Drawing.Point(6, 19);
            this.checkBoxRadius.Name = "checkBoxRadius";
            this.checkBoxRadius.Size = new System.Drawing.Size(79, 17);
            this.checkBoxRadius.TabIndex = 0;
            this.checkBoxRadius.Text = "Radius (m):";
            this.toolTip1.SetToolTip(this.checkBoxRadius, "Whether radius is taken into consideration when\r\ndeterming if a location is withi" +
        "n a waypoint");
            this.checkBoxRadius.UseVisualStyleBackColor = true;
            // 
            // buttonSetAsTarget
            // 
            this.buttonSetAsTarget.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonSetAsTarget.Location = new System.Drawing.Point(389, 133);
            this.buttonSetAsTarget.Name = "buttonSetAsTarget";
            this.buttonSetAsTarget.Size = new System.Drawing.Size(32, 23);
            this.buttonSetAsTarget.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonSetAsTarget, "Track (locate) the current selecte waypoint.\r\nThe Locator must already be open.");
            this.buttonSetAsTarget.UseVisualStyleBackColor = true;
            this.buttonSetAsTarget.Click += new System.EventHandler(this.buttonSetAsTarget_Click);
            // 
            // buttonMoveWaypointDown
            // 
            this.buttonMoveWaypointDown.Image = global::SRVTracker.Properties.Resources.ExpandDown_lg_16x;
            this.buttonMoveWaypointDown.Location = new System.Drawing.Point(389, 104);
            this.buttonMoveWaypointDown.Name = "buttonMoveWaypointDown";
            this.buttonMoveWaypointDown.Size = new System.Drawing.Size(34, 23);
            this.buttonMoveWaypointDown.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonMoveWaypointDown, "Move the waypoint down the list");
            this.buttonMoveWaypointDown.UseVisualStyleBackColor = true;
            this.buttonMoveWaypointDown.Click += new System.EventHandler(this.buttonMoveWaypointDown_Click);
            // 
            // buttonMoveWaypointUp
            // 
            this.buttonMoveWaypointUp.Image = global::SRVTracker.Properties.Resources.CollapseUp_lg_16x;
            this.buttonMoveWaypointUp.Location = new System.Drawing.Point(389, 77);
            this.buttonMoveWaypointUp.Name = "buttonMoveWaypointUp";
            this.buttonMoveWaypointUp.Size = new System.Drawing.Size(34, 23);
            this.buttonMoveWaypointUp.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonMoveWaypointUp, "Move the waypoint up the list");
            this.buttonMoveWaypointUp.UseVisualStyleBackColor = true;
            this.buttonMoveWaypointUp.Click += new System.EventHandler(this.buttonMoveWaypointUp_Click);
            // 
            // buttonDeleteWaypoint
            // 
            this.buttonDeleteWaypoint.Image = global::SRVTracker.Properties.Resources.Remove_color_16x;
            this.buttonDeleteWaypoint.Location = new System.Drawing.Point(389, 48);
            this.buttonDeleteWaypoint.Name = "buttonDeleteWaypoint";
            this.buttonDeleteWaypoint.Size = new System.Drawing.Size(34, 23);
            this.buttonDeleteWaypoint.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonDeleteWaypoint, "Delete the currently selected waypoint");
            this.buttonDeleteWaypoint.UseVisualStyleBackColor = true;
            this.buttonDeleteWaypoint.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAddWaypoint
            // 
            this.buttonAddWaypoint.Image = global::SRVTracker.Properties.Resources.Add_16x;
            this.buttonAddWaypoint.Location = new System.Drawing.Point(389, 19);
            this.buttonAddWaypoint.Name = "buttonAddWaypoint";
            this.buttonAddWaypoint.Size = new System.Drawing.Size(34, 23);
            this.buttonAddWaypoint.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonAddWaypoint, "Add the currently selected location as a waypoint");
            this.buttonAddWaypoint.UseVisualStyleBackColor = true;
            this.buttonAddWaypoint.Click += new System.EventHandler(this.buttonAddWaypoint_Click);
            // 
            // listBoxWaypoints
            // 
            this.listBoxWaypoints.DisplayMember = "Name";
            this.listBoxWaypoints.FormattingEnabled = true;
            this.listBoxWaypoints.Location = new System.Drawing.Point(9, 19);
            this.listBoxWaypoints.Name = "listBoxWaypoints";
            this.listBoxWaypoints.Size = new System.Drawing.Size(374, 134);
            this.listBoxWaypoints.TabIndex = 0;
            this.toolTip1.SetToolTip(this.listBoxWaypoints, "Waypoints that make up this route");
            this.listBoxWaypoints.ValueMember = "Name";
            this.listBoxWaypoints.SelectedIndexChanged += new System.EventHandler(this.listBoxWaypoints_SelectedIndexChanged);
            // 
            // locationManager
            // 
            this.locationManager.Location = new System.Drawing.Point(447, 12);
            this.locationManager.LocatorForm = null;
            this.locationManager.Name = "locationManager";
            this.locationManager.Size = new System.Drawing.Size(281, 287);
            this.locationManager.TabIndex = 2;
            // 
            // FormRoutePlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 311);
            this.Controls.Add(this.locationManager);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormRoutePlanner";
            this.Text = "Route Planner";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRouteName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonAddWaypoint;
        private System.Windows.Forms.ListBox listBoxWaypoints;
        private LocationManager locationManager;
        private System.Windows.Forms.Button buttonLoadRoute;
        private System.Windows.Forms.Button buttonSaveRouteAs;
        private System.Windows.Forms.Button buttonSetAsTarget;
        private System.Windows.Forms.Button buttonMoveWaypointDown;
        private System.Windows.Forms.Button buttonMoveWaypointUp;
        private System.Windows.Forms.Button buttonDeleteWaypoint;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownRadius;
        private System.Windows.Forms.CheckBox checkBoxRadius;
        private System.Windows.Forms.Button buttonSaveRoute;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}