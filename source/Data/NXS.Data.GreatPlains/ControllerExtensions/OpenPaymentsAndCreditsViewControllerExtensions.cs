using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using AR = NXS.Data.GreatPlains.OpenPaymentsAndCreditsView;
using ARCollection = NXS.Data.GreatPlains.OpenPaymentsAndCreditsViewCollection;
using ARController = NXS.Data.GreatPlains.OpenPaymentsAndCreditsViewController;

namespace NXS.Data.GreatPlains.ControllerExtensions
{
	public static class OpenPaymentsAndCreditsViewControllerExtensions
	{
		public static ARCollection GetByCustomer(this ARController controller, string customerNumber)
		{
			Query qry = AR.Query()
							.WHERE(AR.Columns.CustomerNumber, customerNumber);

			return controller.LoadCollection(qry);
		}

		public static ARCollection GetAll(this ARController controller)
		{
			Query qry = AR.Query()
							.ORDER_BY(AR.Columns.CustomerNumber);

			return controller.LoadCollection(qry);
		}
	}
}