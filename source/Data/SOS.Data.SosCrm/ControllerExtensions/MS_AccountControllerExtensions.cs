using AR = SOS.Data.SosCrm.MS_Account;
using ARCollection = SOS.Data.SosCrm.MS_AccountCollection;
using ARController = SOS.Data.SosCrm.MS_AccountController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class MS_AccountControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR GetByLaipacUnitID(this ARController oCntlr, string sUnitID)
		{
			/** Initialize. */
			AR result = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountGetByLaipacUnitID(sUnitID));

			/** Return result. */
			return result;
		}
	}
}
