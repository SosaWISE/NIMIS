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
using System.Collections.Generic;
using Nancy;
using Api.Core;

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
			var appIDs = applicationID == null ? null : new string[] { applicationID };
			var actionIDs = actionID == null ? null : new string[] { actionID };
			return AuthorizeAny(functionName, appIDs, actionIDs, action);
		}
		/// <summary>
		/// Authorize request to perform action
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="functionName">Name of calling function</param>
		/// <param name="applicationIDs">IDs of applications. The logged in user needs rights to atleast ONE in order to perform the action.</param>
		/// <param name="actionIDs">IDs of actions. The logged in user needs rights to atleast ONE in order to perform the action.</param>
		/// <param name="action">Action to perform</param>
		/// <returns>Returns the results from the action</returns>
		public static Result<T> AuthorizeAny<T>(string functionName, IEnumerable<string> applicationIDs, IEnumerable<string> actionIDs, Func<SseCmsUser, Result<T>> action)
		{
			try
			{
				// ** Initialize
				var user = GetUnitTestUser();
				WebModules.AuthNeeds authNeeds;
				if (user == null && !Authorize(applicationIDs, actionIDs, out user, out authNeeds))
				{
					return new ResultAny<T>((int)HttpStatusCode.Unauthorized, string.Format("Authorization Failed for '{0}'", functionName), authNeeds);
				}
				/** Perform action. */
				return action(user);
			}
			catch (ResultException rex)
			{
				return new Result<T>(rex.Code, rex.Message);
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
				var user = GetUnitTestUser();
				WebModules.AuthNeeds authNeeds;
				if (user == null && !Authorize(null, null, out user, out authNeeds))
				{
					return new CmsCORSResult<T>((int)HttpStatusCode.Unauthorized,
						string.Format("Validating Authentication Failed for '{0}'", functionName), typeof(T).ToString());
				}
				/** Perform action. */
				return action(user);
			}
			catch (ResultException rex)
			{
				return new CmsCORSResult<T>(rex.Code, rex.Message);
			}
			catch (Exception ex)
			{
				//@TODO: log exception

				// return error result
				return new CmsCORSResult<T>((int)CmsResultCodes.ExceptionThrown,
					string.Format("The following exception was thrown from '{0}' method: {1}", functionName, ex.Message));
			}
		}

		private static bool Authorize(IEnumerable<string> applicationIDs, IEnumerable<string> actionIDs, out SseCmsUser user, out WebModules.AuthNeeds authNeeds)
		{
			var tokenConfig = SosServiceEngine.Instance.FunctionalServices.Instance<TokenAuthenticationConfiguration>();
			var identity = HttpContext.Current.GetIdentity(tokenConfig);
			var authService = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
			if (identity == null)
			{
				user = null;
				authNeeds = new WebModules.AuthNeeds(null, null);
				return false;
			}
			if (authService.HasPermission(applicationIDs, actionIDs, identity.Claims, identity.Applications, identity.Actions))
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
				authNeeds = null;
				return true;
			}

			user = null;
			authNeeds = new WebModules.AuthNeeds(applicationIDs, actionIDs);
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