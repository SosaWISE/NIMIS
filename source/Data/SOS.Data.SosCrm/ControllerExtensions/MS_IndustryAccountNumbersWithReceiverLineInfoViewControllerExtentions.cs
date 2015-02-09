using AR = SOS.Data.SosCrm.MS_IndustryAccountNumbersWithReceiverLineInfoView;
using ARCollection = SOS.Data.SosCrm.MS_IndustryAccountNumbersWithReceiverLineInfoViewCollection;
using ARController = SOS.Data.SosCrm.MS_IndustryAccountNumbersWithReceiverLineInfoViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_IndustryAccountNumbersWithReceiverLineInfoViewControllerExtentions
	{
		public static ARCollection Get(this ARController cntlr, long accountId, string gpEmployeeId)
		{
			return
				cntlr.LoadCollection(
					SosCrmDataStoredProcedureManager.MS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID(accountId, gpEmployeeId));
		}
	}
}
