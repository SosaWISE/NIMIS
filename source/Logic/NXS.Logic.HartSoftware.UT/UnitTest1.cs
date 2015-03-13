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
			//ExecuteByLeadId(1081385);
			//ExecuteByLeadId(1081386);
			//ExecuteByLeadId(1081387);
			//ExecuteByLeadId(1081393);
			//ExecuteByLeadId(1081394);
			//ExecuteByLeadId(1081395);
			//ExecuteByLeadId(1081396);
			//ExecuteByLeadId(1081397);
			//ExecuteByLeadId(1081398);
			//ExecuteByLeadId(1081399);
			//ExecuteByLeadId(1081400);
			//ExecuteByLeadId(1081401);
			//ExecuteByLeadId(1081402);
			//ExecuteByLeadId(1081403);
			//ExecuteByLeadId(1081404);
			//ExecuteByLeadId(1081405);
			//ExecuteByLeadId(1081406);
			//ExecuteByLeadId(1081407);
			//ExecuteByLeadId(1081408);
			//ExecuteByLeadId(1081409);
			//ExecuteByLeadId(1081410);
			//ExecuteByLeadId(1081411);
			//ExecuteByLeadId(1081412);
			//ExecuteByLeadId(1081413);
			//ExecuteByLeadId(1081414);
			//ExecuteByLeadId(1081415);
			//ExecuteByLeadId(1081416);
			//ExecuteByLeadId(1081417);
			//ExecuteByLeadId(1081418);
			//ExecuteByLeadId(1081419);
			//ExecuteByLeadId(1081420);
			//ExecuteByLeadId(1081421);
			//ExecuteByLeadId(1081422);
			//ExecuteByLeadId(1081423);
			//ExecuteByLeadId(1081424);
			//ExecuteByLeadId(1081425);
			//ExecuteByLeadId(1081426);
			//ExecuteByLeadId(1081427);
			//ExecuteByLeadId(1081428);
			//ExecuteByLeadId(1081429);
			//ExecuteByLeadId(1081430);
			//ExecuteByLeadId(1081431);
			//ExecuteByLeadId(1081432);
			//ExecuteByLeadId(1081433);
			//ExecuteByLeadId(1081434);
			//ExecuteByLeadId(1081435);
			//ExecuteByLeadId(1081436);
			//ExecuteByLeadId(1081437);
			//ExecuteByLeadId(1081438);
			//ExecuteByLeadId(1081439);
			//ExecuteByLeadId(1081440);
		}

		private void ExecuteByLeadId(long leadId)
		{
			var hartService = new Main();
			var lead = new SosCrmDataContext().QL_Leads.LoadByPrimaryKey(leadId);
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
