using NXS.DataServices.Crm;

namespace WebModules.Crm
{
	public class CustomerMasterFilesModule : BaseModule
	{
		public CustomerMasterFilesModule()
			: base("/Qualify/CustomerMasterFiles")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/{id:long}/Leads/{customerTypeId}", true] = async (x, ct) =>
			{
				var srv = new QualifyService();
				return await srv.MasterFileLeadAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			};
			Get["/{id:long}/Leads", true] = async (x, ct) =>
			{
				var srv = new QualifyService();
				return await srv.MasterFileLeadsAsync((long)x.id).ConfigureAwait(false);
			};

			Post["/{id:long}/MasterLeads/{customerTypeId}/{leadID:long}", true] = async (x, ct) =>
			{
				var srv = new QualifyService();
				return await srv.AddCustomerMasterLeadAsync((long)x.id, (string)x.customerTypeId, (long)x.leadID).ConfigureAwait(false);
			};
		}
	}
}
