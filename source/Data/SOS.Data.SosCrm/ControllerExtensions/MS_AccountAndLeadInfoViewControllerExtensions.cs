using AR = SOS.Data.SosCrm.MS_AccountAndLeadInfoView;
using ARCollection = SOS.Data.SosCrm.MS_AccountAndLeadInfoViewCollection;
using ARController = SOS.Data.SosCrm.MS_AccountAndLeadInfoViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable once InconsistentNaming
	public static class MS_AccountAndLeadInfoViewControllerExtensions
	{
		public static AR ByAccountID(this ARController cntlr, long accountID)
		{
			return cntlr.LoadSingle(AR.Query().WHERE(AR.Columns.AccountID, accountID));
		}
		//public static AR Create(this ARController cntlr, long leadId, string gpEmployeeId)
		//{
		//	return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountAndLeadInfoCreate(leadId, gpEmployeeId));
		//}
	}
}
