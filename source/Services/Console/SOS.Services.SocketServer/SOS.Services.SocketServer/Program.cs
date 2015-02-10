using System;
using System.Net.Sockets;
using System.Text;

namespace SOS.Services.SocketServer
{
	/// <summary>
	/// http://csharp.net-informations.com/communications/csharp-server-socket.htm
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			var serverSocket = new TcpListener(5138);
			int requestCount = 0;
			TcpClient clientSocket = default(TcpClient);
			serverSocket.Start();
			Console.WriteLine(" >> Server Started");
			clientSocket = serverSocket.AcceptTcpClient();
			Console.WriteLine(" >> Accept connection from client");
			requestCount = 0;

			while ((true))
			{
				try
				{
					/** Check client socket. */
					if (!clientSocket.Connected) break;
					/** Initialize. */
					requestCount = requestCount + 1;
					NetworkStream networkStream = clientSocket.GetStream();
					byte[] bytesFrom = new byte[10025];

					/** Read stream. */
					networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
					string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
					dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("\0"));

					/** Display on console message. */
					Console.WriteLine(" >> Data from client - " + dataFromClient);
					string serverResponse = "Server response " + Convert.ToString(requestCount);
					Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);

					/** Send message back */
					networkStream.Write(sendBytes, 0, sendBytes.Length);
					networkStream.Flush();
					Console.WriteLine(" >> " + serverResponse);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}

			clientSocket.Close();
			serverSocket.Stop();
			Console.WriteLine(" >> exit");
			Console.ReadLine();
		}
	}
}
