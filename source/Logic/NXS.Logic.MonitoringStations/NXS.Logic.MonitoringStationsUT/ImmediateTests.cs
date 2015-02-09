using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Logic.MonitoringStations;
using NXS.Logic.MonitoringStations.Models;

namespace NXS.Logic.MonitoringStationsUT
{
	[TestClass]
	public class ImmediateTests
	{
		#region Properteis

		private const string _USERID = "wsi_828070003";
		private const string _PASWRD = "password";
//		private const string _SERVICE_NO = "828070003";
		private const string _CS_NO_DIGITAL_PRIMARY = "768247000";

		#endregion Properteis

		#region Test Methods
		
		[TestMethod]
		public void DigitalAddTwoWayDeviceTest()
		{
			// ** Initialize TwoWay object
			var twoway = new Twoways
			{
				Twoway = new TwowayInfo
				{
					TwoWayDeviceId = "E00002"
				}
			};

			// Convert to XML.
			var xmlizedString = twoway.Serialize();
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Immediate("Two-Way", _CS_NO_DIGITAL_PRIMARY, xmlizedString, out dsRaw), string.Format("The following error was generated"));
		}

		[TestMethod]
		public void PullPanelTest()
		{
			// Convert to XML.
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Immediate("PullPanel", _CS_NO_DIGITAL_PRIMARY, null, out dsRaw), string.Format("The following error was generated"));
			
		}

		#endregion Test Methods
	}
}
