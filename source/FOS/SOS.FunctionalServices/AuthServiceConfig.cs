﻿using NXS.Lib.Web;
using NXS.Lib.Web.Caching;
using SOS.Data.AuthenticationControl;
using SOS.FunctionalServices.Contracts;
using SOS.Lib.Util.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IO;

namespace SOS.FunctionalServices
{
	//class InMemorySessionDb
	//{
	//	Dictionary<int, AC_UserSession> _dict;
	//	int _count = 0;
	//	object _lockObj = new object();
	//	internal InMemorySessionDb()
	//	{
	//		_dict = new Dictionary<int, AC_UserSession>();
	//	}
	//	internal AC_UserSession Clone(AC_UserSession userSession)
	//	{
	//		if (userSession == null)
	//		{
	//			return null;
	//		}
	//		var result = Newtonsoft.Json.JsonConvert.DeserializeObject<AC_UserSession>(
	//			Newtonsoft.Json.JsonConvert.SerializeObject(userSession)
	//		);
	//		return result;
	//	}
	//	internal AC_UserSession BySessionKey(string sessionKey)
	//	{
	//		lock (_lockObj)
	//		{
	//			return Clone(_dict.Values.Where((a) => a.SessionKey == sessionKey).FirstOrDefault());
	//		}
	//	}
	//	internal AC_UserSession LoadByPrimaryKey(int id)
	//	{
	//		lock (_lockObj)
	//		{
	//			if (_dict.ContainsKey(id))
	//			{
	//				return Clone(_dict[id]);
	//			}
	//			return null;
	//		}
	//	}
	//	internal void SaveSession(AC_UserSession sess)
	//	{
	//		lock (_lockObj)
	//		{
	//			if (sess.ID == 0)
	//			{
	//				// add new
	//				sess.ID = ++_count;
	//				_dict.Add(sess.ID, Clone(sess));
	//			}
	//			else if (_dict.ContainsKey(sess.ID))
	//			{
	//				// update
	//				_dict[sess.ID] = Clone(sess);
	//			}
	//			else
	//			{
	//				throw new Exception("invalid id");
	//			}
	//		}
	//	}
	//}

	public static class AuthServiceConfig
	{
		public static void Configure(IFunctionalServiceFactory functionalServices)
		{
			{ // Session Store
				var sessionStore = CreateSessionStore();
				functionalServices.Register(() => sessionStore);
			}
			{ // User Store
				var mockADGroups = string.Compare(Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("MockADGroups"), "true", true) == 0;
				var userStore = CreateUserStore(mockADGroups);
				functionalServices.Register(() => userStore);
			}
			{ // Auth Service
				var groupApps = new Dictionary<string, HashSet<string>>();
				foreach (var item in SosAuthControlDataContext.Instance.AC_GroupApplications.LoadAll())
				{
					if (!item.IsActive || item.IsDeleted) continue;
					HashSet<string> hset;
					var key = item.GroupName.ToLower();
					if (!groupApps.TryGetValue(key, out hset))
					{
						hset = new HashSet<string>();
						groupApps.Add(key, hset);
					}
					hset.Add(item.ApplicationId.ToLower());
				}

				var groupActions = new Dictionary<string, HashSet<string>>();
				foreach (var item in SosAuthControlDataContext.Instance.AC_GroupActions.LoadAll())
				{
					if (!item.IsActive || item.IsDeleted) continue;
					HashSet<string> hset;
					var key = item.GroupName.ToLower();
					if (!groupActions.TryGetValue(key, out hset))
					{
						hset = new HashSet<string>();
						groupActions.Add(key, hset);
					}
					hset.Add(item.ActionId.ToLower());
				}

				var authService = new AuthService(
					groupApps: groupApps,
					groupActions: groupActions
				);
				functionalServices.Register(() => authService);
			}
		}

		static SessionStore CreateSessionStore()
		{
			//var sessionDb = new InMemorySessionDb();
			return new SessionStore(
				createSession: (s) =>
				{
					var sess = new AC_UserSession
					{
						SessionKey = s.SessionKey,
						Username = s.Username,
						IPAddress = s.IPAddress,
						LastAccessedOn = s.LastAccessedOn,
						Terminated = false,
						CreatedOn = DateTime.UtcNow,
					};
					sess.Save();
					//sessionDb.SaveSession(sess);
					s.ID = sess.ID;
					return s;
				},
				readSession: (sessionKey) =>
				{
					var sess = SosAuthControlDataContext.Instance.AC_UserSessions.BySessionKey(sessionKey);
					//var sess = sessionDb.BySessionKey(sessionKey);
					if (sess == null)
						throw new Exception("no session read. sessionKey: " + sessionKey);
					if (sess.Terminated)
						throw new Exception("session is terminated");
					return new Session
					{
						ID = sess.ID,
						SessionKey = sess.SessionKey,
						Username = sess.Username,
						IPAddress = sess.IPAddress,
						LastAccessedOn = sess.LastAccessedOn,
					};
				},
				updateSession: (s) =>
				{
					var sess = SosAuthControlDataContext.Instance.AC_UserSessions.LoadByPrimaryKey(s.ID);
					//var sess = sessionDb.LoadByPrimaryKey(s.ID);
					if (sess == null)
						throw new Exception("no session to update. id: " + s.ID);
					if (sess.Terminated)
						throw new Exception("session is terminated");
					if (sess.Username != null && sess.Username != s.Username)
						throw new Exception("username cannot change");
					sess.SessionKey = s.SessionKey;
					sess.Username = s.Username;
					sess.IPAddress = s.IPAddress;
					sess.LastAccessedOn = s.LastAccessedOn;
					sess.Save();
					//sessionDb.SaveSession(sess);
				},
				terminateSession: (sessionKey) =>
				{
					var sess = SosAuthControlDataContext.Instance.AC_UserSessions.BySessionKey(sessionKey);
					//var sess = sessionDb.BySessionKey(sessionKey);
					if (sess == null)
						throw new Exception("no session to terminate. sessionKey:" + sessionKey);
					if (!sess.Terminated)
					{
						sess.Terminated = true;
						sess.Save();
						//sessionDb.SaveSession(sess);
					}
				},
				slidingExpiration: TimeSpan.FromMinutes(20),
				memoryLimitMb: 10,
				physicalMemoryLimitPercent: 5,
				pollingInterval: TimeSpan.FromMinutes(2)
			);
		}

		static UserStore CreateUserStore(bool mockADGroups)
		{
			return new UserStore(
				readUser: (username) =>
				{
					//var user = SosAuthControlDataContext.Instance.AC_Users.ByUsername(username);
					var authUser = SosAuthControlDataContext.Instance.AC_UsersAppAuthenticationViews.ByUsername(username);
					if (authUser == null)
					{
						return default(User);
					}

					var password = authUser.Password;
					// primitive check to test if the password is a bcrypt hash
					if (password.Length < 60 || !password.StartsWith("$"))
					{
						// password is not a bcrypt hash, so hash it and then save it
						var user = SosAuthControlDataContext.Instance.AC_Users.ByUsername(username);
						password = BCrypt.Net.BCrypt.HashPassword(password);
						user.Password = password;
						user.Save();
					}

					var groups = GetGroupsForUser(username, mockADGroups);
					return new User()
					{
						UserID = authUser.UserID,
						Username = authUser.Username,
						Password = password,
						FirstName = authUser.FirstName,
						LastName = authUser.LastName,
						GPEmployeeID = authUser.GPEmployeeID,
						Groups = groups.ToArray(),

						DealerId = authUser.DealerId,
					};
				},
				hardExpiration: TimeSpan.FromMinutes(20),
				memoryLimitMb: 10,
				physicalMemoryLimitPercent: 5,
				pollingInterval: TimeSpan.FromMinutes(2)
			);
		}

		private static List<string> GetGroupsForUser(string username, bool mockADGroups)
		{
			if (!mockADGroups)
			{
				return ADHelper.GetGroupsForUser(username);
			}
			else
			{
				var filepath = Path.Combine(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, "mockadgroups.json");
				if (File.Exists(filepath))
				{
					var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(filepath));
					if (dict.ContainsKey(username))
					{
						return dict[username];
					}
				}
				// default
				return new List<string> { "Nexsense", };
			}
		}
	}
}
