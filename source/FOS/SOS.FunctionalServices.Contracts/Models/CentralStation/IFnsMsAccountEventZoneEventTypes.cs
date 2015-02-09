using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountEventZoneEventTypes
	{
		#region Properties

		[DataMember]
		int EquipmentTypesZoneEventTypeID { get; }

		[DataMember]
		int EquipmentTypeID { get; }

		[DataMember]
		int AccountEventId { get; }

		[DataMember]
		string MonitoringStationOSID { get; }

		[DataMember]
		int SortOrder { get; }

		[DataMember]
		int? MoniEventID { get; }

		[DataMember]
		int? AGEventID { get; }

		[DataMember]
		int? ZoneEventTypeId { get; }

		[DataMember]
		int EventID { get; }

		[DataMember]
		string ServtypeID { get; }

		[DataMember]
		string Description { get; }

		#endregion Properties
	}
}