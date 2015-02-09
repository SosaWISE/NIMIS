using AR = SOS.Data.SosCrm.MS_EquipmentLocationsView;
using ARCollection = SOS.Data.SosCrm.MS_EquipmentLocationsViewCollection;
using ARController = SOS.Data.SosCrm.MS_EquipmentLocationsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_EquipmentLocationViewControllerExtensions
	{
		public static ARCollection GetByMOSID(this ARController cntlr, string msOSID, string gpEmployeeId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_EquipmentLocationsByMSOID(msOSID, gpEmployeeId));
		}

		public static AR GetPanelLocationByAccountId(this ARController cntlr, long accountId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_EquipmentLocationGetPanelLocationByAccountId(accountId));
		}
	}
}
