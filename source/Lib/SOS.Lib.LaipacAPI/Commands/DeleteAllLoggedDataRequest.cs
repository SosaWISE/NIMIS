using SOS.Lib.LaipacAPI.ExceptionHandling;
using SOS.Lib.LaipacAPI.Models;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class DeleteAllLoggedDataRequest : Requests
	{
		/********************************************
		 * 8.1 Delete All Logged Data
		 *******************************************/

		public DeleteAllLoggedDataRequest(string sPassword) : base(sPassword)
		{
		}

		public static string GetRequest(string sPassword)
		{
			if (sPassword.Length != 8)
				throw new LaipacParameterFixedLengthException(string.Format(INVALID_LENGTH_MSG
					, "AVREQ", "PSW", FIXED, 8, sPassword.Length));
			return string.Format(REQ_DEL_ALL_DAT_COMMAND, sPassword);
		}


		public static DeleteAllLoggedDataSentence CurrentPositionResponse(string rawSentence)
		{
			/** Initialize. */
			var oSentence = new DeleteAllLoggedDataSentence(rawSentence);

			/** Validate. */
			oSentence.ValidateFixedLength();

			/** Return result. */
			return oSentence;
		}
	}
}
