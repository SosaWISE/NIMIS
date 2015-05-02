using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class HoldsModule : BaseModule
	{
		AccountHoldsService Srv { get { return new AccountHoldsService(this.User.GPEmployeeID); } }

		public HoldsModule()
			: base("/Ms/Holds")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/Catg1s", true] = async (x, ct) =>
			{
				return await Srv.Catg1s().ConfigureAwait(false);
			};
			Get["/Catg2s", true] = async (x, ct) =>
			{
				return await Srv.Catg2s().ConfigureAwait(false);
			};

			// fix an account hold
			Post["/{id:int}", true] = async (x, ct) =>
			{
				var input = this.BindBody<MsHoldFix>();
				input.AccountHoldID = (int)x.id;
				return await Srv.FixHold(input).ConfigureAwait(false);
			};
		}
	}
}
