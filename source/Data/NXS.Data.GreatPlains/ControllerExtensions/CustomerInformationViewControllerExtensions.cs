using SubSonic;
using AR = NXS.Data.GreatPlains.CustomerInformationView;
using ARCollection = NXS.Data.GreatPlains.CustomerInformationViewCollection;
using ARController = NXS.Data.GreatPlains.CustomerInformationViewController;

namespace NXS.Data.GreatPlains.ControllerExtensions
{
	public static class CustomerInformationViewControllerExtensions
	{
		public static AR GetByCustomerNumber(this ARController controller, string customerNumber)
		{
			Query qry = AR.Query()
							.WHERE(AR.Columns.CustomerNumber, customerNumber);

			return controller.LoadSingle(qry);
		}
	}
}