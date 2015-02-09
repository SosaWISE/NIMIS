using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NXS.Lib.Web;

namespace System.Web
{
	public static class AuthCookieExtensions
	{
		private const string SESS_COOKIE = "SID";
		private const string XSRF_COOKIE = "TKN";

		private static DateTime GetExpires()
		{
			return DateTime.UtcNow.AddMinutes(30);
		}

		public static UserSession GetUserSession(this HttpContext context)
		{
			var cookie = context.Request.Cookies[SESS_COOKIE];
			if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return null;

			return DecodeSessionCookie(cookie.Value);
		}
		public static string GetXsrfToken(this HttpContext context)
		{
			var cookie = context.Request.Cookies[XSRF_COOKIE];
			if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return null;

			return cookie.Value;
		}

		public static void SetXsfrTokenCookie(this HttpContext context, string xsrfToken)
		{
			var expires = GetExpires();
			context.SetCookie(XSRF_COOKIE, xsrfToken, false, expires);
		}
		public static void SetUserSessionCookie(this HttpContext context, UserSession userSession = null)
		{
			var expires = GetExpires();
			var cookieValue = (userSession != null) ? EncodeSessionCookie(userSession) : "";
			context.SetCookie(SESS_COOKIE, cookieValue, true, expires);
		}
		public static void DestroyAuthCookies(this HttpContext context)
		{
			var expires = DateTime.UtcNow.AddDays(-1);
			context.SetCookie(SESS_COOKIE, "", true, DateTime.UtcNow.AddDays(-1));
			context.SetCookie(XSRF_COOKIE, "", false, DateTime.UtcNow.AddDays(-1));
		}


		public static void SetCookie(this HttpContext context, string cookieName, string cookieValue, bool httpOnly, DateTime? expires)
		{
			bool secure = context.Request.IsSecureConnection;
			var cookie = new HttpCookie(cookieName, cookieValue)
			{
				//Domain = ,
				HttpOnly = httpOnly,
				Secure = secure,
			};
			if (expires.HasValue)
			{
				cookie.Expires = expires.Value;
			}
			context.Response.Cookies.Add(cookie);
		}

		private static string EncodeSessionCookie(UserSession userSession)
		{
			//return SecureCookie.Instance.Encode(SESS_COOKIE, userSession);

			var now = DateTime.UtcNow;
			var ticket = new System.Web.Security.FormsAuthenticationTicket(
				version: 1,
				name: userSession.Username ?? "[unknown user]",
				issueDate: now,
				expiration: now.AddYears(1), //???
				isPersistent: true, //???
				userData: Newtonsoft.Json.JsonConvert.SerializeObject(userSession)
			);
			return System.Web.Security.FormsAuthentication.Encrypt(ticket);
		}
		private static UserSession DecodeSessionCookie(string cookieValue)
		{
			//return SecureCookie.Instance.Decode<UserSession>(SESS_COOKIE, cookieValue);

			try
			{
				var ticket = System.Web.Security.FormsAuthentication.Decrypt(cookieValue);
				if (ticket != null && !ticket.Expired)
				{
					return Newtonsoft.Json.JsonConvert.DeserializeObject<UserSession>(ticket.UserData);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Error decoding session cookie: {0}", ex.Message);
			}
			return null;
		}
	}
}
