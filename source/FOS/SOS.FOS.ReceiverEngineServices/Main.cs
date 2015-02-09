using System.Web;
using SOS.Data.ReceiverEngine;

namespace SOS.FOS.ReceiverEngineServices
{
	public class Main
	{
		public REResult SaveRequest(HttpContext oContext)
		{
			/**  Initialize. */
			var oRaw = new RE_RequestsRaw {RawQS = oContext.Request.QueryString.ToString(), CreatedBy = "SYSTEM"};
			oRaw.Save("SYSTEM");

			/** Initialize result. */
			var oResult = new REResult(REErrorCodes.Success, "Success", oRaw);
			return oResult;
		}

		public REResult SaveSignal(string szTitle, string szCode, string szShortCode, string szMessage, string szPhone,
						string szCarrier, string szKeyword, string szGroupName, string szCustomTicket, string szDefaultKeyword,
						string szUsername, string szPassword, string szApiKey)
		{
			/** Initialize. */
			var oRequest = new RE_TxtWireRequest
						{
							Title = szTitle,
							Code = szCode,
							ShortCode = szShortCode,
							Message = szMessage,
							Phone = szPhone,
							Carrier = szCarrier,
							Keyword = szKeyword,
							GroupName = szGroupName,
							CustomTicket = szCustomTicket,
							DefaultKeyword = szDefaultKeyword,
							Username = szUsername,
							Password = szPassword,
							ApiKey = szApiKey,
						};
			oRequest.Save("SYSTEM");

			/** Initialize Result. */
			var oResult = new REResult(REErrorCodes.Success, "Success", oRequest);
			return oResult;
		}
	}
}