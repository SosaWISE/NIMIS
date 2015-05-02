using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class AccountContractModules : BaseModule
	{
		AccountContractService Srv { get { return new AccountContractService(this.User.GPEmployeeID); } }

		public AccountContractModules()
			: base("/Ms/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			// get contract on an account
			Get["/{id:long}/Contract", true] = async (x, ct) =>
			{
				long accountId = x.id;
				return await Srv.AccountContract(accountId).ConfigureAwait(false);
			};
			Post["/{id:long}/Contract", true] = async (x, ct) =>
			{
				long accountId = x.id;
				var input = this.BindBody<AeContract>();
				return await Srv.SaveContract(accountId, input).ConfigureAwait(false);
			};
		}
	}
}
