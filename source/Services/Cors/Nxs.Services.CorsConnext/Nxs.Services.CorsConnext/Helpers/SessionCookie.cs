using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using SOS.Lib.Util;
using SOS.Services.Interfaces.Models;

namespace Nxs.Services.CorsConnext.Helpers
{
	public static class SessionCookie
	{
		#region Properties

		private const string _SESSION_USER_NAME = "D'Vincie";
		private const string _SESSION_COOKIE_NAME = "WHITE_SOUCE";
		private const string _SESSION_USER_NAME2 = "GeorgeWashingtom";
		private const string _SESSION_COOKIE_REM_ME = "DRIED_SOUCE";
		private const int _SESSSION_COOKIE_VERSION = 1;
		private const int _SESSION_EXP_LENGTH_MIN = 30;

		#endregion Properties

		#region Methods

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
				SetSessionCookie(oCustomer, true, oContext, CORSSecurity.LocalDateTime.Value.AddDays(-1));
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

		public static bool ValidateSessionCookie(HttpContext oContext, bool bIsPersistent, Func<long, string, bool> fxValidate, string szApplicationToken, out SseCmsUser oSseCmsUser)
		{
			/** Initialize. */
			oSseCmsUser = GetSessionCookie(oContext);
			if (oSseCmsUser == null) return false;

			/** Check if the session has expired. */

			if (oSseCmsUser.SessionID == 0) return false;
			if (fxValidate != null)
			{
				if (!fxValidate(oSseCmsUser.SessionID, szApplicationToken))
				{
					DestroySessionCookie(oContext);
					return false;
				}
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
			var oIssueDate = CORSSecurity.LocalDateTime.Value;
			var oExpDate = CORSSecurity.LocalDateTime.Value.AddYears(1);

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