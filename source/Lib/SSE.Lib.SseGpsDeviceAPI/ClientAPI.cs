using System;
using System.Net;
using SOS.Data.Logging;
using SSE.Lib.SseGpsDeviceAPI.Commands;
using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.ExceptionHandling;
using SSE.Lib.SseGpsDeviceAPI.Models;

namespace SSE.Lib.SseGpsDeviceAPI
{
    public class ClientAPI
	{
		#region Singleton .ctor

	    private ClientAPI()
	    {
	    }

	    private static volatile ClientAPI _instance;
		private static readonly object _instanceSync = new object();
	    public static ClientAPI Instance
	    {
		    get
		    {
			    if (_instance == null)
			    {
				    lock (_instanceSync)
				    {
					    if (_instance == null)
					    {
							_instance = new ClientAPI();
					    }
				    }
			    }

				/** Return intance. */
			    return _instance;
		    }
	    }

	    #endregion Singleton .ctor

		#region Member Vairables
		#endregion Member Vairables

		#region Member Functions

		public string SendSystemInfoRequest(EndPoint remoteEndPoint)
		{
			/** Validate Client Table. */
			DBErrorManager.Instance.AddSuccessMessage("****Sending Initial Handshake to Device"
				, string.Format("Remote End Point: {0}", remoteEndPoint));

			/** Return request. */
			var systemInfoRequest = new ClientRequestSIR();
			var responseString = systemInfoRequest.GetSentence();

			/** Display Message to output device. */
			DBErrorManager.Instance.AddSuccessMessage("**** ->System Info Request:", string.Format("-->{0}", responseString));

			/** Return string. */
			return responseString;
		}

		#endregion Member Functions

		#region HandleRequestSentence and ReadSentence

		/// <summary>
		/// This is the entry point for the sentence execution.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sentence"></param>
		/// <param name="remoteEndPoint"></param>
		/// <param name="returnDeviceInfo"></param>
		/// <param name="dispatchToCs"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public T HandleRequestSentence<T>(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs, Func<string, EndPoint, Func<IPAddress, long?, DeviceInfo>, Func<string, decimal, decimal, string, string, bool?, bool>, T> action)
		{
			#region TRY

			try
			{
				// ** Validate CheckSum.
				string checkSum = Helper.SentenceParser.GetCheckSumRawSentence(sentence);
				string[] sentenceObj = Helper.SentenceParser.RawSentenceToSentence(sentence);
				bool resultValue = checkSum.Equals(sentenceObj[Helper.SentenceParser.Fields.ChkSum]);

				// ** Run action.
				if (resultValue)
					return action(sentence, remoteEndPoint, returnDeviceInfo, dispatchToCs);

				// ** Default path of execution.
				throw new SseGpsDeviceChkSumFailed(sentenceObj[Helper.SentenceParser.Fields.ChkSum], checkSum, string.Format("The checksum that was received in the sentence is invalid"));
			}
			#endregion TRY
			#region CATCH
			catch (Exception ex)
			{
				DBErrorManager.Instance.AddCriticalMessage(ex
					, "Critical Error"
					, string.Format("Exception thrown in HandleRequest Sentence on {0}"
						, action));
				throw;
			}
			#endregion CATCH
		}

	    public string ReadSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo = null, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs = null)
	    {
			// ** Initialize
		    var oResponse = (IResponse) Response.Factory(sentence, remoteEndPoint, dispatchToCs);
		    IPAddress deviceIPAddress = ((IPEndPoint) (remoteEndPoint)).Address;

			if (oResponse != null)
			{
				switch (oResponse.CommandName)
				{
					case CommandDef.SIR:
						var clientResponseSIR = (ClientResponseSIR) oResponse;
						if (returnDeviceInfo != null) returnDeviceInfo(deviceIPAddress, clientResponseSIR.AccountID);
						return clientResponseSIR.GetResponseBack();
				}
			}

			// ** Default path of execution.
		    return oResponse != null
			           ? oResponse.GetResponseBack()
			           : string.Empty;
	    }

	    #endregion HandleRequestSentence and ReadSentence

	    public string ExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo = null, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs = null)
	    {
		    return HandleRequestSentence(sentence, remoteEndPoint, returnDeviceInfo, dispatchToCs, ReadSentence);
	    }
	}
}
