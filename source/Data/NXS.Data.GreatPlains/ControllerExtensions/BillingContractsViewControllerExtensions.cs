using AR = NXS.Data.GreatPlains.BillingContractsView;
using ARCollection = NXS.Data.GreatPlains.BillingContractsViewCollection;
using ARController = NXS.Data.GreatPlains.BillingContractsViewController;

namespace NXS.Data.GreatPlains.ControllerExtensions
{
	public static class BillingContractsViewControllerExtensions
	{
		public static ARCollection GetBillingContractsByAccount(this ARController controller, int accountID)
		{
			return controller.LoadCollection(AR.Query().WHERE(AR.Columns.CustomerNumber, accountID.ToString()));
		}
	}
}