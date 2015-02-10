using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using PPro.Lib.Data.Recruiting;
using DevExpress.XtraEditors.Controls;
using PPro.Lib.Data.Interim;
using PPro.Lib.Util.Cryptography;
using PPro.Lib.Data.Licensing;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using DevExpress.XtraEditors.Repository;
using System.IO;
using PPro.Lib.Windows.Forms.ErrorHandling;
using PPro.Lib.Windows.LicenseManagement;

namespace PPro.WindowsClients.LicenseManagement
{
    public partial class MasterEmployeeFile : ManagedErrorForm
    {
        #region Constructors
        public MasterEmployeeFile()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Properties

        public int nUserID = 0;

        private RU_User _oUser = null;

        private RU_User oUser
        {
            get
            {
                if (_oUser == null)
                {
                    _oUser = RU_User.LoadByPrimaryKey(nUserID);
                }
                return _oUser;
            }
        }

        private RU_Recruit _recruit = null;

        private RU_Recruit oRecruit
        {
            get
            {
                if (_recruit == null)
                {
                    _recruit = new RU_Recruit();
                    _recruit.LoadAndCloseReader(RU_Recruit.Query().WHERE(RU_Recruit.Columns.UserID, oUser.UserID).
                                                                AND(RU_Recruit.Columns.SeasonID, RU_Season.Current.SeasonID).
                                                                AND(RU_Recruit.Columns.IsDeleted, false).ExecuteReader());
                }
                return _recruit;
            }
        }

        #endregion Properties

        #region Methods

        #region private

        private void _loadDDLs()
        {
            //locals
            DataTable oStates = new DataTable();
            oStates.Load(MC_PoliticalState.FetchAll());
            DataTable oCountries = new DataTable();
            oCountries.Load(MC_PoliticalCountry.FetchAll());
            LookUpColumnInfo oStateCol = new LookUpColumnInfo(RU_State.Columns.StateName);
            oStateCol.Caption = "State";
            LookUpColumnInfo oCountryCol = new LookUpColumnInfo(MC_PoliticalCountry.Columns.CountryName);
            oCountryCol.Caption = "Country";

            //load state ddls
            lkPermanentState.Properties.DataSource = lkCurrentState.Properties.DataSource = oStates;
            lkPermanentState.Properties.DisplayMember = lkCurrentState.Properties.DisplayMember = MC_PoliticalState.Columns.StateName;
            lkPermanentState.Properties.ValueMember = lkCurrentState.Properties.ValueMember = MC_PoliticalState.Columns.StateID;
            lkPermanentState.Properties.Columns.Add(oStateCol);
            lkCurrentState.Properties.Columns.Add(oStateCol);

            //load Country ddl;
            lkPermanentCountry.Properties.DataSource = lkCurrentCountry.Properties.DataSource = oCountries;
            lkPermanentCountry.Properties.DisplayMember = lkCurrentCountry.Properties.DisplayMember = MC_PoliticalCountry.Columns.CountryName;
            lkPermanentCountry.Properties.ValueMember = lkCurrentCountry.Properties.ValueMember = MC_PoliticalCountry.Columns.CountryID;
            lkPermanentCountry.Properties.Columns.Add(oCountryCol);
            lkCurrentCountry.Properties.Columns.Add(oCountryCol);


        }

        private void _loadLicenseGrid()
        {
            int nType = 0;
            if (oRecruit.IsLoaded)
            {
                switch (oRecruit.UserTypeId)
                {
                    case (int)RU_UserType.UserTypeValues.RegionalManagerSales:
                    case (int)RU_UserType.UserTypeValues.SalesAssistantManager:
                    case (int)RU_UserType.UserTypeValues.SalesCoManager:
                    case (int)RU_UserType.UserTypeValues.SalesManager:
                    case (int)RU_UserType.UserTypeValues.SalesRep:
                        nType = (int)LM_RequirementType.EnumRequirementType.SalesRep;
                        break;
                    case (int)RU_UserType.UserTypeValues.Technician:
                    case (int)RU_UserType.UserTypeValues.TechnicianAssistantLead:
                    case (int)RU_UserType.UserTypeValues.TechnicianLead:
                        nType = (int)LM_RequirementType.EnumRequirementType.Technician;
                        break;
                }
            }
            if (nType != 0 && tbAreaOfWork.Text != "")
            {
                gvLicenses.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.GetRequirementsMetAndNeeded(oUser.GPEmployeeID, nType, tbAreaOfWork.Text).GetDataSet().Tables[0];
                gvLicenses.ForceInitialize();
            }


        }

        private void _loadNotes()
        {
            DataTable oDt = new DataTable();
            oDt.Load(LM_Note.Query().WHERE(LM_Note.Columns.ForeignKeyID, nUserID).
                                    AND(LM_Note.Columns.NoteTypeID, (int)LM_NoteType.EnumNoteType.User).ExecuteReader());
            gvNotes.DataSource = oDt;
            gvNotes.ForceInitialize();
        }

        private void _loadPage()
        {
            _loadDDLs();
            lbUserName.Text = lbUserName.Text + oUser.FullName;
            if (oUser.BirthDate != null)
                tbDOB.Text = DateTime.Parse(oUser.BirthDate.ToString()).ToShortDateString();
            tbPOB.Text = oUser.BirthCity + ", " + oUser.BirthState;
            tbDriversLicense.Text = oUser.DLNumber;
            tbDriversLicenseState.Text = oUser.DLState;
            tbSSN.Text = TripleDES.DecryptString(oUser.Ssn, null).Insert(3, "-").Insert(6, "-"); 
            //photo here

            //permanent Address
            if (oUser.PermanentAddressID != null)
            {
                tbPermanentStreetAddress.Text = oUser.PermanentAddress.StreetAddress;
                tbPermanentCity.Text = oUser.PermanentAddress.City;
                lkPermanentState.ItemIndex = lkPermanentState.Properties.GetDataSourceRowIndex(MC_PoliticalState.Columns.StateID, oUser.PermanentAddress.StateId);
                lkPermanentCountry.ItemIndex = lkPermanentCountry.Properties.GetDataSourceRowIndex(MC_PoliticalCountry.Columns.CountryID, oUser.PermanentAddress.CountryId);
                tbPermanentPostalCode.Text = oUser.PermanentAddress.PostalCode;
                if (oUser.PermanentAddress.PlusFour != null)
                    tbPermanentPostalCode.Text = tbPermanentPostalCode.Text + " - " + oUser.PermanentAddress.PlusFour;
            }
            else
            {
                gcPermanentAddress.Enabled = false;
            }

            //current address
            if (oRecruit.IsLoaded && oRecruit.CurrentAddressID != null)
            {
                tbCurrentStreetAddress.Text = oRecruit.CurrentAddress.StreetAddress;
                tbCurrentCity.Text = oRecruit.CurrentAddress.City;
                lkCurrentState.ItemIndex = lkCurrentState.Properties.GetDataSourceRowIndex(MC_PoliticalState.Columns.StateID, oRecruit.CurrentAddress.StateId);
                lkCurrentCountry.ItemIndex = lkCurrentCountry.Properties.GetDataSourceRowIndex(MC_PoliticalCountry.Columns.CountryID, oRecruit.CurrentAddress.CountryId);
                tbCurrentPostalCode.Text = oRecruit.CurrentAddress.PostalCode;
                if (oRecruit.CurrentAddress.PlusFour != null)
                    tbCurrentPostalCode.Text = tbCurrentPostalCode.Text + " - " + oRecruit.CurrentAddress.PlusFour;
            }
            else
            {
                gcCurrentAddress.Enabled = false;
            }

            //_loadLicenseGrid();
            _loadNotes();

        }

        private void _saveInfo()
        {
            //save current address
            int CurrentAddressID = 0;
            if (int.TryParse(oRecruit.CurrentAddressID.ToString(), out CurrentAddressID))
            {
                RU_RecruitAddress oCurrentAddress = RU_RecruitAddress.LoadByPrimaryKey(CurrentAddressID);
                if (tbCurrentStreetAddress.Text != "")
                    oCurrentAddress.StreetAddress = tbCurrentStreetAddress.Text;
                if (tbCurrentCity.Text != "")
                    oCurrentAddress.City = tbCurrentCity.Text;
                if (lkCurrentState.ItemIndex != -1)
                    oCurrentAddress.StateId = (int)lkCurrentState.Properties.GetDataSourceValue(MC_PoliticalState.Columns.StateID, lkCurrentState.ItemIndex);
                if (tbCurrentPostalCode.Text != "" && tbCurrentPostalCode.Text.Length >= 5)
                {
                    oCurrentAddress.PostalCode = tbCurrentPostalCode.Text.Substring(0, 5);
                    if (tbCurrentPostalCode.Text.Length > 6 && tbCurrentPostalCode.Text.Contains("-"))
                        oCurrentAddress.PlusFour = tbCurrentPostalCode.Text.Substring(tbCurrentPostalCode.Text.Length - 4);
                }
                if (lkCurrentCountry.ItemIndex != -1)
                    oCurrentAddress.CountryId = (int)lkCurrentCountry.Properties.GetDataSourceValue(MC_PoliticalCountry.Columns.CountryID, lkCurrentCountry.ItemIndex);
                oCurrentAddress.Save();
            }

            //save permanent address
            int PermanentAddressID = 0;
            if (int.TryParse(oUser.PermanentAddressID.ToString(), out PermanentAddressID))
            {
                RU_RecruitAddress oPermanentAddress = RU_RecruitAddress.LoadByPrimaryKey(PermanentAddressID);
                if (tbPermanentStreetAddress.Text != "")
                    oPermanentAddress.StreetAddress = tbPermanentStreetAddress.Text;
                if (tbPermanentCity.Text != "")
                    oPermanentAddress.City = tbPermanentCity.Text;
                if (lkPermanentState.ItemIndex != -1)
                    oPermanentAddress.StateId = (int)lkPermanentState.Properties.GetDataSourceValue(MC_PoliticalState.Columns.StateID, lkPermanentState.ItemIndex);
                if (lkPermanentCountry.ItemIndex != -1)
                    oPermanentAddress.CountryId = (int)lkPermanentCountry.Properties.GetDataSourceValue(MC_PoliticalCountry.Columns.CountryID, lkPermanentCountry.ItemIndex);
                if (tbPermanentPostalCode.Text != "" && tbPermanentPostalCode.Text.Length >= 5)
                {
                    oPermanentAddress.PostalCode = tbPermanentPostalCode.Text.Substring(0, 5);
                    if (tbPermanentPostalCode.Text.Length > 5 && tbPermanentPostalCode.Text.Contains("-"))
                        oPermanentAddress.PlusFour = tbPermanentPostalCode.Text.Substring(tbPermanentPostalCode.Text.Length - 4);
                }
                oPermanentAddress.Save();
            }

            //save user items
            RU_User SaveUser = RU_User.LoadByPrimaryKey(oUser.UserID);

            DateTime DOB;
            if (DateTime.TryParse(tbDOB.Text,out DOB))
                SaveUser.BirthDate = DOB;
            if (tbPOB.Text != "")
            {
                string[] szPOB = tbPOB.Text.Split(',');
                SaveUser.BirthCity = szPOB[0];
                SaveUser.BirthState = szPOB[1].Replace(" ", string.Empty);
            }
            if (tbDriversLicense.Text != "")
                SaveUser.DLNumber = tbDriversLicense.Text;
            if (tbDriversLicenseState.Text != "")
                SaveUser.DLState = tbDriversLicenseState.Text;
            if (tbSSN.Text != "")
                SaveUser.Ssn = TripleDES.EncryptString(tbSSN.Text.Replace("-", string.Empty), null);
			SaveUser.Save(ADUserInstance.UserName);

            Form.ActiveForm.DialogResult = DialogResult.OK;
            Form.ActiveForm.Close();
        }

        #endregion Private

        #region Event Handlers

        private void MasterEmployeeFile_Load(object sender, EventArgs e)
        {
            _loadPage();
        }

        private void tbAreaOfWork_TextChanged(object sender, EventArgs e)
        {
            _loadLicenseGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NoteView Nv = new NoteView();
            Nv.ViewType = EditViewHelper.ViewType.New;
            Nv.nForeignKeyID = nUserID;
            Nv.nNoteTypeID = (int)LM_NoteType.EnumNoteType.User;

            if (Nv.ShowDialog() == DialogResult.OK)
            {
                _loadNotes();
            }
        }

        private void gvNotesMain_DoubleClick(object sender, EventArgs e)
        {
            int NoteKey = (int)gvNotesMain.GetRowCellValue(gvNotesMain.FocusedRowHandle, gvNotesMain.Columns["NoteID"]);
            NoteView Nv = new NoteView();
            Nv.ViewType = EditViewHelper.ViewType.Existing;
            Nv.nForeignKeyID = nUserID;
            Nv.nNoteID = NoteKey;
            Nv.nNoteTypeID = (int)LM_NoteType.EnumNoteType.User;

            if (Nv.ShowDialog() == DialogResult.OK)
            {
                _loadNotes();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _saveInfo();
        }

        private void gvLicensesMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvLicensesMain.GetRowCellValue(gvLicensesMain.FocusedRowHandle, gvLicensesMain.Columns["LicenseID"]);
            if (LicenseKey != 0)
            {
                //locals
                LicenseEditView Lev = new LicenseEditView();
                LM_License oLicense = LM_License.LoadByPrimaryKey(LicenseKey);

                Lev.ViewType = EditViewHelper.ViewType.Existing;
                Lev.ViewItem = EditViewHelper.ViewItem.License;
                Lev.nLicenseID = LicenseKey;
                Lev.nRequirementTypeID = oLicense.Requirement.RequirementTypeID;
                Lev.nLocationID = oLicense.Requirement.LocationID;

                //open license view
                if (Lev.ShowDialog() == DialogResult.OK)
                {
                    _loadLicenseGrid();
                }
            }
            else
            {
                LM_Requirement oRequirement = LM_Requirement.LoadByPrimaryKey((int)gvLicensesMain.GetRowCellValue(gvLicensesMain.FocusedRowHandle, gvLicensesMain.Columns["RequirementID"]));
                LicenseEditView Lev = new LicenseEditView();
                Lev.ViewType = EditViewHelper.ViewType.New;
                Lev.ViewItem = EditViewHelper.ViewItem.License;
                Lev.nRequirementTypeID = oRequirement.RequirementTypeID;
                Lev.nLocationID = oRequirement.LocationID;
                Lev.nRequirementID = oRequirement.RequirementID;
                if (oUser.GPEmployeeID != null)
                    Lev.OwnerID = oUser.GPEmployeeID;

                //open license view
                if (Lev.ShowDialog() == DialogResult.OK)
                {
                    _loadLicenseGrid();
                }
            }
        }

        private void gvLicensesMain_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                GridView view = sender as GridView;
                Image img = null;

                DataTable oDt = gvLicenses.DataSource as DataTable;
                if ((int)oDt.Rows[e.ListSourceRowIndex]["LicenseID"] == 0)
                {
                    switch ((int)oDt.Rows[e.ListSourceRowIndex]["LockID"])
                    {
                        case (int)LM_Lock.EnumLock.NoLock:
                            e.Value = "No Lock";
                            break;
                        case (int)LM_Lock.EnumLock.SoftLock:
                            e.Value = "Soft Lock";
                            break;
                        case (int)LM_Lock.EnumLock.HardLock:
                            e.Value = "Hard Lock";
                            break;
                    }
                }
                else
                {
                    e.Value = "Incomplete";
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.DialogResult = DialogResult.OK;
            Form.ActiveForm.Close();
        }

        #endregion Event Handlers

        #endregion Methods


    }
}
