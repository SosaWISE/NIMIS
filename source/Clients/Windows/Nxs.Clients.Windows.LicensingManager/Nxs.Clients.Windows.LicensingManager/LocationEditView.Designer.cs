namespace PPro.WindowsClients.LicenseManagement
{
    partial class LocationEditView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationEditView));
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.tbLocationName = new DevExpress.XtraEditors.TextEdit();
			this.lbLocationName = new DevExpress.XtraEditors.LabelControl();
			this.lbAbbreviation = new DevExpress.XtraEditors.LabelControl();
			this.tbAbbreviation = new DevExpress.XtraEditors.TextEdit();
			this.cbCanSolicit = new DevExpress.XtraEditors.CheckEdit();
			this.lbCanSolicit = new DevExpress.XtraEditors.LabelControl();
			this.lbModifiedBy = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.tbLocationName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbAbbreviation.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cbCanSolicit.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(269, 85);
			this.btnCancel.LookAndFeel.SkinName = "The Asphalt World";
			this.btnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(188, 85);
			this.btnSave.LookAndFeel.SkinName = "The Asphalt World";
			this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// tbLocationName
			// 
			this.tbLocationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbLocationName.Location = new System.Drawing.Point(187, 9);
			this.tbLocationName.Name = "tbLocationName";
			this.tbLocationName.Size = new System.Drawing.Size(153, 20);
			this.tbLocationName.TabIndex = 2;
			// 
			// lbLocationName
			// 
			this.lbLocationName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lbLocationName.Location = new System.Drawing.Point(12, 12);
			this.lbLocationName.Name = "lbLocationName";
			this.lbLocationName.Size = new System.Drawing.Size(35, 13);
			this.lbLocationName.TabIndex = 3;
			this.lbLocationName.Text = "Name:";
			// 
			// lbAbbreviation
			// 
			this.lbAbbreviation.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lbAbbreviation.Location = new System.Drawing.Point(12, 38);
			this.lbAbbreviation.Name = "lbAbbreviation";
			this.lbAbbreviation.Size = new System.Drawing.Size(76, 13);
			this.lbAbbreviation.TabIndex = 4;
			this.lbAbbreviation.Text = "Abbreviation:";
			// 
			// tbAbbreviation
			// 
			this.tbAbbreviation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbAbbreviation.Location = new System.Drawing.Point(187, 31);
			this.tbAbbreviation.Name = "tbAbbreviation";
			this.tbAbbreviation.Size = new System.Drawing.Size(88, 20);
			this.tbAbbreviation.TabIndex = 5;
			// 
			// cbCanSolicit
			// 
			this.cbCanSolicit.EditValue = true;
			this.cbCanSolicit.Location = new System.Drawing.Point(185, 55);
			this.cbCanSolicit.Name = "cbCanSolicit";
			this.cbCanSolicit.Properties.Caption = "";
			this.cbCanSolicit.Size = new System.Drawing.Size(75, 19);
			this.cbCanSolicit.TabIndex = 8;
			// 
			// lbCanSolicit
			// 
			this.lbCanSolicit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lbCanSolicit.Location = new System.Drawing.Point(12, 57);
			this.lbCanSolicit.Name = "lbCanSolicit";
			this.lbCanSolicit.Size = new System.Drawing.Size(105, 13);
			this.lbCanSolicit.TabIndex = 9;
			this.lbCanSolicit.Text = "Allows Solicitation:";
			// 
			// lbModifiedBy
			// 
			this.lbModifiedBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbModifiedBy.Location = new System.Drawing.Point(12, 94);
			this.lbModifiedBy.Name = "lbModifiedBy";
			this.lbModifiedBy.Size = new System.Drawing.Size(0, 13);
			this.lbModifiedBy.TabIndex = 10;
			// 
			// LocationEditView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(352, 120);
			this.Controls.Add(this.lbModifiedBy);
			this.Controls.Add(this.lbCanSolicit);
			this.Controls.Add(this.cbCanSolicit);
			this.Controls.Add(this.tbAbbreviation);
			this.Controls.Add(this.lbAbbreviation);
			this.Controls.Add(this.lbLocationName);
			this.Controls.Add(this.tbLocationName);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LocationEditView";
			this.Text = "Location";
			this.Activated += new System.EventHandler(this.LocationEditView_Activated);
			this.Load += new System.EventHandler(this.LocationEditView_Load);
			((System.ComponentModel.ISupportInitialize)(this.tbLocationName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbAbbreviation.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cbCanSolicit.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit tbLocationName;
        private DevExpress.XtraEditors.LabelControl lbLocationName;
        private DevExpress.XtraEditors.LabelControl lbAbbreviation;
        private DevExpress.XtraEditors.TextEdit tbAbbreviation;
        private DevExpress.XtraEditors.CheckEdit cbCanSolicit;
        private DevExpress.XtraEditors.LabelControl lbCanSolicit;
        private DevExpress.XtraEditors.LabelControl lbModifiedBy;

    }
}