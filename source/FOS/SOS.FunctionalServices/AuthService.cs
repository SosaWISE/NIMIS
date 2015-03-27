using NXS.Lib.Web;
using NXS.Lib.Web.Caching;
using SOS.Lib.Core;
using SOS.Lib.Util.ActiveDirectory;
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

	public class AuthService
	{
		SessionStore _sessionStore;
		UserStore _userStore;

		Dictionary<string, HashSet<string>> _groupApps;
		Dictionary<string, HashSet<string>> _groupActions;
		public AuthService(Dictionary<string, HashSet<string>> groupApps, Dictionary<string, HashSet<string>> groupActions)//,
		//SessionStore sessionStore = null, UserStore userStore = null)
		{
			_groupApps = groupApps;
			_groupActions = groupActions;

			_sessionStore = /*sessionStore ??*/ SosServiceEngine.Instance.FunctionalServices.Instance<SessionStore>();
			_userStore = /*userStore ??*/ SosServiceEngine.Instance.FunctionalServices.Instance<UserStore>();
		}

		public Result<SystemUserIdentity> Authenticate(string username, string password, string ipAddress)
		{
			var result = new Result<SystemUserIdentity>();
			if (username != null)
			{
				//
				RemoveCachedUser(username);
				//
				var user = _userStore.Get(username);
				// compare passwords (first to password in database, then in active directory)
				if (BCrypt.Net.BCrypt.HashAndPasswordAreEqual(password, user.Password) ||
					ADHelper.IsValidLogin(username, password, ADUtility.Domain))
				{
					var sessionNum = _sessionStore.Create(username, ipAddress);
					result.Value = new SystemUserIdentity(sessionNum, user);
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
				userModel.Apps = GetApps(identity.Claims);
				userModel.Actions = GetActions(identity.Claims);
			}
			return userModel;
		}
		// get apps the user can open
		private List<string> GetApps(IEnumerable<string> groups)
		{
			return GetPermissions(_groupApps, groups).ToList();
		}
		// get actions the user can perform
		private List<string> GetActions(IEnumerable<string> groups)
		{
			return GetPermissions(_groupActions, groups).ToList();
		}
		private static HashSet<string> GetPermissions(Dictionary<string, HashSet<string>> dict, IEnumerable<string> groups)
		{
			var result = new HashSet<string>();
			if (groups != null)
			{
				foreach (var grp in groups)
				{
					HashSet<string> hset;
					if (!dict.TryGetValue(grp.ToLower(), out hset)) continue;

					foreach (var id in hset)
					{
						result.Add(id);
					}
				}
			}
			return result;
		}
		public bool HasPermission(IEnumerable<string> groups, string applicationID, string actionID)
		{
			//@REVIEW: this could be optimized...
			return ((applicationID == null || GetApps(groups).Contains(applicationID.ToLower())) && // applicationID must be null or in list
					(actionID == null || GetActions(groups).Contains(actionID.ToLower()))); // actionID must be null or in list
		}

		public void EndSession(byte[] sessionNum)
		{
			if (sessionNum == null) return;

			_sessionStore.Terminate(sessionNum);
		}

		public string SessionNumToKey(byte[] sessionNum)
		{
			return _sessionStore.SessionNumToKey(sessionNum);
		}
	}
}
