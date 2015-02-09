using AR = SOS.Data.SosCrm.MS_AccountSubmit;
using ARCollection = SOS.Data.SosCrm.MS_AccountSubmitCollection;
using ARController = SOS.Data.SosCrm.MS_AccountSubmitController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class MS_AccountSubmitControllerExtensions
	{
		public static ARCollection GetSuccessfulForAccount(this ARController cntlr, long accountId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.WasSuccessfull, true)
				.WHERE(AR.Columns.AccountId, accountId);

			return cntlr.LoadCollection(qry);
		}
	}
}
