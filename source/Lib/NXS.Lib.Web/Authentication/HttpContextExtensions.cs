using Nancy;
using Nancy.Authentication.Token;
using Nancy.Security;
using NXS.Lib.Web;
using NXS.Lib.Web.Caching;
using System.Linq;
using System.Collections.Generic;

namespace System.Web
{
	public static class HttpContextExtensions
	{
		//@HACK: to get the username and session num from a token
		private class LiteUserIdentityResolver : IUserIdentityResolver
		{
			public string UserName;
			public byte[] SessionNum;
			public IUserIdentity GetUser(string userName, IEnumerable<string> claims, NancyContext context)
			{
				UserName = userName;
				SessionNum = SystemUserIdentity.SessionNumFromClaims(claims);
				return null;
			}
		}
		public static void GetSessionData(this HttpContext context, ITokenizer tokenizer, out string token, out string username, out byte[] sessionNum)
		{
			token = context.ExtractTokenFromHeader();
			if (token == null)
			{
				username = null;
				sessionNum = null;
				return;
			}

			var sessionNumResolver = new LiteUserIdentityResolver();
			tokenizer.Detokenize(token, context: null, userIdentityResolver: sessionNumResolver);
			username = sessionNumResolver.UserName;
			sessionNum = sessionNumResolver.SessionNum;
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
