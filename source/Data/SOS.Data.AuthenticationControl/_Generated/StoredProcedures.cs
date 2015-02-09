


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

namespace SOS.Data.AuthenticationControl {
	public partial class SosAuthControlDataStoredProcedureManager {
		public static StoredProcedure AC_ActionsForSession(long? SessionID) {
			StoredProcedure sp = new StoredProcedure("custAC_ActionsForSession" ,DataService.GetInstance("SosAuthControlProvider"));
			sp.Command.AddParameter("@SessionID", SessionID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AC_ApplicationsForSession(long? SessionID) {
			StoredProcedure sp = new StoredProcedure("custAC_ApplicationsForSession" ,DataService.GetInstance("SosAuthControlProvider"));
			sp.Command.AddParameter("@SessionID", SessionID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AC_SessionStart(string ApplicationId,string IPAddress,int? TimezoneOffset) {
			StoredProcedure sp = new StoredProcedure("custAC_SessionStart" ,DataService.GetInstance("SosAuthControlProvider"));
			sp.Command.AddParameter("@ApplicationId", ApplicationId, DbType.AnsiString);
			sp.Command.AddParameter("@IPAddress", IPAddress, DbType.AnsiString);
			sp.Command.AddParameter("@TimezoneOffset", TimezoneOffset, DbType.Int32);
			return sp;
		}
		public static StoredProcedure AC_SessionValidate(long? SessionID,string ApplicationId,int? MinutesThreshold) {
			StoredProcedure sp = new StoredProcedure("custAC_SessionValidate" ,DataService.GetInstance("SosAuthControlProvider"));
			sp.Command.AddParameter("@SessionID", SessionID, DbType.Int64);
			sp.Command.AddParameter("@ApplicationId", ApplicationId, DbType.AnsiString);
			sp.Command.AddParameter("@MinutesThreshold", MinutesThreshold, DbType.Int32);
			return sp;
		}
		public static StoredProcedure AC_UsersCrmAuthentication(string username,string password,long? SessionId,string ApplicationId,string Groups) {
			StoredProcedure sp = new StoredProcedure("custAC_UsersCrmAuthentication" ,DataService.GetInstance("SosAuthControlProvider"));
			sp.Command.AddParameter("@username", username, DbType.AnsiString);
			sp.Command.AddParameter("@password", password, DbType.AnsiString);
			sp.Command.AddParameter("@SessionId", SessionId, DbType.Int64);
			sp.Command.AddParameter("@ApplicationId", ApplicationId, DbType.AnsiString);
			sp.Command.AddParameter("@Groups", Groups, DbType.String);
			return sp;
		}
		public static StoredProcedure AC_UsersDealerUsersAuthenticate(long? SessionId,long? DealerId,string Username,string Password) {
			StoredProcedure sp = new StoredProcedure("custAC_UsersDealerUsersAuthenticate" ,DataService.GetInstance("SosAuthControlProvider"));
			sp.Command.AddParameter("@SessionId", SessionId, DbType.Int64);
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int64);
			sp.Command.AddParameter("@Username", Username, DbType.String);
			sp.Command.AddParameter("@Password", Password, DbType.String);
			return sp;
		}
		public static StoredProcedure AC_UsersDealerUsersAuthenticateViaToken(long? SessionId,int? DealerUserID,string Token) {
			StoredProcedure sp = new StoredProcedure("custAC_UsersDealerUsersAuthenticateViaToken" ,DataService.GetInstance("SosAuthControlProvider"));
			sp.Command.AddParameter("@SessionId", SessionId, DbType.Int64);
			sp.Command.AddParameter("@DealerUserID", DealerUserID, DbType.Int32);
			sp.Command.AddParameter("@Token", Token, DbType.String);
			return sp;
		}
		public static StoredProcedure AC_UsersGeneralAuthentication(string Username,string Password) {
			StoredProcedure sp = new StoredProcedure("custAC_UsersGeneralAuthentication" ,DataService.GetInstance("SosAuthControlProvider"));
			sp.Command.AddParameter("@Username", Username, DbType.String);
			sp.Command.AddParameter("@Password", Password, DbType.String);
			return sp;
		}
	}
}
 
