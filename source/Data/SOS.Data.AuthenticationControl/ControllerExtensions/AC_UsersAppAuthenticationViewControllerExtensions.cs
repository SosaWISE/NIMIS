using SOS.Lib.Util;
using System.Collections.Generic;
using AR = SOS.Data.AuthenticationControl.AC_UsersAppAuthenticationView;
using ARCollection = SOS.Data.AuthenticationControl.AC_UsersAppAuthenticationViewCollection;
using ARController = SOS.Data.AuthenticationControl.AC_UsersAppAuthenticationViewController;

namespace SOS.Data.AuthenticationControl
{
	// ReSharper disable InconsistentNaming
	public static class AC_UsersAppAuthenticationViewControllerExtensions
	// ReSharper restore InconsistentNaming
	{
		public static AR SigninToApp(this ARController cntlr, string username, string password, long sessionID,
									 string appToken, List<string> groups)
		{
			// ** Init
			AR result = cntlr.LoadSingle(SosAuthControlDataStoredProcedureManager.AC_UsersCrmAuthentication(username, password, sessionID,
																						   appToken, StringHelper.Join(groups, ",")));

			// ** Return result
			return result;
		}

		public static AR ByUsername(this ARController cntlr, string username)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.Username, username)
				.WHERE(AR.Columns.IsActive, true)
				.WHERE(AR.Columns.IsDeleted, false);
			return cntlr.LoadSingle(qry);
		}
	}
}
