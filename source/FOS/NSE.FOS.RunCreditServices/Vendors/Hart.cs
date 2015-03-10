using System.Collections.Generic;
using NSE.FOS.RunCreditServices.Interfaces;
using SOS.Lib.Core.CreditReportService;

namespace NSE.FOS.RunCreditServices.Vendors
{
	public class Hart : IVendor
	{
		public bool RunCredit(IWSLead wsLead, IWSAddress wsAddress, string[] bureausList, IWSSeason season, string username,
			ref List<WSMessage> messageList, out IWSCreditReportInfo crInfo)
		{
			var hartSoftware = new NXS.Logic.HartSoftware.Main();

			return hartSoftware.RunService(wsLead, wsAddress, bureausList, season, username, ref messageList, out crInfo);
		}
	}
}
