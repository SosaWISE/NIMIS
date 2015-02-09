using AR = SOS.Data.SosCrm.MS_EquipmentAccountZoneTypeEventsView;
using ARCollection = SOS.Data.SosCrm.MS_EquipmentAccountZoneTypeEventsViewCollection;
using ARController = SOS.Data.SosCrm.MS_EquipmentAccountZoneTypeEventsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class MS_EquipmentAccountZoneTypeEventControllerExtensions
	{
		public static ARCollection Get(this ARController cntlr, string equipmentId, int equipmentAccountZoneTypeId, string monitoringStationOSId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_EquipmentAccountZoneTypeEventsViewGet(equipmentId, equipmentAccountZoneTypeId, monitoringStationOSId));
		}
	}
}
