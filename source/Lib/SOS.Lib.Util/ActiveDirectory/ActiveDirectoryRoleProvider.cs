using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace SOS.Lib.Util.ActiveDirectory
{
	public class ActiveDirectoryRoleProvider : RoleProvider, IRoleManager
	{
		#region Properties

		#region Private

		#endregion Private

		#region Public

		#endregion Public

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Private

		#endregion Private

		#region Public

		public override string ApplicationName { get; set; }

		public override string[] GetAllRoles()
		{
			List<ADGroup> groups = ADManager.Instance.LoadAllGroups();
			var result = new string[groups.Count];
			for (int i = 0; i < result.Length; i++)
				result[i] = groups[i].Name;
			return result;
		}

		public override string[] GetRolesForUser(string username)
		{
			ADUser user = ADManager.Instance.LoadUser(username);
			if (user != null)
			{
				var result = new string[user.Groups.Count];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = user.Groups[i].Name;
				}
				return result;
			}
			else
				return new string[0];
		}

		public override string[] GetUsersInRole(string roleName)
		{
			ADGroup group = ADManager.Instance.LoadGroup(roleName);
			if (group != null)
			{
				var result = new string[group.Users.Count];
				for (int i = 0; i < result.Length; i++)
					result[i] = group.Users[i].UserName;
				return result;
			}
			else
				return new string[0];
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			ADUser user = ADManager.Instance.LoadUser(username);
			if (user == null)
				return false;
			else
				return user.IsInGroup(roleName);
		}

		public override bool RoleExists(string roleName)
		{
			return (ADManager.Instance.LoadGroup(roleName) != null);
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new InvalidOperationException("Users must be added through the Active Directory administrator");
		}

		public override void CreateRole(string roleName)
		{
			throw new InvalidOperationException("Roles must be created through the Active Directory administrator");
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new InvalidOperationException("Roles must be managed through the Active Directory administrator");
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			var result = new List<string>();
			foreach (string curr in GetUsersInRole(roleName))
			{
				if (Regex.IsMatch(curr, usernameToMatch))
					result.Add(curr);
			}
			return result.ToArray();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new InvalidOperationException("Roles and membership must be managed through the Active Directory administrator");
		}

		#endregion Public

		#endregion Methods
	}
}