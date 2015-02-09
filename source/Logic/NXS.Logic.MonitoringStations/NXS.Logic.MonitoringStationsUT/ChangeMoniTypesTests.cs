using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Logic.MonitoringStations;
using NXS.Logic.MonitoringStations.Models;

namespace NXS.Logic.MonitoringStationsUT
{


    [TestClass]
    public class ChangeMoniTypesTests
    {
        #region Properteis

		private const string _USERID = "wsi_828070003";
		private const string _PASWRD = "password";
		//		private const string _SERVICE_NO = "828070003";
		private const string _CS_NO_DIGITAL_PRIMARY = "768247000";

		#endregion Properteis

        [TestMethod]
        public void ChangeMoniTypeTest()
        {
            // ** Initlialize XML
            var changeTypes = new ChangeMonTypes
            {
                ChangeMonType = new ChangeMonTypesInfo
                {
                    FromConfig = "Cell Primary",
                    ToConfig = "Digital",
                    PanelPhone = "9722437443",
                    SitePhone1 = "2145366496"
                    
                }
            };

            var xmlizedString = changeTypes.Serialize();
            	var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Immediate("ChangeMonType", _CS_NO_DIGITAL_PRIMARY, xmlizedString, out dsRaw), string.Format("The following error was generated"));
		
        }
    }
}
