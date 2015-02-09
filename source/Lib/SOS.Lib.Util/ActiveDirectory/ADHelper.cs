using System;
using System.Collections.Generic;
using System.Linq;

namespace SOS.Lib.Util.ActiveDirectory
{
	public class ADHelper
	{
		public static bool IsValidLogin(string username, string password, string domain = null)
		{
			username = EmailToUsername(username);
			ADUtility.LoginResult loginResult = ADManager.Login(username, password, domain);
			return (loginResult == ADUtility.LoginResult.LOGIN_OK);
		}

		public static List<string> GetGroupsForUser(string username)
		{
			var userGroups = new List<string>();

			username = EmailToUsername(username);
			ADUser user = ADManager.Instance.LoadUser(username);

			if (user != null)
			{
				userGroups.AddRange(user.Groups.Select(t => t.Name));
			}

			return userGroups;
		}

		private static string EmailToUsername(string username)
		{
			return (string.IsNullOrEmpty(username) || !username.Contains("@")) ? username : username.Substring(0, username.IndexOf("@"));
		}
	}
}
