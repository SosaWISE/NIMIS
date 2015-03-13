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
	public class QualifyService
	{
		//System.Data.Common.DbConnection _cn;
		//public QualifyService(System.Data.Common.DbConnection cn)
		//{
		//	_cn = cn;
		//}

		public async Task<Result<QlLead>> MasterFileLeadAsync(long cmfid, string customerTypeId)
		{
			using (var db = CrmDb.Connect())
			{
				var item = await db.QL_Leads.MasterFileLeadAsync(cmfid, customerTypeId).ConfigureAwait(false);
				var result = new Result<QlLead>(value: QlLead.FromDb(item, nullable: true));
				return result;
			}
		}

		public async Task<Result<List<QlLead>>> MasterFileLeadsAsync(long cmfid)
		{
			using (var db = CrmDb.Connect())
			{
				var items = await db.QL_Leads.MasterFileLeadsAsync(cmfid).ConfigureAwait(false);
				var result = new Result<List<QlLead>>(value: items.ConvertAll(item => QlLead.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<bool>> AddCustomerMasterLeadAsync(long cmfid, string customerTypeId, long leadID)
		{
			using (var db = CrmDb.Connect())
			{
				var added = await db.QL_CustomerMasterLeads.AddCustomerMasterLeadAsync(cmfid, customerTypeId, leadID).ConfigureAwait(false);
				var result = new Result<bool>(
					code: added ? 0 : -1,
					message: added ? "" :
						string.Format("A CustomerMasterLead already exists for Customer Number {0} and Type {1} ", cmfid, customerTypeId),
					value: added
				);
				return result;
			}
		}
	}
}
