using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nxs.Services.CorsConnext.Models;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AuthenticationControl;
using SOS.Lib.Util;
using SOS.Services.Interfaces.Models;
using SOS.Lib.Core;

namespace Nxs.Services.CorsConnext.Helpers
{
	public static class CORSSecurity
	{
		public const string AppID = SOS.Data.AuthenticationControl.AC_Application.MetaData.NXSConnextCORSID;

		/// <summary>
		/// Authorize request to perform action
		/// </summary>
		/// <typeparam name="T">Result type</typeparam>
		/// <param name="functionName">Name of calling function</param>
		/// <param name="actionID">ID of action the logged in user needs rights to in order to perform action. Null call be passed.</param>
		/// <param name="action">Action to perform</param>
		/// <returns>Returns the results from the action</returns>
		public static Result<T> Authorize<T>(string functionName, string actionID, Func<SseCmsUser, Result<T>> action)
		{
			try
			{
				// ** Initialize
				var user = GetUnitTestUser();
				if (user == null && !Authorize(AppID, actionID, out user))
				{
					return new Result<T>((int)CmsResultCodes.NotAuthorized, String.Format("Authorization Failed for '{0}'", functionName));
				}
				/** Perform action. */
				return action(user);
			}
			catch (Exception ex)
			{
				//@TODO: log exception

				// return error result
				return new Result<T>((int)CmsResultCodes.ExceptionThrown,
					string.Format("The following exception was thrown from '{0}' method: {1}", functionName, ex.Message));
			}
		}

		private static bool Authorize(string applicationID, string actionID, out SseCmsUser user)
		{
			var service = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
			UserModel authUser;
			var context = HttpContext.Current;
			var identity = context.User.Identity;
			if (identity.IsAuthenticated)
			{
				var username = identity.GetUsername();
				var result = service.GetUser(username, false);
				authUser = (result.Success) ? result.Value : null;
			}
			else
			{
				authUser = service.Authorize(context.GetUserSession(), context.ClientIPAddress(), applicationID, actionID);
			}

			user = (authUser == null) ? null : new SseCmsUser
			{
				Username = authUser.Username,
				Firstname = authUser.Firstname,
				Lastname = authUser.Lastname,
				GPEmployeeID = authUser.GPEmployeeID,
				DealerId = authUser.DealerId,
			};
			return user != null;
		}

		private static SseCmsUser GetUnitTestUser()
		{
			if (!UnitTestingHelper.CmsCORS.IsActive)
			{
				return null;
			}

			if (UnitTestingHelper.CmsCORS.SseCmsUser == null)
			{
				UnitTestingHelper.CmsCORS.AppToken = AppID;
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
				var oSrvSession = oService.SosStart(UnitTestingHelper.CmsCORS.AppToken, UnitTestingHelper.CmsCORS.IPAddress, 0);
				IFnsResult<IFnsAcCmsUser> fnsResult = oService.AuthenticateCmsUser(UnitTestingHelper.CmsCORS.Username,
					UnitTestingHelper.CmsCORS.Password,
					oSrvSession.SessionID,
					UnitTestingHelper.CmsCORS.AppToken);
				var sseCmsUser = (IFnsAcCmsUser)fnsResult.GetValue();
				UnitTestingHelper.CmsCORS.SseCmsUser = sseCmsUser;

			}
			return (SseCmsUser)UnitTestingHelper.CmsCORS.SseCmsUser;
		}
	}
}