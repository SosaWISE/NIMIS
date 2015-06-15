using Nancy.Authentication.Token;
using NXS.Lib;
using NXS.Lib.Authentication;
using Tokenizer = NXS.Lib.Authentication.Tokenizer;
using NXS.Lib.Caching;
//using SOS.Data.AuthenticationControl;
//using SOS.FunctionalServices.Contracts;
using SOS.Lib.Util.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IO;
using SOS.Lib.Core;
using NXS.DataServices.AuthenticationControl;
using NXS.Data.AuthenticationControl;

namespace Api.Core
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
		public static TokenAuthenticationConfiguration Configure(Func<Nancy.NancyContext, string> integratedUserAgent, out AuthService authService)
		{
			var maxAge = TimeSpan.FromHours(24); // this should match TokenExpiration
			// Session Store
			var sessionStore = CreateSessionStore(maxAge);
			//functionalServices.Register(() => sessionStore);

			// User Store
			var mockADGroups =  string.Equals(WebConfig.Instance.GetConfig("MockADGroups"), "true", StringComparison.OrdinalIgnoreCase);
			var userStore = CreateUserStore(mockADGroups);
			//functionalServices.Register(() => userStore);

			// ActionRequest Store
			var arStore = CreateActionRequestStore(maxAge);
			//functionalServices.Register(() => arStore);

			//
			var keyStore = CreateKeyStore();
			var configuration = new TokenAuthenticationConfiguration(CreateAuthTokenizer(keyStore, integratedUserAgent), new SystemUserIdentityResolver(sessionStore, userStore, arStore));
			//functionalServices.Register(() => configuration);

			// Auth Service
			authService = new AuthService(
			   sessionStore: sessionStore,
			   userStore: userStore,
			   getGroupApps: () =>
			   {
				   var srv = new AuthenticationService();
				   var dict = new StringInsensitiveDictionary<StringInsensitiveHashSet>();
				   foreach (var item in srv.GroupApplications())
					   AddToDict(dict, item.GroupName, item.ApplicationId);
				   return dict;
			   },
			   getGroupActions: () =>
			   {
				   var srv = new AuthenticationService();
				   var dict = new StringInsensitiveDictionary<StringInsensitiveHashSet>();
				   foreach (var item in srv.GroupActions())
					   AddToDict(dict, item.GroupName, item.ActionId);
				   return dict;
			   }
		   );
			//functionalServices.Register(() => authService);

			return configuration;
		}
		static void AddToDict(StringInsensitiveDictionary<StringInsensitiveHashSet> dict, string key, string id)
		{
			StringInsensitiveHashSet hash;
			if (!dict.TryGetValue(key, out hash))
			{
				hash = new StringInsensitiveHashSet();
				dict.Add(key, hash);
			}
			hash.Add(id);
		}

		static SessionStore CreateSessionStore(TimeSpan maxAge)
		{
			return new SessionStore(
				save: (s) =>
				{
					var sess = new AC_UserSession
					{
						SessionKey = s.SessionKey,
						Username = s.Username,
						IPAddress = s.IPAddress,
						LastAccessedOn = DateTime.UtcNow,
						Terminated = false,
						CreatedOn = s.CreatedOn,
					};
					var srv = new AuthenticationService();
					srv.UserSessionAdd(sess);
					return s;
				},
				read: (sessionKey) =>
				{
					var srv = new AuthenticationService();
					var sess = srv.UserSessionBySessionKey(sessionKey);
					if (sess == null)
						return default(Session); //throw new Exception("no session to read. sessionKey: " + sessionKey);
					if (sess.Terminated)
						return default(Session); //throw new Exception("read session is terminated");
					return new Session
					{
						//ID = sess.ID,
						SessionKey = sess.SessionKey,
						Username = sess.Username,
						IPAddress = sess.IPAddress,
						//LastAccessedOn = sess.LastAccessedOn,
						CreatedOn = sess.CreatedOn.ToUniversalTime(),
					};
				},
				touch: (sessionKey) =>
				{
					var srv = new AuthenticationService();
					srv.UserSessionTouch(sessionKey);
				},
				terminate: (sessionKey) =>
				{
					var srv = new AuthenticationService();
					srv.UserSessionTerminate(sessionKey);
				},
				maxAge: maxAge,
				slidingCacheExpiration: TimeSpan.FromMinutes(30),
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
					var srv = new AuthenticationService();
					var authUser = srv.UserByUsername(username);
					if (authUser == null)
					{
						return default(User);
					}

					var password = authUser.Password;
					// primitive check to test if the password is a bcrypt hash
					if (password.Length < 60 || !password.StartsWith("$"))
					{
						// password is not a bcrypt hash, so hash it and then save it
						password = BCrypt.Net.BCrypt.HashPassword(password);
						srv.UserSavePassword(username, password);
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
				hardExpirationLength: TimeSpan.FromMinutes(20),
				memoryLimitMb: 10,
				physicalMemoryLimitPercent: 5,
				pollingInterval: TimeSpan.FromMinutes(2)
			);
		}

		static ActionRequestStore CreateActionRequestStore(TimeSpan maxAge)
		{
			return new ActionRequestStore(
				use: (actionKey, gpEmployeeId) =>
				{
					var arService = new NXS.DataServices.AuthenticationControl.ActionRequestsService(gpEmployeeId);
					var result = arService.Use(actionKey);
					if (result == null)
						return default(ActionRequest);

					var ar = new ActionRequest();
					ar.UserId = result.UserId;
					ar.Username = result.Username;
					ar.ApplicationId = result.ApplicationId;
					ar.ActionId = result.ActionId;
					return ar;
				}
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
				//var filepath = Path.Combine(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, "mockadgroups.json");
				var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mockadgroups.json");
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

		private static Tokenizer CreateAuthTokenizer(IFarmTokenKeyStore keyStore, Func<Nancy.NancyContext, string> integratedUserAgent)
		{
			var tokenizer = new Tokenizer(cfg =>
			{
				// length of time a key can be used to generate new tokens
				cfg.KeyExpiration(() => TimeSpan.FromHours(8));
				// length of time a token is valid for after its generating key has expired.
				cfg.TokenExpiration(() => TimeSpan.FromHours(24));

				if (integratedUserAgent != null)
					cfg.AdditionalItems(integratedUserAgent);

				// Save keys to db
				cfg.WithKeyCache(keyStore);
			});
			return tokenizer;
		}

		private static PersistentKeyStore CreateKeyStore()
		{
			return new PersistentKeyStore(
				update: (DateTime purgeExpiration, DateTime validExpiration, byte[] newKey) =>
				{
					var kvService = new NXS.DataServices.AuthenticationControl.KeyValueService();
					var newKeyValue = (newKey == null) ? null : SOS.Lib.Util.Cryptography.TripleDES.EncryptString(newKey, null);
					return ConvertKeyValues(kvService.UpdateAll(purgeExpiration, validExpiration, newKeyValue));
				},
				read: () =>
				{
					var kvService = new NXS.DataServices.AuthenticationControl.KeyValueService();
					return ConvertKeyValues(kvService.ReadAll());
				}
			);
		}
		private static List<KeyValue> ConvertKeyValues(List<NXS.DataServices.AuthenticationControl.Models.AcKeyValue> items)
		{
			var list = new List<KeyValue>();
			foreach (var item in items)
			{
				var kv = new KeyValue();
				kv.Value = SOS.Lib.Util.Cryptography.TripleDES.DecryptBytes(item.KeyValue, null);
				kv.CreatedOn = item.CreatedOn;
				list.Add(kv);
			}
			return list;
		}
	}
}
