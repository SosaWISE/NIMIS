using AR = SOS.Data.SosCrm.AE_CustomerAccount;
using ARCollection = SOS.Data.SosCrm.AE_CustomerAccountCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerAccountController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable once InconsistentNaming
	public static class AE_CustomerAccountControllerExtensions
	{
		public static AR ByAccountId(this ARController cntlr, long accountId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.AccountId, accountId);

			return cntlr.LoadSingle(qry);
		}
		public static ARCollection GetByAccountId(this ARController cntlr, long accountId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.AccountId, accountId);

			return cntlr.LoadCollection(qry);
		}
	}
}
