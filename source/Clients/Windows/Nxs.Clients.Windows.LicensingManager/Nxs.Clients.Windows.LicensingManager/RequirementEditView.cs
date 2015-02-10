using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using PPro.Lib.Data.Licensing;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using PPro.Lib.Windows.Forms.ErrorHandling;
using PPro.Lib.Util.ActiveDirectory;
using PPro.Lib.Windows.LicenseManagement;

namespace PPro.WindowsClients.LicenseManagement
{
    public partial class RequirementEditView : ManagedErrorForm
    {
        #region Constructors
        public RequirementEditView()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region properties

        public int nRequirementID = 0;

        public string ViewType = "";

        public string ViewItem = "";

        public int nLocationID = 0;

        public int nRequirementTypeID = 0;

        private LM_Requirement _requirement = null;

        private LM_Requirement Requirement
        {
            get
            {
                if (_requirement == null)
                {
                    _requirement = LM_Requirement.LoadByPrimaryKey(nRequirementID);
                }
                return _requirement;
            }
        }

        #endregion Properties

        #region Methods

        #region Private

        private bool FormIsValid
        {
            get
            {
                //locals
                bool IsValid = true;
                string szMessage = string.Empty;

                if (nLocationID == 0)
                {
                    szMessage = szMessage + "Location ID is missing.\r\n";
                    IsValid = false;
                }
                if (nRequirementTypeID == 0)
                {
                    szMessage = szMessage + "Requirement Type ID is missing.\r\n";
                    IsValid = false;
                }
                //if (lkAgency.ItemIndex == -1)
                //{
                //    szMessage = szMessage + "No Agency was Chosen.\r\n";
                //    IsValid = false;
                //}
                if (lkLock.ItemIndex == -1)
                {
                    szMessage = szMessage + "No lock type was chosen.\r\n";
                    IsValid = false;
                }
                if (tbRequirementName.Text == "")
                {
                    szMessage = szMessage + "Requirement name is missing.\r\n";
                    IsValid = false;
                }
                if (tbCallCenterMessage.Text == "")
                {
                    szMessage = szMessage + "Call center message is missing.\r\n";
                    IsValid = false;
                }
                if (!IsValid)
                {
                    MessageBox.Show(szMessage, "Unable to Save");
                }
                return IsValid;
            }
        }

        private void _loadlockDDL()
        {
            //load the lock ddl
            DataTable oDt = new DataTable();
            oDt.Load(LM_Lock.FetchAll());
            lkLock.Properties.DataSource = oDt;
            lkLock.Properties.DisplayMember = LM_Lock.Columns.Description;
            lkLock.Properties.ValueMember = LM_Lock.Columns.LockID;
            DevExpress.XtraEditors.Controls.LookUpColumnInfo oCol = new LookUpColumnInfo(LM_Lock.Columns.Description);
            oCol.Caption = "Lock Type";
            lkLock.Properties.Columns.Add(oCol);

            //load
        }

        private void _loadAgencyDDL()
        {
            //load agency ddl
            DataTable oDt = new DataTable();
            oDt.Load(LM_Agency.Query().WHERE(LM_Agency.Columns.LocationID, nLocationID).ExecuteReader());
            lkAgency.Properties.DataSource = oDt;
            lkAgency.Properties.DisplayMember = LM_Agency.Columns.AgencyName;
            lkAgency.Properties.ValueMember = LM_Agency.Columns.AgencyID;
            LookUpColumnInfo oAgencyCol = new LookUpColumnInfo(LM_Agency.Columns.AgencyName);
            oAgencyCol.Caption = "Agency";
            lkAgency.Properties.Columns.Add(oAgencyCol);
            lkAgency.Properties.ForceInitialize();
        }

        private void SaveExisting()
        {
            var oRequirement = LM_Requirement.LoadByPrimaryKey(nRequirementID);
            if (tbRequirementName.Text != "")
            {
                oRequirement.RequirementName = tbRequirementName.Text;
            }
            if (tbDesciption.Text != "")
            {
                oRequirement.ApplicationDescription = tbDesciption.Text;
            }
            if (lkAgency.ItemIndex != -1)
            {
                oRequirement.AgencyID = (int)lkAgency.Properties.GetDataSourceValue(LM_Agency.Columns.AgencyID, lkAgency.ItemIndex);
            }
            if (lkLock.ItemIndex != -1)
            {
                oRequirement.LockID = (int)lkLock.Properties.GetDataSourceValue(LM_Lock.Columns.LockID, lkLock.ItemIndex);
            }
            int dummy;
            if (tbTemplateID.Visible && int.TryParse(tbTemplateID.Text, out dummy))
                oRequirement.TemplateID = dummy;
            oRequirement.CallCenterMessage = tbCallCenterMessage.Text;
            oRequirement.RequiredForFunding = cbRequiredForFunding.Checked;
            decimal dFee;
            oRequirement.Fee = decimal.TryParse(tbFees.Text, out dFee) ? dFee : 0;
            oRequirement.ModifiedByDate = DateTime.Now;
			oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
            oRequirement.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveNew()
        {
            if (!FormIsValid) return;

            var oRequirement = new LM_Requirement
                                   {
                                       LocationID = nLocationID,
                                       RequirementTypeID = nRequirementTypeID
                                   };
            if (lkAgency.ItemIndex != -1)
                oRequirement.AgencyID = (int)lkAgency.Properties.GetDataSourceValue(LM_Agency.Columns.AgencyID, lkAgency.ItemIndex);
            oRequirement.LockID = (int)lkLock.Properties.GetDataSourceValue(LM_Lock.Columns.LockID, lkLock.ItemIndex);
            oRequirement.RequirementName = tbRequirementName.Text;
            if (tbDesciption.Text != "")
                oRequirement.ApplicationDescription = tbDesciption.Text;
            oRequirement.CallCenterMessage = tbCallCenterMessage.Text;
            oRequirement.RequiredForFunding = cbRequiredForFunding.Checked;
            decimal dFee;
            oRequirement.Fee = decimal.TryParse(tbFees.Text, out dFee) ? dFee : 0;
            oRequirement.IsActive = true;
            oRequirement.IsDeleted = false;
            oRequirement.CreatedByID = oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
            oRequirement.CreatedByDate = DateTime.Now;
            oRequirement.ModifiedByDate = DateTime.Now;

            oRequirement.Save();
            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _loadPage()
        {
            _loadlockDDL();
            _loadAgencyDDL();
            SetVisibility();

            if (ViewType == EditViewHelper.ViewType.Existing)
            {
                _loadExistingRequirement();
            }
            else 
            {
                gcItems.Enabled = gcNotes.Enabled = false;
            }
        }

        private void SetVisibility()
        {
            if (ADUserInstance.IsInGroup(ADGroup.GroupNames.ITShares) || ADUserInstance.IsInGroup(ADGroup.GroupNames.Licensing))
                lbTemplateID.Visible = tbTemplateID.Visible = true;
        }

        private void _loadExistingRequirement()
        {
            gvItems.DataSource = Requirement.LM_RequirementItems;
            _loadNotes();
            tbRequirementName.Text = Requirement.RequirementName;
            tbDesciption.Text = Requirement.ApplicationDescription;
            tbCallCenterMessage.Text = Requirement.CallCenterMessage;
            if (Requirement.RequiredForFunding != null && (bool)Requirement.RequiredForFunding)
                cbRequiredForFunding.Checked = true;
            else
                cbRequiredForFunding.Checked = false;
            if (Requirement.Fee != null)
                tbFees.Text = string.Format("{0:F2}", Requirement.Fee);
            else
                tbFees.Text = "0.00";
            lkLock.ItemIndex = lkLock.Properties.GetDataSourceRowIndex(LM_Lock.Columns.LockID, Requirement.LockID);
            lkAgency.ItemIndex = lkAgency.Properties.GetDataSourceRowIndex(LM_Agency.Columns.AgencyID, Requirement.AgencyID);
            lbModifiedBy.Text = Requirement.ModifiedByID + " " + Requirement.ModifiedByDate.ToShortDateString();
            if (tbTemplateID.Visible)
                tbTemplateID.Text = Requirement.TemplateID.ToString();
        }

        private void _loadItems()
        {
            LM_Requirement oRequirement = LM_Requirement.LoadByPrimaryKey(nRequirementID);
            gvItems.DataSource = oRequirement.LM_RequirementItems;
            gvItems.ForceInitialize();
        }

        private void _loadNotes()
        {
            DataTable oDt = new DataTable();
            oDt.Load(LM_Note.Query().WHERE(LM_Note.Columns.ForeignKeyID, nRequirementID).WHERE(LM_Note.Columns.NoteTypeID, (int)LM_NoteType.EnumNoteType.Requirement).ExecuteReader());
            gvNotes.DataSource = oDt;
            gvNotes.ForceInitialize();
        }

        #endregion Private

        #region Event Handlers

        private void RequirementEditView_Load(object sender, EventArgs e)
        {
            _loadPage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (ViewType)
            {
                case EditViewHelper.ViewType.New:
                    SaveNew();
                    break;
                case EditViewHelper.ViewType.Existing:
                    SaveExisting();
                    break;
            }
        }

        private void BtnAddItemClick(object sender, EventArgs e)
        {
            var oEiv = new EditItemView
                       	{
                       		ViewType = EditViewHelper.ViewType.New,
                       		ViewItem = EditViewHelper.ViewItem.RequirementItem,
                       		ParentID = nRequirementID
                       	};

        	if (oEiv.ShowDialog() == DialogResult.OK)
            {
                _loadItems();
            }
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            var oNv = new NoteView
                      	{
                      		ViewType = EditViewHelper.ViewType.New,
                      		nForeignKeyID = nRequirementID,
                      		nNoteTypeID = (int) LM_NoteType.EnumNoteType.Requirement
                      	};

        	if (oNv.ShowDialog() == DialogResult.OK)
            {
                _loadNotes();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Are you sure you want to close without saving?", "Close", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //{
                Form.ActiveForm.DialogResult = DialogResult.Cancel;
                Form.ActiveForm.Close();
            //}
        }

        private void gvItemsView_DoubleClick(object sender, EventArgs e)
        {
            int ItemKey = (int)gvItemsView.GetRowCellValue(gvItemsView.FocusedRowHandle, gvItemsView.Columns["RequirementItemID"]);
            EditItemView Eiv = new EditItemView();
            Eiv.ParentID = nRequirementID;
            Eiv.ItemID = ItemKey;
            Eiv.ViewType = EditViewHelper.ViewType.Existing;
            Eiv.ViewItem = EditViewHelper.ViewItem.RequirementItem;

            if (Eiv.ShowDialog() == DialogResult.OK)
            {
                _loadItems();
            }
        }

        private void gvNotesMainView_DoubleClick(object sender, EventArgs e)
        {
            int NoteKey = (int)gvNotesMainView.GetRowCellValue(gvNotesMainView.FocusedRowHandle, gvNotesMainView.Columns["NoteID"]);
            NoteView Nv = new NoteView();
            Nv.ViewType = EditViewHelper.ViewType.Existing;
            Nv.nForeignKeyID = nRequirementID;
            Nv.nNoteID = NoteKey;
            Nv.nNoteTypeID = (int)LM_NoteType.EnumNoteType.Requirement;

            if (Nv.ShowDialog() == DialogResult.OK)
            {
                _loadNotes();
            }
        }

		private void gvItemsView_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
			{
				if (MessageBox.Show("Are you sure you want to delete this item", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					LM_RequirementItem oItem = LM_RequirementItem.LoadByPrimaryKey((int)gvItemsView.GetFocusedRowCellValue(LM_RequirementItem.Columns.RequirementItemID));
					oItem.IsDeleted = true;
                    oItem.ModifiedByDate = DateTime.Now;
					oItem.ModifiedByID = LicenseManagementHelper.LoggedInUser;
					oItem.Save();
					_loadItems();
				}
			}
		}

        #endregion Event Handlers

        #endregion Methods
    }
}
