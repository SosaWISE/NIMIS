using AR  = SOS.Data.SosCrm.MS_IndustryAccountNumbersView;
using ARCollection  = SOS.Data.SosCrm.MS_IndustryAccountNumbersViewCollection;
using ARController  = SOS.Data.SosCrm.MS_IndustryAccountNumbersViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_IndustryAccountNumbersViewControllerExtensions
	{
		public static AR Generate(this ARController cntlr, long accountId, string accountType, string monitoringStationOSId, string gpEmployeeId, bool isPrimary = true)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_IndustryAccountGenerate(accountId, isPrimary, accountType, monitoringStationOSId, gpEmployeeId));
		}

		public static ARCollection GetByAccountId(this ARController cntlr, long accountId, string gpEmployeeId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_IndustryAccountNumbersViewGetByAccountId(accountId));
		}
	}
}
