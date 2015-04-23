using NXS.DataServices.Crm;

namespace WebModules.Crm.Ticket
{
	public class TeamsModule : BaseModule
	{
		TicketService Srv { get { return new TicketService(this.User.GPEmployeeID); } }

		public TeamsModule()
			: base("/Ticket/Teams")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/", true] = async (x, ct) =>
			{
				return await Srv.TeamsAsync().ConfigureAwait(false);
			};
			//Post["/{id:int}", true] = async (x, ct) =>
			//{
			//	var input = this.Bind<AeCustomerAccountInput>();
			//	return await Srv.SaveTeamAsync((int)x.id).ConfigureAwait(false);
			//};
		}
	}
}
