using SOS.Lib.LaipacAPI.ExceptionHandling;
using SOS.Lib.LaipacAPI.Models;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class CurrentPositionRequest : Requests
	{

		public CurrentPositionRequest(string sPassword)
			: base(sPassword)
		{
		}

		/********************************************
		 * 3. Request Current Position
		 *******************************************/
		public static string GetRequest(string sPassword)
		{
			if (sPassword.Length != 8)
				throw new LaipacParameterFixedLengthException(string.Format(INVALID_LENGTH_MSG
					, "AVREQ", "PSW", FIXED, 8, sPassword.Length));
			return GetRequestWrapper(string.Format(REQ_CURRENT_POS_COMMAND, sPassword));
		}

		public static CurrentPositionSentence CurrentPositionResponse(string rawSentence)
		{
			/** Initialize. */
			var oSentence = new CurrentPositionSentence(rawSentence);

			/** Validate. */
			oSentence.ValidateFixedLength();

			/** Return result. */
			return oSentence;
		}

	}
}
