using System;
using System.Net;
using SOS.Data.Logging;
using SOS.Lib.KW621API.Commands;
using SOS.Lib.KW621API.Models;

namespace SOS.Lib.KW621API
{
	public class ClientApi
	{
		#region Singleton .ctor
		private ClientApi() {}

		private static volatile ClientApi _instance;
		private static readonly object _instanceSync = new object();
		public static ClientApi Instance
		{
			get
			{
				if (_instance == null)
				{
					lock(_instanceSync)
					{
						if (_instance == null)
						{
							_instance = new ClientApi();
						}
					}
				}

				/** Return instance. */
				return _instance;
			}
		}

		#endregion Singleton .ctor

		#region Member Functions

		public string ExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo = null, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs = null)
		{
			return HandleRequestSentence(sentence, remoteEndPoint, returnDeviceInfo, dispatchToCs, ReadSentence);
		}

		private string ReadSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo = null, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs = null)
		{
			/** Initialize. */
			var oResponse = (IResponse)Response.Factory(sentence, remoteEndPoint, dispatchToCs);
			IPAddress unitIPAddress = ((IPEndPoint)(remoteEndPoint)).Address;

			/** Check that a response has been found. */
			if (oResponse != null)
			{
				switch (oResponse.CommandName)
				{
					case CommandDef.HE910:
						if (returnDeviceInfo != null) returnDeviceInfo(unitIPAddress, (int) CommandDef.HE910);
						return "OK";
				}
			}

			/** Default path of execution */
			return oResponse != null
			       	? oResponse.GetResponseBack()
			       	: string.Empty;
		}

		public string SendSystemInfoRequest(EndPoint remoteEndPoint)
		{
			/** Validate Client Table. */
			DBErrorManager.Instance.AddSuccessMessage("****Sending Initial Handshake to Device"
				, string.Format("Remote End Point: {0}", remoteEndPoint));

			/** Return request. */
			var systemInfoRequest = new SystemInfoRequest(remoteEndPoint);
			var responseString = systemInfoRequest.GetRequest();

			/** Display Message to output device. */
			DBErrorManager.Instance.AddSuccessMessage("**** ->System Info Request:", string.Format("-->{0}", responseString));

			/** Return string. */
			return responseString;
		}

		#endregion Member Functions

		#region HandleRequestSentence and ReadSentence

		public T HandleRequestSentence<T>(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs, Func<string, EndPoint, Func<IPAddress, long?, DeviceInfo>, Func<string, decimal, decimal, string, string, bool?, bool>, T> action)
		{
			try
			{
				return action(sentence, remoteEndPoint, returnDeviceInfo, dispatchToCs);
			}
			catch (Exception oEx)
			{
				DBErrorManager.Instance.AddCriticalMessage(oEx
					, "Critical Error"
					, string.Format("Exception thrown in HandleRequestSentence on {0}"
						, action));
				throw;
			}
			/** Do some type of checksum here. */
		}

		#endregion HandleRequestSentence and ReadSentence
	}
}
