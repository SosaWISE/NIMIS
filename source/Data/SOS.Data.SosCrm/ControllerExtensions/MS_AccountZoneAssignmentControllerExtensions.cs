using AR = SOS.Data.SosCrm.MS_AccountZoneAssignment;
using ARCollection = SOS.Data.SosCrm.MS_AccountZoneAssignmentCollection;
using ARController = SOS.Data.SosCrm.MS_AccountZoneAssignmentController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable once InconsistentNaming
	public static class MS_AccountZoneAssignmentControllerExtensions
	{
		public static ARCollection GetZoneAssignmentsByAccountId(this ARController cntlr, long accountId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_AccountZoneAssignmentsByAccountId(accountId));
		}
		public static bool DeleteByAccountEquipmentId(this ARController cntlr, long accountEquipmentID, string username)
		{
			var list = cntlr.LoadCollection(AR.Query().WHERE(AR.Columns.AccountEquipmentId, accountEquipmentID));
			var deleted = false;
			foreach (var item in list)
			{
				if (!item.IsDeleted)
				{
					item.IsDeleted = true;
					item.Save(username);
					deleted = true;
				}
			}
			return deleted;
		}
	}
}
