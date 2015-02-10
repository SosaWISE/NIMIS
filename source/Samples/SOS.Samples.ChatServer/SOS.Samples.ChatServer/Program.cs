using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections;

namespace SOS.Samples.ChatServer
{
	class Program
	{
		public static Hashtable ClientsList = new Hashtable();

		static void Main(/**string[] args*/)
		{
			// ReSharper disable CSharpWarnings::CS0612
			var serverSocketListener = new TcpListener(8888);
			// ReSharper restore CSharpWarnings::CS0612
			TcpClient clientSocket = default(TcpClient);

			try
			{
				serverSocketListener.Start();
				Console.WriteLine("Chat Server Started ....");
				while ((true))
				{
					clientSocket = serverSocketListener.AcceptTcpClient();

					var bytesFrom = new byte[10025];

					NetworkStream networkStream = clientSocket.GetStream();
					/* Reads<-- */
					networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
					string sClientName = Encoding.ASCII.GetString(bytesFrom);
					sClientName = sClientName.Substring(0, sClientName.IndexOf("$", StringComparison.Ordinal));

					ClientsList.Add(sClientName, clientSocket);

					Broadcast(sClientName + " Joined ", sClientName, false);

					Console.WriteLine(sClientName + " Joined chat room ");
					var client = new HandleClient();
					client.StartClient(clientSocket, sClientName, ClientsList);
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown: {0}", ex.Message);
			}
			finally
			{
				if (clientSocket != null)
				{
					clientSocket.Close();
					serverSocketListener.Stop();
				}
				Console.WriteLine("exit");
				Console.ReadLine();
			}
			// ReSharper disable FunctionNeverReturns
		}
		// ReSharper restore FunctionNeverReturns

		public static void Broadcast(string msg, string uName, bool flag)
		{
			foreach (DictionaryEntry item in ClientsList)
			{
				var broadcastSocket = (TcpClient)item.Value;
				NetworkStream broadcastStream = broadcastSocket.GetStream();
				Byte[] broadcastBytes = flag
					? Encoding.ASCII.GetBytes(uName + " says : " + msg)
					: Encoding.ASCII.GetBytes(msg);

				broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
				broadcastStream.Flush();
			}
		}  //end broadcast function
	}//end Main class


	public class HandleClient
	{
		TcpClient _clientSocket;
		string _clNo;
		/**Hashtable _clientsList;*/

		public void StartClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
		{
			_clientSocket = inClientSocket;
			_clNo = clineNo;
			//_clientsList = cList;
			var ctThread = new Thread(DoChat);
			ctThread.Start();
		}

		private void DoChat()
		{
			var bytesFrom = new byte[10025];
			//Byte[] sendBytes = null;
			//string serverResponse = null;
			//string rCount = null;
			int requestCount = 0;

			while ((true))
			{
				try
				{
					requestCount = requestCount + 1;
					NetworkStream networkStream = _clientSocket.GetStream();
					networkStream.Read(bytesFrom, 0, _clientSocket.ReceiveBufferSize);
					string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
					dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$", StringComparison.Ordinal));
					Console.WriteLine("From client - " + _clNo + " : " + dataFromClient);

					//rCount = Convert.ToString(requestCount);

					Program.Broadcast(dataFromClient, _clNo, true);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}//end while
			// ReSharper disable FunctionNeverReturns
		}//end doChat
		// ReSharper restore FunctionNeverReturns
	} //end class handleClient
}//end namespace