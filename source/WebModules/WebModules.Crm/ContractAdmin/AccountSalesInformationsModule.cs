//using NXS.DataServices.Crm;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Nancy.ModelBinding;
//using NXS.DataServices.Crm.Models;
//
//namespace WebModules.Crm.ContractAdmin
//{
//	public class AccountSalesInformationsModule : BaseModule
//	{
//		public AccountSalesInformationsModule()
//			: base("/ContractAdmin/AccountSalesInformations")
//		{
//			this.RequiresPermission(applicationID: null, actionID: null);
//
//			Post["/{id:long}", true] = async (x, ct) =>
//			{
//				var srv = new ContractAdminService(this.User.GPEmployeeID);
//				return await srv.AccountSalesInformation((long)x.id).ConfigureAwait(false);
//			};
//		}
//	}
//}
