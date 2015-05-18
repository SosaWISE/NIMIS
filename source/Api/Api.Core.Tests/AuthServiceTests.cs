using NXS.Lib.Web.Authentication;
using NXS.Lib.Web.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Api.Core.Tests
{
	public class AuthServiceTests
	{
		[Fact]
		public void Test_Should_start_with_permission()
		{
			var groupApps = new StringInsensitiveDictionary<StringInsensitiveHashSet>();
			var groupActions = new StringInsensitiveDictionary<StringInsensitiveHashSet>();
			var srv = GetAuthService(groupApps, groupActions);

			Assert.Equal(true, srv.HasPermission(
				applicationIDs: new string[] { },
				actionIDs: new string[] { },
				userGroups: new string[] { },
				userApplications: new string[] { },
				userActions: new string[] { }
			));
		}
		[Fact]
		public void Test_User_s_groups_should_have_atleast_one_app_and_one_action()
		{
			var groupApps = new StringInsensitiveDictionary<StringInsensitiveHashSet>();
			var groupActions = new StringInsensitiveDictionary<StringInsensitiveHashSet>();
			var srv = GetAuthService(groupApps, groupActions);

			// apps
			groupApps.Add("one", new StringInsensitiveHashSet()
			{
				"app1",
				"app10",
			});
			groupApps.Add("two", new StringInsensitiveHashSet()
			{
				"app2",
				"app20",
			});
			// actions
			groupActions.Add("one", new StringInsensitiveHashSet()
			{
				"view",
			});
			groupActions.Add("two", new StringInsensitiveHashSet()
			{
				"add",
				"edit",
			});

			Assert.Equal(false, srv.HasPermission(
				applicationIDs: new string[] { "app1" },
				actionIDs: new string[] { "edit" },
				userGroups: new string[] { },
				userApplications: new string[] { },
				userActions: new string[] { }
			));
			Assert.Equal(true, srv.HasPermission(
				applicationIDs: new string[] { "app1" },
				actionIDs: new string[] { "edit" },
				userGroups: new string[] { "one", "two" },
				userApplications: new string[] { },
				userActions: new string[] { }
			));
		}
		[Fact]
		public void Test_User_s_actions()
		{
			var groupApps = new StringInsensitiveDictionary<StringInsensitiveHashSet>();
			var groupActions = new StringInsensitiveDictionary<StringInsensitiveHashSet>();
			var srv = GetAuthService(groupApps, groupActions);

			// actions
			groupActions.Add("one", new StringInsensitiveHashSet()
			{
				"view",
			});
			groupActions.Add("two", new StringInsensitiveHashSet()
			{
				"add",
				"edit",
			});

			Assert.Equal(false, srv.HasPermission(
				applicationIDs: new string[] { },
				actionIDs: new string[] { "edit" },
				userGroups: new string[] { },
				userApplications: new string[] { },
				userActions: new string[] { }
			));
			Assert.Equal(true, srv.HasPermission(
				applicationIDs: new string[] { },
				actionIDs: new string[] { "edit" },
				userGroups: new string[] { },
				userApplications: new string[] { },
				userActions: new string[] { "edit" }
			));
		}

		public AuthService GetAuthService(StringInsensitiveDictionary<StringInsensitiveHashSet> groupApps, StringInsensitiveDictionary<StringInsensitiveHashSet> groupActions)
		{
			var sessionStore = new SessionStore(null, null, null, null, TimeSpan.FromDays(1), TimeSpan.FromDays(1));
			var userStore = new UserStore(null, TimeSpan.FromDays(1));
			return new AuthService(() => groupApps, () => groupActions, sessionStore, userStore);
		}
	}
}