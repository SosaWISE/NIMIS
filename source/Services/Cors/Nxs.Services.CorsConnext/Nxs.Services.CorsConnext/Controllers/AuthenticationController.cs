using System;
using System.Collections.Generic;
using System.Web.Http;
using Nxs.Services.CorsConnext.Helpers;
using Nxs.Services.CorsConnext.Models;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AuthenticationControl;
using SOS.Services.Interfaces.Models;
using SOS.Lib.Core;
using System.Web;
using NXS.Lib.Web;

namespace Nxs.Services.CorsConnext.Controllers
{
    public class AuthenticationController : ApiController
    {
		[HttpPost]//, Route("SessionStart")]
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
				service.RenewOrStartSession(ref userSession, context.ClientIPAddress());
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
		[HttpPost]//, Route("UserAuth")]
		public Result<UserModel> UserAuth(Credentials credentials)
		{
			var context = HttpContext.Current;
			if (context.User.Identity.IsAuthenticated)
			{
				return new Result<UserModel>(code: -1, message: "login not allowed when using windows authentication");
			}

			var userSession = context.GetUserSession();
			var service = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
			var result = service.Authenticate(userSession, credentials.Username, credentials.Password, context.ClientIPAddress());
			if (result.Success)
			{
				// set session cookie
				context.SetUserSessionCookie(userSession);
			}
			return result;
		}
    }
}
