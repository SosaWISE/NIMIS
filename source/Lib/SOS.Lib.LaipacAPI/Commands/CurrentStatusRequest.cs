using SOS.Lib.LaipacAPI.ExceptionHandling;
using SOS.Lib.LaipacAPI.Models;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class CurrentStatusRequest : Requests
	{

		public CurrentStatusRequest(string sPassword)
			: base(sPassword)
		{
		}

		/********************************************
		 * 4. Request Current Status
		 *******************************************/
		public static string GetRequest(string sPassword)
		{
			if (sPassword.Length != 8)
				throw new LaipacParameterFixedLengthException(string.Format(INVALID_LENGTH_MSG
					, "AVREQ", "PSW", FIXED, 8, sPassword.Length));
			return string.Format(REQ_CURRENT_STA_COMMAND, sPassword);
		}


		public static CurrentStatusSentence CurrentPositionResponse(string rawSentence)
		{
			/** Initialize. */
			var oSentence = new CurrentStatusSentence(rawSentence);

			/** Validate. */
			oSentence.ValidateFixedLength();

			/** Return result. */
			return oSentence;
		}

	}
}
