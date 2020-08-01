﻿namespace SRVTracker
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
            this.listBoxWaypoints = new System.Windows.Forms.ListBox();
            this.buttonSaveRoute = new System.Windows.Forms.Button();
            this.buttonLoadRoute = new System.Windows.Forms.Button();
            this.buttonSetAsTarget = new System.Windows.Forms.Button();
            this.buttonMoveWaypointDown = new System.Windows.Forms.Button();
            this.buttonMoveWaypointUp = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAddWaypoint = new System.Windows.Forms.Button();
            this.locationManager1 = new SRVTracker.LocationManager();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonLoadRoute);
            this.groupBox1.Controls.Add(this.buttonSaveRoute);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Route Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSetAsTarget);
            this.groupBox2.Controls.Add(this.buttonMoveWaypointDown);
            this.groupBox2.Controls.Add(this.buttonMoveWaypointUp);
            this.groupBox2.Controls.Add(this.buttonDelete);
            this.groupBox2.Controls.Add(this.buttonAddWaypoint);
            this.groupBox2.Controls.Add(this.listBoxWaypoints);
            this.groupBox2.Location = new System.Drawing.Point(12, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(429, 218);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Waypoints";
            // 
            // textBoxRouteName
            // 
            this.textBoxRouteName.Location = new System.Drawing.Point(9, 32);
            this.textBoxRouteName.Name = "textBoxRouteName";
            this.textBoxRouteName.Size = new System.Drawing.Size(294, 20);
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
            // listBoxWaypoints
            // 
            this.listBoxWaypoints.FormattingEnabled = true;
            this.listBoxWaypoints.Location = new System.Drawing.Point(9, 19);
            this.listBoxWaypoints.Name = "listBoxWaypoints";
            this.listBoxWaypoints.Size = new System.Drawing.Size(374, 186);
            this.listBoxWaypoints.TabIndex = 0;
            // 
            // buttonSaveRoute
            // 
            this.buttonSaveRoute.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSaveRoute.Location = new System.Drawing.Point(369, 30);
            this.buttonSaveRoute.Name = "buttonSaveRoute";
            this.buttonSaveRoute.Size = new System.Drawing.Size(54, 23);
            this.buttonSaveRoute.TabIndex = 3;
            this.buttonSaveRoute.UseVisualStyleBackColor = true;
            // 
            // buttonLoadRoute
            // 
            this.buttonLoadRoute.Image = global::SRVTracker.Properties.Resources.OpenFile_16x;
            this.buttonLoadRoute.Location = new System.Drawing.Point(309, 30);
            this.buttonLoadRoute.Name = "buttonLoadRoute";
            this.buttonLoadRoute.Size = new System.Drawing.Size(54, 23);
            this.buttonLoadRoute.TabIndex = 4;
            this.buttonLoadRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLoadRoute.UseVisualStyleBackColor = true;
            // 
            // buttonSetAsTarget
            // 
            this.buttonSetAsTarget.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonSetAsTarget.Location = new System.Drawing.Point(391, 182);
            this.buttonSetAsTarget.Name = "buttonSetAsTarget";
            this.buttonSetAsTarget.Size = new System.Drawing.Size(32, 23);
            this.buttonSetAsTarget.TabIndex = 5;
            this.buttonSetAsTarget.UseVisualStyleBackColor = true;
            // 
            // buttonMoveWaypointDown
            // 
            this.buttonMoveWaypointDown.Image = global::SRVTracker.Properties.Resources.ExpandDown_lg_16x;
            this.buttonMoveWaypointDown.Location = new System.Drawing.Point(389, 104);
            this.buttonMoveWaypointDown.Name = "buttonMoveWaypointDown";
            this.buttonMoveWaypointDown.Size = new System.Drawing.Size(34, 23);
            this.buttonMoveWaypointDown.TabIndex = 4;
            this.buttonMoveWaypointDown.UseVisualStyleBackColor = true;
            // 
            // buttonMoveWaypointUp
            // 
            this.buttonMoveWaypointUp.Image = global::SRVTracker.Properties.Resources.CollapseUp_lg_16x;
            this.buttonMoveWaypointUp.Location = new System.Drawing.Point(389, 77);
            this.buttonMoveWaypointUp.Name = "buttonMoveWaypointUp";
            this.buttonMoveWaypointUp.Size = new System.Drawing.Size(34, 23);
            this.buttonMoveWaypointUp.TabIndex = 3;
            this.buttonMoveWaypointUp.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = global::SRVTracker.Properties.Resources.Remove_color_16x;
            this.buttonDelete.Location = new System.Drawing.Point(389, 48);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(34, 23);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // buttonAddWaypoint
            // 
            this.buttonAddWaypoint.Image = global::SRVTracker.Properties.Resources.Add_16x;
            this.buttonAddWaypoint.Location = new System.Drawing.Point(389, 19);
            this.buttonAddWaypoint.Name = "buttonAddWaypoint";
            this.buttonAddWaypoint.Size = new System.Drawing.Size(34, 23);
            this.buttonAddWaypoint.TabIndex = 1;
            this.buttonAddWaypoint.UseVisualStyleBackColor = true;
            // 
            // locationManager1
            // 
            this.locationManager1.Location = new System.Drawing.Point(447, 12);
            this.locationManager1.Name = "locationManager1";
            this.locationManager1.Size = new System.Drawing.Size(281, 287);
            this.locationManager1.TabIndex = 2;
            // 
            // FormRoutePlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 311);
            this.Controls.Add(this.locationManager1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormRoutePlanner";
            this.Text = "Route Planner";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRouteName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonAddWaypoint;
        private System.Windows.Forms.ListBox listBoxWaypoints;
        private LocationManager locationManager1;
        private System.Windows.Forms.Button buttonLoadRoute;
        private System.Windows.Forms.Button buttonSaveRoute;
        private System.Windows.Forms.Button buttonSetAsTarget;
        private System.Windows.Forms.Button buttonMoveWaypointDown;
        private System.Windows.Forms.Button buttonMoveWaypointUp;
        private System.Windows.Forms.Button buttonDelete;
    }
}