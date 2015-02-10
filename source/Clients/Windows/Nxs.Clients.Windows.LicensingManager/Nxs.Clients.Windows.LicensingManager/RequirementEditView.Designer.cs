namespace PPro.WindowsClients.LicenseManagement
{
    partial class RequirementEditView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequirementEditView));
			this.gcItems = new DevExpress.XtraEditors.GroupControl();
			this.btnAddItem = new DevExpress.XtraEditors.SimpleButton();
			this.gvItems = new DevExpress.XtraGrid.GridControl();
			this.gvItemsView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gcGeneral = new DevExpress.XtraEditors.GroupControl();
			this.cbRequiredForFunding = new System.Windows.Forms.CheckBox();
			this.tbFees = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.tbCallCenterMessage = new DevExpress.XtraEditors.MemoEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.lkLock = new DevExpress.XtraEditors.LookUpEdit();
			this.lkAgency = new DevExpress.XtraEditors.LookUpEdit();
			this.lbAgency = new DevExpress.XtraEditors.LabelControl();
			this.lbLock = new DevExpress.XtraEditors.LabelControl();
			this.tbDesciption = new DevExpress.XtraEditors.TextEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.tbRequirementName = new DevExpress.XtraEditors.TextEdit();
			this.lbName = new DevExpress.XtraEditors.LabelControl();
			this.gcNotes = new DevExpress.XtraEditors.GroupControl();
			this.btnAddNote = new DevExpress.XtraEditors.SimpleButton();
			this.gvNotes = new DevExpress.XtraGrid.GridControl();
			this.gvNotesMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.lbModifiedBy = new DevExpress.XtraEditors.LabelControl();
			this.tbTemplateID = new DevExpress.XtraEditors.TextEdit();
			this.lbTemplateID = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.gcItems)).BeginInit();
			this.gcItems.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvItemsView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gcGeneral)).BeginInit();
			this.gcGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbFees.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbCallCenterMessage.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkLock.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkAgency.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbDesciption.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbRequirementName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gcNotes)).BeginInit();
			this.gcNotes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gvNotes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvNotesMainView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbTemplateID.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// gcItems
			// 
			this.gcItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gcItems.Controls.Add(this.btnAddItem);
			this.gcItems.Controls.Add(this.gvItems);
			this.gcItems.Location = new System.Drawing.Point(12, 263);
			this.gcItems.LookAndFeel.SkinName = "The Asphalt World";
			this.gcItems.LookAndFeel.UseDefaultLookAndFeel = false;
			this.gcItems.Name = "gcItems";
			this.gcItems.Size = new System.Drawing.Size(431, 198);
			this.gcItems.TabIndex = 6;
			this.gcItems.Text = "Items";
			// 
			// btnAddItem
			// 
			this.btnAddItem.Location = new System.Drawing.Point(6, 24);
			this.btnAddItem.LookAndFeel.SkinName = "The Asphalt World";
			this.btnAddItem.LookAndFeel.UseDefaultLookAndFeel = false;
			this.btnAddItem.Name = "btnAddItem";
			this.btnAddItem.Size = new System.Drawing.Size(75, 23);
			this.btnAddItem.TabIndex = 1;
			this.btnAddItem.Text = "Add";
			this.btnAddItem.Click += new System.EventHandler(this.BtnAddItemClick);
			// 
			// gvItems
			// 
			this.gvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gvItems.Location = new System.Drawing.Point(0, 53);
			this.gvItems.LookAndFeel.SkinName = "The Asphalt World";
			this.gvItems.LookAndFeel.UseDefaultLookAndFeel = false;
			this.gvItems.MainView = this.gvItemsView;
			this.gvItems.Name = "gvItems";
			this.gvItems.Size = new System.Drawing.Size(426, 140);
			this.gvItems.TabIndex = 0;
			this.gvItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItemsView,
            this.gridView2});
			// 
			// gvItemsView
			// 
			this.gvItemsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
			this.gvItemsView.GridControl = this.gvItems;
			this.gvItemsView.Name = "gvItemsView";
			this.gvItemsView.OptionsBehavior.Editable = false;
			this.gvItemsView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvItemsView_KeyDown);
			this.gvItemsView.DoubleClick += new System.EventHandler(this.gvItemsView_DoubleClick);
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "ItemID";
			this.gridColumn1.FieldName = "RequirementItemID";
			this.gridColumn1.Name = "gridColumn1";
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "Name";
			this.gridColumn2.FieldName = "Name";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 0;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "Description";
			this.gridColumn3.FieldName = "Description";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 1;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "Created By";
			this.gridColumn4.FieldName = "CreatedByID";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 2;
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "Date";
			this.gridColumn5.FieldName = "CreatedByDate";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 3;
			// 
			// gridView2
			// 
			this.gridView2.GridControl = this.gvItems;
			this.gridView2.Name = "gridView2";
			// 
			// gcGeneral
			// 
			this.gcGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gcGeneral.Controls.Add(this.cbRequiredForFunding);
			this.gcGeneral.Controls.Add(this.tbFees);
			this.gcGeneral.Controls.Add(this.labelControl3);
			this.gcGeneral.Controls.Add(this.tbCallCenterMessage);
			this.gcGeneral.Controls.Add(this.labelControl2);
			this.gcGeneral.Controls.Add(this.lkLock);
			this.gcGeneral.Controls.Add(this.lkAgency);
			this.gcGeneral.Controls.Add(this.lbAgency);
			this.gcGeneral.Controls.Add(this.lbLock);
			this.gcGeneral.Controls.Add(this.tbDesciption);
			this.gcGeneral.Controls.Add(this.labelControl1);
			this.gcGeneral.Controls.Add(this.tbRequirementName);
			this.gcGeneral.Controls.Add(this.lbName);
			this.gcGeneral.Location = new System.Drawing.Point(12, 12);
			this.gcGeneral.LookAndFeel.SkinName = "The Asphalt World";
			this.gcGeneral.LookAndFeel.UseDefaultLookAndFeel = false;
			this.gcGeneral.Name = "gcGeneral";
			this.gcGeneral.Size = new System.Drawing.Size(431, 245);
			this.gcGeneral.TabIndex = 1;
			this.gcGeneral.Text = "General Information";
			// 
			// cbRequiredForFunding
			// 
			this.cbRequiredForFunding.AutoSize = true;
			this.cbRequiredForFunding.Location = new System.Drawing.Point(117, 221);
			this.cbRequiredForFunding.Name = "cbRequiredForFunding";
			this.cbRequiredForFunding.Size = new System.Drawing.Size(125, 17);
			this.cbRequiredForFunding.TabIndex = 14;
			this.cbRequiredForFunding.Text = "Required for Funding";
			this.cbRequiredForFunding.UseVisualStyleBackColor = true;
			// 
			// tbFees
			// 
			this.tbFees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbFees.Location = new System.Drawing.Point(117, 194);
			this.tbFees.Name = "tbFees";
			this.tbFees.Size = new System.Drawing.Size(309, 20);
			this.tbFees.TabIndex = 13;
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.labelControl3.Location = new System.Drawing.Point(6, 197);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(29, 13);
			this.labelControl3.TabIndex = 12;
			this.labelControl3.Text = "Fees:";
			// 
			// tbCallCenterMessage
			// 
			this.tbCallCenterMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbCallCenterMessage.Location = new System.Drawing.Point(117, 132);
			this.tbCallCenterMessage.Name = "tbCallCenterMessage";
			this.tbCallCenterMessage.Size = new System.Drawing.Size(309, 56);
			this.tbCallCenterMessage.TabIndex = 11;
			// 
			// labelControl2
			// 
			this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.labelControl2.Location = new System.Drawing.Point(6, 135);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(53, 13);
			this.labelControl2.TabIndex = 10;
			this.labelControl2.Text = "Message:";
			// 
			// lkLock
			// 
			this.lkLock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lkLock.EditValue = "Select One";
			this.lkLock.Location = new System.Drawing.Point(117, 102);
			this.lkLock.Name = "lkLock";
			this.lkLock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkLock.Size = new System.Drawing.Size(309, 20);
			this.lkLock.TabIndex = 9;
			// 
			// lkAgency
			// 
			this.lkAgency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lkAgency.EditValue = "Select One";
			this.lkAgency.Location = new System.Drawing.Point(117, 76);
			this.lkAgency.Name = "lkAgency";
			this.lkAgency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkAgency.Size = new System.Drawing.Size(309, 20);
			this.lkAgency.TabIndex = 8;
			// 
			// lbAgency
			// 
			this.lbAgency.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lbAgency.Location = new System.Drawing.Point(5, 78);
			this.lbAgency.Name = "lbAgency";
			this.lbAgency.Size = new System.Drawing.Size(45, 13);
			this.lbAgency.TabIndex = 7;
			this.lbAgency.Text = "Agency:";
			// 
			// lbLock
			// 
			this.lbLock.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lbLock.Location = new System.Drawing.Point(5, 104);
			this.lbLock.Name = "lbLock";
			this.lbLock.Size = new System.Drawing.Size(29, 13);
			this.lbLock.TabIndex = 5;
			this.lbLock.Text = "Lock:";
			// 
			// tbDesciption
			// 
			this.tbDesciption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDesciption.Location = new System.Drawing.Point(117, 49);
			this.tbDesciption.Name = "tbDesciption";
			this.tbDesciption.Size = new System.Drawing.Size(309, 20);
			this.tbDesciption.TabIndex = 3;
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.labelControl1.Location = new System.Drawing.Point(6, 56);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(92, 13);
			this.labelControl1.TabIndex = 2;
			this.labelControl1.Text = "App Description:";
			// 
			// tbRequirementName
			// 
			this.tbRequirementName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbRequirementName.Location = new System.Drawing.Point(117, 23);
			this.tbRequirementName.Name = "tbRequirementName";
			this.tbRequirementName.Size = new System.Drawing.Size(309, 20);
			this.tbRequirementName.TabIndex = 1;
			// 
			// lbName
			// 
			this.lbName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lbName.Location = new System.Drawing.Point(6, 31);
			this.lbName.Name = "lbName";
			this.lbName.Size = new System.Drawing.Size(35, 13);
			this.lbName.TabIndex = 0;
			this.lbName.Text = "Name:";
			// 
			// gcNotes
			// 
			this.gcNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gcNotes.Controls.Add(this.btnAddNote);
			this.gcNotes.Controls.Add(this.gvNotes);
			this.gcNotes.Location = new System.Drawing.Point(12, 467);
			this.gcNotes.LookAndFeel.SkinName = "The Asphalt World";
			this.gcNotes.LookAndFeel.UseDefaultLookAndFeel = false;
			this.gcNotes.Name = "gcNotes";
			this.gcNotes.Size = new System.Drawing.Size(431, 165);
			this.gcNotes.TabIndex = 7;
			this.gcNotes.Text = "Notes";
			// 
			// btnAddNote
			// 
			this.btnAddNote.Location = new System.Drawing.Point(6, 24);
			this.btnAddNote.LookAndFeel.SkinName = "The Asphalt World";
			this.btnAddNote.LookAndFeel.UseDefaultLookAndFeel = false;
			this.btnAddNote.Name = "btnAddNote";
			this.btnAddNote.Size = new System.Drawing.Size(75, 23);
			this.btnAddNote.TabIndex = 1;
			this.btnAddNote.Text = "Add";
			this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
			// 
			// gvNotes
			// 
			this.gvNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gvNotes.Location = new System.Drawing.Point(0, 53);
			this.gvNotes.LookAndFeel.SkinName = "The Asphalt World";
			this.gvNotes.LookAndFeel.UseDefaultLookAndFeel = false;
			this.gvNotes.MainView = this.gvNotesMainView;
			this.gvNotes.Name = "gvNotes";
			this.gvNotes.Size = new System.Drawing.Size(424, 107);
			this.gvNotes.TabIndex = 0;
			this.gvNotes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNotesMainView,
            this.gridView4});
			// 
			// gvNotesMainView
			// 
			this.gvNotesMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
			this.gvNotesMainView.GridControl = this.gvNotes;
			this.gvNotesMainView.Name = "gvNotesMainView";
			this.gvNotesMainView.OptionsBehavior.Editable = false;
			this.gvNotesMainView.DoubleClick += new System.EventHandler(this.gvNotesMainView_DoubleClick);
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "NoteID";
			this.gridColumn6.FieldName = "NoteID";
			this.gridColumn6.Name = "gridColumn6";
			// 
			// gridColumn7
			// 
			this.gridColumn7.Caption = "Note";
			this.gridColumn7.FieldName = "Note";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 0;
			// 
			// gridColumn8
			// 
			this.gridColumn8.Caption = "Created By";
			this.gridColumn8.FieldName = "CreatedByID";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 1;
			// 
			// gridColumn9
			// 
			this.gridColumn9.Caption = "Date";
			this.gridColumn9.FieldName = "CreatedByDate";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 2;
			// 
			// gridView4
			// 
			this.gridView4.GridControl = this.gvNotes;
			this.gridView4.Name = "gridView4";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(367, 639);
			this.btnCancel.LookAndFeel.SkinName = "The Asphalt World";
			this.btnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(286, 639);
			this.btnSave.LookAndFeel.SkinName = "The Asphalt World";
			this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 9;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// lbModifiedBy
			// 
			this.lbModifiedBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbModifiedBy.Location = new System.Drawing.Point(12, 639);
			this.lbModifiedBy.Name = "lbModifiedBy";
			this.lbModifiedBy.Size = new System.Drawing.Size(0, 13);
			this.lbModifiedBy.TabIndex = 10;
			// 
			// tbTemplateID
			// 
			this.tbTemplateID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbTemplateID.Enabled = false;
			this.tbTemplateID.Location = new System.Drawing.Point(239, 640);
			this.tbTemplateID.Name = "tbTemplateID";
			this.tbTemplateID.Size = new System.Drawing.Size(41, 20);
			this.tbTemplateID.TabIndex = 13;
			this.tbTemplateID.Visible = false;
			// 
			// lbTemplateID
			// 
			this.lbTemplateID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lbTemplateID.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lbTemplateID.Location = new System.Drawing.Point(160, 643);
			this.lbTemplateID.Name = "lbTemplateID";
			this.lbTemplateID.Size = new System.Drawing.Size(73, 13);
			this.lbTemplateID.TabIndex = 12;
			this.lbTemplateID.Text = "Template ID:";
			this.lbTemplateID.Visible = false;
			// 
			// RequirementEditView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(455, 664);
			this.Controls.Add(this.tbTemplateID);
			this.Controls.Add(this.lbTemplateID);
			this.Controls.Add(this.lbModifiedBy);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.gcNotes);
			this.Controls.Add(this.gcGeneral);
			this.Controls.Add(this.gcItems);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RequirementEditView";
			this.Text = "Requirement";
			this.Load += new System.EventHandler(this.RequirementEditView_Load);
			((System.ComponentModel.ISupportInitialize)(this.gcItems)).EndInit();
			this.gcItems.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gvItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvItemsView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gcGeneral)).EndInit();
			this.gcGeneral.ResumeLayout(false);
			this.gcGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbFees.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbCallCenterMessage.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkLock.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkAgency.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbDesciption.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbRequirementName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gcNotes)).EndInit();
			this.gcNotes.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gvNotes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvNotesMainView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbTemplateID.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcItems;
        private DevExpress.XtraGrid.GridControl gvItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemsView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GroupControl gcGeneral;
        private DevExpress.XtraEditors.TextEdit tbDesciption;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tbRequirementName;
        private DevExpress.XtraEditors.LabelControl lbName;
        private DevExpress.XtraEditors.LabelControl lbLock;
        private DevExpress.XtraEditors.GroupControl gcNotes;
        private DevExpress.XtraGrid.GridControl gvNotes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNotesMainView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.LabelControl lbAgency;
        private DevExpress.XtraEditors.LookUpEdit lkLock;
        private DevExpress.XtraEditors.LookUpEdit lkAgency;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnAddItem;
        private DevExpress.XtraEditors.SimpleButton btnAddNote;
        private DevExpress.XtraEditors.MemoEdit tbCallCenterMessage;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lbModifiedBy;
        private DevExpress.XtraEditors.TextEdit tbTemplateID;
        private DevExpress.XtraEditors.LabelControl lbTemplateID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.CheckBox cbRequiredForFunding;
        private DevExpress.XtraEditors.TextEdit tbFees;
    }
}