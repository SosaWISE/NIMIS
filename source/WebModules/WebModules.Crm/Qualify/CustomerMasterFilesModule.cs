using NXS.DataServices.Crm;

namespace WebModules.Crm
{
	public class CustomerMasterFilesModule : BaseModule
	{
		QualifyService Srv { get { return new QualifyService(this.User.GPEmployeeID); } }

		public CustomerMasterFilesModule()
			: base("/Qualify/CustomerMasterFiles")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/{cmfid:long}/Leads/{customerTypeId}", true] = async (x, ct) =>
			{
				return await Srv.MasterFileLeadAsync((long)x.cmfid, (string)x.customerTypeId).ConfigureAwait(false);
			};
			Get["/{cmfid:long}/Leads", true] = async (x, ct) =>
			{
				return await Srv.MasterFileLeadsAsync((long)x.cmfid).ConfigureAwait(false);
			};

			Post["/{cmfid:long}/MasterLeads/{customerTypeId}/{leadID:long}", true] = async (x, ct) =>
			{
				return await Srv.AddCustomerMasterLeadAsync((long)x.cmfid, (string)x.customerTypeId, (long)x.leadID).ConfigureAwait(false);
			};
		}
	}
}
