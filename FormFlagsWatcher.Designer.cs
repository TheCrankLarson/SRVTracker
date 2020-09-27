namespace SRVTracker
{
    partial class FormFlagsWatcher
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
            this.listViewCurrentFlags = new System.Windows.Forms.ListView();
            this.columnHeaderFlagName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxStatusHistory = new System.Windows.Forms.ListBox();
            this.buttonShowCurrent = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxFlags = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listViewCurrentFlags);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 367);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status Flags";
            // 
            // listViewCurrentFlags
            // 
            this.listViewCurrentFlags.BackColor = System.Drawing.SystemColors.Window;
            this.listViewCurrentFlags.CheckBoxes = true;
            this.listViewCurrentFlags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFlagName});
            this.listViewCurrentFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCurrentFlags.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewCurrentFlags.HideSelection = false;
            this.listViewCurrentFlags.Location = new System.Drawing.Point(3, 16);
            this.listViewCurrentFlags.Name = "listViewCurrentFlags";
            this.listViewCurrentFlags.ShowGroups = false;
            this.listViewCurrentFlags.Size = new System.Drawing.Size(236, 348);
            this.listViewCurrentFlags.TabIndex = 0;
            this.listViewCurrentFlags.UseCompatibleStateImageBehavior = false;
            this.listViewCurrentFlags.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderFlagName
            // 
            this.columnHeaderFlagName.Text = "Status Flag Name";
            this.columnHeaderFlagName.Width = 200;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxStatusHistory);
            this.groupBox2.Location = new System.Drawing.Point(260, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 367);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "History";
            // 
            // listBoxStatusHistory
            // 
            this.listBoxStatusHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxStatusHistory.FormattingEnabled = true;
            this.listBoxStatusHistory.Location = new System.Drawing.Point(3, 16);
            this.listBoxStatusHistory.Name = "listBoxStatusHistory";
            this.listBoxStatusHistory.Size = new System.Drawing.Size(194, 348);
            this.listBoxStatusHistory.TabIndex = 0;
            this.listBoxStatusHistory.SelectedIndexChanged += new System.EventHandler(this.listBoxStatusHistory_SelectedIndexChanged);
            // 
            // buttonShowCurrent
            // 
            this.buttonShowCurrent.Location = new System.Drawing.Point(15, 385);
            this.buttonShowCurrent.Name = "buttonShowCurrent";
            this.buttonShowCurrent.Size = new System.Drawing.Size(122, 23);
            this.buttonShowCurrent.TabIndex = 2;
            this.buttonShowCurrent.Text = "Show current status";
            this.buttonShowCurrent.UseVisualStyleBackColor = true;
            this.buttonShowCurrent.Click += new System.EventHandler(this.buttonShowCurrent_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(382, 385);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxFlags
            // 
            this.textBoxFlags.Location = new System.Drawing.Point(143, 387);
            this.textBoxFlags.Name = "textBoxFlags";
            this.textBoxFlags.Size = new System.Drawing.Size(100, 20);
            this.textBoxFlags.TabIndex = 4;
            this.textBoxFlags.TextChanged += new System.EventHandler(this.textBoxFlags_TextChanged);
            // 
            // FormFlagsWatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 418);
            this.Controls.Add(this.textBoxFlags);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonShowCurrent);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormFlagsWatcher";
            this.Text = "FormFlagsWatcher";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewCurrentFlags;
        private System.Windows.Forms.ColumnHeader columnHeaderFlagName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxStatusHistory;
        private System.Windows.Forms.Button buttonShowCurrent;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxFlags;
    }
}