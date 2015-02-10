using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace SOS.Clients.Console.SocketServer
{
	class EmployeeTCPServer
	{
		static TcpListener _listener;
		const int _LIMIT = 5; //5 concurrent clients
		//private const int _PORTNUMBER = 2055;
		private const int _PORTNUMBER = 5138;

		public static void Main()
		{
#pragma warning disable 612,618
			_listener = new TcpListener(_PORTNUMBER);
#pragma warning restore 612,618
			_listener.Start();
#if LOG
            System.Console.WriteLine(@"Server mounted, 
                            listening to port {0}", _PORTNUMBER);
#endif
			for (int i = 0; i < _LIMIT; i++)
			{
				var t = new Thread(Service);
				t.Start();
			}
		}
		public static void Service()
		{
			while (true)
			{
				Socket soc = _listener.AcceptSocket();
				//soc.SetSocketOption(SocketOptionLevel.Socket,
				//        SocketOptionName.ReceiveTimeout,10000);
#if LOG
				System.Console.WriteLine("Connected: {0}", 
                                         soc.RemoteEndPoint);
#endif
				try
				{
					Stream s = new NetworkStream(soc);
					var sr = new StreamReader(s);
					var sw = new StreamWriter(s);

					sw.AutoFlush = true; // enable automatic flushing
					sw.WriteLine("{0} Employees available",
						  ConfigurationSettings.AppSettings.Count);
					while (true)
					{
						string name = sr.ReadLine();
						if (name == "" || name == null) break;
						string job =
							ConfigurationSettings.AppSettings[name];
						if (job == null) job = "No such employee";
						sw.WriteLine(job);
					}
					s.Close();
				}
				catch (Exception e)
				{
#if LOG
					System.Console.WriteLine(e.Message);
#endif
				}
#if LOG
				System.Console.WriteLine("Disconnected: {0}", 
                                        soc.RemoteEndPoint);
#endif
				soc.Close();
			}
		}
	}
}
