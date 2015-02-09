using System.Net;

namespace SOS.Lib.LaipacAPI.Models
{
	public class DeviceInfo
	{
		#region Member Variables

		public long? UnitID { get; set; }
		public IPAddress UnitIPAddress { get; set; }

		#endregion Member Variables
	}
}
