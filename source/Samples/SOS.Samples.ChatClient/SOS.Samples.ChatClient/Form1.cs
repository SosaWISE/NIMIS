using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace SOS.Samples.ChatClient
{
	public partial class Form1 : Form
	{
		private readonly TcpClient _clientSocket = new TcpClient();
		private NetworkStream _serverStream = default(NetworkStream);
		private string _readData;

		public Form1()
		{
			InitializeComponent();
		}

		private void SendMessageBtnClick(object sender, EventArgs e)
		{
			byte[] outStream = System.Text.Encoding.ASCII.GetBytes(messageToSendTb.Text + "$");
			_serverStream.Write(outStream, 0, outStream.Length);
			_serverStream.Flush();
		}

		private void ConnectToServerBtnClick(object sender, EventArgs e)
		{
			_readData = "Conected to Chat Server ...";
			OutputMessage();
			_clientSocket.Connect("127.0.0.1", 8888);
			_serverStream = _clientSocket.GetStream();

			byte[] outStream = System.Text.Encoding.ASCII.GetBytes(chatNameTb.Text + "$");
			_serverStream.Write(outStream, 0, outStream.Length);
			_serverStream.Flush();

			var ctThread = new Thread(ReadMessage);
			ctThread.Start();
		}

		private void ReadMessage()
		{
			while (true)
			{
				_serverStream = _clientSocket.GetStream();
				var inStream = new byte[10025];
				int buffSize = _clientSocket.ReceiveBufferSize;
				_serverStream.Read(inStream, 0, buffSize);
				string returndata = System.Text.Encoding.ASCII.GetString(inStream);
				_readData = "" + returndata;
				OutputMessage();
			}
// ReSharper disable FunctionNeverReturns
		}
// ReSharper restore FunctionNeverReturns

		private void OutputMessage()
		{
			if (InvokeRequired)
				Invoke(new MethodInvoker(OutputMessage));
			else
				outputTb.Text = string.Format("{0}{1} >> {2}", outputTb.Text, Environment.NewLine, _readData);
		}
	}
}
