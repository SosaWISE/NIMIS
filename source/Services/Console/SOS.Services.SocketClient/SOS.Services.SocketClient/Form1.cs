using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SOS.Services.SocketClient
{
	/// <summary>
	/// http://csharp.net-informations.com/communications/csharp-client-socket.htm
	/// </summary>
	public partial class Form1 : Form
	{
		private readonly TcpClient _clientSocket = new TcpClient();

		public Form1()
		{
			InitializeComponent();
		}

		#region Member Functions

		#region Private 
		private void Form1Load(object sender, EventArgs e)
		{
			msg("Client Started");
			_clientSocket.Connect("192.168.1.110", 5138);
			label1.Text = @"Client Socket Program - Server Connected ...";
		}

		private void Button1Click(object sender, EventArgs e)
		{
			NetworkStream serverStream = _clientSocket.GetStream();
			byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Message from Client$");
			serverStream.Write(outStream, 0, outStream.Length);
			serverStream.Flush();

			byte[] inStream = new byte[10025];
			serverStream.Read(inStream, 0, (int)_clientSocket.ReceiveBufferSize);
			string returndata = System.Text.Encoding.ASCII.GetString(inStream);
			msg("Data from Server : " + returndata);

		}

		private void Form1FormClosing(object sender, FormClosingEventArgs e)
		{
			_clientSocket.Close();
		}

		#endregion Private 

		#region Public
		public void msg (string mesg)
		{
			textBox1.Text = textBox1.Text + Environment.NewLine + " >> " + mesg;
		}

		#endregion Public

		#endregion Member Functions
	}
}
