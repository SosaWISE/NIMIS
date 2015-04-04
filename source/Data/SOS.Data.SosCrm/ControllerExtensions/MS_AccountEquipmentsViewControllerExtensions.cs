using System;
using AR = SOS.Data.SosCrm.MS_AccountEquipmentsView;
using ARCollection = SOS.Data.SosCrm.MS_AccountEquipmentsViewCollection;
using ARController = SOS.Data.SosCrm.MS_AccountEquipmentsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountEquipmentsViewControllerExtensions
	{
		public static AR LookupByPartNumber(this ARController cntlr, long accountId, string partNumber, string techId,
			string gpEmployeeId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountEquipmentsViewNextAssignment(accountId, partNumber, techId, gpEmployeeId));
		}
		public static AR LookupByBarcode(this ARController cntlr, long accountId, string barcode, string techId,
			string gpEmployeeId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountEquipmentsViewNextAssignmentByBarcode(accountId, barcode, techId, gpEmployeeId));
		}
		public static ARCollection ByAccountId(this ARController cntlr, long accountId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.AccountId, accountId)
				.WHERE(AR.Columns.IsDeleted, false)
				.ORDER_BY(AR.Columns.Zone);
			return cntlr.LoadCollection(qry);
		}

		[Obsolete("This is never used", true)]
		public static MS_AccountEquipmentsView ExistingEquipmentAdd(this MS_AccountEquipmentsViewController cntlr, long? accountId, string equipmentID, int? equipmentLocationId, int? zoneEventTypeId, string zone, string comments, bool? isExisting, bool? isExistingWiring, bool? isMainPanel, string gpEmployeeId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountEquipmentsViewAddExistingEquipment(accountId, equipmentID, equipmentLocationId, zoneEventTypeId, zone, comments, isExisting, isExistingWiring, isMainPanel, gpEmployeeId));
		}

		public static AR ByAccountZoneAssignment(this ARController cntlr, long accountZoneAssignmentId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.AccountZoneAssignmentID, accountZoneAssignmentId);
			return cntlr.LoadSingle(qry);
		}
	}
}
