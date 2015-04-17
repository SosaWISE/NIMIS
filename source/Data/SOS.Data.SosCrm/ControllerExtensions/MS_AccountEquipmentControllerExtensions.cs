using AR = SOS.Data.SosCrm.MS_AccountEquipment;
using ARCollection = SOS.Data.SosCrm.MS_AccountEquipmentCollection;
using ARController = SOS.Data.SosCrm.MS_AccountEquipmentController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountEquipmentControllerExtensions
	{
		public static bool Delete(this ARController cntlr, long accountEquipmentID, string username)
		{
			var item = cntlr.LoadByPrimaryKey(accountEquipmentID);
			if (item != null && !item.IsDeleted)
			{
				item.IsDeleted = true;
				item.Save(username);
				return true;
			}
			return false;
		}

		public static AR SyncAssignmentBetweenInvoiceItem(this ARController cntlr, long accountEquipmentId,
			string gpEmployeeId)
		{
			return
				cntlr.LoadSingle(
					SosCrmDataStoredProcedureManager.MS_AccountEquipmentSyncAssignmetBetweenInvoiceItem(accountEquipmentId,
						gpEmployeeId));
		}
	}
}

