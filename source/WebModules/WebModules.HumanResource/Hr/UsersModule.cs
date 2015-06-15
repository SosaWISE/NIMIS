using NXS.DataServices.HumanResource;

namespace WebModules.HumanResource.Ticket
{
	public class UsersModule : BaseModule
	{
		HrService Srv { get { return new HrService(this.User.GPEmployeeID); } }

		public UsersModule()
			: base("/Hr/Users")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/Sales", true] = async (x, ct) =>
			{
				return await Srv.SalesUsersAsync().ConfigureAwait(false);
			};
		}
	}
}
