using NXS.Lib.Web;
using NXS.Lib.Web.Authentication;
using NXS.Lib.Web.Caching;
using SOS.Lib.Core;
using SOS.Lib.Util.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOS.FunctionalServices
{
	public class UserModel
	{
		public int UserID;
		public string Username;
		public string Firstname;
		public string Lastname;
		public string GPEmployeeID;
		public List<string> Apps;
		public List<string> Actions;

		public int DealerId;
	}

	public class StringInsensitiveDictionary<TValue> : Dictionary<string, TValue>
	{
		public StringInsensitiveDictionary() : base(StringComparer.OrdinalIgnoreCase) { }
		public StringInsensitiveDictionary(int capacity) : base(capacity, StringComparer.OrdinalIgnoreCase) { }
	}
	public class StringInsensitiveHashSet : HashSet<string>
	{
		public StringInsensitiveHashSet() : base(StringComparer.OrdinalIgnoreCase) { }
		public StringInsensitiveHashSet(IEnumerable<string> collection) : base(collection, StringComparer.OrdinalIgnoreCase) { }
	}

	public class AuthService
	{
		SessionStore _sessionStore;
		UserStore _userStore;

		object _loadLocker = new object();
		Func<StringInsensitiveDictionary<StringInsensitiveHashSet>> _getGroupApps;
		Func<StringInsensitiveDictionary<StringInsensitiveHashSet>> _getGroupActions;
		StringInsensitiveDictionary<StringInsensitiveHashSet> _groupApps;
		StringInsensitiveDictionary<StringInsensitiveHashSet> _groupActions;
		public AuthService(
			Func<StringInsensitiveDictionary<StringInsensitiveHashSet>> getGroupApps,
			Func<StringInsensitiveDictionary<StringInsensitiveHashSet>> getGroupActions,
			SessionStore sessionStore = null, UserStore userStore = null)
		{
			_getGroupApps = getGroupApps;
			_getGroupActions = getGroupActions;
			ReloadGroupActionItems();

			_sessionStore = sessionStore ?? SosServiceEngine.Instance.FunctionalServices.Instance<SessionStore>();
			_userStore = userStore ?? SosServiceEngine.Instance.FunctionalServices.Instance<UserStore>();
		}

		public void ReloadGroupActionItems()
		{
			lock (_loadLocker)
			{
				var groupApp = _getGroupApps();
				var groupActions = _getGroupActions();
				_groupApps = groupApp;
				_groupActions = groupActions;
			}
		}

		public Result<SystemUserIdentity> Authenticate(string username, string password, string ipAddress)
		{
			var result = new Result<SystemUserIdentity>();
			if (username != null)
			{
				//
				RemoveCachedUser(username);
				//
				var user = GetUser(username);
				// compare passwords (first to password in database, then in active directory)
				if (BCrypt.Net.BCrypt.HashAndPasswordAreEqual(password, user.Password) ||
					ADHelper.IsValidLogin(username, password, ADUtility.Domain))
				{
					var sessionNum = _sessionStore.Create(username, ipAddress);
					result.Value = new SystemUserIdentity(SystemUserIdentity.AuthTypes.Session, sessionNum, user, null, null);
					return result;
				}
			}

			result.Code = (int)SOS.FunctionalServices.Contracts.Helper.ErrorCodes.LoginFailure;
			result.Message = "Login failed";
			return result;
		}
		public void RemoveCachedUser(string username)
		{
			_userStore.RemoveCached(username);
		}
		public User GetUser(string username)
		{
			return _userStore.Get(username);
		}

		public UserModel ToUserModel(SystemUserIdentity identity, bool includeLists = true)
		{
			var userModel = new UserModel
			{
				UserID = identity.UserID,
				Username = identity.UserName,
				Firstname = identity.FirstName,
				Lastname = identity.LastName,
				GPEmployeeID = identity.GPEmployeeID,
				DealerId = identity.DealerId,
			};
			if (includeLists)
			{
				userModel.Apps = GetAppsHash(identity.Claims).ToList();
				userModel.Actions = GetActionsHash(identity.Claims).ToList();
			}
			return userModel;
		}

		public bool HasPermission(IEnumerable<string> applicationIDs, IEnumerable<string> actionIDs, IEnumerable<string> userGroups, IEnumerable<string> userApplications, IEnumerable<string> userActions)
		{
			// applicationIDs must be null or empty or in apps list
			// and actionIDs must be null or empty or in actions lists
			var hasApps = applicationIDs != null && applicationIDs.Count() != 0;
			var hasActions = actionIDs != null && actionIDs.Count() != 0;
			if (!hasApps && !hasActions)
				return true;

			StringInsensitiveHashSet hash;
			if (hasApps)
			{
				hash = GetAppsHash(userGroups);
				if (userApplications != null)
					hash.UnionWith(userApplications);
				if (!applicationIDs.Any(item => hash.Contains(item)))
					return false;
			}
			if (hasActions)
			{
				hash = GetActionsHash(userGroups);
				if (userActions != null)
					hash.UnionWith(userActions);
				if (!actionIDs.Any(item => hash.Contains(item)))
					return false;
			}
			return true;
		}
		// get apps the user can open
		private StringInsensitiveHashSet GetAppsHash(IEnumerable<string> groups)
		{
			return GetPermissionsHash(_groupApps, groups);
		}
		// get actions the user can perform
		private StringInsensitiveHashSet GetActionsHash(IEnumerable<string> groups)
		{
			return GetPermissionsHash(_groupActions, groups);
		}
		private static StringInsensitiveHashSet GetPermissionsHash(StringInsensitiveDictionary<StringInsensitiveHashSet> dict, IEnumerable<string> groups)
		{
			var result = new StringInsensitiveHashSet();
			if (groups == null)
				return result;

			foreach (var grp in groups)
			{
				StringInsensitiveHashSet hash;
				if (!dict.TryGetValue(grp, out hash))
					continue;
				result.UnionWith(hash);
			}
			return result;
		}

		public void EndSession(byte[] sessionNum)
		{
			if (sessionNum == null) return;

			_sessionStore.Terminate(sessionNum);
		}
	}
}
