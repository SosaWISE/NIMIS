using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class AccountSalesInformationsModule : BaseModule
	{
		AccountsService Srv { get { return new AccountsService(this.User.GPEmployeeID); } }

		public AccountSalesInformationsModule()
			: base("/Ms/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/{id:long}/AccountSalesInformations", true] = async (x, ct) =>
			{
				return await Srv.AccountSalesInformation((long)x.id).ConfigureAwait(false);
			};
			Post["/{id:long}/AccountSalesInformations", true] = async (x, ct) =>
			{
				var input = this.BindBody<MsAccountSalesInformation>();
				input.ID = x.id;
				return await Srv.SaveAccountSalesInformation(input).ConfigureAwait(false);
			};
		}
	}
}
