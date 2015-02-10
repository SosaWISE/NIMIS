using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using PPro.Lib.Data.Licensing;
using PPro.Lib.Data.Recruiting;
using PPro.Lib.Util.ActiveDirectory;
using PPro.Lib.Windows.Forms.ErrorHandling;
using PPro.Lib.Windows.LicenseManagement;

namespace PPro.WindowsClients.LicenseManagement
{
    public partial class Form1 : ManagedErrorForm
    {
		public override ADGroupNameCollection GetADGroupView()
		{
			return new ADGroupNameCollection(GetADGroupEdit());
		}
		public override ADGroupNameCollection GetADGroupEdit()
		{
			ADGroupNameCollection adGroups = new ADGroupNameCollection();

			adGroups.Add(ADGroup.GroupNames.Licensing);

			return adGroups;
		}

        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        private int LocationID = 0;

        //might use this to load all data at one time
        private DataSet MasterDataSet
        {
            get
            {
                return PPro.Lib.Data.Licensing.StoredProcedureManager.GetMasterLicensingDataSet(LocationID).GetDataSet();
            }
        }

        #endregion Properties

        #region Methods

        #region Private

        #region Grids

        private void _loadAllGrids()
        {
            _loadCompanyLicensesGrid();
            _loadSalesRepLicenseGrid();
            _loadTechLicenseGrid();
            _loadCustomerLicenseGrid();
            _loadCompanyRequirementGrid();
            _loadRepRequirementsGrid();
            _loadTechRequirementsGrid();
            _loadCustomerRequirementGrid();
            _loadAgencyGrid();
        }

        private void _loadCompanyLicensesGrid()
        {
            DataSet oDs = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_LicenseGetLicensesByLocationIDAndRequirementTypeID(LocationID, (int)LM_RequirementType.EnumRequirementType.Company).GetDataSet();
            DataColumn licenseKeyColumn = oDs.Tables[0].Columns["LicenseID"];
            DataColumn noteForeignKeyColumn = oDs.Tables[1].Columns["ForeignKeyID"];
            DataColumn itemForeignKeycolumn = oDs.Tables[2].Columns["LicenseID"];
            DataColumn itemKeyColumn = oDs.Tables[2].Columns["LicenseItemID"];
            DataColumn attachmentForeignKeyColumn = oDs.Tables[3].Columns["ForeignKeyID"];
            oDs.Relations.Add("LicenseNotes", licenseKeyColumn, noteForeignKeyColumn);
            oDs.Relations.Add("LicenseItems", licenseKeyColumn, itemForeignKeycolumn);
            oDs.Relations.Add("LicenseItemAttachments", itemKeyColumn, attachmentForeignKeyColumn);
            gvCompanyLicenses.DataSource = oDs.Tables[0];
            gvCompanyLicenses.ForceInitialize();
        }

        private void _loadCompanyRequirementGrid()
        {
            gvCompanyRequirements.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_RequirementGetRequirementByLocationIDAndRequirementTypeID(LocationID, (int)LM_RequirementType.EnumRequirementType.Company).GetDataSet().Tables[0];
            gvCompanyRequirements.ForceInitialize();
        }

        private void _loadSalesRepLicenseGrid()
        {
            DataSet oDs = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_LicenseGetLicensesByLocationIDAndRequirementTypeID(LocationID, (int)LM_RequirementType.EnumRequirementType.SalesRep).GetDataSet();
            DataColumn licenseKeyColumn = oDs.Tables[0].Columns["LicenseID"];
            DataColumn noteForeignKeyColumn = oDs.Tables[1].Columns["ForeignKeyID"];
            DataColumn itemForeignKeycolumn = oDs.Tables[2].Columns["LicenseID"];
            DataColumn itemKeyColumn = oDs.Tables[2].Columns["LicenseItemID"];
            DataColumn attachmentForeignKeyColumn = oDs.Tables[3].Columns["ForeignKeyID"];
            oDs.Relations.Add("LicenseNotes", licenseKeyColumn, noteForeignKeyColumn);
            oDs.Relations.Add("LicenseItems", licenseKeyColumn, itemForeignKeycolumn);
            oDs.Relations.Add("LicenseItemAttachments", itemKeyColumn, attachmentForeignKeyColumn);
            gvRepLicenses.DataSource = oDs.Tables[0];
            gvRepLicenses.ForceInitialize();
        }

        private void _loadRepRequirementsGrid()
        {
            gvRepRequirements.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_RequirementGetRequirementByLocationIDAndRequirementTypeID(LocationID, (int)LM_RequirementType.EnumRequirementType.SalesRep).GetDataSet().Tables[0];
            gvRepRequirements.ForceInitialize();
        }

        private void _loadTechLicenseGrid()
        {
            DataSet oDs = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_LicenseGetLicensesByLocationIDAndRequirementTypeID(LocationID, (int)LM_RequirementType.EnumRequirementType.Technician).GetDataSet();
            DataColumn licenseKeyColumn = oDs.Tables[0].Columns["LicenseID"];
            DataColumn noteForeignKeyColumn = oDs.Tables[1].Columns["ForeignKeyID"];
            DataColumn itemForeignKeycolumn = oDs.Tables[2].Columns["LicenseID"];
            DataColumn itemKeyColumn = oDs.Tables[2].Columns["LicenseItemID"];
            DataColumn attachmentForeignKeyColumn = oDs.Tables[3].Columns["ForeignKeyID"];
            oDs.Relations.Add("LicenseNotes", licenseKeyColumn, noteForeignKeyColumn);
            oDs.Relations.Add("LicenseItems", licenseKeyColumn, itemForeignKeycolumn);
            oDs.Relations.Add("LicenseItemAttachments", itemKeyColumn, attachmentForeignKeyColumn);
            gvTechLicenses.DataSource = oDs.Tables[0];
            gvTechLicenses.ForceInitialize();
        }

        private void _loadTechRequirementsGrid()
        {
            gvTechRequirements.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_RequirementGetRequirementByLocationIDAndRequirementTypeID(LocationID, (int)LM_RequirementType.EnumRequirementType.Technician).GetDataSet().Tables[0];
            gvTechRequirements.ForceInitialize();
        }

        private void _loadCustomerLicenseGrid()
        {
            DataSet oDs = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_LicenseGetLicensesByLocationIDAndRequirementTypeID(LocationID, (int)LM_RequirementType.EnumRequirementType.CustomerPermit).GetDataSet();
            DataColumn licenseKeyColumn = oDs.Tables[0].Columns["LicenseID"];
            DataColumn noteForeignKeyColumn = oDs.Tables[1].Columns["ForeignKeyID"];
            DataColumn itemForeignKeycolumn = oDs.Tables[2].Columns["LicenseID"];
            DataColumn itemKeyColumn = oDs.Tables[2].Columns["LicenseItemID"];
            DataColumn attachmentForeignKeyColumn = oDs.Tables[3].Columns["ForeignKeyID"];
            oDs.Relations.Add("LicenseNotes", licenseKeyColumn, noteForeignKeyColumn);
            oDs.Relations.Add("LicenseItems", licenseKeyColumn, itemForeignKeycolumn);
            oDs.Relations.Add("LicenseItemAttachments", itemKeyColumn, attachmentForeignKeyColumn);
            gvCustomerLicenses.DataSource = oDs.Tables[0];
            gvCustomerLicenses.ForceInitialize();
        }

        private void _loadCustomerRequirementGrid()
        {
            gvCustomerRequirements.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_RequirementGetRequirementByLocationIDAndRequirementTypeID(LocationID, (int)LM_RequirementType.EnumRequirementType.CustomerPermit).GetDataSet().Tables[0];
            gvCustomerRequirements.ForceInitialize();
        }

        private void _loadAgencyGrid()
        {
            gvAgencies.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_AgencyGetAgenciesByLocationID(LocationID).GetDataSet().Tables[0];
            gvAgencies.ForceInitialize();
        }

        private void _loadUserGrid()
        {
	        try
	        {
				DataTable oDt = new DataTable();
				oDt.Load(RU_User.Query().WHERE(RU_User.Columns.IsActive, true).
														AND(RU_User.Columns.IsDeleted, false).ExecuteReader());
				gvUsers.DataSource = oDt;
				gvUsers.ForceInitialize();
			}
	        catch (Exception ex)
	        {
		        Console.WriteLine("Error on _loadUserGrid: {0}", ex.Message);
		        throw;
	        }
        }

        #endregion Grids

        private void _loadTree()
        {
	        try
	        {
				tlLocations.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_LocationsLoadActiveLocations().GetDataSet().Tables[0];
	        }
	        catch (Exception ex)
	        {
		        
				Console.WriteLine("The following error was thrown: {0}", ex);
		        throw;
	        }
        }

        private void _SaveNode(int LocationID, int ParentLocationID)
        {
            LM_Location oLocation = LM_Location.LoadByPrimaryKey(LocationID);
            LM_Location ParentLocation = LM_Location.LoadByPrimaryKey(ParentLocationID);
            oLocation.LocationTypeID = ParentLocation.LocationTypeID + 1;
            oLocation.ParentLocationID = ParentLocationID;

            switch (oLocation.LocationTypeID)
            {
                case (int)LM_LocationType.EnumLocationType.Country:
                    oLocation.ParentCountryID = oLocation.ParentStateID = oLocation.ParentCountyID = oLocation.ParentCityID = null;//default
                    break;
                case (int)LM_LocationType.EnumLocationType.State:
                    oLocation.ParentCountryID = ParentLocation.LocationID;
                    oLocation.ParentStateID = oLocation.ParentCountyID = oLocation.ParentCityID = null;
                    break;
                case (int)LM_LocationType.EnumLocationType.County:
                    oLocation.ParentCountryID = ParentLocation.ParentCountryID;
                    oLocation.ParentStateID = ParentLocation.LocationID;
                    oLocation.ParentCountyID = oLocation.ParentCityID = null;
                    break;
                case (int)LM_LocationType.EnumLocationType.City:
                    oLocation.ParentCountryID = ParentLocation.ParentCountryID;
                    oLocation.ParentStateID = ParentLocation.ParentStateID;
                    oLocation.ParentCountyID = ParentLocation.LocationID;
                    oLocation.ParentCityID = null;
                    break;
                case (int)LM_LocationType.EnumLocationType.Township:
                    oLocation.ParentCountryID = ParentLocation.ParentCountryID;
                    oLocation.ParentStateID = ParentLocation.ParentStateID;
                    oLocation.ParentCountyID = ParentLocation.ParentCountyID;
                    oLocation.ParentCityID = ParentLocation.LocationID;
                    break;
            }
            oLocation.Save();
        }

        #endregion Private

        #region Event Handlers

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeListViewState._loadTreeListViewState(tlLocations);
            _loadTree();
            _loadAllGrids();
            _loadUserGrid();
            if (ADUserInstance.IsInGroup(ADGroup.GroupNames.ITShares) || ADUserInstance.IsInGroup(ADGroup.GroupNames.Licensing))
			{
				btnTemplateManager.Enabled = true;
			}
        }

        #region Location Tree

        private void tlLocations_DoubleClick(object sender, EventArgs e)
        {
            LocationEditView lev = new LocationEditView();
            lev.nLocationID = LocationID;
            lev.ViewType = EditViewHelper.ViewType.Existing;
            TreeListViewState.SaveState();
            if (lev.ShowDialog() == DialogResult.OK)
            {
                _loadTree();
                TreeListViewState.LoadState();
            }
        }

        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            if (LocationID != 0)
            {
                LocationEditView lev = new LocationEditView();
                lev.nParentLocationID = LocationID;
                lev.ViewType = EditViewHelper.ViewType.New;
                TreeListViewState.SaveState();
                if (lev.ShowDialog() == DialogResult.OK)
                {
                    _loadTree();
                    TreeListViewState.LoadState();
                }
            }
        }

        private void tlLocations_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            //this even appears to go with the AfterFocusNode event below
        }

        private void tlLocations_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LocationID = (int)e.Node.GetValue(tlLocations.KeyFieldName);
            lbLocationName.Text = e.Node.GetValue("LocationName").ToString();
            bool CanSolicit = (e.Node.GetValue("CanSolicit").ToString() == "True") ? true : false;
            if (!CanSolicit)
                lbLocationName.Text += " (NO SOLICITATION)";
            _loadAllGrids();//all except the recruit grid

            //only show the recruits page on global setting
            if (e.Node.RootNode == e.Node)
            {
                pgRecruits.PageVisible = true;
                _loadUserGrid();
            }
            else
            {
                pgRecruits.PageVisible = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void tlLocations_AfterDragNode(object sender, NodeEventArgs e)
        {
            if (e.Node != tlLocations.Nodes.FirstNode)
            {
                LM_Location Location = LM_Location.LoadByPrimaryKey((int)e.Node.GetValue(LM_Location.Columns.LocationID));
                LM_Location ParentLocation = LM_Location.LoadByPrimaryKey((int)e.Node.ParentNode.GetValue(LM_Location.Columns.LocationID));
                bool CanDrop = true;

                //Level = locationType -1 
                switch (e.Node.Level)
                {
                    case (int)LM_LocationType.EnumLocationType.Country - 1:
                        foreach (TreeListNode Child in e.Node.Nodes)//countries
                        {
                            foreach (TreeListNode GrandChild in Child.Nodes)//states
                            {
                                foreach (TreeListNode GreatGrandchild in GrandChild.Nodes)//counties
                                {
                                    foreach (TreeListNode GreatGreatGrandchild in GreatGrandchild.Nodes)//Cities
                                    {
                                        if (GreatGreatGrandchild.HasChildren)//townships
                                            CanDrop = false;
                                        //this would be a global location placed in a country space
                                    }
                                }
                            }
                        }
                        break;
                    case (int)LM_LocationType.EnumLocationType.State - 1:
                        foreach (TreeListNode Child in e.Node.Nodes)//States
                        {
                            foreach (TreeListNode GrandChild in Child.Nodes)//Counties
                            {
                                foreach (TreeListNode GreatGrandchild in GrandChild.Nodes)//Cities
                                {
                                    if (GreatGrandchild.HasChildren)//Townships
                                        CanDrop = false;
                                    //this would be a Country placed in a State space   
                                }
                            }
                        }
                        break;
                    case (int)LM_LocationType.EnumLocationType.County - 1:
                        foreach (TreeListNode Child in e.Node.Nodes)//Counties
                        {
                            foreach (TreeListNode GrandChild in Child.Nodes)//Cities
                            {
                                if (GrandChild.HasChildren)//Townships
                                    CanDrop = false;
                                //this would be a State placed in a County space   
                            }
                        }
                        break;
                    case (int)LM_LocationType.EnumLocationType.City - 1:
                        foreach (TreeListNode Child in e.Node.Nodes)//Cities
                        {
                            if (Child.HasChildren)//Townships
                                CanDrop = false;
                            //this would be a County placed in a City space      
                        }
                        break;
                    case (int)LM_LocationType.EnumLocationType.Township - 1:
                        if (e.Node.HasChildren)//Townships
                            CanDrop = false;
                        //this would be a City placed in a Township space      
                        break;
                }
                if (CanDrop)
                {
                    _SaveNode((int)e.Node.GetValue(LM_Location.Columns.LocationID), (int)e.Node.ParentNode.GetValue(LM_Location.Columns.LocationID));
                    foreach (TreeListNode Child in e.Node.Nodes)
                    {
                        _SaveNode((int)Child.GetValue(LM_Location.Columns.LocationID), (int)Child.ParentNode.GetValue(LM_Location.Columns.LocationID));
                        foreach (TreeListNode GrandChild in Child.Nodes)
                        {
                            _SaveNode((int)GrandChild.GetValue(LM_Location.Columns.LocationID), (int)GrandChild.ParentNode.GetValue(LM_Location.Columns.LocationID));
                            foreach (TreeListNode GreatGrandchild in GrandChild.Nodes)
                            {
                                _SaveNode((int)GreatGrandchild.GetValue(LM_Location.Columns.LocationID), (int)GreatGrandchild.ParentNode.GetValue(LM_Location.Columns.LocationID));
                                foreach (TreeListNode GreatGreatGrandchild in GreatGrandchild.Nodes)
                                {
                                    _SaveNode((int)GreatGreatGrandchild.GetValue(LM_Location.Columns.LocationID), (int)GreatGreatGrandchild.ParentNode.GetValue(LM_Location.Columns.LocationID));
                                    foreach (TreeListNode GreatGreatGreatGrandChild in GreatGreatGrandchild.Nodes)
                                    {
                                        _SaveNode((int)GreatGreatGreatGrandChild.GetValue(LM_Location.Columns.LocationID), (int)GreatGreatGreatGrandChild.ParentNode.GetValue(LM_Location.Columns.LocationID));
                                    }
                                }
                            }
                        }
                    }
                }
                TreeListViewState.SaveState();
                _loadTree();
                TreeListViewState.LoadState();
            }
        }

        private void tlLocations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                TreeListNode ParentNode = tlLocations.FocusedNode;
                if (MessageBox.Show("Are you sure you want to delete " + tlLocations.FocusedNode.GetValue(LM_Location.Columns.LocationName).ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_Location oLocation = LM_Location.LoadByPrimaryKey((int)tlLocations.FocusedNode.GetValue(LM_Location.Columns.LocationID));
                    oLocation.IsDeleted = true;
                    oLocation.Save();
                    foreach (TreeListNode Child in tlLocations.FocusedNode.Nodes)
                    {
                        LM_Location oChildLocation = LM_Location.LoadByPrimaryKey((int)Child.GetValue(LM_Location.Columns.LocationID));
                        oChildLocation.IsDeleted = true;
                        oChildLocation.Save();
                        foreach (TreeListNode GrandChild in Child.Nodes)
                        {
                            LM_Location oGrandChildLocation = LM_Location.LoadByPrimaryKey((int)GrandChild.GetValue(LM_Location.Columns.LocationID));
                            oGrandChildLocation.IsDeleted = true;
                            oGrandChildLocation.Save();
                            foreach (TreeListNode GreatGrandchild in GrandChild.Nodes)
                            {
                                LM_Location oGreatGrandchildLocation = LM_Location.LoadByPrimaryKey((int)GreatGrandchild.GetValue(LM_Location.Columns.LocationID));
                                oGreatGrandchildLocation.IsDeleted = true;
                                oGreatGrandchildLocation.Save();
                                foreach (TreeListNode GreatGreatGrandchild in GreatGrandchild.Nodes)
                                {
                                    LM_Location oGreatGreatGrandchildLocation = LM_Location.LoadByPrimaryKey((int)GreatGreatGrandchild.GetValue(LM_Location.Columns.LocationID));
                                    oGreatGreatGrandchildLocation.IsDeleted = true;
                                    oGreatGreatGrandchildLocation.Save();
                                    foreach (TreeListNode GreatGreatGreatGrandChild in GreatGreatGrandchild.Nodes)
                                    {
                                        LM_Location oGreatGreatGreatGrandChildLocation = LM_Location.LoadByPrimaryKey((int)GreatGreatGreatGrandChild.GetValue(LM_Location.Columns.LocationID));
                                        oGreatGreatGreatGrandChildLocation.IsDeleted = true;
                                        oGreatGreatGreatGrandChildLocation.Save();
                                    }
                                }
                            }
                        }
                    }
                    tlLocations.Nodes.Remove(tlLocations.FocusedNode);
                    TreeListViewState.SaveState();
                    _loadTree();
                    TreeListViewState.LoadState();
                }
            }
        }

        private void tbSearchLocations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Search

                tlLocations.CollapseAll();
                DataTable oDt = tlLocations.DataSource as DataTable;
                DataRow[] Matches = oDt.Select(string.Format("{0} = '{1}'", LM_Location.Columns.LocationName, tbSearchLocations.Text));
                foreach (DataRow currRow in Matches)
                {
                    TreeListNode oNode = tlLocations.FindNodeByKeyID((int)currRow[LM_Location.Columns.LocationID]);
                    tlLocations.FocusedNode = oNode;
                    switch (oNode.Level)
                    {
                        case 0://global
                            oNode.Expanded = true;
                            break;
                        case 1://country
                            oNode.Expanded = oNode.ParentNode.Expanded = true;
                            break;
                        case 2://state
                            oNode.Expanded = oNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.Expanded = true;
                            break;
                        case 3://county
                            oNode.Expanded = oNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.ParentNode.Expanded = true;
                            break;
                        case 4://city
                            oNode.Expanded = oNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.ParentNode.ParentNode.Expanded = true;
                            break;
                        case 5://township
                            oNode.Expanded = oNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.ParentNode.ParentNode.Expanded = oNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.Expanded = true;
                            break;
                    }
                }

                #endregion

                Cursor.Current = Cursors.Default;
            }
        }

        private void tbSearchLocations_Enter(object sender, EventArgs e)
        {
            tbSearchLocations.Text = "";
        }

        #endregion Location Tree

        #region Company Licenses

        private void gvCompanyLicensesMain_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView View = gvCompanyLicensesMain;
            GridColumn colNumber = View.Columns["LicenseNumber"];
            GridColumn colExpDate = View.Columns["ExpirationDate"];

            string szLicenseNumber = (string)View.GetRowCellValue(e.RowHandle, colNumber);
            DateTime ExpirationDate = (DateTime)View.GetRowCellValue(e.RowHandle, colExpDate);

            if (szLicenseNumber == "")
            {
                e.Valid = false;
                View.SetColumnError(colNumber, "You must enter an identification number for this license");
            }
            if (ExpirationDate.ToString() == "")
            {
                e.Valid = false;
                View.SetColumnError(colExpDate, "You must enter a valid expiration date");
            }

            //if (e.Valid)
            //{
            //    //if (View.FocusedRowModified)
            //    //{
            //        //locals
            //        GridRow currRow = (GridRow)View.GetRow(e.RowHandle);
            //        LM_License oLicense = LM_License.LoadByPrimaryKey((int)currRow.RowKey);
            //        oLicense.ExpirationDate = (DateTime)currRow.
            //    //}
            //}

        }

        private void gvCompanyLicensesMain_HiddenEditor(object sender, EventArgs e)
        {
            GridView view = gvCompanyLicensesMain;
            //view.fo
        }

        private void gvCompanyLicensesMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvCompanyLicensesMain.GetRowCellValue(gvCompanyLicensesMain.FocusedRowHandle, gvCompanyLicensesMain.Columns["LicenseID"]);
            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.Existing;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nLicenseID = LicenseKey;
            Lev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
            Lev.nLocationID = LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadCompanyLicensesGrid();
            }
        }

        private void btnAddCompanyLicense_Click(object sender, EventArgs e)
        {
            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.New;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
            Lev.nLocationID = LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadCompanyLicensesGrid();
            }
        }

        #endregion Company Licenses

        #region Sales Rep Licenses

        private void btnAddRepLicense_Click(object sender, EventArgs e)
        {
            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.New;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.SalesRep;
            Lev.nLocationID = LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadSalesRepLicenseGrid();
            }
        }

        private void gvRepLicensesMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvRepLicensesMain.GetRowCellValue(gvRepLicensesMain.FocusedRowHandle, gvRepLicensesMain.Columns["LicenseID"]);
            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.Existing;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nLicenseID = LicenseKey;
            Lev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.SalesRep;
            Lev.nLocationID = LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadSalesRepLicenseGrid();
            }
        }

        #endregion Sales Rep Licenses

        #region Tech Licenses

        private void btnAddTechLicense_Click(object sender, EventArgs e)
        {
            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.New;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Technician;
            Lev.nLocationID = LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadTechLicenseGrid();
            }
        }

        private void gvTechLicensesMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvTechLicensesMain.GetRowCellValue(gvTechLicensesMain.FocusedRowHandle, gvTechLicensesMain.Columns["LicenseID"]);
            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.Existing;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nLicenseID = LicenseKey;
            Lev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Technician;
            Lev.nLocationID = LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadTechLicenseGrid();
            }
        }

        #endregion Tech Licenses

        #region Customer Licenses

        private void btnAddCustomerLicense_Click(object sender, EventArgs e)
        {
            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.New;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.CustomerPermit;
            Lev.nLocationID = LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadCustomerLicenseGrid();
            }
        }

        private void gvCustomerLicensesMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvCustomerLicensesMain.GetRowCellValue(gvCustomerLicensesMain.FocusedRowHandle, gvCustomerLicensesMain.Columns["LicenseID"]);
            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.Existing;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nLicenseID = LicenseKey;
            Lev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.CustomerPermit;
            Lev.nLocationID = LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadCustomerLicenseGrid();
            }
        }

        #endregion Customer Licenses

        #region Company Requirements

        private void btnAddCompanyRequirement_Click(object sender, EventArgs e)
        {
            RequirementEditView Rev = new RequirementEditView();
            Rev.ViewType = EditViewHelper.ViewType.New;
            Rev.ViewItem = EditViewHelper.ViewItem.Requirement;
            Rev.nLocationID = LocationID;
            Rev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;

            if (Rev.ShowDialog() == DialogResult.OK)
            {
                _loadCompanyRequirementGrid();
            }
        }

        private void gvCompanyRequirementMain_DoubleClick(object sender, EventArgs e)
        {
            int RequirementKey = (int)gvCompanyRequirementMain.GetRowCellValue(gvCompanyRequirementMain.FocusedRowHandle, gvCompanyRequirementMain.Columns["RequirementID"]);
            RequirementEditView Rev = new RequirementEditView();
            Rev.ViewType = EditViewHelper.ViewType.Existing;
            Rev.ViewItem = EditViewHelper.ViewItem.Requirement;
            Rev.nRequirementID = RequirementKey;
            Rev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
            Rev.nLocationID = LocationID;

            if (Rev.ShowDialog() == DialogResult.OK)
            {
                _loadCompanyRequirementGrid();
            }
        }

        #endregion Company Requirements

        #region Rep Requirements

        private void btnAddRepRequirement_Click(object sender, EventArgs e)
        {
            RequirementEditView Rev = new RequirementEditView();
            Rev.ViewType = EditViewHelper.ViewType.New;
            Rev.ViewItem = EditViewHelper.ViewItem.Requirement;
            Rev.nLocationID = LocationID;
            Rev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.SalesRep;

            if (Rev.ShowDialog() == DialogResult.OK)
            {
                _loadRepRequirementsGrid();
            }
        }

        private void gvRepRequirementsMain_DoubleClick(object sender, EventArgs e)
        {
            int RequirementKey = (int)gvRepRequirementsMain.GetRowCellValue(gvRepRequirementsMain.FocusedRowHandle, gvRepRequirementsMain.Columns["RequirementID"]);
            RequirementEditView Rev = new RequirementEditView();
            Rev.ViewType = EditViewHelper.ViewType.Existing;
            Rev.ViewItem = EditViewHelper.ViewItem.Requirement;
            Rev.nRequirementID = RequirementKey;
            Rev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.SalesRep;
            Rev.nLocationID = LocationID;

            if (Rev.ShowDialog() == DialogResult.OK)
            {
                _loadRepRequirementsGrid();
            }
        }

        #endregion Rep Requirements

        #region Tech Requirements

        private void btnAddTechRequirement_Click(object sender, EventArgs e)
        {
            RequirementEditView Rev = new RequirementEditView();
            Rev.ViewType = EditViewHelper.ViewType.New;
            Rev.ViewItem = EditViewHelper.ViewItem.Requirement;
            Rev.nLocationID = LocationID;
            Rev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Technician;

            if (Rev.ShowDialog() == DialogResult.OK)
            {
                _loadTechRequirementsGrid();
            }
        }

        private void gvTechRequirementsMain_DoubleClick(object sender, EventArgs e)
        {
            int RequirementKey = (int)gvTechRequirementsMain.GetRowCellValue(gvTechRequirementsMain.FocusedRowHandle, gvTechRequirementsMain.Columns["RequirementID"]);
            RequirementEditView Rev = new RequirementEditView();
            Rev.ViewType = EditViewHelper.ViewType.Existing;
            Rev.ViewItem = EditViewHelper.ViewItem.Requirement;
            Rev.nRequirementID = RequirementKey;
            Rev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Technician;
            Rev.nLocationID = LocationID;

            if (Rev.ShowDialog() == DialogResult.OK)
            {
                _loadTechRequirementsGrid();
            }
        }

        #endregion Tech Requirements

        #region Customer Requirements

        private void btnAddCustomerRequirement_Click(object sender, EventArgs e)
        {
            RequirementEditView Rev = new RequirementEditView();
            Rev.ViewType = EditViewHelper.ViewType.New;
            Rev.ViewItem = EditViewHelper.ViewItem.Requirement;
            Rev.nLocationID = LocationID;
            Rev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.CustomerPermit;

            if (Rev.ShowDialog() == DialogResult.OK)
            {
                _loadCustomerRequirementGrid();
            }
        }

        private void gvCustomerRequirementsMain_DoubleClick(object sender, EventArgs e)
        {
            int RequirementKey = (int)gvCustomerRequirementsMain.GetRowCellValue(gvCustomerRequirementsMain.FocusedRowHandle, gvCustomerRequirementsMain.Columns["RequirementID"]);
            RequirementEditView Rev = new RequirementEditView();
            Rev.ViewType = EditViewHelper.ViewType.Existing;
            Rev.ViewItem = EditViewHelper.ViewItem.Requirement;
            Rev.nRequirementID = RequirementKey;
            Rev.nRequirementTypeID = (int)LM_RequirementType.EnumRequirementType.CustomerPermit;
            Rev.nLocationID = LocationID;

            if (Rev.ShowDialog() == DialogResult.OK)
            {
                _loadCustomerRequirementGrid();
            }
        }

        #endregion Customer Requirements

        #region Agencies

        private void btnAddAgency_Click(object sender, EventArgs e)
        {
            AgencyEditView Aev = new AgencyEditView();
            Aev.ViewType = EditViewHelper.ViewType.New;
            Aev.LocationID = LocationID;

            if (Aev.ShowDialog() == DialogResult.OK)
            {
                _loadAgencyGrid();
            }
        }

        private void gvAgenciesMain_DoubleClick(object sender, EventArgs e)
        {
            int AgencyKey = (int)gvAgenciesMain.GetRowCellValue(gvAgenciesMain.FocusedRowHandle, gvAgenciesMain.Columns["AgencyID"]);
            AgencyEditView Aev = new AgencyEditView();
            Aev.ViewType = EditViewHelper.ViewType.Existing;
            Aev.AgencyID = AgencyKey;
            Aev.LocationID = LocationID;

            if (Aev.ShowDialog() == DialogResult.OK)
            {
                _loadAgencyGrid();
            }
        }

        #endregion Agencies

        #region User Grid

        private void gvUsersMain_DoubleClick(object sender, EventArgs e)
        {
            MasterEmployeeFile MEF = new MasterEmployeeFile();
            MEF.nUserID = (int)gvUsersMain.GetRowCellValue(gvUsersMain.FocusedRowHandle, gvUsersMain.Columns["UserID"]);

            if (MEF.ShowDialog() == DialogResult.OK)
            {
                _loadUserGrid();
            }
        }

        #endregion User Grid

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Close", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Form.ActiveForm.DialogResult = DialogResult.OK;
                Form.ActiveForm.Close();
            }
        }

		private void btnTemplateManager_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			TemplateManager tempman = new TemplateManager();
			tempman.Show();
		}

		private void btnReports_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ReportingConsole oReports = new ReportingConsole();
			oReports.ShowDialog();
		}

        private void gvCompanyLicensesMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow oRow = gvCompanyLicensesMain.GetFocusedDataRow();
                if (MessageBox.Show("Are you sure you want to delete " + oRow["LicenseName"].ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_License oLicense = LM_License.LoadByPrimaryKey(int.Parse(oRow["LicenseID"].ToString()));
                    oLicense.IsDeleted = true;
                    oLicense.ModifiedByDate = DateTime.Now;
                    oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                    oLicense.Save();

                    _loadCompanyLicensesGrid();
                }
            }
        }

        private void gvRepLicensesMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow oRow = gvRepLicensesMain.GetFocusedDataRow();
                if (MessageBox.Show("Are you sure you want to delete " + oRow["LicenseName"].ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_License oLicense = LM_License.LoadByPrimaryKey(int.Parse(oRow["LicenseID"].ToString()));
                    oLicense.IsDeleted = true;
                    oLicense.ModifiedByDate = DateTime.Now;
                    oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                    oLicense.Save();

                    _loadSalesRepLicenseGrid();
                }
            }
        }

        private void gvTechLicensesMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow oRow = gvTechLicensesMain.GetFocusedDataRow();
                if (MessageBox.Show("Are you sure you want to delete " + oRow["LicenseName"].ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_License oLicense = LM_License.LoadByPrimaryKey(int.Parse(oRow["LicenseID"].ToString()));
                    oLicense.IsDeleted = true;
                    oLicense.ModifiedByDate = DateTime.Now;
					oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                    oLicense.Save();

                    _loadTechLicenseGrid();
                }
            }
        }

        private void gvCustomerLicensesMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow oRow = gvCustomerLicensesMain.GetFocusedDataRow();
                if (MessageBox.Show("Are you sure you want to delete " + oRow["LicenseName"].ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_License oLicense = LM_License.LoadByPrimaryKey(int.Parse(oRow["LicenseID"].ToString()));
                    oLicense.IsDeleted = true;
                    oLicense.ModifiedByDate = DateTime.Now;
                    oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                    oLicense.Save();

                    _loadCustomerLicenseGrid();
                }
            }
        }

        private void gvCompanyRequirementMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow oRow = gvCompanyRequirementMain.GetFocusedDataRow();
                if (MessageBox.Show("Are you sure you want to delete " + oRow["RequirementName"].ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_Requirement oReq = LM_Requirement.LoadByPrimaryKey(int.Parse(oRow["RequirementID"].ToString()));
                    oReq.IsDeleted = true;
                    oReq.ModifiedByDate = DateTime.Now;
                    oReq.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                    oReq.Save();

                    _loadCompanyRequirementGrid();
                }
            }
        }

        private void gvRepRequirementsMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow oRow = gvRepRequirementsMain.GetFocusedDataRow();
                if (MessageBox.Show("Are you sure you want to delete " + oRow["RequirementName"].ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_Requirement oReq = LM_Requirement.LoadByPrimaryKey(int.Parse(oRow["RequirementID"].ToString()));
                    oReq.IsDeleted = true;
                    oReq.ModifiedByDate = DateTime.Now;
                    oReq.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                    oReq.Save();

                    _loadRepRequirementsGrid();
                }
            }
        }

        private void gvTechRequirementsMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow oRow = gvTechRequirementsMain.GetFocusedDataRow();
                if (MessageBox.Show("Are you sure you want to delete " + oRow["RequirementName"].ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_Requirement oReq = LM_Requirement.LoadByPrimaryKey(int.Parse(oRow["RequirementID"].ToString()));
                    oReq.IsDeleted = true;
                    oReq.ModifiedByDate = DateTime.Now;
                    oReq.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                    oReq.Save();

                    _loadTechRequirementsGrid();
                }
            }
        }

        private void gvCustomerRequirementsMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow oRow = gvCustomerRequirementsMain.GetFocusedDataRow();
                if (MessageBox.Show("Are you sure you want to delete " + oRow["RequirementName"].ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_Requirement oReq = LM_Requirement.LoadByPrimaryKey(int.Parse(oRow["RequirementID"].ToString()));
                    oReq.IsDeleted = true;
                    oReq.ModifiedByDate = DateTime.Now;
                    oReq.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                    oReq.Save();

                    _loadCustomerRequirementGrid();
                }
            }
        }

        private void gvAgenciesMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow oRow = gvAgenciesMain.GetFocusedDataRow();
                if (MessageBox.Show("Are you sure you want to delete " + oRow["AgencyName"].ToString() + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LM_Agency oAgency = LM_Agency.LoadByPrimaryKey(int.Parse(oRow["AgencyID"].ToString()));
                    oAgency.IsDeleted = true;
                    oAgency.ModifiedByDate = DateTime.Now;
                    oAgency.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                    oAgency.Save();

                    _loadAgencyGrid();
                }
            }
        }

        #endregion Event Handlers

        #endregion Methods
    }
}
