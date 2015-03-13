using NXS.Data.Crm;
using NXS.Data.Crm.Repos;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class ContractAdminService
	{
		//System.Data.Common.DbConnection _cn;
		//public ContractAdminService(System.Data.Common.DbConnection cn)
		//{
		//	_cn = cn;
		//}

		public async Task<Result<AeCustomer>> CustomerByTypeAsync(long accountId, string customerTypeId)
		{
			using (var db = CrmDb.Connect())
			{
				var item = await db.AE_Customers.CustomerByTypeAsync(accountId, customerTypeId).ConfigureAwait(false);
				var result = new Result<AeCustomer>(value: AeCustomer.FromDb(item, nullable: true));
				return result;
			}
		}

		public async Task<Result<AeCustomer>> SetCustomerFromLeadAsync(long accountId, string customerTypeId, long leadID)
		{
			using (var db = CrmDb.Connect())
			{
				var lead = db.QL_Leads.ByIdAsync(leadID);

				throw new NotImplementedException();
			}
		}
	}
}
