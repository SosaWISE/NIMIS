using NXS.DataServices.AuthenticationControl;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;

namespace WebModules.AuthControl
{
	public class ApplicationsModule : BaseModule
	{
		AdminService Srv { get { return new AdminService(this.User.GPEmployeeID); } }

		public ApplicationsModule()
			: base("/Admin/Applications")
		{
			this.RequiresPermission(applicationID: AuthApplications.AdminID, actionID: null);

			Get["/", true] = async (x, ct) =>
			{
				return await Srv.ApplicationsAsync().ConfigureAwait(false);
			};
		}
	}
}
