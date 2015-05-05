using NXS.DataServices.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;
using Nancy.Authentication.Token;
using NXS.Lib.Web;

namespace WebModules.AuthControl
{
	public class ActionRequestsModule : BaseModule
	{
		ActionRequestsService Srv { get { return new ActionRequestsService(this.User.GPEmployeeID); } }

		public ActionRequestsModule(
			TokenAuthenticationConfiguration tokenConfig
		)
			: base("/AC/ActionRequests")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			// create action request
			Post["/", true] = async (x, ct) =>
			{
				var input = this.BindBody<AcActionRequestNew>();
				input.UserId = this.User.UserID;
				byte[] authNum = SystemUserIdentity.NewAuthNum();
				input.ActionKey = SystemUserIdentity.AuthNumToKey(authNum);
				var result = await Srv.CreateActionRequestAsync(input).ConfigureAwait(false);
				if (result.Value != null)
				{
					var authInfo = AuthInformation.Create(SystemUserIdentity.AuthTypes.ActionRequest, authNum);
					var identity = new TokenUserIdentity(authInfo, this.User.UserName);
					result.Value.Token = tokenConfig.Tokenizer.Tokenize(identity, this.Context);
				}
				return result;
			};
		}
	}
}
