using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class QualifyService
	{
		string _gpEmployeeId;
		public QualifyService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<QlLead>> MasterFileLeadAsync(long cmfid, string customerTypeId)
		{
			using (var db = DBase.Connect())
			{
				var item = await db.QL_Leads.MasterFileLeadAsync(cmfid, customerTypeId).ConfigureAwait(false);
				var result = new Result<QlLead>(value: QlLead.FromDb(item, nullable: true));
				return result;
			}
		}

		public async Task<Result<List<QlLead>>> MasterFileLeadsAsync(long cmfid)
		{
			using (var db = DBase.Connect())
			{
				var items = await db.QL_Leads.MasterFileLeadsAsync(cmfid).ConfigureAwait(false);
				var result = new Result<List<QlLead>>(value: items.ConvertAll(item => QlLead.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<bool>> AddCustomerMasterLeadAsync(long cmfid, string customerTypeId, long leadID)
		{
			using (var db = DBase.Connect())
			{
				var added = await db.QL_CustomerMasterLeads.AddCustomerMasterLeadAsync(cmfid, customerTypeId, leadID, _gpEmployeeId).ConfigureAwait(false);
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
