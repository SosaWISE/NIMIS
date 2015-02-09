using AR = SOS.Data.SosCrm.MS_AccountEventView;
using ARCollection = SOS.Data.SosCrm.MS_AccountEventViewCollection;
using ARController = SOS.Data.SosCrm.MS_AccountEventViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountEventViewControllerExtensions
	{
		public static ARCollection Get(this ARController cntlr, string msoid, int equipmentTypeId, string gpEmployeeId)
		{
			return
				cntlr.LoadCollection(
					SosCrmDataStoredProcedureManager.MS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId(msoid,
						equipmentTypeId, gpEmployeeId));
		}
	}
}
