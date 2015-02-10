using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using PPro.Lib.Data.Licensing;
using PPro.Lib.Data.Recruiting;
using DevExpress.XtraEditors.Controls;
using PPro.Lib.Windows.LicenseManagement;

namespace PPro.WindowsClients.LicenseManagement
{
    public partial class LocationEditView : Form
    {
        #region Constructors

        public LocationEditView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public int nParentLocationID = 0;

        public int nLocationID = 0;

        private bool FormIsValid(ref string szErrMsg)
        {
            bool Isvalid = true;
            int ntest;
            if (tbLocationName.Text == "")
            {
                szErrMsg = szErrMsg + "You must enter a location name. \r\n";
                Isvalid = false;
            }
            return Isvalid;
        }

        private LM_Location _parentLocation = null;

        private LM_Location ParentLocation
        {
            get
            {
                if (_parentLocation == null)
                {
                    _parentLocation = new LM_Location();
                    _parentLocation.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationID, nParentLocationID).ExecuteReader());
                }
                return _parentLocation;
            }
        }

        private LM_Location _location = null;

        private LM_Location CurrentLocation
        {
            get
            {
                if(_location == null)
                {
                    _location = new LM_Location();
                    _location = LM_Location.LoadByPrimaryKey(nLocationID);
                }
                return _location;
            }
        }

        public string ViewType = "";

        #endregion

        #region Methods

        #region Private

        private void _UpdateExisting()
        {
            LM_Location oLocation = new LM_Location();
            oLocation = LM_Location.LoadByPrimaryKey(nLocationID);
            oLocation.LocationName = tbLocationName.Text;
            oLocation.Abbreviation = (tbAbbreviation.Text != "") ? tbAbbreviation.Text.ToUpper() : "";
            oLocation.CanSolicit = cbCanSolicit.Checked;
            oLocation.ModifiedByDate = DateTime.Now;
            oLocation.ModifiedByID = LicenseManagementHelper.LoggedInUser;
            oLocation.Save();

            Form.ActiveForm.DialogResult = DialogResult.OK;
            Form.ActiveForm.Close();
        }

        private void _loadExisting()
        {
            tbLocationName.Text = CurrentLocation.LocationName;
            tbAbbreviation.Text = (CurrentLocation.Abbreviation != null) ? CurrentLocation.Abbreviation : "";
            cbCanSolicit.Checked = CurrentLocation.CanSolicit;
            lbModifiedBy.Text = CurrentLocation.ModifiedByID + " " + CurrentLocation.ModifiedByDate.ToShortDateString();
        }

        private void _SaveNew()
        {
            if (nParentLocationID != 0)
            {
                //locals
                LM_Location oLocation = new LM_Location();

                //save info
                oLocation.LocationTypeID = ParentLocation.LocationTypeID + 1;//1 entitiy lower than the parent;
                oLocation.ParentLocationID = nParentLocationID;
                oLocation.LocationName = tbLocationName.Text;
                oLocation.Abbreviation = (tbAbbreviation.Text != "") ? tbAbbreviation.Text.ToUpper() : "";
                oLocation.CanSolicit = cbCanSolicit.Checked;
                oLocation.IsActive = true;
                oLocation.IsDeleted = false;
				oLocation.CreatedByID = oLocation.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                oLocation.CreatedByDate = DateTime.Now;
                oLocation.ModifiedByDate = DateTime.Now;
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
            else
            {
                MessageBox.Show("No parent Location ID could be found", "Unable to save");
            }
            Form.ActiveForm.DialogResult = DialogResult.OK;
            Form.ActiveForm.Close();
        }

        #endregion Private

        #region Event Handlers

        private void LocationEditView_Load(object sender, EventArgs e)
        {
            if (ViewType == EditViewHelper.ViewType.Existing)
            {
                _loadExisting();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string szErrMsg = "";
            if (FormIsValid(ref szErrMsg))
            {
                switch (ViewType)
                {
                    case EditViewHelper.ViewType.New:
                        {
                            _SaveNew();
                        }
                        break;
                    case EditViewHelper.ViewType.Existing:
                        {
                            _UpdateExisting();
                        }
                        break;
                }
                
            }
            else
            {
                MessageBox.Show(szErrMsg);
            }
        }

        private void LocationEditView_Activated(object sender, EventArgs e)
        {
            tbLocationName.Focus();
            if (ViewType == EditViewHelper.ViewType.New)
            {
                switch (ParentLocation.LocationTypeID)
                {
                    case (int)LM_LocationType.EnumLocationType.Global:
                        Form.ActiveForm.Text = "Add a Country to " + ParentLocation.LocationName;
                        break;
                    case (int)LM_LocationType.EnumLocationType.Country:
                        Form.ActiveForm.Text = "Add a State to " + ParentLocation.LocationName;
                        break;
                    case (int)LM_LocationType.EnumLocationType.State:
                        Form.ActiveForm.Text = "Add a County to " + ParentLocation.LocationName;
                        break;
                    case (int)LM_LocationType.EnumLocationType.County:
                        Form.ActiveForm.Text = "Add a City to " + ParentLocation.LocationName;
                        break;
                    case (int)LM_LocationType.EnumLocationType.City:
                        Form.ActiveForm.Text = "Add a Township to " + ParentLocation.LocationName;
                        break;
                }
            }
            else
            {
                Form.ActiveForm.Text = "Edit Information on " + CurrentLocation.LocationName;
            }
        }

        #endregion EventHandlers

        #endregion Methods
    }
}
