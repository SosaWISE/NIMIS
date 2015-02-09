using System.Net;

namespace SSE.Lib.SseGpsDeviceAPI.Models
{
	public class DeviceInfo
	{
		#region Member Variables

		public long? AccountID { get; set; }
		public IPAddress UnitIPAddress { get; set; }
		public string IMEI { get; set; }
		public string SIMNumber { get; set; }

		#endregion Member Variables
	}
}
