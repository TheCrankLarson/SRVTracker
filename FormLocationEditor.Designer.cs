namespace SRVTracker
{
    partial class FormLocationEditor
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
            this.locationManager1 = new SRVTracker.LocationManager();
            this.SuspendLayout();
            // 
            // locationManager1
            // 
            this.locationManager1.AllowSelectionOnly = false;
            this.locationManager1.Location = new System.Drawing.Point(0, 0);
            this.locationManager1.LocatorForm = null;
            this.locationManager1.Name = "locationManager1";
            this.locationManager1.Size = new System.Drawing.Size(281, 287);
            this.locationManager1.TabIndex = 0;
            // 
            // FormLocationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 287);
            this.Controls.Add(this.locationManager1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLocationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormLocationEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private LocationManager locationManager1;
    }
}