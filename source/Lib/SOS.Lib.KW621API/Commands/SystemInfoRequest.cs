using System.Net;

namespace SOS.Lib.KW621API.Commands
{
	public class SystemInfoRequest : Requests
	{
		#region .ctor
		public SystemInfoRequest(EndPoint remoteEndPoint) : base(DEFAULT_BLANK_PASSWRD)
		{
			IPAddress = ((IPEndPoint) (remoteEndPoint)).Address.ToString();
			Port = ((IPEndPoint) (remoteEndPoint)).Port;
		}

		#endregion .ctor

		#region Memeber Properties

		protected string IPAddress { get; private set; }
		protected int Port { get; private set; }

		#endregion Memeber Properties

		#region Member Functions
		#endregion Member Functions

		#region System Information

		public string GetRequest()
		{
			//TODO:  REMOVE THIS ONCE WE ARE DONE.
			/** return "GetRequest is not implemented"; */

			//return "vilmar\r12345";
			return "AT+CIMI\r";
			//return "TESTUSER\r\nTESTPASSW";
			//return "AT#MONI=7";
		}

		#endregion System Information
	}
}
