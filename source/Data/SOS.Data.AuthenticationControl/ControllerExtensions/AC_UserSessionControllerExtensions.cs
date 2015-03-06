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
		public static void Touch(this ARController cntlr, string sessionKey)
		{
			//@TODO: do update without first loading
			// e.g.: UPDATE SET LastAccessedOn=@0 FROM AC_UserSessions WHERE LastAccessedOn<@1 AND SessionKey=@2 AND Terminated=0
			var now = System.DateTime.UtcNow;
			var qry = AR.Query()
				.WHERE(AR.Columns.LastAccessedOn, SubSonic.Comparison.LessThan, now)
				.AND(AR.Columns.SessionKey, SubSonic.Comparison.Equals, sessionKey)
				.AND(AR.Columns.Terminated, SubSonic.Comparison.Equals, false);
			var sess = cntlr.LoadSingle(qry);
			if (sess != null)
			{
				sess.LastAccessedOn = now;
				sess.Save();
			}
			else
			{
				int t = 0;
			}
		}
	}
}
