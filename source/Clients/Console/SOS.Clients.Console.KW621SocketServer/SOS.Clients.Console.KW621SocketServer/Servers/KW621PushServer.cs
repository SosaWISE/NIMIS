using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using SOS.Clients.Console.KW621SocketServer.ClientListeners;
using SOS.Data.Logging;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking.KW621;

namespace SOS.Clients.Console.KW621SocketServer.Servers
{
	public class KW621PushServer : PushServerBase
	{
		#region .ctor

		public KW621PushServer(Hashtable clientsList)
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
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IKW621DeviceServices>();

			while(true)
			{
				/** Check the queue. */
				IFnsResult<List<IFnsKwRequest>> queueItemResult = oService.QueueItemsGet(AttemptNumberPerCmd);

				/** Check that the call was successfull. */
				if (queueItemResult.Code == (int)ErrorCodes.Success)
				{
					/** Loop through each request items. */
					foreach (IFnsKwRequest fnsLpRequest in (List<IFnsKwRequest>)queueItemResult.GetValue())
					{
						/** Initialize. */
						KW621GpsTracker client = FindBraceletLocator(fnsLpRequest.UnitID);
						if (client == null) continue;

						using (Stream s = new NetworkStream(client.ClientSocket))
						{
							/** Locals. */
							var sw = new StreamWriter(s) { AutoFlush = true };

							/** Increment Attempt. */
							oService.QueueItemIncrementAttempt(fnsLpRequest.RequestID, 1);

							try
							{
								/** Send message. */
								sw.Write("{0}", fnsLpRequest.Sentence);
								//sw.Write(char.ConvertFromUtf32(13)); //Carage Return

								/** Flag attempt as successfull. */
								oService.QueueItemAttemptSuccessfull(fnsLpRequest.RequestID);
								DBErrorManager.Instance.AddSuccessMessage(
									string.Format("**** UnitID: {0} | ThID: {1} | RID: {2}", fnsLpRequest.UnitID, client.ThreadNumberID
										, fnsLpRequest.RequestID)
									, string.Format("*->{0}", fnsLpRequest.Sentence)); 
							}
							catch (Exception ex)
							{
								DBErrorManager.Instance.AddCriticalMessage(ex, "**** Exception on Request"
									, string.Format("Exception on RequestID: {0} | UnitID: {1} | Message: {2}", fnsLpRequest.RequestID
										, client.UnitID
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

		private KW621GpsTracker FindBraceletLocator(long lUnitID)
		{
			/** Initialize. */
			KW621GpsTracker result = null;

			/** Loop through until found. */
			foreach (DictionaryEntry dictionaryEntry in _clientsList)
			{
				result = (KW621GpsTracker)dictionaryEntry.Value;
				/** Check to see if we have a hit. */
				if (result.UnitID == lUnitID) break;

				/** reset result. */
				result = null;
			}

			/** Return result. */
			return result;
		}

		#endregion Member Functions
	}
}
