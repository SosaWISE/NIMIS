using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Logic.MonitoringStations;
using NXS.Logic.MonitoringStations.Models;

namespace NXS.Logic.MonitoringStationsUT
{
	[TestClass]
	public class OosTests
	{
		#region Properteis

		private const string _USERID = "wsi_828070003";
		private const string _PASWRD = "password";
		//		private const string _SERVICE_NO = "828070003";
		private const string _CS_NO_DIGITAL_PRIMARY = "768247000";
		private const string _NEW_CS_NO_DIGITAL_PRIMARY = "768247001";

		#endregion Properteis

		#region Methods
		[TestMethod]
		public void PutAccountOnTest()
		{
			// ** Initialize OnTest Object
			var onTest = new OnTests
			{
				OnTest = new MasterMindTest
				{
					OnOffFlag = "On",
					TestCategoryId = "NEWINS",
					TestHours = "1",
					TestMinutes = "30"
				}
			};

			var xmlizedString = onTest.Serialize();
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Immediate("OnTest", _CS_NO_DIGITAL_PRIMARY, xmlizedString, out dsRaw), string.Format("The following error was generated"));
		}

		[TestMethod]
		public void PutAccountOffTest()
		{
			// ** Initialize OnTest Object
			var onTest = new OnTests
			{
				OnTest = new MasterMindTest
				{
					OnOffFlag = "Off"
				}
			};

			var xmlizedString = onTest.Serialize();
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Immediate("OnTest", _CS_NO_DIGITAL_PRIMARY, xmlizedString, out dsRaw), string.Format("The following error was generated"));
		}

		[TestMethod]
		public void PutAccountOutOfServiceTest()
		{
			// ** Initialize OOS object
			var oos = new OutOfServices
			{
				OutOfService = new OutOfServiceInfo
				{
					StatusFlag = "O",  // This is not a zero it's a capital O.
					OosCatId = "DLRREQ"
				}
			};

			var xmlizedString = oos.Serialize();
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Immediate("OutOfService", _CS_NO_DIGITAL_PRIMARY, xmlizedString, out dsRaw), string.Format("The following error was generated."));
		}

		[TestMethod]
		public void ReplaceABadPanelTest()
		{
			// ** Initialize BadPanels object
			var badPanel = new BadPanels
			{
				BadPanel = new BadPanelInfo
				{
					NewCsNo = _NEW_CS_NO_DIGITAL_PRIMARY
				}
			};

			var xmlizedString = badPanel.Serialize();
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Immediate("BadPanel", _CS_NO_DIGITAL_PRIMARY, xmlizedString, out dsRaw), string.Format("The following error was generated."));
		}

		[TestMethod]
		public void PullPanelTest()
		{
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Immediate("PullPanel", _CS_NO_DIGITAL_PRIMARY, null, out dsRaw), string.Format("The following error was generated."));

		}

		[TestMethod]
		public void LogValidateTest()
		{
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Immediate("LogValidate", _CS_NO_DIGITAL_PRIMARY, null, out dsRaw), string.Format("The following error was generated."));
		}

		#endregion Methods
	}
}
