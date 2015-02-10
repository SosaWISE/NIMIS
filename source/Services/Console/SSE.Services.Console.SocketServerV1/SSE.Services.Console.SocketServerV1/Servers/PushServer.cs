using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using SOS.Data.Logging;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using SSE.Services.Console.SocketServerV1.ClientListeners;

namespace SSE.Services.Console.SocketServerV1.Servers
{
	public class PushServer : PushServerBase
	{
		#region .ctor

		public PushServer(Hashtable clientsList)
		{
			_clientsList = clientsList;
		}

		#endregion .ctor

		#region Member Variables

		public bool TerminateServer { get; set; }
		private readonly Hashtable _clientsList;

		#endregion Member Variables

		#region Member Functions

		public void ServerEntryPoint()
		{
			/** Initialize. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<ISSEGpsDeviceServices>();

			while (true)
			{
				/** Check the queue. */
				IFnsResult<List<IFnsSsDeviceRequest>> queueItemResult = oService.QueueItemGet(AttemptNumberPerCmd);
 
				/** Check that the call was successfull. */
				if (queueItemResult.Code == (int) ErrorCodes.Success)
				{
					/** Loop through each request items. */
					foreach (IFnsSsDeviceRequest fnsSsDeviceRequest in (List<IFnsSsDeviceRequest>)queueItemResult.GetValue())
					{
						/** Init and find. */
						DeviceLocator device = FindDeviceLocator(fnsSsDeviceRequest.AccountId);
						if (device == null) continue;


						/** Create a socket and send to the device. */
						using (Stream s = new NetworkStream(device.ClientSocket))
						{
							/** Init. */
							var sw = new StreamWriter(s) { AutoFlush = true };

							/** Increment attempts. */
							oService.QueueItemIncrementAttempt(fnsSsDeviceRequest.DeviceRequestID, 1);

							try
							{
								/** Send the message. */
								sw.WriteLine(fnsSsDeviceRequest.Sentence);

								/** Flag attempt was successfull. */
								oService.QueueItemAttemptSuccessfull(fnsSsDeviceRequest.DeviceRequestID);
								DBErrorManager.Instance.AddSuccessMessage(
									string.Format("**** UnitID: {0} | ThID: {1} | RID: {2}", fnsSsDeviceRequest.AccountId, device.ThreadNumberID
										, fnsSsDeviceRequest.DeviceRequestID)
									, string.Format("*->{0}", fnsSsDeviceRequest.Sentence));
							}
							catch (Exception ex)
							{
								DBErrorManager.Instance.AddCriticalMessage(ex, "**** Exception on Request"
								                                           ,
								                                           string.Format(
									                                           "Exception on RequestID: {0} | UnitID: {1} | Message: {2}",
									                                           fnsSsDeviceRequest.DeviceRequestID
									                                           , device.AccountID
									                                           , ex.Message));
							}
							finally
							{
								sw.Close();
							}
						}
					}
				}

				/** Check to see if we are terminating the server. */
				if (TerminateServer) break;

				/** Sleep the thread for waiting. */
				Thread.Sleep(PausePeriodInSeconds * 1000);
			}
		}

		private DeviceLocator FindDeviceLocator(long accountId)
		{
			/** Initialize. */
			DeviceLocator result = null;

			/** Loop through unitl found. */
			foreach (DictionaryEntry dicEntry in _clientsList)
			{
				result = (DeviceLocator) dicEntry.Value;

				/** Check that we have a hit. */
				if (result.AccountID == accountId) break;

				/** Reset result. */
				result = null;
			}

			/** Return result. */
			return result;
		}

		#endregion Member Functions
	}
}
