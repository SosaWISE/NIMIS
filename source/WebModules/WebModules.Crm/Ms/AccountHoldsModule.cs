using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class AccountHoldsModule : BaseModule
	{
		public AccountHoldsModule()
			: base("/Ms/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			// get holds on an account
			Get["/{id:long}/Holds", true] = async (x, ct) =>
			{
				var srv = new AccountHoldsService(this.User.GPEmployeeID);
				var accountId = (long)x.id;
				return await srv.Holds(accountId).ConfigureAwait(false);
			};

			// add a hold on an account
			Post["/{id:long}/Holds", true] = async (x, ct) =>
			{
				var srv = new AccountHoldsService(this.User.GPEmployeeID);
				var input = this.BindBody<MsHoldNew>();
				input.AccountId = (long)x.id;
				return await srv.NewHold(input).ConfigureAwait(false);
			};
		}
	}
}
