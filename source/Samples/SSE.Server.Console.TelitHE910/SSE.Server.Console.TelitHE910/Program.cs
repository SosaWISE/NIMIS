using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace SSE.Server.Console.TelitHE910
{
	internal class Program
	{
		#region Properties
		private static TcpListener _listener;
		private const int _LIMIT = 5; //5 concurrent clients
		//private const int _PORTNUMBER = 2055;
		private const int _PORTNUMBER = 5138;

		private static int _pauseBetweenCmds;

		private static string[] _bootLines;

		#endregion Properties

		#region MAIN
		static void Main()
		{
			/** Initialization */
			_initilizeMain();
			_initializeSockets();
			_initializeBootStrap();
		}
		#endregion MAIN

		#region Methods

		#region Private

		private static void _initilizeMain()
		{
			string environment = ConfigurationManager.AppSettings["Environment"] ?? Environment.MachineName;
			ConfigurationSettings.Current.SetProperties("Preferences", environment);
		}

		private static void _initializeBootStrap()
		{
			/** Initialize. */
			var path = ConfigurationSettings.Current.GetConfig("TELIT.HE910-G.BootStrapFile");
			_bootLines = File.ReadAllLines(path);
			_pauseBetweenCmds = 1000 * Convert.ToInt32(ConfigurationSettings.Current.GetConfig("TELIT.HE910-G.PauseBetweenCmds"));

#if LOG
			System.Console.WriteLine("This is what I found: ");
			var counter = 0;
			foreach (var bootLine in _bootLines)
			{
				counter++;
				System.Console.WriteLine("{0,3}: {1}", counter, bootLine);
			}
#endif
		}

		private static void _initializeSockets()
		{
			/** Initialize program */
#pragma warning disable 612,618
			_listener = new TcpListener(_PORTNUMBER);
#pragma warning restore 612,618
			_listener.Start();

			System.Console.WriteLine(@"Server mounted on port '{0}'.", _PORTNUMBER);


			for (int i = 0; i < _LIMIT; i++)
			{
				var t = new Thread(Service);
				t.Start();
			}


		}

		#endregion Private

		#region Public
		public static void Service()
		{
			while (true)
			{
				Socket soc = _listener.AcceptSocket();

#if LOG
				System.Console.WriteLine("Connected: {0}", soc.RemoteEndPoint);
#endif
				try
				{
					Stream s = new NetworkStream(soc);
					var sr = new StreamReader(s);
					var sw = new StreamWriter(s) { AutoFlush = true };

					System.Console.WriteLine("-->: {0}", _bootLines[0]);
					sw.WriteLine(_bootLines[0]);
#if LOG
					System.Console.WriteLine("Waiting {0} seconds.", _pauseBetweenCmds/1000);
#endif
					Thread.Sleep(_pauseBetweenCmds); // Wait 10 seconds

					string clientRequest = sr.ReadLine();
					System.Console.WriteLine("<--: {0}", clientRequest);

					for (var i = 1; i < _bootLines.Length; i++ )
					{
						/** RESPOND. */
						System.Console.WriteLine("-->: {0}", _bootLines[i]);
						sw.WriteLine(_bootLines[i]);
#if LOG
						System.Console.WriteLine("Waiting {0} seconds.", _pauseBetweenCmds / 1000);
#endif
						Thread.Sleep(_pauseBetweenCmds); // Wait 10 seconds

						/** Read REQUEST. */
						clientRequest = sr.ReadLine();
						System.Console.WriteLine("<--: {0}", clientRequest);
					}
				}
				catch (Exception e)
				{
#if LOG
					System.Console.WriteLine(e.Message);
#endif
				}
#if LOG
				System.Console.WriteLine("Disconnected: {0}", soc.RemoteEndPoint);
#endif

				soc.Close();
			}
		}
		#endregion Public

		#endregion Methods

	}
}
