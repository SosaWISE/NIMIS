using AR = SOS.Data.SosCrm.MS_EquipmentAccountZoneTypesView;
using ARCollection = SOS.Data.SosCrm.MS_EquipmentAccountZoneTypesViewCollection;
using ARController = SOS.Data.SosCrm.MS_EquipmentAccountZoneTypesViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_EquipmentAccountZoneTypeControllerExtensions
	{
		public static ARCollection Get(this ARController cntlr, string equipmentId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_EquipmentAccountZoneTypesViewGet(equipmentId));
		}
	}
}