using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR = NXS.Data.GreatPlains.CustomerInvoicesView;
using ARCollection = NXS.Data.GreatPlains.CustomerInvoicesViewCollection;
using ARController = NXS.Data.GreatPlains.CustomerInvoicesViewController;

namespace NXS.Data.GreatPlains.ControllerExtensions
{
	public static class CustomerInvoicesViewControllerExtensions
	{
		public static ARCollection GetInvoicesByCustomer(this ARController controller, string customerNumber)
		{
			return controller.LoadCollection(AR.Query()
														.WHERE(AR.Columns.CustomerNumber, customerNumber)
														.ORDER_BY(AR.Columns.InvoiceDate));
		}
	}
}