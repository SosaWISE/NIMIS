using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Logic.MonitoringStations;
using NXS.Logic.MonitoringStations.Schemas;

namespace NXS.Logic.MonitoringStationsUT
{
	[TestClass]
	public class CreditRequestTests
	{
		#region Properteis

		private const string _USERID = "wsi_828070003";
		private const string _PASWRD = "password";
		//		private const string _SERVICE_NO = "828070003";
		private const string _CS_NO_DIGITAL_PRIMARY = "768247002";
		//		private const string _NEW_CS_NO_DIGITAL_PRIMARY = "768247001";

		#endregion Properteis

		#region Methods

		[TestMethod]
		public void GetCreditReportTest()
		{
			// ** Initialize
			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;

			Assert.IsTrue(moniService.GetCreditRequestTry(_CS_NO_DIGITAL_PRIMARY, out dsRaw));
		}

		[TestMethod]
		public void UpdateCreditRequestTest()
		{
			// ** Initialize
			var moniService = new Monitronics(_USERID, _PASWRD);
			ErrorsOnBoardAccount dsRaw;

			// This should fail since there is no XML being passed.
			Assert.IsFalse(moniService.UpdateCreditRequestTry(_CS_NO_DIGITAL_PRIMARY, _CS_NO_DIGITAL_PRIMARY, out dsRaw));


			const string XML_REPORT = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<CreditRequest xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://schemas.datacontract.org/2004/07/CreditReporting.DataController"">
<RequestId>891</RequestId>
<CS>{0}</CS>
<SSN>333445555</SSN>
<FirstName>First</FirstName>
<LastName>Last</LastName>
<StreetNumber>111</StreetNumber>
<StreetName>Street</StreetName>
<City>NewCity</City>
<State>TX</State>
<Zip>22222</Zip>
<FICO>777</FICO>
<TransactionID>123456</TransactionID>
<Token>abc123def456</Token>
<DealerId>DealerNumber</DealerId>
<UserId>UserName</UserId>
<RequestDate>2010-10-05T15:40:36.063</RequestDate>
<ProcessDate>1753-01-01T00:00:00</ProcessDate>
</CreditRequest>
";

			// This should work since there is no XML being passed.
			Assert.IsTrue(moniService.UpdateCreditRequestTry(_CS_NO_DIGITAL_PRIMARY, string.Format(XML_REPORT, _CS_NO_DIGITAL_PRIMARY), out dsRaw));

		}

		#endregion Methods
	}
}
