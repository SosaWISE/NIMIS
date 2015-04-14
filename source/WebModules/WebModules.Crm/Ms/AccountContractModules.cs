using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class AccountContractModules : BaseModule
	{
		public AccountContractModules()
			: base("/Ms/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			// get contract on an account
			Get["/{id:long}/Contract", true] = async (x, ct) =>
			{
				var srv = new AccountContractService(this.User.GPEmployeeID);
				long accountId = x.id;
				return await srv.AccountContract(accountId).ConfigureAwait(false);
			};
			Post["/{id:long}/Contract", true] = async (x, ct) =>
			{
				var srv = new AccountContractService(this.User.GPEmployeeID);
				long accountId = x.id;
				var input = this.BindBody<AeContract>();
				return await srv.SaveContract(accountId, input).ConfigureAwait(false);
			};
		}
	}
}
