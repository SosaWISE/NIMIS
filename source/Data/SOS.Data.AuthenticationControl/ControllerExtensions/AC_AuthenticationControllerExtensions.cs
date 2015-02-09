using AR = SOS.Data.AuthenticationControl.AC_Authentication;
using ARCollection = SOS.Data.AuthenticationControl.AC_AuthenticationCollection;
using ARController = SOS.Data.AuthenticationControl.AC_AuthenticationController;

namespace SOS.Data.SosCrm
{
	public static class AC_AuthenticationControllerExtensions
	{
		public static AR SaveEvent(this ARController oCntlr, long lSessionId, int nUserId, string szUsername, string szPassword)
		{
			/** Initialize. */
			var oItem = new AR
				{
					SessionId = lSessionId
					, UserId = nUserId
					, Username = szUsername
					, Password = szPassword
				};
			oItem.Save(nUserId);

			/** Return result. */
			return oItem;
		}
	}
}
