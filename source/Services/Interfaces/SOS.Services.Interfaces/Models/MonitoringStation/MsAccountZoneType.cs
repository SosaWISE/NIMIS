using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountZoneType : IMsAccountZoneType
	{
		public string AccountZoneTypeID { get; set; }
		public string AccountZoneType { get; set; }
	}

	public interface IMsAccountZoneType
	{
		[DataMember]
		string AccountZoneTypeID { get; set; }

		[DataMember]
		string AccountZoneType { get; set; }
	}
}
