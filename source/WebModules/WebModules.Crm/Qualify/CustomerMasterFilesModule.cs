using NXS.DataServices.Crm;

namespace WebModules.Crm
{
	public class CustomerMasterFilesModule : BaseModule
	{
		public CustomerMasterFilesModule()
			: base("/Qualify/CustomerMasterFiles")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/{cmfid:long}/Leads/{customerTypeId}", true] = async (x, ct) =>
			{
				var srv = new QualifyService(this.User.GPEmployeeID);
				return await srv.MasterFileLeadAsync((long)x.cmfid, (string)x.customerTypeId).ConfigureAwait(false);
			};
			Get["/{cmfid:long}/Leads", true] = async (x, ct) =>
			{
				var srv = new QualifyService(this.User.GPEmployeeID);
				return await srv.MasterFileLeadsAsync((long)x.cmfid).ConfigureAwait(false);
			};

			Post["/{cmfid:long}/MasterLeads/{customerTypeId}/{leadID:long}", true] = async (x, ct) =>
			{
				var srv = new QualifyService(this.User.GPEmployeeID);
				return await srv.AddCustomerMasterLeadAsync((long)x.cmfid, (string)x.customerTypeId, (long)x.leadID).ConfigureAwait(false);
			};
		}
	}
}
