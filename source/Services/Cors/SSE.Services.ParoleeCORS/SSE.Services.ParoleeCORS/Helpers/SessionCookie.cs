using System;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Xml.Serialization;
using SOS.Services.Interfaces.Models;
using SSE.Services.ParoleeCORS.Models;

namespace SSE.Services.ParoleeCORS.Helpers
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

		#endregion Properties

		#region Methods

		/// <summary>
		/// This method sets the Server Side Cookie on the header of the response.
		/// </summary>
		/// <param name="oSosOfficer">SosCustomer</param>
		/// <param name="bIsPersistent">bool</param>
		/// <param name="oContext">HttpContext</param>
		/// <param name="oIssueDate">DateTime?</param>
		/// <param name="oExpDate">DateTime?</param>
		public static void SetSessionCookie(SosOfficer oSosOfficer, bool bIsPersistent, HttpContext oContext, DateTime? oIssueDate = null, DateTime? oExpDate = null)
		{
			/** Initialize. */
			if (oIssueDate == null)
			{
				oIssueDate = DateTime.Now;
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
				, SerializeToken(oSosOfficer)); // ticket expires in 30 minutes.
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
			SosOfficer oOfficer = GetSessionCookie(oContext);

			/** Check that the cookie was present. */
			if (oOfficer != null)
				SetSessionCookie(oOfficer, true, oContext, DateTime.Now.AddDays(-1));
		}

		public static SosOfficer GetSessionCookie(HttpContext oContext)
		{
			/** Initialize. */
			SosOfficer oResult = null;

			HttpCookie oCookie = oContext.Request.Cookies[_SESSION_COOKIE_NAME];
			if (oCookie != null)
			{
				try
				{
					FormsAuthenticationTicket oTicket = FormsAuthentication.Decrypt(oCookie.Value);
					if (!oTicket.Expired)
					{
						string oXml = oTicket.UserData;
						var oSerializer = new XmlSerializer(typeof(SosCustomer));

						using (var oSReader = new StringReader(oXml))
						{
							oResult = (SosOfficer)oSerializer.Deserialize(oSReader);
						}
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

		public static bool ValidateSessionCookie(HttpContext oContext, bool bIsPersistent, Func<long, string, bool> fxValidate, string szApplicationToken, out SosOfficer oSosOfficer)
		{
			/** Initialize. */
			oSosOfficer = GetSessionCookie(oContext);
			if (oSosOfficer == null) return false;

			/** Check if the session has expired. */

			if (oSosOfficer.SessionID == 0) return false;
			if (fxValidate != null)
			{
				if (!fxValidate(oSosOfficer.SessionID, szApplicationToken))
				{
					DestroySessionCookie(oContext);
					return false;
				}
			}

			/** Reset Session. */
			SetSessionCookie(oSosOfficer, bIsPersistent, oContext);

			/** Return success. */
			return true;
		}

		#region Remember Me Cookie

		/// <summary>
		/// This method sets the remeber me cookie
		/// </summary>
		/// <param name="oSosOfficer">SosCustomer</param>
		/// <param name="oContext">HttpContext</param>
		public static void SetRememberMeCookie(SosOfficer oSosOfficer, HttpContext oContext)
		{
			/** Initialize. */
			var oIssueDate = DateTime.Now;
			var oExpDate = DateTime.Now.AddYears(1);

			// password is correct, add cookie
			var oTicket = new FormsAuthenticationTicket(_SESSSION_COOKIE_VERSION
				, _SESSION_USER_NAME2
				, oIssueDate
				, oExpDate
				, true
				, SerializeToken(oSosOfficer)); // ticket expires in 30 minutes.
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

		private static string SerializeToken(SosOfficer oSosOfficer)
		{
			/** Initialization. */
			var oSerializer = new XmlSerializer(oSosOfficer.GetType());
			string szXml;

			/** Execute */
			using (var sWriter = new StringWriter())
			{
				oSerializer.Serialize(sWriter, oSosOfficer);
				szXml = sWriter.ToString();
			}

			/** Return result. */
			return szXml;
		}

		#endregion Methods
	}
}