using SOS.Lib.LaipacAPI.ExceptionHandling;
using SOS.Lib.LaipacAPI.Models;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class LoggedDataRequest : Requests
	{

		/********************************************
		 * 2. Request Logged Data
		 *******************************************/
		#region Logged Data

		public LoggedDataRequest(string sPassword) : base(sPassword)
		{
		}

		public static string GetRequest(string sPassword)
		{
			if (sPassword.Length != 8)
				throw new LaipacParameterFixedLengthException(string.Format(INVALID_LENGTH_MSG
					, "AVREQ", "PSW", FIXED, 8, sPassword.Length));
			return string.Format(REQ_LOGGED_DATA_COMMAND, sPassword);
		}

		public static LoggedDataSentence LoggedDataResponse(string sentence)
		{
			/** Inititalize. */
			var oSentence = new LoggedDataSentence(sentence);

			/** Validate. */
			oSentence.ValidateFixedLength();

			/** Return result. */
			return oSentence;
		}

		#endregion Logged Data
	}
}
