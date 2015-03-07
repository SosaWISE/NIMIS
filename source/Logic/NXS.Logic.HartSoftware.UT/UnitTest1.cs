using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSE.FOS.RunCreditServices.Models;
using SOS.Data.HumanResource;
using SOS.Data.SosCrm;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;

namespace NXS.Logic.HartSoftware.UT
{
	[TestClass]
	public class UnitTest1
	{
		#region Test Initialization.

		[AssemblyInitialize]
		public static void AssemblyInit(TestContext context)
		{
			System.Diagnostics.Debug.WriteLine("Testng Hart Post Calls.");

			MainConfig.Instance.Initialize();
		}

		#endregion Test Initialization.

		[TestMethod]
		public void TestMethod1()
		{
			var hartService = new Main();

			// var lead = new SosCrmDataContext().QL_Leads.LoadByPrimaryKey(1081385);
			//var lead = new SosCrmDataContext().QL_Leads.LoadByPrimaryKey(1081386);
			var lead = new SosCrmDataContext().QL_Leads.LoadByPrimaryKey(1081387);

			IWSLead oWSLead = new WSLead(lead);
			IWSAddress wsAddress = new WSAddress(lead.Address);
			string[] szaBureaus = { "TU" };
			var ruSeason = HumanResourceDataContext.Instance.RU_Seasons.LoadByPrimaryKey(lead.SeasonId);
			IWSSeason season = new WSSeason(ruSeason);
			const string AD_USERNAME = "ASOSA001";
			var messageList = new List<WSMessage>();
			IWSCreditReportInfo wsCreditReportInfo;

			var result = hartService.RunService(oWSLead, wsAddress, szaBureaus, season, AD_USERNAME, ref messageList,
				out wsCreditReportInfo);

			foreach (var wsMessage in messageList)
			{
				Assert.IsTrue(wsMessage.MessageType == ErrorMessageType.Success, string.Format("The following error was generated: {0}", wsMessage.DisplayMessage));
			}

			Assert.IsTrue(result, "There was an error when executing a credit.");
		}
	}
}
