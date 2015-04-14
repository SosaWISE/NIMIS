using AR = SOS.Data.SosCrm.MS_AccountHold;
using ARCollection = SOS.Data.SosCrm.MS_AccountHoldCollection;
using ARController = SOS.Data.SosCrm.MS_AccountHoldController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountHoldControllerExtensions
	{
		public static AR Create(this ARController cntlr, long accountId, int catg2Id, string holdDescription,
			string gpEmployeeId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountHoldsCreate(accountId, catg2Id, holdDescription, gpEmployeeId));
		}
	}
}
