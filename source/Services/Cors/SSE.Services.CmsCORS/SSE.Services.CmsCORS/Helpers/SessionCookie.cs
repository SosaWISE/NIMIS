using SOS.Lib.Util;
using SOS.Services.Interfaces.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SSE.Services.CmsCORS.Models;
using SOS.FunctionalServices.Contracts.Models;

namespace SSE.Services.CmsCORS.Helpers
{
	public static class SessionCookie
	{
		#region Properties

		private const string _SESSION_USER_NAME = "Leaonardo";
		private const string _SESSION_COOKIE_NAME = "TOMATOE_SOUCE";
		private const string _SESSION_USER_NAME2 = "ThomasJefferson";
		private const string _SESSION_COOKIE_REM_ME = "ALFREDO_SOUCE";
		private const int _SESSSION_COOKIE_VERSION = 1;
		private const int _SESSION_EXP_LENGTH_MIN = 30;

		private static DateTime? _now;
		private static readonly object _syncNow = new object();
		public static DateTime? LocalDateTime
		{
			get
			{
				if (_now == null || _now != DateTime.Now)
				{
					lock (_syncNow)
					{
						if (_now == null || _now != DateTime.Now)
						{
							var timeService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();

							_now = timeService.GetLocalDateTime();
						}
					}
				}

				/** Return result. */
				return _now;
			}
		}

		#endregion Properties

		#region Methods

		public static bool ValidateSession(long lSessionID, string szApplicationToken)
		{
			/** Initialize. */
			bool bResult = false;
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();

			/** Get session information and check timestamp if expired. */
			var oSessionResult = oService.SessionValidate(lSessionID, szApplicationToken, 30);

			/** Check expiration. */
			if (oSessionResult.Code == (int)CmsCORSResult.MessageCodes.Success)
			{
				var oSession = (IFnsAcSessionModel)oSessionResult.GetValue();
				bResult = oSession.LastAccessedOn.AddMinutes(30) > LocalDateTime;

			}

			/** Return result. */
			return bResult;
		}

		/// <summary>
		/// This method sets the Server Side Cookie on the header of the response.
		/// </summary>
		/// <param name="oSseCmsUser">SseCmsUser</param>
		/// <param name="bIsPersistent">bool</param>
		/// <param name="oContext">HttpContext</param>
		/// <param name="oIssueDate">DateTime?</param>
		/// <param name="oExpDate">DateTime?</param>
		public static void SetSessionCookie(SseCmsUser oSseCmsUser, bool bIsPersistent, HttpContext oContext, DateTime? oIssueDate = null, DateTime? oExpDate = null)
		{
			/** Initialize. */
			if (oIssueDate == null)
			{
				oIssueDate = DateTime.UtcNow;
			}
			if (oExpDate == null)
			{
				oExpDate = oIssueDate.Value.AddMinutes(_SESSION_EXP_LENGTH_MIN);
			}

			// password is correct, add cookie
			var oTicket = new FormsAuthenticationTicket(_SESSSION_COOKIE_VERSION
				, _SESSION_USER_NAME
				, oIssueDate.Value
				, oExpDate.Value
				, bIsPersistent
				, JsonConvert.SerializeObject(oSseCmsUser)); // ticket expires in 30 minutes.
			string szEncryptedTicket = FormsAuthentication.Encrypt(oTicket) ?? "no_auth";
			var cookie = new HttpCookie(_SESSION_COOKIE_NAME, szEncryptedTicket)
			{
				HttpOnly = true,
				Expires = oTicket.IsPersistent // If false then only accessed through server side.
							? oTicket.Expiration
							: DateTime.MinValue
			};

			/** Place cookie in header. */
			oContext.Response.Cookies.Add(cookie);
		}

		public static void DestroySessionCookie(HttpContext oContext)
		{
			/** Initialize. */
			SseCmsUser oCustomer = GetSessionCookie(oContext);

			/** Check that the cookie was present. */
			if (oCustomer != null)
				SetSessionCookie(oCustomer, true, oContext, LocalDateTime.Value.AddDays(-1));
		}

		public static SseCmsUser GetSessionCookie(HttpContext oContext)
		{
			/** Initialize. */
			SseCmsUser oResult = null;

			// ** Unit Test check.
			if (UnitTestingHelper.CmsCORS.IsActive)
				return (SseCmsUser)UnitTestingHelper.CmsCORS.SseCmsUser;

			HttpCookie oCookie = oContext.Request.Cookies[_SESSION_COOKIE_NAME];
			if (oCookie != null)
			{
				try
				{
					FormsAuthenticationTicket oTicket = FormsAuthentication.Decrypt(oCookie.Value);
					if (oTicket != null && !oTicket.Expired)
					{
						oResult = JsonConvert.DeserializeObject<SseCmsUser>(oTicket.UserData);
					}
				}
				catch (Exception oEx)
				{
					System.Diagnostics.Debug.WriteLine("The following error occurred: {0}", oEx.Message);
				}
			}

			/** Return result. */
			return oResult;
		}

		public static bool ValidateSessionCookie(HttpContext oContext, bool bIsPersistent, string szApplicationToken, out SseCmsUser oSseCmsUser)
		{
			/** Initialize. */
			oSseCmsUser = GetSessionCookie(oContext);
			if (oSseCmsUser == null) return false;

			/** Check if the session has expired. */

			if (oSseCmsUser.SessionID == 0) return false;
			if (!ValidateSession(oSseCmsUser.SessionID, szApplicationToken))
			{
				DestroySessionCookie(oContext);
				return false;
			}

			/** Reset Session. */
			SetSessionCookie(oSseCmsUser, bIsPersistent, oContext);

			/** Return success. */
			return true;
		}

		#region Remember Me Cookie

		/// <summary>
		/// This method sets the remeber me cookie
		/// </summary>
		/// <param name="oSseCmsUser">SseCmsUser</param>
		/// <param name="oContext">HttpContext</param>
		public static void SetRememberMeCookie(SseCmsUser oSseCmsUser, HttpContext oContext)
		{
			/** Initialize. */
			var oIssueDate = LocalDateTime.Value;
			var oExpDate = LocalDateTime.Value.AddYears(1);

			// password is correct, add cookie
			var oTicket = new FormsAuthenticationTicket(_SESSSION_COOKIE_VERSION
				, _SESSION_USER_NAME2
				, oIssueDate
				, oExpDate
				, true
				, JsonConvert.SerializeObject(oSseCmsUser)); // ticket expires in 30 minutes.
			string szEncryptedTicket = FormsAuthentication.Encrypt(oTicket) ?? "no_auth";
			var cookie = new HttpCookie(_SESSION_COOKIE_REM_ME, szEncryptedTicket)
			{
				HttpOnly = false,
				Expires = oTicket.IsPersistent // If false then only accessed through server side.
							? oTicket.Expiration
							: DateTime.MinValue
			};

			/** Place cookie in header. */
			oContext.Response.Cookies.Add(cookie);
		}

		#endregion Remember Me Cookie

		#endregion Methods
	}
}