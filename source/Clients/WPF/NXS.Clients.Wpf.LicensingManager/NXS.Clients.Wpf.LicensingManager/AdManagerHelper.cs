using System.Collections.Generic;
using SOS.Lib.Util.ActiveDirectory;

namespace NXS.Clients.Wpf.LicensingManager
{
	class AdManagerHelper
	{
		#region Singleton Implementation

		private AdManagerHelper()
		{
		}

		public static AdManagerHelper Instance
		{
			get
			{
				return Nested.ControllerInstance;
			}
		}

		private class Nested
		{
			static Nested()
			{
			}

			internal static readonly AdManagerHelper ControllerInstance = new AdManagerHelper();
		}

		#endregion Singleton Implementation

		#region Methods
		public bool IsValidLogin(string username, string password)
		{
			username = EmailToUsername(username);
			ADUtility.LoginResult loginResult = ADManager.Login(username, password);
			return (loginResult == ADUtility.LoginResult.LOGIN_OK);
		}

		public List<string> GetGroupsForUser(string username)
		{
			var userGroups = new List<string>();

			username = EmailToUsername(username);
			ADUser user = ADManager.Instance.LoadUser(username);

			if (user != null)
			{
				for (int i = 0; i < user.Groups.Count; i++)
				{
					userGroups.Add(user.Groups[i].Name);
				}
			}

			return userGroups;
		}

		private string EmailToUsername(string username)
		{
			return (string.IsNullOrEmpty(username) || !username.Contains("@")) ? username : username.Substring(0, username.IndexOf("@", System.StringComparison.Ordinal));
		}
		#endregion Methods
	}
}
