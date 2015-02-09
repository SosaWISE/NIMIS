using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountEventZoneEventTypes : IMsAccountEventZoneEventTypes
	{
		public int? ZoneEventTypeID { get; set; }
		public string MonitoringStationOSID { get; set; }
		public string Descrption { get; set; }
	}

	public interface IMsAccountEventZoneEventTypes
	{
		#region Properties

		[DataMember]
		int? ZoneEventTypeID { get; set; }

		[DataMember]
		string MonitoringStationOSID { get; set; }

		[DataMember]
		string Descrption { get; set; }

		#endregion Properties
	}

	public class MsAccountEventZoneEventTypeArgs : IMsAccountEventZoneEventTypeArgs
	{
		public string MonitoringStationId { get; set; }
		public int EquipmentTypeId { get; set; }
	}

	public interface IMsAccountEventZoneEventTypeArgs
	{
		[DataMember]
		string MonitoringStationId { get; set; }

		[DataMember]
		int EquipmentTypeId { get; set; }
	}
}
