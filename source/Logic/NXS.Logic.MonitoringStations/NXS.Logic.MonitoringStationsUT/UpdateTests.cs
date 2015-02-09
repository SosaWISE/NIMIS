using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Logic.MonitoringStations;
using NXS.Logic.MonitoringStations.Models;

namespace NXS.Logic.MonitoringStationsUT
{
	[TestClass]
	public class UpdateTests
	{
		#region Properteis

		private const string _USERID = "wsi_828070003";
		private const string _PASWRD = "password";
		//		private const string _SERVICE_NO = "828070003";
		private const string _CS_NO_DIGITAL_PRIMARY = "768247000";

		#endregion Properteis

		#region Test Methods

		[TestMethod]
		public void ChangeCrossStreet()
		{
			// ** Initialize
			var account = new Account
			{
				SiteSystems = new List<SiteSystem>
				{
					new SiteSystem
					{
						CrossStreet = "N 840 E"
					}
				}
			};

			// Convert to XML.
			var xmlizedString = account.Serialize();
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			Assert.IsTrue(moniService.Update(RequestTypes.C(), _CS_NO_DIGITAL_PRIMARY, xmlizedString, out dsRaw), string.Format("The following error was generated"));
		}

		#endregion Test Methods
	}
}
