using AR = SOS.Data.SosCrm.GS_Account;
using ARCollection = SOS.Data.SosCrm.GS_AccountCollection;
using ARController = SOS.Data.SosCrm.GS_AccountController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class GS_AccountControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR GetByLaipacUnitID(this ARController oCntlr, string sUnitID)
		{
			/** Initialize. */
			AR result = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.GS_AccountGetByLaipacUnitID(sUnitID));

			/** Return result. */
			return result;
		}
	}
}
