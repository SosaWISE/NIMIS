using System;
using AR = SOS.Data.SosCrm.QL_CreditReport;
using ARCollection = SOS.Data.SosCrm.QL_CreditReportCollection;
using ARController = SOS.Data.SosCrm.QL_CreditReportController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class QL_CreditReportControllerExtensions
	{
		public static AR MaxScoreByCmfID(this ARController cntlr, long cmfid)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.QL_CreditReportMaxScoreByCmfID(cmfid));
		}
	}
}
