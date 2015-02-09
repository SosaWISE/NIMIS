using AR = SOS.Data.AuthenticationControl.AC_UserGeneralAuthenticationView;
using ARCollection = SOS.Data.AuthenticationControl.AC_UserGeneralAuthenticationViewCollection;
using ARController = SOS.Data.AuthenticationControl.AC_UserGeneralAuthenticationViewController;

namespace SOS.Data.AuthenticationControl
{
	public static class AC_UserGeneralAuthenticationViewControllerExtensions
	{
		public static ARCollection GetListOfAccountByUsername (this ARController oCntlr, string szUsername, string szPassword)
		{
			/** Inititliaze. */
			var oResultCol =
				oCntlr.LoadCollection(SosAuthControlDataStoredProcedureManager.AC_UsersGeneralAuthentication(szUsername, szPassword));

			/** Return result. */
			return oResultCol;
		}
	}
}
