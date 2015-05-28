using NXS.Data;
using NXS.Data.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.AuthenticationControl
{
	public class AuthenticationService
	{
		public IEnumerable<AC_GroupAction> GroupActions()
		{
			using (var db = DBase.Connect())
				return db.AC_GroupActions.AllActive();
		}
		public IEnumerable<AC_GroupApplication> GroupApplications()
		{
			using (var db = DBase.Connect())
				return db.AC_GroupApplications.AllActive();
		}
		public void UserSessionAdd(AC_UserSession item)
		{
			using (var db = DBase.Connect())
				item.ID = db.AC_UserSessions.Insert(item);
		}
		public AC_UserSession UserSessionBySessionKey(string sessionKey)
		{
			using (var db = DBase.Connect())
				return db.AC_UserSessions.BySessionKey(sessionKey);
		}
		public void UserSessionTouch(string sessionKey)
		{
			using (var db = DBase.Connect())
				db.AC_UserSessions.Touch(sessionKey);
		}
		public void UserSessionTerminate(string sessionKey)
		{
			using (var db = DBase.Connect())
				db.AC_UserSessions.Terminate(sessionKey);
		}
		public AC_UsersAppAuthenticationView UserByUsername(string username)
		{
			using (var db = DBase.Connect())
				return db.AC_UsersAppAuthenticationViews.ByUsername(username);
		}
		public void UserSavePassword(string username, string password)
		{
			using (var db = DBase.Connect())
				db.AC_Users.UpdatePassword(username, password);
		}
	}
}
