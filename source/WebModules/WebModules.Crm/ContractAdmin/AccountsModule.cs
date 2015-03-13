using NXS.DataServices.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules.Crm.ContractAdmin
{
	public class AccountsModule : BaseModule
	{
		public AccountsModule()
			: base("/ContractAdmin/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/{id:long}/Customers/{customerTypeId}", true] = async (x, ct) =>
			{
				var srv = new ContractAdminService();
				return await srv.CustomerByTypeAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			};

			Post["/{id:long}/MasterLeads/{customerTypeId}/{leadID:long}", true] = async (x, ct) =>
			{
				var srv = new ContractAdminService();
				return await srv.SetCustomerFromLeadAsync((long)x.id, (string)x.customerTypeId, (long)x.leadID).ConfigureAwait(false);
			};
		}
	}
}
