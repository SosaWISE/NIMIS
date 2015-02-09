using System;

namespace SOS.Data
{
	public static class SosSqlExceptionHandler
	{
		#region Member Properties

		public static Func<int, int, int, string, int, string, string> FxDefaultFormatter { get; set; }

		#endregion Member Properties

		#region Enums

		public enum MessageFields
		{
			ErrorNumber = 0,
			ErrorSeverity = 1,
			ErrorState = 2,
			ErrorProc = 3,
			ErrorLine = 4,
			ErrorMessage = 5
		}

		#endregion Enums

		#region Methods

		public static string ParseSqlException(string sSqlMessage, Func<int, int, int, string, int, string, string> fxFormatter)
		{
			/** Initialize. */
			string sResult = sSqlMessage;
			string[] saParts = sSqlMessage.Split('|');

			/** Check that the message has the right format. */
			if (saParts.Length != 6) return sSqlMessage;
			/** Check that we have a function */
			if (fxFormatter == null) return sResult;

			/** Parse the message and format. */
			int nErrorNumber = Convert.ToInt32(saParts[(int) MessageFields.ErrorNumber]);
			int nErrorSeverity = Convert.ToInt32(saParts[(int) MessageFields.ErrorSeverity]);
			int nErrorState = Convert.ToInt32(saParts[(int) MessageFields.ErrorState]);
			string sErrorProc = saParts[(int) MessageFields.ErrorProc];
			int nErrorLine = Convert.ToInt32(saParts[(int) MessageFields.ErrorLine]);
			string sErrorMessage = saParts[(int) MessageFields.ErrorMessage];

			/** Execute formater. */
			sResult = fxFormatter(nErrorNumber, nErrorSeverity, nErrorState, sErrorProc, nErrorLine, sErrorMessage);

			/** Return result. */
			return sResult;
		}

		#endregion Methods
	}
}
