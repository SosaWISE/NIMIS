namespace PPro.WindowsClients.LicenseManagement
{
    partial class UploadExcelFile
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadExcelFile));
			this.btnUpload = new DevExpress.XtraEditors.SimpleButton();
			this.ofdExcelFile = new System.Windows.Forms.OpenFileDialog();
			this.gcStateCounty = new DevExpress.XtraGrid.GridControl();
			this.gvCountyState = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.tbEntry = new System.Windows.Forms.TextBox();
			this.tbAccountIDCol = new System.Windows.Forms.TextBox();
			this.tbPermitCol = new System.Windows.Forms.TextBox();
			this.tbSubmitDate = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.gcStateCounty)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvCountyState)).BeginInit();
			this.SuspendLayout();
			// 
			// btnUpload
			// 
			this.btnUpload.Location = new System.Drawing.Point(12, 12);
			this.btnUpload.Name = "btnUpload";
			this.btnUpload.Size = new System.Drawing.Size(75, 23);
			this.btnUpload.TabIndex = 0;
			this.btnUpload.Text = "Upload File";
			this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
			// 
			// ofdExcelFile
			// 
			this.ofdExcelFile.FileName = "openFileDialog1";
			// 
			// gcStateCounty
			// 
			this.gcStateCounty.Location = new System.Drawing.Point(13, 42);
			this.gcStateCounty.MainView = this.gvCountyState;
			this.gcStateCounty.Name = "gcStateCounty";
			this.gcStateCounty.Size = new System.Drawing.Size(400, 200);
			this.gcStateCounty.TabIndex = 1;
			this.gcStateCounty.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCountyState});
			// 
			// gvCountyState
			// 
			this.gvCountyState.GridControl = this.gcStateCounty;
			this.gvCountyState.Name = "gvCountyState";
			this.gvCountyState.OptionsSelection.MultiSelect = true;
			// 
			// tbEntry
			// 
			this.tbEntry.Location = new System.Drawing.Point(94, 14);
			this.tbEntry.Name = "tbEntry";
			this.tbEntry.Size = new System.Drawing.Size(100, 20);
			this.tbEntry.TabIndex = 2;
			this.tbEntry.Text = "RequirementID";
			// 
			// tbAccountIDCol
			// 
			this.tbAccountIDCol.Location = new System.Drawing.Point(201, 14);
			this.tbAccountIDCol.Name = "tbAccountIDCol";
			this.tbAccountIDCol.Size = new System.Drawing.Size(100, 20);
			this.tbAccountIDCol.TabIndex = 3;
			this.tbAccountIDCol.Text = "AccountID Col";
			// 
			// tbPermitCol
			// 
			this.tbPermitCol.Location = new System.Drawing.Point(308, 13);
			this.tbPermitCol.Name = "tbPermitCol";
			this.tbPermitCol.Size = new System.Drawing.Size(100, 20);
			this.tbPermitCol.TabIndex = 4;
			this.tbPermitCol.Text = "Permit Col";
			// 
			// tbSubmitDate
			// 
			this.tbSubmitDate.Location = new System.Drawing.Point(415, 12);
			this.tbSubmitDate.Name = "tbSubmitDate";
			this.tbSubmitDate.Size = new System.Drawing.Size(100, 20);
			this.tbSubmitDate.TabIndex = 5;
			// 
			// UploadExcelFile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(555, 361);
			this.Controls.Add(this.tbSubmitDate);
			this.Controls.Add(this.tbPermitCol);
			this.Controls.Add(this.tbAccountIDCol);
			this.Controls.Add(this.tbEntry);
			this.Controls.Add(this.gcStateCounty);
			this.Controls.Add(this.btnUpload);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "UploadExcelFile";
			this.Text = "UploadExcelFile";
			((System.ComponentModel.ISupportInitialize)(this.gcStateCounty)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvCountyState)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnUpload;
        private System.Windows.Forms.OpenFileDialog ofdExcelFile;
        private DevExpress.XtraGrid.GridControl gcStateCounty;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCountyState;
        private System.Windows.Forms.TextBox tbEntry;
        private System.Windows.Forms.TextBox tbAccountIDCol;
        private System.Windows.Forms.TextBox tbPermitCol;
        private System.Windows.Forms.TextBox tbSubmitDate;
    }
}