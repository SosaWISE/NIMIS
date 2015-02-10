using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using PPro.Lib.Data.Licensing;
using PPro.Lib.Data.Interim;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Diagnostics;
using PPro.Lib.Data.Recruiting;
using DevExpress.XtraGrid.Views.Grid;
using System.Text.RegularExpressions;
using PPro.Lib.Windows.LicenseManagement;

namespace PPro.WindowsClients.LicenseManagement
{

    public partial class ReportingConsole : Form
    {
        #region Constructors

        public ReportingConsole()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        private DataSet _expiringLicenses = null;

        private DataSet ExpiringLicenses(bool DataIsChanged)
        {
            if (_expiringLicenses == null || DataIsChanged)
            {
                _expiringLicenses = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_LicenseGetExpiringLicenses().GetDataSet();
            }
            return _expiringLicenses;
        }

        private enum tcMainPageIndex
        {
            pgExpiringLicenses = 0
            , pgIncompleteLicensing = 1
            , pgLocations = 2
        }

        private enum tcExpiringLicensesPageIndex
        {
            pgExpiringCompanyLicenses = 0
            , pgExpiringSalesRepLicenses = 1
            , pgExpiringTechnicianLicenses = 2
            , pgExpiringCustomerLicenses = 3
        }

        private enum tcIncompleteLicensingPageIndex
        {
            pgIncompleteCompanyLicensing = 0
            , pgIncompleteSalesRepLicensing = 1
            , pgIncompleteTechnicianLicensing = 2
            , pgIncompleteCustomerLicensing = 3
        }

        private enum tcLocationsPageIndex
        {
            pgCountries = 0
            , pgStates = 1
            , pgCounties = 2
            , pgCities = 3
            , pgTownships = 4
        }

        #endregion

        #region Methods

        #region Private

        private void _loadSelectedPage(bool DataIsChanged)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            switch (tcMain.SelectedTabPageIndex)
            {
                case (int)tcMainPageIndex.pgExpiringLicenses:
                    _loadExpiringCompanyLicenses(DataIsChanged);
                    _loadExpiringSalesRepLicenses(DataIsChanged);
                    _loadExpiringTechnicianLicenses(DataIsChanged);
                    _loadExpiringCustomerLicenses(DataIsChanged);
                    break;
                case (int)tcMainPageIndex.pgIncompleteLicensing:
                    switch (tcIncompleteLicensing.SelectedTabPageIndex)
                    {
                        case (int)tcIncompleteLicensingPageIndex.pgIncompleteCompanyLicensing:
                            _loadIncompleteCompanyLicensing(DataIsChanged);
                            break;
                        case (int)tcIncompleteLicensingPageIndex.pgIncompleteSalesRepLicensing:
                            _loadIncompleteSalesRepLicensing(DataIsChanged);
                            break;
                        case (int)tcIncompleteLicensingPageIndex.pgIncompleteTechnicianLicensing:
                            _loadIncompleteTechnicianLicensing(DataIsChanged);
                            break;
                        case (int)tcIncompleteLicensingPageIndex.pgIncompleteCustomerLicensing:
                            _loadIncompleteCustomerLicensing(DataIsChanged);
                            break;
                    }
                    break;
                case (int)tcMainPageIndex.pgLocations:
                    switch (tcLocations.SelectedTabPageIndex)
                    {
                        case (int)tcLocationsPageIndex.pgCountries:
                            _loadCountries(DataIsChanged);
                            break;
                        case (int)tcLocationsPageIndex.pgStates:
                            _loadStates(DataIsChanged);
                            break;
                        case (int)tcLocationsPageIndex.pgCounties:
                            _loadCounties(DataIsChanged);
                            break;
                        case (int)tcLocationsPageIndex.pgCities:
                            _loadCities(false);
                            break;
                        case (int)tcLocationsPageIndex.pgTownships:
                            _loadTownships(DataIsChanged);
                            break;
                    }
                    break;
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void _loadExpiringCompanyLicenses(bool DataIsChanged)
        {
            gvExpiringCompanyLicenses.DataSource = ExpiringLicenses(DataIsChanged).Tables[0];
            gvExpiringCompanyLicenses.ForceInitialize();
        }

        private void _loadExpiringSalesRepLicenses(bool DataIsChanged)
        {
            gvExpiringSalesRepLicenses.DataSource = ExpiringLicenses(DataIsChanged).Tables[1];
            gvExpiringSalesRepLicenses.ForceInitialize();
        }

        private void _loadExpiringTechnicianLicenses(bool DataIsChanged)
        {
            gvExpiringTechnicianLicenses.DataSource = ExpiringLicenses(DataIsChanged).Tables[2];
            gvExpiringTechnicianLicenses.ForceInitialize();
        }

        private void _loadExpiringCustomerLicenses(bool DataIsChanged)
        {
            gvExpiringCustomerLicenses.DataSource = ExpiringLicenses(DataIsChanged).Tables[3];
            gvExpiringCustomerLicenses.ForceInitialize();
        }

        private void _loadIncompleteCompanyLicensing(bool DataIsChanged)
        {
            if (DataIsChanged || gvIncompleteCompanyLicensing.DataSource == null)
            {
                gvIncompleteCompanyLicensing.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_RequirementGetIncompleteCompanyLicensing().GetDataSet().Tables[0];
                gvIncompleteCompanyLicensing.ForceInitialize();
            }
        }

        private void _loadIncompleteSalesRepLicensing(bool DataIsChanged)
        {
            if (DataIsChanged || gvIncompleteSalesRepLicensing.DataSource == null)
            {
                gvIncompleteSalesRepLicensing.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_RequirementGetIncompleteSalesRepLicensing(RU_Season.Current.SeasonID).GetDataSet().Tables[0];
                gvIncompleteSalesRepLicensing.ForceInitialize();
            }
        }

        private void _loadIncompleteTechnicianLicensing(bool DataIsChanged)
        {
            if (DataIsChanged || gvIncompleteTechnicianLicensing.DataSource == null)
            {
                gvIncompleteTechnicianLicensing.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_RequirementGetIncompleteTechLicensing(RU_Season.Current.SeasonID).GetDataSet().Tables[0];
                gvIncompleteTechnicianLicensing.ForceInitialize();
            }
        }

        private void _loadIncompleteCustomerLicensing(bool DataIsChanged)
        {
            if (DataIsChanged || gvIncompleteCustomerLicensing.DataSource == null)
            {
                gvIncompleteCustomerLicensing.DataSource = PPro.Lib.Data.Licensing.StoredProcedureManager.LM_RequirementGetIncompleteCustomerLicensing().GetDataSet().Tables[0];
                gvIncompleteCustomerLicensing.ForceInitialize();
            }
        }

        private void _loadCountries(bool DataIsChanged)
        {
            
            if (DataIsChanged || gvCountries.DataSource == null)
            {
                gvCountries.DataSource = dnCountries.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_LeadGetNonSolicitingAndNewCountries().GetDataSet().Tables[0];
                gvCountries.ForceInitialize();
            }
        }

        private void _loadStates(bool DataIsChanged)
        {
            if (DataIsChanged || gvStates.DataSource == null)
            {
                gvStates.DataSource = dnStates.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_LeadGetNonSolicitingAndNewStates().GetDataSet().Tables[0];
                gvStates.ForceInitialize();
            }
        }

        private void _loadCounties(bool DataIsChanged)
        {
            if (DataIsChanged || gvCounties.DataSource == null)
            {
                gvCounties.DataSource = dnCounties.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_LeadGetNonSolicitingAndNewCounties().GetDataSet().Tables[0];
                gvCounties.ForceInitialize();
            }
        }

        private void _loadCities(bool DataIsChanged)
        {
            if (DataIsChanged || gvCities.DataSource == null)
            {
                gvCities.DataSource = dnCities.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_LeadGetNonSolicitingAndNewCities().GetDataSet().Tables[0];
                gvCities.ForceInitialize();
            }
        }

        private void _loadTownships(bool DataIsChanged)
        {
            if (DataIsChanged || gvTownships.DataSource == null)
            {
                gvTownships.DataSource = dnTownships.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_LeadGetNonSolicitingTownships().GetDataSet().Tables[0];
                gvTownships.ForceInitialize();
            }
        }

        private void _deleteSelectedRows(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            DataRow[] oRows = new DataRow[view.SelectedRowsCount];
            for (int i = 0; i < view.SelectedRowsCount; i++)
                oRows[1] = view.GetDataRow(view.GetSelectedRows()[i]);
            view.BeginSort();
            foreach (DataRow currRow in oRows)
                currRow.Delete();
            view.EndSort();
        }

        #endregion

        #region Event Handlers

        private void ReportingConsole_Load(object sender, EventArgs e)
        {
            _loadSelectedPage(false);
        }

        private void tcExpiringLicenses_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            _loadSelectedPage(false);
        }

        private void tcIncompleteLicensing_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            _loadSelectedPage(false);
        }

        private void tcLocations_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            _loadSelectedPage(false);
        }

        private void tcMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            _loadSelectedPage(false);
        }

        private void gvExpiringCompanyLicensesMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvExpiringCompanyLicensesMain.GetRowCellValue(gvExpiringCompanyLicensesMain.FocusedRowHandle, gvExpiringCompanyLicensesMain.Columns["LicenseID"]);
            LM_License oLicense = LM_License.LoadByPrimaryKey(LicenseKey);

            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.Existing;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nLicenseID = LicenseKey;
            Lev.nRequirementTypeID = oLicense.Requirement.RequirementTypeID;
            Lev.nLocationID = oLicense.Requirement.LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadSelectedPage(true);
            }
        }

        private void gvExpiringSalesRepLicensesMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvExpiringSalesRepLicensesMain.GetRowCellValue(gvExpiringSalesRepLicensesMain.FocusedRowHandle, gvExpiringSalesRepLicensesMain.Columns["LicenseID"]);
            LM_License oLicense = LM_License.LoadByPrimaryKey(LicenseKey);

            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.Existing;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nLicenseID = LicenseKey;
            Lev.nRequirementTypeID = oLicense.Requirement.RequirementTypeID;
            Lev.nLocationID = oLicense.Requirement.LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadSelectedPage(true);
            }
        }

        private void gvExpiringTechnicianLicensesMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvExpiringTechnicianLicensesMain.GetRowCellValue(gvExpiringTechnicianLicensesMain.FocusedRowHandle, gvExpiringTechnicianLicensesMain.Columns["LicenseID"]);
            LM_License oLicense = LM_License.LoadByPrimaryKey(LicenseKey);

            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.Existing;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nLicenseID = LicenseKey;
            Lev.nRequirementTypeID = oLicense.Requirement.RequirementTypeID;
            Lev.nLocationID = oLicense.Requirement.LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadSelectedPage(true);
            }
        }

        private void gvExpiringCustomerLicensesMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvExpiringCustomerLicensesMain.GetRowCellValue(gvExpiringCustomerLicensesMain.FocusedRowHandle, gvExpiringCustomerLicensesMain.Columns["LicenseID"]);
            LM_License oLicense = LM_License.LoadByPrimaryKey(LicenseKey);

            LicenseEditView Lev = new LicenseEditView();
            Lev.ViewType = EditViewHelper.ViewType.Existing;
            Lev.ViewItem = EditViewHelper.ViewItem.License;
            Lev.nLicenseID = LicenseKey;
            Lev.nRequirementTypeID = oLicense.Requirement.RequirementTypeID;
            Lev.nLocationID = oLicense.Requirement.LocationID;

            //open license view
            if (Lev.ShowDialog() == DialogResult.OK)
            {
                _loadSelectedPage(true);
            }
        }

        private void gvIncompleteCompanyLicensingMain_DoubleClick(object sender, EventArgs e)
        {
            int LicenseKey = (int)gvIncompleteCompanyLicensingMain.GetRowCellValue(gvIncompleteCompanyLicensingMain.FocusedRowHandle, gvIncompleteCompanyLicensingMain.Columns["LicenseID"]);
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
                    _loadSelectedPage(true);
                }
            }
            else
            {
                LM_Requirement oRequirement = LM_Requirement.LoadByPrimaryKey((int)gvIncompleteCompanyLicensingMain.GetRowCellValue(gvIncompleteCompanyLicensingMain.FocusedRowHandle, gvIncompleteCompanyLicensingMain.Columns["RequirementID"]));
                LicenseEditView Lev = new LicenseEditView();
                Lev.ViewType = EditViewHelper.ViewType.New;
                Lev.ViewItem = EditViewHelper.ViewItem.License;
                Lev.nRequirementTypeID = oRequirement.RequirementTypeID;
                Lev.nLocationID = oRequirement.LocationID;
                Lev.nRequirementID = oRequirement.RequirementID;

                //open license view
                if (Lev.ShowDialog() == DialogResult.OK)
                {
                    _loadSelectedPage(true);
                }
            }
        }

        private void gvIncompleteSalesRepLicensingMain_DoubleClick(object sender, EventArgs e)
        {
            MasterEmployeeFile Mef = new MasterEmployeeFile();
            Mef.nUserID = (int)gvIncompleteSalesRepLicensingMain.GetRowCellValue(gvIncompleteSalesRepLicensingMain.FocusedRowHandle, gvIncompleteSalesRepLicensingMain.Columns["UserID"]);

            if (Mef.ShowDialog() == DialogResult.OK)
            {
                _loadSelectedPage(true);
            }
        }

        private void gvIncompleteTechnicianLicensingMain_DoubleClick(object sender, EventArgs e)
        {
            MasterEmployeeFile Mef = new MasterEmployeeFile();
            Mef.nUserID = (int)gvIncompleteTechnicianLicensingMain.GetRowCellValue(gvIncompleteTechnicianLicensingMain.FocusedRowHandle, gvIncompleteTechnicianLicensingMain.Columns["UserID"]);

            if (Mef.ShowDialog() == DialogResult.OK)
            {
                _loadSelectedPage(true);
            }
        }

        private void gvIncompleteCustomerLicensing_DoubleClick(object sender, EventArgs e)
        {
            Form1 oMainForm = new Form1();

            if (oMainForm.DialogResult == DialogResult.OK)
            {
                _loadSelectedPage(true);
            }
        }

        private void gvCountriesMain_DoubleClick(object sender, EventArgs e)
        {
            LocationInfo Li = new LocationInfo();
            string CountryName = gvCountriesMain.GetRowCellValue(gvCountriesMain.FocusedRowHandle, gvCountriesMain.Columns["Name"]).ToString();
            Li.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_PoliticalCountryGetCreditsRanByCountryname(CountryName).GetDataSet().Tables[0];
            Li.Location = CountryName;
            Li.ShowDialog();
        }

        private void gvStatesMain_DoubleClick(object sender, EventArgs e)
        {
            LocationInfo Li = new LocationInfo();
            string StateName = gvStatesMain.GetRowCellValue(gvStatesMain.FocusedRowHandle, gvStatesMain.Columns["Name"]).ToString();
            Li.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_PoliticalStateGetCreditsRanByStateName(StateName).GetDataSet().Tables[0];
            Li.Location = StateName;
            Li.ShowDialog();
        }

        private void gvCountiesMain_DoubleClick(object sender, EventArgs e)
        {
            LocationInfo Li = new LocationInfo();
            string szCountyName = gvCountiesMain.GetRowCellValue(gvCountiesMain.FocusedRowHandle, gvCountiesMain.Columns["Name"]).ToString();
            string CountyName = szCountyName.Remove(szCountyName.Length - 5, 5); 
            Li.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_AddressGetCreditsRanByCountyName(CountyName).GetDataSet().Tables[0];
            Li.Location = szCountyName;
            Li.ShowDialog();
        }

        private void gvCitiesMain_DoubleClick(object sender, EventArgs e)
        {
            LocationInfo Li = new LocationInfo();
            string szCityName = gvCitiesMain.GetRowCellValue(gvCitiesMain.FocusedRowHandle, gvCitiesMain.Columns["Name"]).ToString();
            string CityName = szCityName.Remove(szCityName.Length - 4, 4);
            Li.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_AddressGetCreditsRanByCityName(CityName).GetDataSet().Tables[0];
            Li.Location = szCityName;
            Li.ShowDialog();
        }

        private void gvTownshipsMain_DoubleClick(object sender, EventArgs e)
        {
            LocationInfo Li = new LocationInfo();
            string szTownshipName = gvTownshipsMain.GetRowCellValue(gvTownshipsMain.FocusedRowHandle, gvTownshipsMain.Columns["Name"]).ToString();
            string TownshipName = szTownshipName.Remove(szTownshipName.Length - 4, 4);
            Li.DataSource = PPro.Lib.Data.Interim.StoredProcedureManager.MC_AddressGetCreditsRanByCityName(TownshipName).GetDataSet().Tables[0];
            Li.Location = szTownshipName;
            Li.ShowDialog();
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form1 oMainForm = new Form1();

            if (oMainForm.ShowDialog() == DialogResult.OK)
            {
                _loadSelectedPage(true);
            }
        }

        private void dnStates_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (MessageBox.Show("Are you sure you want to ignore this selection?", "Ignore", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    gvStatesMain.DeleteRow(gvStatesMain.FocusedRowHandle);
                    //PPro.Lib.Windows.Forms.Utilities.PlaySound(@"C:\Documents and Settings\dfrost\Desktop\wha-bam.wav", 0, 0);
                    //_deleteSelectedRows(gvStatesMain);
                }
            }

        }

        private void dnCountries_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (MessageBox.Show("Are you sure you want to ignore this selection?", "Ignore", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    gvCountriesMain.DeleteRow(gvCountriesMain.FocusedRowHandle);
                    //_deleteSelectedRows(gvCountriesMain);
                }
            }
        }

        private void dnCounties_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (MessageBox.Show("Are you sure you want to ignore this selection?", "Ignore", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    gvCountiesMain.DeleteRow(gvCountiesMain.FocusedRowHandle);
                    //_deleteSelectedRows(gvCountiesMain);
                }
            }
        }

        private void dnCities_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (MessageBox.Show("Are you sure you want to ignore this selection?", "Ignore", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    gvCitiesMain.DeleteRow(gvCitiesMain.FocusedRowHandle);
                    //_deleteSelectedRows(gvCitiesMain);
                }
            }
        }

        private void dnTownships_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (MessageBox.Show("Are you sure you want to ignore this selection?", "Ignore", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    gvTownshipsMain.DeleteRow(gvTownshipsMain.FocusedRowHandle);
                    //_deleteSelectedRows(gvTownshipsMain);
                }
            }
        }

        #endregion

        #endregion

		public override string ToString()
		{
			return this.Text;
		}
    }

}
