using Nancy;
using Nancy.Authentication.Token;
using Nancy.Security;
using System.Collections.Generic;

namespace NXS.Lib.Web.Authentication
{
	//@HACK: to get the username and session num from a token
	public class SessionDataIdentityResolver : IUserIdentityResolver
	{
		public string UserName;
		public AuthInformation AuthInfo;
		public IUserIdentity GetUser(string userName, IEnumerable<string> claims, NancyContext context)
		{
			UserName = userName;
			AuthInfo = SystemUserIdentity.AuthInfoFromClaims(claims);
			return null;
		}
	}
}
