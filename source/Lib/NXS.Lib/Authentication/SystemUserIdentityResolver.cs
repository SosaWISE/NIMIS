using Nancy;
using Nancy.Authentication.Token;
using Nancy.Security;
using NXS.Lib.Caching;
using System.Collections.Generic;

namespace NXS.Lib.Authentication
{
	public class SystemUserIdentityResolver : IUserIdentityResolver
	{
		SessionStore _sessionStore;
		UserStore _userStore;
		ActionRequestStore _arStore;
		public SystemUserIdentityResolver(SessionStore sessionStore, UserStore userStore, ActionRequestStore arStore)
		{
			_sessionStore = sessionStore;
			_userStore = userStore;
			_arStore = arStore;
		}

		public IUserIdentity GetUser(string usernameInToken, IEnumerable<string> claims, NancyContext context)
		{
			// get session id (use claims to store id)
			var authInfo = SystemUserIdentity.AuthInfoFromClaims(claims);
			if (authInfo == null)
				return null;

			string username;
			IEnumerable<string> apps;
			IEnumerable<string> actions;
			switch (authInfo.AuthType)
			{
				case SystemUserIdentity.AuthTypes.SESSION:
					// validate session
					Session sess;
					if (!_sessionStore.Access(authInfo.AuthNum, out sess))
						return null;
					username = sess.Username;
					apps = null;
					actions = null;
					break;

				case SystemUserIdentity.AuthTypes.ACTION_REQUEST:
					// get gpEmployeeId for username in token
					var tmpUser = _userStore.Get(usernameInToken);
					if (tmpUser.GPEmployeeID == null)
						return null;
					// validate action request
					ActionRequest ar;
					if (!_arStore.Access(authInfo.AuthNum, tmpUser.GPEmployeeID, out ar))
						return null;
					username = ar.Username;
					apps = (ar.ApplicationId == null) ? null : new[] { ar.ApplicationId };
					actions = (ar.ActionId == null) ? null : new[] { ar.ActionId };
					break;

				default:
					return null;
			}

			// get the user
			var user = _userStore.Get(username);
			// ensure user names match
			if (user.Username != usernameInToken)
				return null;

			// create identity
			return new SystemUserIdentity(authInfo.AuthType, authInfo.AuthNum, user, apps, actions);
		}
	}
}
