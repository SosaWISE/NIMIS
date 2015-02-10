using System;
using System.Web;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.Services.Interfaces.Models;
using SSE.Services.ParoleeCORS.Models;


namespace SSE.Services.ParoleeCORS.Helpers
{
	public static class CORSSecurity
	{
		#region Private Member Properties

		private const string _SOS_GPS_CLNT = "SSE_MAIN_PORTAL";

		#endregion Private Member Properties

		#region Private Member functions

		/// <summary>
		/// Given a Session ID it validates through the database that the session is still good.
		/// </summary>
		/// <param name="lSessionID">long</param>
		/// <param name="szApplicationToken">string</param>
		/// <returns>bool</returns>
		public static bool ValidateSession(long lSessionID, string szApplicationToken)
		{
			/** Initialize. */
			bool bResult = false;
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();

			/** Get session information and check timestamp if expired. */
			var oSessionResult = oService.SessionValidate(lSessionID, szApplicationToken, 30);

			/** Check expiration. */
			if (oSessionResult.Code == (int)SosCORSResult.MessageCodes.Success)
			{
				var oSession = (IFnsAcSessionModel)oSessionResult.GetValue();
				bResult = oSession.LastAccessedOn.AddMinutes(30) > DateTime.Now;

			}

			/** Return result. */
			return bResult;
		}

		#endregion Private Member functions

		#region Public Methods

		/// <summary>
		/// This is the single point of authentication for the entire CORS service.
		/// </summary>
		/// <typeparam name="T">Generic Type</typeparam>
		/// <param name="functionName">string</param>
		/// <param name="action">Func</param>
		/// <returns>SosCORSResult</returns>
		public static ParoleeCORSResult<T> AuthenticationWrapper<T>(string functionName, Func<SosOfficer, ParoleeCORSResult<T>> action)
		{
			#region Authentication
			/** Authenticate. */
			SosOfficer oUser;
			var oResult = new ParoleeCORSResult<T>((int)SosResultCodes.CookieInvalid
				, String.Format("Validating Authentication Failed for '{0}'", functionName), typeof(T).ToString());
			// Check user
			if (!SessionCookie.ValidateSessionCookie(HttpContext.Current, true, ValidateSession, _SOS_GPS_CLNT, out oUser))
				return oResult;
			#endregion Authentication

			/** Perform action. */
			return action(oUser);
		}
		#endregion Public Methods
	}
}