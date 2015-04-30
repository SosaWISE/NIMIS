using NXS.DataServices.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using SOS.FunctionalServices;
using SOS.Lib.Core;
using System.Collections.Generic;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;

namespace WebModules.AuthControl
{
	public class UsersModule : BaseModule
	{
		public UsersModule(
			AuthService authService
		)
			: base("/Admin/Users")
		{
			this.RequiresPermission(applicationID: AuthApplications.AdminID, actionID: null);

			Get["/{username}/Groups"] = (x) =>
			{
				string username = x.username;
				if ((bool)this.Context.Request.Query.clear)
					authService.RemoveCachedUser(username);
				var user = authService.GetUser(username);
				return new Result<string[]>(
					message: (user.UserID > 0) ? "" : "User not found",
					value: user.Groups
				);
			};
			Post["/ReloadGroupActionItems"] = (x) =>
			{
				authService.ReloadGroupActionItems();
				return new Result<string[]>();
			};
		}
	}
}
