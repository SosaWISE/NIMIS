using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SOS.Data.Logging;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using SOS.Lib.Util.Configuration;

namespace SOS.Clients.Console.LaipacSocketServer.ClientListeners
{
	public class S911BraceletLocator : ClientListenersBase
	{
		#region .ctor

		public S911BraceletLocator(TcpListener inServerListener, int threadNumberID)
		{
			_serverSocket = inServerListener;
			ThreadNumberID = threadNumberID;
		}

		#endregion .ctor

		#region Member Properties

		private static TcpListener _serverSocket;
		public int ThreadNumberID { get; private set; }
		private const int _NULL_COUNT = 3;
		private const int _CR_COUNT = 2;

		public long? UnitID { get; private set; }
		public IPAddress UnitIPAddress { get; private set; }
		public Socket ClientSocket { get; private set; }

		private class FnsDeviceInfo : IFnsDeviceInfo
		{
			public long? UnitID { get; set; }
			public long? AccountID { get; set; }
			public IPAddress UnitIPAddress { get; set; }
		}

		protected int CrCount
		{
			get
			{
				int count;

				if (!int.TryParse(ConfigurationSettings.Current.GetConfig("LAIPACAPI.MaxCR_InSentence"), out count))
				{
					count = _CR_COUNT;  // Set default value.
				}

				/** Return result. */
				return count;
			}
		}

		#endregion Member Properties

		#region Member Functions

		public void ServiceEntryPoint()
		{
			/** Initialize. */
			while (true)
			{
				/** Accept socket. */
				int managedThreadId = Thread.CurrentThread.ManagedThreadId;
				//Socket clientSocket = _serverSocket.AcceptSocket();
				ClientSocket = _serverSocket.AcceptSocket();
				EndPoint sRemoteEndPoint = ClientSocket.RemoteEndPoint;
				DBErrorManager.Instance.AddSuccessMessage("*** Socket Connection"
					, string.Format("Connected RemoteEndPoint: {0} | ThreadID: {1}", sRemoteEndPoint, managedThreadId));

				#region BEGIN TRY
				try
				{
					/** Initialize. */
					int nullCount = 0;
					Stream s = new NetworkStream(ClientSocket);
					var sr = new StreamReader(s);
					var sw = new StreamWriter(s) { AutoFlush = true /* enable automatic flushing */};
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<ILaipacDeviceServices>();
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
					sw.WriteLine(sResponse);

					/** Wait for a response. */
					while (true)
					{
						/** Read and check. */
						string sRequest = null;
						//while(!sr.EndOfStream)
						//{
						//    sRequest += (char)sr.Read();
						//}
						sRequest = sr.ReadLine();
						if (string.IsNullOrEmpty(sRequest))
						{
							nullCount++;
							if (nullCount > _NULL_COUNT) break;
							continue;
						}
						/** Reset the nullCount. */
						nullCount = 0;

						int crCount = 0;
						/** Check for valid sentence [0-9a-fA-F].  Makes sure that the sentence is all there. We are going to assume that there is x number of carrage returns on a sentence handled by the app.config setting*/
						while (!System.Text.RegularExpressions.Regex.IsMatch(sRequest, "\\*[0-9a-fA-F][0-9a-fA-F]$"))
						{
							/** Add to count. */
							crCount++;

							/** Execute a second readline. */
							var sPart = sr.ReadLine(); 
							/** Check that this is the 2nd half of the sentence and not an entire sentence.  If entire sentence then use it and continue. */
							if (!string.IsNullOrWhiteSpace(sPart) && System.Text.RegularExpressions.Regex.IsMatch(sPart, @"^\$[\S+ ]*\*[0-9a-fA-F][0-9a-fA-F]$"))
							{
								DBErrorManager.Instance.AddWarningMessage("**** Unable to complete sentence ", string.Format("<->{0}", sRequest));
								sRequest = sPart;
								break;
							}

						}


						/** Perform tasks. */
						DBErrorManager.Instance.AddSuccessMessage("**** <- Received Request from Device"
							, string.Format("<--{0}", sRequest));

						var oResult = oService.ExecuteSentence(sRequest, sRemoteEndPoint, fnReturnDeviceInfo);
						if (oResult.Code == 0 && oResult.GetValue() != null) // Success
						{
							sResponse = oResult.GetValue().ToString();

							if (!string.IsNullOrWhiteSpace(sResponse))
							{
								DBErrorManager.Instance.AddSuccessMessage("**** ->Sending to Response Device"
									, string.Format("-->{0}", sResponse));
								sw.WriteLine(sResponse);
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

				/** Disconnect from remote end point. */
				ClientSocket.Close();
				UnitID = null;
				UnitIPAddress = null;
				DBErrorManager.Instance.AddSuccessMessage("*** Socket Disconnected"
					, string.Format("Disconnected RemoteEndPoint: {0} | ThreadID: {1}", sRemoteEndPoint, managedThreadId));
			}
		}

		private string HandleRequests(EndPoint remoteEndPoint, Func<EndPoint, IFnsResult<string>> action)
		{
			if (action != null)
			{
				var result = action(remoteEndPoint);
				if (result.Code == 0) return (string)result.GetValue();

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
