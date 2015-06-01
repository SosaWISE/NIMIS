using Nancy;
using System;

namespace NXS.Lib.Authentication
{
	public static class NancyExtensions
	{
		// copied from TokenAuthentication.cs
		private const string Scheme = "Token";
		public static string ExtractTokenFromHeader(this Request request)
		{
			var authorization = request.Headers.Authorization;

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
