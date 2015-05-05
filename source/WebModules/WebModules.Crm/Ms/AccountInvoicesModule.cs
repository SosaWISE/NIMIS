using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class AccountInvoicesModule : BaseModule
	{
		AccountInvoicesService Srv { get { return new AccountInvoicesService(this.User.GPEmployeeID); } }

		public AccountInvoicesModule()
			: base("/Ms/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			//// get invoices on an account
			//Get["/{id:long}/Invoices", true] = async (x, ct) =>
			//{
			//	long accountId = x.id;
			//	return await Srv.Invoices(accountId).ConfigureAwait(false);
			//};

			// get invoice with type of INSTALL on an account
			Get["/{id:long}/Invoices/INSTALL", true] = async (x, ct) =>
			{
				var accountId = (long)x.id;
				bool canCreate = this.Request.Query.canCreate;
				return await Srv.InstallInvoice(accountId, canCreate).ConfigureAwait(false);
			};
		}
	}
}
