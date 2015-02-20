using AR = SOS.Data.AuthenticationControl.AC_User;
using ARCollection = SOS.Data.AuthenticationControl.AC_UserCollection;
using ARController = SOS.Data.AuthenticationControl.AC_UserController;

namespace SOS.Data.AuthenticationControl
{
	public static class AC_UserControllerExtensions
	{
		public static AR ByUsername(this ARController cntlr, string username)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.Username, username)
				.WHERE(AR.Columns.IsActive, true);
			return cntlr.LoadSingle(qry);
		}
		public static AR ByHRUserId(this ARController cntlr, int hrUserId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.HRUserId, hrUserId);
			return cntlr.LoadSingle(qry);
		}
	}
}
