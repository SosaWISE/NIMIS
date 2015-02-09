using AR = SOS.Data.SosCrm.MS_AccountSalesInformationsView;
using ARCollection = SOS.Data.SosCrm.MS_AccountSalesInformationsViewCollection;
using ARController = SOS.Data.SosCrm.MS_AccountSalesInformationsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountSalesInfosViewControllerExtensions
	{
		public static AR Read(this ARController cntlr, long? accountId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountSalesInfoViewRead(accountId));
		}
	}
}
