using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class AccountSalesInformationsModule : BaseModule
	{
		public AccountSalesInformationsModule()
			: base("/Ms/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/{id:long}/AccountSalesInformations", true] = async (x, ct) =>
			{
				var srv = new AccountsService(this.User.GPEmployeeID);
				return await srv.AccountSalesInformation((long)x.id).ConfigureAwait(false);
			};
			Post["/{id:long}/AccountSalesInformations", true] = async (x, ct) =>
			{
				var srv = new AccountsService(this.User.GPEmployeeID);
				var input = this.BindBody<MsAccountSalesInformation>();
				input.ID = x.id;
				return await srv.SaveAccountSalesInformation(input).ConfigureAwait(false);
			};
		}
	}
}
