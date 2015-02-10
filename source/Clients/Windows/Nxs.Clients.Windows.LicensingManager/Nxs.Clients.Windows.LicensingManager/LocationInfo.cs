using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PPro.WindowsClients.LicenseManagement
{
    public partial class LocationInfo : Form
    {
        #region Constructors
        public LocationInfo()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        public DataTable DataSource = null;

        public string Location = "";

        #endregion

        #region Methods

        private void btnOkay_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.DialogResult = DialogResult.OK;
            Form.ActiveForm.Close();
        }

        private void LocationInfo_Load(object sender, EventArgs e)
        {
            if (DataSource != null)
            {
                gvInfo.DataSource = DataSource;
                gvInfo.ForceInitialize();

                gvInfoMain.Columns["CreditsRan"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gvInfoMain.Columns["CreditsRan"].SummaryItem.FieldName = "CreditsRan";
                gvInfoMain.Columns["CreditsRan"].SummaryItem.DisplayFormat = "{0} Credit(s) Ran";
            }
        }

        private void LocationInfo_Activated(object sender, EventArgs e)
        {
            Form.ActiveForm.Text = Location;
        }

        #endregion
    }
}
