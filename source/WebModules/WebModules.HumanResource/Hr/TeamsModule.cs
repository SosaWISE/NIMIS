using NXS.DataServices.Crm;

namespace WebModules.Sales
{
	public class TeamsModule : BaseModule
	{
		TicketService Srv { get { return new TicketService(this.User.GPEmployeeID); } }

		public TeamsModule()
			: base("/Hr/Teams")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/Sales", true] = async (x, ct) =>
			{
				return await Srv.SalesTeamsAsync().ConfigureAwait(false);
			};
		}
	}
}
