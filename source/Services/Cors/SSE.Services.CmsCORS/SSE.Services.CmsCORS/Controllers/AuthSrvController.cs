using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AuthenticationControl;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.Services.Interfaces.Models;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SOS.Lib.Core;
using NXS.Lib.Web;

namespace SSE.Services.CmsCORS.Controllers
{
	[RoutePrefix("AuthSrv")]
	public class AuthSrvController : ApiController
	{
		#region SECURITY HOLE
		//public class DecodedValue
		//{
		//	public string Username { get; set; }
		//	public byte[] SessionNum { get; set; }
		//	public string SessionKey { get; set; }
		//}
		//public class SidDecode
		//{
		//	public string SID { get; set; }
		//	public DecodedValue Decoded { get; set; }
		//}
		//[HttpGet, Route("DecodeSID")]
		//public SidDecode DecodeSID()
		//{
		//	var context = HttpContext.Current;
		//	var userSession = context.GetUserSession();
		//	if (userSession == null)
		//		return null;
		//
		//	var service = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
		//	return new SidDecode
		//	{
		//		SID = context.Request.Cookies["SID"].Value,
		//		Decoded = new DecodedValue
		//		{
		//			Username = userSession.Username,
		//			SessionNum = userSession.SessionNum,
		//			SessionKey = service.SessionNumToKey(userSession.SessionNum),
		//		},
		//	};
		//}
		#endregion //SECURITY HOLE

		[HttpPost, Route("SessionStart")]
		public Result<UserModel> SessionStart()
		{
			var service = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
			UserSession userSession;
			string username;

			var context = HttpContext.Current;
			var identity = context.User.Identity;
			if (identity.IsAuthenticated)
			{
				// session not needed for windows auth
				userSession = null;
				username = identity.GetUsername();
			}
			else
			{
				userSession = context.GetUserSession();
				service.RenewOrStartSession(ref userSession, IPAddressUtil.ClientIPAddress());
				username = userSession.Username;
			}

			var result = service.GetUser(username);
			if (result.Success)
			{
				if (identity.IsAuthenticated)
				{
					result.Message = "using windows authentication";
				}
				else
				{
					// set cookies
					context.SetXsfrTokenCookie(service.NewXsrfToken());
					context.SetUserSessionCookie(userSession);
				}
			}
			return result;
		}

		public class Credentials
		{
			public string Username { get; set; }
			public string Password { get; set; }
		}
		[HttpPost, Route("UserAuth")]
		public Result<UserModel> UserAuth(Credentials credentials)
		{
			var context = HttpContext.Current;
			if (context.User.Identity.IsAuthenticated)
			{
				return new Result<UserModel>(code: -1, message: "login not allowed when using windows authentication");
			}

			var userSession = context.GetUserSession();
			var service = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
			var result = service.Authenticate(userSession, credentials.Username, credentials.Password, IPAddressUtil.ClientIPAddress());
			if (result.Success)
			{
				// set session cookie
				context.SetUserSessionCookie(userSession);
			}
			return result;
		}

		[HttpPost, Route("Logout")]
		public Result<bool> Logout()
		{
			var result = new Result<bool>();
			try
			{
				var context = HttpContext.Current;
				var userSession = context.GetUserSession();
				if (userSession != null)
				{
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
					service.EndSession(userSession.SessionNum);
				}
				// remove cookies
				context.DestroyAuthCookies();
				result.Value = true;
			}
			catch (Exception ex)
			{
				result.Code = (int)CmsResultCodes.GeneralError;
				result.Message = string.Format("Thrown exception:\r\n{0}", ex.Message);
			}
			return result;
		}

		#region Sales Rep and Technician
		public class SalesRepInfo
		{
			public string CompanyID { get; set; }
			public long SessionID { get; set; }
		}

		public CmsCORSResult<SseRuSalesRep> SalesRepRead(SalesRepInfo salesRepInfo)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "SalesRepRead";

			#endregion Initialize
			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, oUser =>
				{

					#region Parameter Validation

					var aCORSArg = new List<CORSArg>
				    {
					    new CORSArg(salesRepInfo.CompanyID, (string.IsNullOrEmpty(salesRepInfo.CompanyID)),
						    "<li>'CompanyID' can not be blank.</li>"),
				    };
					CmsCORSResult<SseRuSalesRep> oResult;
					if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;

					#endregion Parameter Validation

					#region Execute
					#region TRY
					/** Execute authentication. */
					try
					{
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
						IFnsResult<IFnsRuUser> oFnsModel = oService.SalesRepRead(salesRepInfo.CompanyID, oUser.Username);

						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							SessionCookie.DestroySessionCookie(HttpContext.Current);
							oResult.Code = oFnsModel.Code;
							oResult.Message = oFnsModel.Message;
							return oResult;
						}

						/** Setup return corsResult. */
						var fnsRuUser = (IFnsRuUser)oFnsModel.GetValue();

						/** Save session cookie. */
						var oSosSalesRep = new SseRuSalesRep
						{
							GPEmployeeID = fnsRuUser.GPEmployeeID,
							//SeasonId = fnsRuUser.SeasonId,
							//SeasonName = fnsRuUser.SeasonName,
							//TeamLocationId = fnsRuUser.TeamLocationId,
							//TeamLocation = fnsRuUser.TeamLocation,
							FirstName = fnsRuUser.FirstName,
							LastName = fnsRuUser.LastName,
							BirthDate = fnsRuUser.BirthDate,
							UserName = fnsRuUser.UserName,
							Sex = fnsRuUser.Sex,
							PhoneCell = fnsRuUser.PhoneCell,
							PhoneHome = fnsRuUser.PhoneHome,
							Email = fnsRuUser.Email,
						};
						SessionCookie.SetSessionCookie(oUser, true, HttpContext.Current);

						oResult.Code = (int)ErrorCodes.Success;
						oResult.SessionId = salesRepInfo.SessionID;
						oResult.Message = "Success";
						oResult.Value = oSosSalesRep;

					}
					#endregion TRY
					#region CATCH
					catch (Exception oEx)
					{
						oResult.Code = (int)CmsResultCodes.GeneralError;
						oResult.Message = string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message);
					}
					#endregion CATCH
					#endregion Execute

					#region Return results

					return oResult;

					#endregion Return results

				});

		}
		#endregion Sales Rep and Technician

	}
}
