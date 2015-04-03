using Nancy.ModelBinding;
using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class HoldsModule : BaseModule
	{
		public HoldsModule()
			: base("/Ms/Holds")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/Catg1s", true] = async (x, ct) =>
			{
				var srv = new AccountHoldsService(this.User.GPEmployeeID);
				return await srv.Catg1s().ConfigureAwait(false);
			};
			Get["/Catg2s", true] = async (x, ct) =>
			{
				var srv = new AccountHoldsService(this.User.GPEmployeeID);
				return await srv.Catg2s().ConfigureAwait(false);
			};

			// fix an account hold
			Post["/{id:int}", true] = async (x, ct) =>
			{
				var srv = new AccountHoldsService(this.User.GPEmployeeID);
				var input = this.Bind<HoldFix>();
				input.AccountHoldID = (int)x.id;
				return await srv.FixHold(input).ConfigureAwait(false);
			};
		}
	}
}
