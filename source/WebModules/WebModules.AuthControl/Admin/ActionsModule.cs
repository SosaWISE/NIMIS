using NXS.DataServices.AuthenticationControl;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;

namespace WebModules.AuthControl
{
	public class ActionsModule : BaseModule
	{
		AdminService Srv { get { return new AdminService(this.User.GPEmployeeID); } }

		public ActionsModule()
			: base("/Admin/Actions")
		{
			this.RequiresPermission(applicationID: AuthApplications.AdminID, actionID: null);

			Get["/", true] = async (x, ct) =>
			{
				return await Srv.ActionsAsync().ConfigureAwait(false);
			};
		}
	}
}
