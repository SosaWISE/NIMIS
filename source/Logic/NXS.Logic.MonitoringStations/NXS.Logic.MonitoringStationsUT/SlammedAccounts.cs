using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Logic.MonitoringStations;
using NXS.Logic.MonitoringStations.Models.Slammed;

namespace NXS.Logic.MonitoringStationsUT
{
	[TestClass]
	public class SlammedAccounts
	{
		#region Properteis

		private const string _USERID = "wsi_828070003";
		private const string _PASWRD = "password";

		#endregion Properteis
		
		#region Test Methods

		[TestMethod]
		public void TestNonSlammed()
		{
			var moniService = new Monitronics(_USERID, _PASWRD);

			var inputFields = new SlammedInputFields
			{
				Address1 = "1184 N 840 E",
				City = "OREM",
				State = "UT",
				Zip = "84097",
				ApplicationName = "Sammy Not Slammed",
				ProcessName = SlammedInputFields.ProcessNameEnum.CreditCheck,
				DealerNumber = "123213",
				DealerName = "Nexsense",
				FirstName = "Andres",
				LastName = "Sosa"
			};

			var result = moniService.IsSlammedAccount(inputFields);

			Assert.IsTrue(result.MatchCode == 0, "Looks like we got a match.  Not good.");
		}

		[TestMethod]
		public void TestSlammedAccount()
		{
			var moniService = new Monitronics(_USERID, _PASWRD);

			var inputFields = new SlammedInputFields
			{
				Address1 = "934 22ND AVE",
				City = "LONGVIEW",
				State = "WA",
				Zip = "98632",
				ApplicationName = "Sammy Not Slammed",
				ProcessName = SlammedInputFields.ProcessNameEnum.CreditCheck,
				DealerNumber = "123213",
				DealerName = "Nexsense",
				FirstName = "Michael",
				LastName = "Sousa",
				Phone1 = "3604257063"
			};

			var result = moniService.IsSlammedAccount(inputFields);

			Assert.IsTrue(result.MatchCode != 0, "Looks like we did not get a match.  Not good.");

			var matchResult = moniService.SlammedReason(result.MatchID);

			Assert.IsFalse(string.IsNullOrEmpty(matchResult), "Looks like no reason was returned.");

		}

		#endregion Test Methods
	}
}
