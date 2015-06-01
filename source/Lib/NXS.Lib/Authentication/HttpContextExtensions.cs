using Nancy;
using Nancy.Authentication.Token;
using Nancy.Security;
using NXS.Lib;
using NXS.Lib.Caching;
using System.Linq;
using System.Collections.Generic;
using NXS.Lib.Authentication;

namespace System.Web
{
	public static class HttpContextExtensions
	{
		public static bool GetSessionData(this HttpContext context, ITokenizer tokenizer, out string token, out string username, out AuthInformation authInfo)
		{
			token = context.ExtractTokenFromHeader();
			if (token == null)
			{
				username = null;
				authInfo = null;
				return false;
			}

			var sessionNumResolver = new SessionDataIdentityResolver();
			tokenizer.Detokenize(token, context: null, userIdentityResolver: sessionNumResolver);
			username = sessionNumResolver.UserName;
			authInfo = sessionNumResolver.AuthInfo;
			return authInfo != null;
		}
		public static SystemUserIdentity GetIdentity(this HttpContext context, TokenAuthenticationConfiguration configuration)
		{
			var token = context.ExtractTokenFromHeader();
			if (token == null)
				return null;

			// we don't have a NancyContext so we pass null. the user identity resolver shouldn't need it anyways.
			return (SystemUserIdentity)configuration.Tokenizer.Detokenize(token,
				context: null, userIdentityResolver: configuration.UserIdentityResolver);
		}

		// copied from TokenAuthentication.cs
		private const string Scheme = "Token";
		public static string ExtractTokenFromHeader(this HttpContext context)
		{
			var authorization = context.Request.Headers["Authorization"];

			if (string.IsNullOrEmpty(authorization))
			{
				return null;
			}

			if (!authorization.StartsWith(Scheme))
			{
				return null;
			}

			try
			{
				var encodedToken = authorization.Substring(Scheme.Length).Trim();
				return String.IsNullOrWhiteSpace(encodedToken) ? null : encodedToken;
			}
			catch (FormatException)
			{
				return null;
			}
		}
	}
}
