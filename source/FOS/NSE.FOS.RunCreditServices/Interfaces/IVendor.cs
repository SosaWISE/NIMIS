using System.Collections.Generic;
using SOS.Lib.Core.CreditReportService;

namespace NSE.FOS.RunCreditServices.Interfaces
{
	public interface IVendor
	{
		bool RunCredit(IWSLead wsLead, IWSAddress wsAddress, string[] bureausList, IWSSeason season, string username, ref List<WSMessage> messageList, out IWSCreditReportInfo crInfo);
	}
}