using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class AccountHoldsModule : BaseModule
	{
		AccountHoldsService Srv { get { return new AccountHoldsService(this.User.GPEmployeeID); } }

		public AccountHoldsModule()
			: base("/Ms/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			// get holds on an account
			Get["/{id:long}/Holds", true] = async (x, ct) =>
			{
				var accountId = (long)x.id;
				return await Srv.Holds(accountId).ConfigureAwait(false);
			};

			// add a hold on an account
			Post["/{id:long}/Holds", true] = async (x, ct) =>
			{
				var input = this.BindBody<MsHoldNew>();
				input.AccountId = (long)x.id;
				return await Srv.NewHold(input).ConfigureAwait(false);
			};
		}
	}
}
