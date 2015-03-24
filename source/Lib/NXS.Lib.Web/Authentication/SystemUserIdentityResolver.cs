using Nancy;
using Nancy.Authentication.Token;
using Nancy.Security;
using NXS.Lib.Web.Caching;
using System.Collections.Generic;
using System.Linq;

namespace NXS.Lib.Web.Authentication
{
	public class SystemUserIdentityResolver : IUserIdentityResolver
	{
		SessionStore _sessionStore;
		UserStore _userStore;
		public SystemUserIdentityResolver(SessionStore sessionStore, UserStore userStore)
		{
			_sessionStore = sessionStore;
			_userStore = userStore;
		}

		public IUserIdentity GetUser(string userName, IEnumerable<string> claims, NancyContext context)
		{
			// get session id (use claims to store id)
			var sessionNum = SystemUserIdentity.SessionNumFromClaims(claims);
			if (sessionNum == null) return null;

			// validate session
			Session sess;
			if (!_sessionStore.Access(sessionNum, out sess)) return null;

			// get the user
			var user = _userStore.Get(sess.Username);
			// ensure user names match
			if (user.Username != userName) return null;

			// create identity
			return new SystemUserIdentity(sessionNum, user);
		}
	}
}
