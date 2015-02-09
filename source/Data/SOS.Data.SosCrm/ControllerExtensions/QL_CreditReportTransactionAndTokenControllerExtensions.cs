using AR = SOS.Data.SosCrm.QL_CreditReportTransactionAndTokenView;
using ARCollection = SOS.Data.SosCrm.QL_CreditReportTransactionAndTokenViewCollection; 
using ARController = SOS.Data.SosCrm.QL_CreditReportTransactionAndTokenViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class QL_CreditReportTransactionAndTokenControllerExtensions
	{
		public static AR Get(this ARController cntlr, long cmfid)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.QL_CreditReportTransactionAndTokenViewGet(cmfid));
		}
	}
}
