using System;
using System.Collections.Generic;
using SOS.Data.ReceiverEngine;

namespace SOS.FOS.ReceiverEngineServices.SmsGateways
{
	public static class TxtWire
	{
		public static REResult ReceiveSignal(TxtWireEnvModel oEnvModel, Func<TxtWireEnvModel, List<REResult>, REResult> fxSuccess
			, Func<List<REResult>, REResult> fxFailure)
		{
			/** Initialize. */
			REResult oResult;
			try
			{
				/** Initialize. */
				var oRequest = new RE_TxtWireRequest
				{
					Title = oEnvModel.Title,
					Code = oEnvModel.Code,
					ShortCode = oEnvModel.ShortCode,
					Message = oEnvModel.Message,
					Phone = oEnvModel.Phone,
					Carrier = oEnvModel.Carrier,
					Keyword = oEnvModel.Keyword,
					GroupName = oEnvModel.GroupName,
					CustomTicket = oEnvModel.CustomTicket,
					DefaultKeyword = oEnvModel.DefaultKeyword,
					Username = oEnvModel.Username,
					Password = oEnvModel.Password,
					ApiKey = oEnvModel.ApiKey,
				};
				oRequest.Save("SYSTEM");

				/** Initialize Result. */
				oResult = new REResult(REErrorCodes.Success, "Success", oRequest);

				/** Check that there is a callback for success. */
				if (fxSuccess != null)
				{
					var oList = new List<REResult> { oResult };
					oResult = fxSuccess(oEnvModel, oList);
				}
			}
			catch (Exception oEx)
			{
				oResult = new REResult(REErrorCodes.Exception, "Exception", oEx);
				if (fxFailure != null)
				{
					var oList = new List<REResult> { oResult };
					oResult = fxFailure(oList);
				}
			}

			/** Return result. */
			return oResult;
		}
	}
}
