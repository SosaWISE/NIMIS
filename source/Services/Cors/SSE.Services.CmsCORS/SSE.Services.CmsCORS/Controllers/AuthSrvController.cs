using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.Services.Interfaces.Models;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SOS.Lib.Core;
using NXS.Lib.Web;
using NXS.Lib.Web.Caching;
using Nancy.Authentication.Token;
using Nancy.Security;
using Nancy;

namespace SSE.Services.CmsCORS.Controllers
{
	[RoutePrefix("AuthSrv")]
	public class AuthSrvController : ApiController
	{
		TokenAuthenticationConfiguration _tokenConfig;
		AuthService _authService;
		public AuthSrvController()
		{
			_tokenConfig = SosServiceEngine.Instance.FunctionalServices.Instance<TokenAuthenticationConfiguration>();
			_authService = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
		}

#if DEBUG
		public class SessData
		{
			public string Token { get; set; }
			public string Username { get; set; }
			public byte[] SessionNum { get; set; }
			public string SessionKey { get; set; }
		}
		[HttpGet, Route("SessionData")]
		public Result<SessData> SessionData()
		{
			var result = new Result<SessData>();
			string token, username;
			byte[] sessionNum;
			HttpContext.Current.GetSessionData(_tokenConfig.Tokenizer, out token, out username, out sessionNum);
			result.Value = new SessData
			{
				Token = token,
				Username = username,
				SessionNum = sessionNum,
				SessionKey = _authService.SessionNumToKey(sessionNum),
			};
			return result;
		}
#endif

		[HttpPost, Route("SessionStart")]
		public Result<UserModel> SessionStart()
		{
			var identity = HttpContext.Current.GetIdentity(_tokenConfig);
			var result = new Result<UserModel>();
			if (identity != null)
			{
				result.Value = _authService.ToUserModel(identity);
			}
			else
			{
				//@NOTE: currently anonymous users don't have a session
			}
			return result;
		}

		public class Credentials
		{
			public string Username { get; set; }
			public string Password { get; set; }
		}
		public class AuthResult
		{
			public string Token { get; set; }
			public UserModel User { get; set; }
		}
		[HttpPost, Route("UserAuth")]
		public Result<AuthResult> UserAuth(Credentials credentials)
		{
			var result = new Result<AuthResult>();
			var authResult = _authService.Authenticate(credentials.Username, credentials.Password, IPAddressUtil.ClientIPAddress());
			if (authResult.Success)
			{
				var identity = authResult.Value;
				result.Value = new AuthResult() { User = _authService.ToUserModel(identity), };
				identity.UseSessionNumAsClaims = true;
				result.Value.Token = _tokenConfig.Tokenizer.Tokenize(identity, null);
			}
			else
			{
				result.Code = authResult.Code;
				result.Message = authResult.Message;
			}
			return result;
		}

		[HttpPost, Route("Logout")]
		public Result<bool> Logout()
		{
			var result = new Result<bool>();
			try
			{
				string token, username;
				byte[] sessionNum;
				HttpContext.Current.GetSessionData(_tokenConfig.Tokenizer, out token, out username, out sessionNum);
				_authService.EndSession(sessionNum);
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
