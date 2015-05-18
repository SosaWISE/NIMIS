﻿using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Lib.Web.Authentication
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

	public interface IAuthService
	{
		void ReloadGroupActionItems();
		Result<SystemUserIdentity> Authenticate(string username, string password, string ipAddress);
		void RemoveCachedUser(string username);
		User GetUser(string username);
		UserModel ToUserModel(SystemUserIdentity identity, bool includeLists = true);
		bool HasPermission(IEnumerable<string> applicationIDs, IEnumerable<string> actionIDs, IEnumerable<string> userGroups, IEnumerable<string> userApplications, IEnumerable<string> userActions);
		void EndSession(byte[] sessionNum);
	}
}
