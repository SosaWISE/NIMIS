using AR = SOS.Data.SosCrm.MS_AccountDispatchAgencyAssignment;
using ARCollection = SOS.Data.SosCrm.MS_AccountDispatchAgencyAssignmentCollection;
using ARController = SOS.Data.SosCrm.MS_AccountDispatchAgencyAssignmentController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountDispatchAgencyAssignmentControllerExtensions
	{
		public static ARCollection LoadCollectionByAccountId(this ARController cntlr, long accountId)
		{
			return cntlr.LoadCollection(AR.Query().WHERE(AR.Columns.AccountId, accountId));
		}
	}
}
