using NXS.DataServices.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.ContractAdmin
{
	public class AccountSalesInformationExtrasModule : BaseModule
	{
		public AccountSalesInformationExtrasModule()
			: base("/ContractAdmin/AccountSalesInformationExtras")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Post["/{id:long}", true] = async (x, ct) =>
			{
				var srv = new ContractAdminService(this.User.GPEmployeeID);
				var input = this.Bind<MsAccountSalesInformationExtras>();
				return await srv.SaveAccountSalesInformationExtras((long)x.id, input).ConfigureAwait(false);
			};
		}
	}
}
