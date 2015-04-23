using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.ContractAdmin
{
	public class AccountSalesInformationExtrasModule : BaseModule
	{
		ContractAdminService Srv { get { return new ContractAdminService(this.User.GPEmployeeID); } }

		public AccountSalesInformationExtrasModule()
			: base("/ContractAdmin/AccountSalesInformationExtras")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Post["/{id:long}", true] = async (x, ct) =>
			{
				var input = this.BindBody<MsAccountSalesInformationExtras>();
				return await Srv.SaveAccountSalesInformationExtras((long)x.id, input).ConfigureAwait(false);
			};
		}
	}
}
