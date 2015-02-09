using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using SOS.Services.Interfaces.Models;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Helpers
{
	[Obsolete("Not implemented yet.", true)]
	public class CustAuthAttribute : AuthorizationFilterAttribute
	{
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			#region Authentication
			/** Authenticate. */
			SseCmsUser oUser;
			var oResult = new CmsCORSResult((int)CmsResultCodes.CookieInvalid
				, String.Format("Validating Authentication Failed for '{0}'", actionContext.ControllerContext.Controller), "Generic");
			// Check user
			if (!SessionCookie.ValidateSessionCookie(HttpContext.Current, true, SOS.Data.AuthenticationControl.AC_Application.MetaData.SSECmsCORSID, out oUser))
				actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
				{
					ReasonPhrase = "Cookie not present or is invalid"
				};

			#endregion Authentication

			base.OnAuthorization(actionContext);
		}

	}
}