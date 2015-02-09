using System.Net;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsDeviceInfo
	{
		long? UnitID { get; set; }
		long? AccountID { get; set; }
		IPAddress UnitIPAddress { get; set; }
	}
}