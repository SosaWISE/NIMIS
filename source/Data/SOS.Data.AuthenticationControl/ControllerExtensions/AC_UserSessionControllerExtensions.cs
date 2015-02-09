using AR = SOS.Data.AuthenticationControl.AC_UserSession;
using ARCollection = SOS.Data.AuthenticationControl.AC_UserSessionCollection;
using ARController = SOS.Data.AuthenticationControl.AC_UserSessionController;

namespace SOS.Data.AuthenticationControl
{
	public static class AC_UserSessionControllerExtensions
	{
		public static AR BySessionKey(this ARController cntlr, string sessionKey)
		{
			var qry = AR.Query().WHERE(AR.Columns.SessionKey, sessionKey);
			return cntlr.LoadSingle(qry);
		}
	}
}
