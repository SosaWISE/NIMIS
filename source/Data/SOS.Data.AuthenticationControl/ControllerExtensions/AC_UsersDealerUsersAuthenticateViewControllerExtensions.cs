using AR = SOS.Data.AuthenticationControl.AC_UsersDealerUsersAuthenticateView;
using ARCollection = SOS.Data.AuthenticationControl.AC_UsersDealerUsersAuthenticateViewCollection;
using ARController = SOS.Data.AuthenticationControl.AC_UsersDealerUsersAuthenticateViewController;

namespace SOS.Data.AuthenticationControl
{
// ReSharper disable InconsistentNaming
	public static class AC_UsersDealerUsersAuthenticateViewControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR Authenticate(this ARController oCntlr, long lSeasonId, long lDealerId, string szUsername, string szPassword)
		{
			/** Initialize. */
			return
				oCntlr.LoadSingle(SosAuthControlDataStoredProcedureManager.AC_UsersDealerUsersAuthenticate(lSeasonId, lDealerId,
						szUsername, szPassword));
		}

		public static AR AuthenticateViaToken(this ARController oCntlr, long lSeasonId, int nUserDealerId, string sTokenStream)
		{
			/** Initialize. */
			return
				oCntlr.LoadSingle(SosAuthControlDataStoredProcedureManager.AC_UsersDealerUsersAuthenticateViaToken(lSeasonId, nUserDealerId,
						sTokenStream));
		}
	}
}
