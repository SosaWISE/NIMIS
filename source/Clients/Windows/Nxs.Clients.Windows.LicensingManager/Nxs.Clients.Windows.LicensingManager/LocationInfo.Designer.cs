namespace PPro.WindowsClients.LicenseManagement
{
    partial class LocationInfo
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationInfo));
			this.gvInfo = new DevExpress.XtraGrid.GridControl();
			this.gvInfoMain = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.btnOkay = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.gvInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvInfoMain)).BeginInit();
			this.SuspendLayout();
			// 
			// gvInfo
			// 
			this.gvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gvInfo.Location = new System.Drawing.Point(13, 13);
			this.gvInfo.MainView = this.gvInfoMain;
			this.gvInfo.Name = "gvInfo";
			this.gvInfo.Size = new System.Drawing.Size(267, 218);
			this.gvInfo.TabIndex = 0;
			this.gvInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInfoMain});
			// 
			// gvInfoMain
			// 
			this.gvInfoMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
			this.gvInfoMain.GridControl = this.gvInfo;
			this.gvInfoMain.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CreditsRan", null, "{0} Credits Ran")});
			this.gvInfoMain.Name = "gvInfoMain";
			this.gvInfoMain.OptionsBehavior.Editable = false;
			this.gvInfoMain.OptionsView.ShowFooter = true;
			this.gvInfoMain.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "Rep";
			this.gridColumn1.FieldName = "FullName";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "# of Credits Ran";
			this.gridColumn2.FieldName = "CreditsRan";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 70;
			// 
			// btnOkay
			// 
			this.btnOkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOkay.Location = new System.Drawing.Point(205, 237);
			this.btnOkay.Name = "btnOkay";
			this.btnOkay.Size = new System.Drawing.Size(75, 23);
			this.btnOkay.TabIndex = 1;
			this.btnOkay.Text = "Okay";
			this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
			// 
			// LocationInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.btnOkay);
			this.Controls.Add(this.gvInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LocationInfo";
			this.Text = "Info";
			this.Activated += new System.EventHandler(this.LocationInfo_Activated);
			this.Load += new System.EventHandler(this.LocationInfo_Load);
			((System.ComponentModel.ISupportInitialize)(this.gvInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvInfoMain)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gvInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInfoMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton btnOkay;
    }
}