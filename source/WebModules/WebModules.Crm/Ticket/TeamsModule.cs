using NXS.DataServices.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ticket
{
	public class TeamsModule : BaseModule
	{
		public TeamsModule()
			: base("/Ticket/Teams")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/", true] = async (x, ct) =>
			{
				var srv = new TicketService(this.User.GPEmployeeID);
				return await srv.TeamsAsync().ConfigureAwait(false);
			};
			//Post["/{id:int}", true] = async (x, ct) =>
			//{
			//	var srv = new TicketService(this.User.GPEmployeeID);
			//	var input = this.Bind<AeCustomerAccountInput>();
			//	return await srv.SaveTeamAsync((int)x.id).ConfigureAwait(false);
			//};
		}
	}
}
