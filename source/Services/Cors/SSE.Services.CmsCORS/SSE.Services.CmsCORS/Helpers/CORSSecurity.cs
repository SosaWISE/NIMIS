using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AuthenticationControl;
using SOS.Lib.Core;
using SOS.Lib.Util;
using SOS.Services.Interfaces.Models;
using SSE.Services.CmsCORS.Models;
using System;
using System.Web;
using NXS.Lib.Web;
using NXS.Lib.Web.Caching;
using Nancy.Authentication.Token;

namespace SSE.Services.CmsCORS.Helpers
{
	public static class CORSSecurity
	{
		/// <summary>
		/// Authorize request to perform action
		/// </summary>
		/// <typeparam name="T">Result type</typeparam>
		/// <param name="functionName">Name of calling function</param>
		/// <param name="applicationID">ID of application the logged in user needs rights to in order to perform action. Null call be passed.</param>
		/// <param name="actionID">ID of action the logged in user needs rights to in order to perform action. Null call be passed.</param>
		/// <param name="action">Action to perform</param>
		/// <returns>Returns the results from the action</returns>
		public static Result<T> Authorize<T>(string functionName, string applicationID, string actionID, Func<SseCmsUser, Result<T>> action)
		{
			try
			{
				// ** Initialize
				var user = GetUnitTestUser();
				if (user == null && !Authorize(applicationID, actionID, out user))
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

		/// <summary>
		/// This is the single point of authentication for the entire CORS service.
		/// </summary>
		/// <typeparam name="T">Generic Type</typeparam>
		/// <param name="functionName">string</param>
		/// <param name="action">Func</param>
		/// <returns>CmsResult</returns>
		public static CmsCORSResult<T> AuthenticationWrapper<T>(string functionName, Func<SseCmsUser, CmsCORSResult<T>> action) where T : new()
		{
			try
			{
				// ** Initialize
				var oUser = GetUnitTestUser();
				if (oUser == null && !Authorize(null, null, out oUser))
				{
					return new CmsCORSResult<T>((int)CmsResultCodes.NotAuthorized,
						string.Format("Validating Authentication Failed for '{0}'", functionName), typeof(T).ToString());
				}
				/** Perform action. */
				return action(oUser);

			}
			catch (Exception ex)
			{
				//@TODO: log exception

				// return error result
				return new CmsCORSResult<T>((int)CmsResultCodes.ExceptionThrown,
					string.Format("The following exception was thrown from '{0}' method: {1}", functionName, ex.Message));
			}
		}

		private static bool Authorize(string applicationID, string actionID, out SseCmsUser user)
		{
			var tokenConfig = SosServiceEngine.Instance.FunctionalServices.Instance<TokenAuthenticationConfiguration>();
			var identity = HttpContext.Current.GetIdentity(tokenConfig);
			var authService = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
			if (identity != null && authService.HasPermission(identity.Claims, applicationID, actionID))
			{
				user = new SseCmsUser
				{
					UserID = identity.UserID,
					Username = identity.UserName,
					Firstname = identity.FirstName,
					Lastname = identity.LastName,
					GPEmployeeID = identity.GPEmployeeID,
					DealerId = identity.DealerId,
				};
				return true;
			}

			user = null;
			return false;
		}

		private static SseCmsUser GetUnitTestUser()
		{
			if (!UnitTestingHelper.CmsCORS.IsActive)
			{
				return null;
			}

			if (UnitTestingHelper.CmsCORS.SseCmsUser == null)
			{
				UnitTestingHelper.CmsCORS.AppToken = SOS.Data.AuthenticationControl.AC_Application.MetaData.SSECmsCORSID;
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