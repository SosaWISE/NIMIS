using NXS.DataServices.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using System.Collections.Generic;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;

namespace WebModules.AuthControl
{
	public class GroupActionItemsModule : BaseModule
	{
		AdminService Srv { get { return new AdminService(this.User.GPEmployeeID); } }

		public GroupActionItemsModule()
			: base("/Admin/GroupActionItems")
		{
			this.RequiresPermission(applicationID: AuthApplications.AdminID, actionID: null);

			Get["/", true] = async (x, ct) =>
			{
				return await Srv.GroupActionItemsAsync().ConfigureAwait(false);
			};
			Post["/{groupName}", true] = async (x, ct) =>
			{
				var inputItems = this.BindBody<List<GroupActionItem>>();
				return await Srv.SaveGroupActionItems((string)x.groupName, inputItems).ConfigureAwait(false);
			};
		}
	}
}
