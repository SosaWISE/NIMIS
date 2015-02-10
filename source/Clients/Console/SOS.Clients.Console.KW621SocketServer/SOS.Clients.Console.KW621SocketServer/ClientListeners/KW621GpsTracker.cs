using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SOS.Data.Logging;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.Clients.Console.KW621SocketServer.ClientListeners
{
	public class KW621GpsTracker : ClientListenersBase
	{
		#region .ctor
		public KW621GpsTracker(TcpListener inServerListener, int threadNumberID)
		{
			_serverSocket = inServerListener;
			threadNumberID = threadNumberID;
		}

		#endregion .ctor

		#region Member Properties

		private static TcpListener _serverSocket;
		public int ThreadNumberID { get; private set; }
		private const int _NULL_COUNT = 3;

		public long? UnitID { get; private set; }
		public IPAddress UnitIPAddress { get; private set; }
		public Socket ClientSocket { get; private set; }

		private class FnsDeviceInfo : IFnsDeviceInfo
		{
			public long? UnitID { get; set; }
			public IPAddress UnitIPAddress { get; set; }
		}

		#endregion Member Properties

		#region Member Functions

		public void ServiceEntryPoint()
		{
			/** Initialize. */
			while(true)
			{
				/** Accept socket. */
				int managedThreadId = Thread.CurrentThread.ManagedThreadId;
				ClientSocket = _serverSocket.AcceptSocket();
				EndPoint sRemoteEndPoint = ClientSocket.RemoteEndPoint;
				DBErrorManager.Instance.AddSuccessMessage("*** Socket Connection"
					, string.Format("Connected RemoteEndPoint: {0} | ThreadID: {1}", sRemoteEndPoint, managedThreadId));

				#region BEGIN TRY
				try
				{
					/** Initialize. */
					int nullCount = 0;
					var s = new NetworkStream(ClientSocket);
					var sr = new StreamReader(s);
					var sw = new StreamWriter(s) {AutoFlush = false /* enable automatic flushing */};
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IKW621DeviceServices>();
					/** Initialize Return Device Info Func. */
					Func<IPAddress, long?, IFnsDeviceInfo> fnReturnDeviceInfo = (oUnitIPAddress, lUnitID) =>
					{
						/** Initialize. */
						var oResult = new FnsDeviceInfo {UnitIPAddress = oUnitIPAddress, UnitID = lUnitID};

						/** Get values. */
						UnitID = lUnitID;
						UnitIPAddress = oUnitIPAddress;

						/** Return result */
						return oResult;
					};

					/** Send 1st request to device. */
					string sResponse = HandleRequests(sRemoteEndPoint, oService.SendRequestSystemInfo);
					if (string.IsNullOrWhiteSpace(sResponse)) { ClientSocket.Close(); break; }
					/** TODO:  Testing the ability of the HE910 to accept the <CR><LF> by forcing it. */
					sw.Write(sResponse);
					//sw.Write("vilmar{0}12345{1}", char.ConvertFromUtf32(13), char.ConvertFromUtf32(13));
					sw.Flush();
					//sw.Write(char.ConvertFromUtf32(13)); //Carage Return

					/** TODO:  Remove this hack.  I need to force this connection to the HE910. */
					UnitID = 910;

					/** Wait for a response. */
					while (true)
					{
						/** Read and check if we lost connection. */
						string sRequest = sr.ReadLine();
						if (string.IsNullOrEmpty(sRequest))
						{
							nullCount++;
							if (nullCount > _NULL_COUNT) break;
							continue;
						}
						/** Rest the nullCount. */
						nullCount = 0;

						/** Perform tasks. */
						DBErrorManager.Instance.AddSuccessMessage("**** <- Received Request from Device"
							, string.Format("<--{0}", sRequest));

						var oResult = oService.ExecuteSentence(sRequest, sRemoteEndPoint, fnReturnDeviceInfo);
						if (oResult.Code == 0 && oResult.GetValue() != null) // Sucess
						{
							sResponse = oResult.GetValue().ToString();

							if (!string.IsNullOrWhiteSpace(sResponse))
							{
								DBErrorManager.Instance.AddSuccessMessage("**** ->Sending to Response Device"
									, string.Format("-->{0}", sResponse));
								sw.Write("{0}", sResponse);
								//sw.Write(char.ConvertFromUtf32(13)); //Carage Return
							}
						}
					}
				}
				#endregion END TRY
				#region BEGIN CATCH
				catch (Exception oEx)
				{
					DBErrorManager.Instance.AddCriticalMessage(oEx, "*** Exception on ServiceEntryPoint"
						, string.Format("The following error was thrown during socket read: {0} | ThreadID: {1}", oEx.Message, managedThreadId));
				}
				#endregion END CATCH
			}
		}

		private string HandleRequests(EndPoint remoteEndPoint, Func<EndPoint, IFnsResult<string>> action)
		{
			if (action != null)
			{
				var result = action(remoteEndPoint);
				if (result.Code == 0) return (string) result.GetValue();

				DBErrorManager.Instance.AddWarningMessage(
					string.Format("****Action {0} returned error", action)
					, string.Format("Code: {0,3} | Message: {1}", result.Code, result.Message));
			}

			/** Default path of execution. */
			return null;
		}

		#endregion Member Functions
	}
}