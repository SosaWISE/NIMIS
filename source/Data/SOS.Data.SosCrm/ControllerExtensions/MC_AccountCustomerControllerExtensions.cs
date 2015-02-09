using AR = SOS.Data.SosCrm.MC_AccountCustomer;
using ARCollection = SOS.Data.SosCrm.MC_AccountCustomerCollection;
using ARController = SOS.Data.SosCrm.MC_AccountCustomerController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MC_AccountCustomerControllerExtensions
	{
		public static AR ByAccountId(this ARController cntlr, long accountId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.AccountId, accountId);

			return cntlr.LoadSingle(qry);
		}
	}
}
