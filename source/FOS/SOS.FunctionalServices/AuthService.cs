using NXS.Lib.Web;
using NXS.Lib.Web.Caching;
using NXS.Lib.Web.Security;
using SOS.Data.AuthenticationControl;
using SOS.FunctionalServices.Contracts;
using SOS.Lib.Core;
using SOS.Lib.Util.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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
		public AuthService(Dictionary<string, HashSet<string>> groupApps, Dictionary<string, HashSet<string>> groupActions,
			SessionStore sessionStore = null, UserStore userStore = null)
		{
			_groupApps = groupApps;
			_groupActions = groupActions;

			_sessionStore = sessionStore ?? SosServiceEngine.Instance.FunctionalServices.Instance<SessionStore>();
			_userStore = userStore ?? SosServiceEngine.Instance.FunctionalServices.Instance<UserStore>();
		}

		public Result<UserModel> Authenticate(UserSession userSession, string username, string password, string ipAddress)
		{
			var result = new Result<UserModel>();
			if (userSession != null && userSession.Username == null && username != null)
			{
				var user = _userStore.Get(username);
				// compare passwords event if a 
				if (BCrypt.Net.BCrypt.HashAndPasswordAreEqual(password, user.Password))
				{
					// set username on session
					userSession.Username = username;
					// access/upate session
					Session session;
					if (_sessionStore.TryAccess(userSession.SessionNum, userSession.Username, ipAddress, out session))
					{
						result.Value = ToUserModel(user);
						return result;
					}
				}
			}

			result.Code = (int)SOS.FunctionalServices.Contracts.Helper.ErrorCodes.LoginFailure;
			result.Message = "Login failed";
			return result;
		}

		public void RenewOrStartSession(ref UserSession userSession, string ipAddress)
		{
			byte[] sessionNum;
			if (userSession == null || !_sessionStore.TryRenew(userSession.SessionNum, userSession.Username, ipAddress, out sessionNum))
			{
				// userSession is null or failed to renew so create new
				userSession = new UserSession();
				sessionNum = _sessionStore.Create(ipAddress);
			}
			userSession.SessionNum = sessionNum;
		}
		public Result<UserModel> GetUser(string username, bool includeLists = true)
		{
			var result = new Result<UserModel>();
			if (username == null)
			{
				return result;
			}

			var user = _userStore.Get(username);
			if (user == default(User))
			{
				result.Code = -1;
				result.Message = "User not found";
			}
			result.Value = ToUserModel(user, includeLists);
			return result;
		}
		private UserModel ToUserModel(User user, bool includeLists = true)
		{
			var userModel = new UserModel
			{
				Username = user.Username,
				Firstname = user.FirstName,
				Lastname = user.LastName,
				GPEmployeeID = user.GPEmployeeID,

				DealerId = user.DealerId,
			};
			if (includeLists)
			{
				userModel.Apps = GetApps(user.Groups);
				userModel.Actions = GetActions(user.Groups);
			}
			return userModel;
		}
		// get apps the user can open
		private List<string> GetApps(string[] groups)
		{
			return GetPermissions(_groupApps, groups).ToList();
		}
		// get actions the user can perform
		private List<string> GetActions(string[] groups)
		{
			return GetPermissions(_groupActions, groups).ToList();
		}
		private static HashSet<string> GetPermissions(Dictionary<string, HashSet<string>> dict, string[] groups)
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
		public bool HasPermission(string[] groups, string applicationID, string actionID)
		{
			//@REVIEW: this could be optimized...
			return ((applicationID == null || GetApps(groups).Contains(applicationID.ToLower())) && // applicationID must be null or in list
					(actionID == null || GetActions(groups).Contains(actionID.ToLower()))); // actionID must be null or in list
		}

		public UserModel Authorize(UserSession userSession, string ipAddress, string applicationID, string actionID)
		{
			Session session;
			if (userSession != null && _sessionStore.TryAccess(userSession.SessionNum, userSession.Username, ipAddress, out session))
			{
				var user = _userStore.Get(userSession.Username);
				if (HasPermission(user.Groups, applicationID, actionID))
				{
					return ToUserModel(user, false);
				}
			}
			return null;
		}

		public void EndSession(byte[] sessionNum)
		{
			_sessionStore.Terminate(sessionNum);
		}

		public string NewXsrfToken()
		{
			return Convert.ToBase64String(AESHMAC.NewKey());
		}

		public string SessionNumToKey(byte[] sessionNum)
		{
			return _sessionStore.SessionNumToKey(sessionNum);
		}
	}
}
