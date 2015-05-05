using Dapper;
using NXS.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NXS.Data.AuthenticationControl
{
	public partial class Sprocs
	{
		private readonly DBase db;
		public Sprocs(DBase db)
		{
			this.db = db;
		}

		public Task<IEnumerable<T>> AC_ActionsForSession<T>(long? SessionID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SessionID", SessionID);
			return db.QueryAsync<T>("custAC_ActionsForSession", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AC_ApplicationsForSession<T>(long? SessionID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SessionID", SessionID);
			return db.QueryAsync<T>("custAC_ApplicationsForSession", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AC_SessionStart<T>(string ApplicationId,string IPAddress,int? TimezoneOffset)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ApplicationId", ApplicationId);
			p.Add("@IPAddress", IPAddress);
			p.Add("@TimezoneOffset", TimezoneOffset);
			return db.QueryAsync<T>("custAC_SessionStart", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AC_SessionValidate<T>(long? SessionID,string ApplicationId,int? MinutesThreshold)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SessionID", SessionID);
			p.Add("@ApplicationId", ApplicationId);
			p.Add("@MinutesThreshold", MinutesThreshold);
			return db.QueryAsync<T>("custAC_SessionValidate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AC_UsersCrmAuthentication<T>(string username,string password,long? SessionId,string ApplicationId,string Groups)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@username", username);
			p.Add("@password", password);
			p.Add("@SessionId", SessionId);
			p.Add("@ApplicationId", ApplicationId);
			p.Add("@Groups", Groups);
			return db.QueryAsync<T>("custAC_UsersCrmAuthentication", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AC_UsersDealerUsersAuthenticate<T>(long? SessionId,long? DealerId,string Username,string Password)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SessionId", SessionId);
			p.Add("@DealerId", DealerId);
			p.Add("@Username", Username);
			p.Add("@Password", Password);
			return db.QueryAsync<T>("custAC_UsersDealerUsersAuthenticate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AC_UsersDealerUsersAuthenticateViaToken<T>(long? SessionId,int? DealerUserID,string Token)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SessionId", SessionId);
			p.Add("@DealerUserID", DealerUserID);
			p.Add("@Token", Token);
			return db.QueryAsync<T>("custAC_UsersDealerUsersAuthenticateViaToken", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AC_UsersGeneralAuthentication<T>(string Username,string Password)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Username", Username);
			p.Add("@Password", Password);
			return db.QueryAsync<T>("custAC_UsersGeneralAuthentication", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> wiseSP_ExceptionsThrown<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("wiseSP_ExceptionsThrown", p, commandType: System.Data.CommandType.StoredProcedure);
		}
	}
}

