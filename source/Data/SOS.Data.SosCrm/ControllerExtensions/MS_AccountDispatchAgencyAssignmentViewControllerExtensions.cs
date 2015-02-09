using AR = SOS.Data.SosCrm.MS_AccountDispatchAgencyAssignmentView;
using ARCollection = SOS.Data.SosCrm.MS_AccountDispatchAgencyAssignmentViewCollection;
using ARController = SOS.Data.SosCrm.MS_AccountDispatchAgencyAssignmentViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountDispatchAgencyAssignmentViewControllerExtensions
	{
		public static AR AssignDa(this ARController cntlr, int dispatchAgencyID, long accountId, long industryAccountId,
			string gpEmployeeId)
		{
			return
				cntlr.LoadSingle(
					SosCrmDataStoredProcedureManager.MS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId(dispatchAgencyID,
						accountId, industryAccountId, gpEmployeeId));
		}
	}
}
