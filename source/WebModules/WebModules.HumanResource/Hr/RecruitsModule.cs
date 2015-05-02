using NXS.DataServices.HumanResource;

namespace WebModules.HumanResource.Ticket
{
	public class RecruitsModule : BaseModule
	{
		HrService Srv { get { return new HrService(this.User.GPEmployeeID); } }

		public RecruitsModule()
			: base("/Hr/Recruits")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/PossibleReportTos", true] = async (x, ct) =>
			{
				int seasonID = this.Context.Request.Query.seasonID;
				int userTypeID = this.Context.Request.Query.userTypeID;
				return await Srv.PossibleReportTosAsync(seasonID, userTypeID).ConfigureAwait(false);
			};
		}
	}
}
