using AR = SOS.Data.SosCrm.SAE_BillingHistory;
using ARCollection = SOS.Data.SosCrm.SAE_BillingHistoryCollection;
using ARController = SOS.Data.SosCrm.SAE_BillingHistoryController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class SAE_BillingHistoryControllerExtensions
	{
		public static ARCollection GetByCMFID(this ARController cntlr, long cmfid)
		{
			return cntlr.LoadCollection(AR.Query().WHERE(AR.Columns.CustomerMasterFileId, cmfid));
		}

		public static ARCollection GetByAccountId(this ARController cntlr, long accountId)
		{
			return cntlr.LoadCollection(AR.Query().WHERE(AR.Columns.AccountId, accountId));
		}
	}
}
