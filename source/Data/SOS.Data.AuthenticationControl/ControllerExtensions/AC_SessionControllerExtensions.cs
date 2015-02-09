using AR = SOS.Data.AuthenticationControl.AC_Session;
using ARCollection = SOS.Data.AuthenticationControl.AC_SessionCollection;
using ARController = SOS.Data.AuthenticationControl.AC_SessionController;

namespace SOS.Data.AuthenticationControl
{
// ReSharper disable once InconsistentNaming
	public static class AC_SessionControllerExtensions
	{

		public static AR StartSession(this ARController cntlr, string applicationId, string ipAddress, int timezoneOffset)
		{
			return cntlr.LoadSingle(SosAuthControlDataStoredProcedureManager.AC_SessionStart(applicationId, ipAddress, timezoneOffset));
		}

		public static AR TerminateSession(this ARController oCntlr, long lSessionID)
		{
			/** Initialize. */
			var oItem = oCntlr.LoadByPrimaryKey(lSessionID);
			oItem.SessionTerminated = true;
			oItem.Save();

			/** Return result. */
			return oItem;
		}

		public static AR SessionGetByIdAndApplicationID(this ARController oCntlr, long lSessionID, string szApplicationID)
		{
			/** Initialize. */
			var oQry = AR.Query()
				.WHERE(AR.Columns.SessionID, lSessionID)
				.WHERE(AR.Columns.ApplicationId, szApplicationID);

			/** Execute query . */
			AR oResult = oCntlr.LoadSingle(oQry);

			/** Return result. */
			return oResult;
		}

		public static AR SessionValidate(this ARController oCntlr, long lSessionID, string szApplicationID, int nMinutesThreshold)
		{
			/** Initialize. */
			/** Execute query . */
			AR oResult = oCntlr.LoadSingle(SosAuthControlDataStoredProcedureManager.AC_SessionValidate(lSessionID, szApplicationID, nMinutesThreshold));

			/** Return result. */
			return oResult;
		}
	}
}
