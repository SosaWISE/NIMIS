using Nancy.ModelBinding;
using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class AccountInvoicesModule : BaseModule
	{
		public AccountInvoicesModule()
			: base("/Ms/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			//// get invoices on an account
			//Get["/{id:long}/Invoices", true] = async (x, ct) =>
			//{
			//	var srv = new AccountInvoicesService(this.User.GPEmployeeID);
			//	long accountId = x.id;
			//	return await srv.Invoices(accountId).ConfigureAwait(false);
			//};

			// get invoice with type of INSTALL on an account
			Get["/{id:long}/Invoices/INSTALL", true] = async (x, ct) =>
			{
				var srv = new AccountInvoicesService(this.User.GPEmployeeID);
				var accountId = (long)x.id;
				bool canCreate = this.Request.Query.canCreate;
				return await srv.InstallInvoice(accountId, canCreate).ConfigureAwait(false);
			};
		}
	}
}
