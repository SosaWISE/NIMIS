using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountEventZoneEventTypes : IFnsMsAccountEventZoneEventTypes
	{
		#region .ctor

		public FnsMsAccountEventZoneEventTypes(MS_AccountEventView item)
		{
			EquipmentTypesZoneEventTypeID = item.EquipmentTypesZoneEventTypeID;
			EquipmentTypeID = item.EquipmentTypeID;
			AccountEventId = item.AccountEventId;
			MonitoringStationOSID = item.MonitoringStationOSID;
			SortOrder = item.SortOrder;
			MoniEventID = item.MoniEventID;
			AGEventID = item.AGEventID;
			ZoneEventTypeId = item.ZoneEventTypeId;
			EventID = item.event_id;
			ServtypeID = item.servtype_id;
			Description = item.Description;
		}

		#endregion .ctor

		#region Properties

		public int EquipmentTypesZoneEventTypeID { get; private set; }

		public int EquipmentTypeID { get; private set; }

		public int AccountEventId { get; private set; }

		public string MonitoringStationOSID { get; private set; }

		public int SortOrder { get; private set; }

		public int? MoniEventID { get; private set; }

		public int? AGEventID { get; private set; }

		public int? ZoneEventTypeId { get; private set; }

		public int EventID { get; private set; }

		public string ServtypeID { get; private set; }

		public string Description { get; private set; }

		#endregion Properties
	}
}
