using SOS.Lib.LaipacAPI.ExceptionHandling;
using SOS.Lib.LaipacAPI.Models;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class CurrentPhoneNumberRequest : Requests
	{

		public CurrentPhoneNumberRequest(string sPassword)
			: base(sPassword)
		{
		}

		/********************************************
		 * 8. Request Current Phone Number
		 *******************************************/

		public static string GetRequest(string sPassword)
		{
			if (sPassword.Length != 8)
				throw new LaipacParameterFixedLengthException(string.Format(INVALID_LENGTH_MSG
					, "AVREQ", "PSW", FIXED, 8, sPassword.Length));
			return GetRequestWrapper(string.Format(REQ_CURRENT_SET_COMMAND, sPassword));
		}


		public static CurrentPhoneNumberSentence CurrentPositionResponse(string rawSentence)
		{
			/** Initialize. */
			var oSentence = new CurrentPhoneNumberSentence(rawSentence);

			/** Validate. */
			oSentence.ValidateFixedLength();

			/** Return result. */
			return oSentence;
		}
	}
}
