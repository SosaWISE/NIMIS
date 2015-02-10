
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SOS.Data;
using SOS.Data.Logging;
using SOS.FunctionalServices;
using SOS.Lib.Core;
using SOS.Lib.Core.ErrorHandling;
using SSE.Services.Console.SocketServerV1.ClientListeners;
using SSE.Services.Console.SocketServerV1.Servers;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace SSE.Services.Console.SocketServerV1
{
	class Program
	{
		#region MAIN ENTRY POINT

		static void Main(string[] args)
		{
			/** 
			 * Initialize the Environment. */
			// ** Init arguments. */
			InitConsoleArguments(args);

			/** Set Environment. */
			InitEnvironment();

			// ** Display Start message. */
			DBErrorManager.Instance.AddSuccessMessage("START SS Configuration"
				, "Server starting up.");

			// ** Initialize Socket Server */
			InitSocketServer();

			// ** Display shutdown message. */
			DBErrorManager.Instance.AddSuccessMessage("  END SS Configuration"
				, "Server initialized.");

		}

		#endregion MAIN ENTRY POINT

		#region Member Properties

		private static int _maxNumberConnections = 5;
		private static bool _displayMessageConsoleOutput = true;

		private static DBErrorManager _errorManager;

		private static TcpListener _serverListener;
		private static readonly Hashtable _clientsList = new Hashtable();

		private static PushServer _pushServer;

		#endregion Member Properties

		#region Member Functions

		private static void InitConsoleArguments(IEnumerable<string> args)
		{
			/** Check that arguments have been passed. */
			if (args == null) return;

			/** Initialize arguments. */
			foreach (var sArg in args)
			{
				if (sArg.Equals("NO_OUTPUT"))
					_displayMessageConsoleOutput = false;
			}

			/** Get limit of connections. */
			int maxLimit;
			if (int.TryParse(SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("CSS_CONN_LIMIT"), null), out maxLimit))
			{
				_maxNumberConnections = maxLimit;
			}
			else
			{
				DBErrorManager.Instance.AddWarningMessage("* Missing Config Settings"
					, string.Format("The configuration setting \"CSS_CONN_LIMIT\" is either missing or invalid.  Connection limit is: {0}"
					, _maxNumberConnections));
			}
		}

		private static void InitEnvironment()
		{
			/** Load configuration. */
			string environment = ConfigurationManager.AppSettings["Environment"]
								 ?? Environment.MachineName;
			ConfigurationSettings.Current.SetProperties("Preferences", environment);

			// Setup SubSonic Connections
			SubSonicConfigHelper.SetupConnectionStrings();

			/** Initialize Fos Engine. */
			SosServiceEngine.Instance.Initialize();

			/** Initialize Error Manager. */
			_errorManager = new DBErrorManager(LogSource.SosClientsConsoleLaipacSocketServer, null);
			_errorManager.MessageAdded += OnErrorManagerMessageAdded;
			DBErrorManager.SetSingletonInstance(_errorManager);

		}

		private static void OnErrorManagerMessageAdded()
		{
			if (_errorManager.MessageCount > 0)
			{
				if (_displayMessageConsoleOutput)
				{
					foreach (var errorMessage in _errorManager.ErrorMessages)
					{
						if (errorMessage == null) continue;

						// ** Check to see if there is console output.
						System.Console.WriteLine("|*{0,-14}*|-{1,-50}:{2}", ErrorMessageTypeReadable.Get(errorMessage.Type), errorMessage.Header, errorMessage.Message);
					}
				}

				// ** Clear the messages from the cache
				_errorManager.ClearMessages();
			}
		}

		private static void InitSocketServer()
		{
			try
			{
				/** Initialize. */
				string sPortNmbr = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("CSS_PORT_NUMBR"), null);
				string sIPAdress = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("CSS_IP_ADDRESS"), null);
				IPAddress ipAddress;
				int portNumber;

				// ** Get IP Address. */
				if (!IPAddress.TryParse(sIPAdress, out ipAddress))
				{
					ipAddress = IPAddress.Any;
					DBErrorManager.Instance.AddWarningMessage("* Missing config Settings"
						, string.Format("The configuration setting \"CSS_IP_ADDRESS\" is either missing or invalid.  Listener is working on IP Address: {0}"
							, ipAddress));
				}

				// ** Get Port number. */
				if (!int.TryParse(sPortNmbr, out portNumber))
				{
					portNumber = 2055;
					DBErrorManager.Instance.AddWarningMessage("* Missing Config Settings"
						, string.Format("The configuration setting \"CSS_PORT_NUMBR\" is either missing or invalid.  Listener working on Port: {0}"
							, portNumber));
				}
				var localEP = new IPEndPoint(ipAddress, portNumber);

				// ** Initialize the listener. */
				_serverListener = new TcpListener(localEP);
				_serverListener.Start();
				DBErrorManager.Instance.AddSuccessMessage("* Listener Started"
					, string.Format("DateTime:{0}|IP:{1}|Port:{2}", DateTime.Now, ipAddress, portNumber));

				// ** Create thread entry points. */
				var sb = new StringBuilder();
				for (int threadNumberID= 0; threadNumberID < _maxNumberConnections; threadNumberID++)
				{
					var deviceLocator = new DeviceLocator(_serverListener, threadNumberID);
					_clientsList.Add(threadNumberID, deviceLocator);
					var t = new Thread(deviceLocator.ServiceEntryPoint);
					t.Start();
					sb.AppendFormat("\r\n|*              *|-**                                                :Started thread {0}", threadNumberID);
				}
				DBErrorManager.Instance.AddSuccessMessage("** Start Threads", string.Format("Number of Connections: {0}{1}", _maxNumberConnections, sb));

				/** Create the new push server. */
				DBErrorManager.Instance.AddSuccessMessage("** START Push Server Initialization", "");
				_pushServer = new PushServer(_clientsList);
				var pushServerThread = new Thread(_pushServer.ServerEntryPoint);
				pushServerThread.Start();
				DBErrorManager.Instance.AddSuccessMessage("**   END Push Server Initialization", "");
			}
			catch (Exception oEx)
			{
				DBErrorManager.Instance.AddCriticalMessage(oEx, "*Exception on InitSocketServer"
					, string.Format("The following error was thrown: {0}", oEx.Message));
			}

		}

		#endregion Member Functions
	}
}
