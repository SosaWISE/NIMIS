using SOS.Lib.LaipacAPI.ExceptionHandling;
using SOS.Lib.LaipacAPI.Models;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class CurrentSettingsRequest : Requests
	{
		/********************************************
		 * 4. Request Current Status
		 *******************************************/

		public CurrentSettingsRequest(string sPassword) : base(sPassword)
		{
		}

		public static string GetRequest(string sPassword)
		{
			if (sPassword.Length != 8)
				throw new LaipacParameterFixedLengthException(string.Format(INVALID_LENGTH_MSG
					, "AVREQ", "PSW", FIXED, 8, sPassword.Length));
			return string.Format(REQ_CURRENT_SET_COMMAND, sPassword);
		}


		public static CurrentSettingsSentence CurrentPositionResponse(string rawSentence)
		{
			/** Initialize. */
			var oSentence = new CurrentSettingsSentence(rawSentence);

			/** Validate. */
			oSentence.ValidateFixedLength();

			/** Return result. */
			return oSentence;
		}
	}
}
