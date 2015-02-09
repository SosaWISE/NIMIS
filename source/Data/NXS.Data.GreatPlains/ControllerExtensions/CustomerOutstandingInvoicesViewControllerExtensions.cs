using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR = NXS.Data.GreatPlains.CustomerOutstandingInvoicesView;
using ARCollection = NXS.Data.GreatPlains.CustomerOutstandingInvoicesViewCollection;
using ARController = NXS.Data.GreatPlains.CustomerOutstandingInvoicesViewController;
using SubSonic;

namespace NXS.Data.GreatPlains.ControllerExtensions
{
	public static class CustomerOutstandingInvoicesViewControllerExtensions
	{
		public static CustomerOutstandingInvoicesViewCollection GetOutstandingInvoices(this ARController controller, string customerNumber)
		{
			return controller.LoadCollection(GreatPlainsStoredProcedureManager.GetCustomerOutstandingInvoices(customerNumber));
		}

		public static decimal GetMaximumPaymentAmount(this ARController controller, string customerNumber)
		{
			decimal result = 0M;

			object dbResult = GreatPlainsStoredProcedureManager.GetCustomerMaximumPaymentAmount(customerNumber).ExecuteScalar();
			if (dbResult is decimal)
				result = (decimal)dbResult;

			return result;
		}

		public static ARCollection GetAll(this ARController controller)
		{
			Query qry = AR.Query()
							.ORDER_BY(AR.Columns.CustomerNumber);

			return controller.LoadCollection(qry);
		}
	}
}