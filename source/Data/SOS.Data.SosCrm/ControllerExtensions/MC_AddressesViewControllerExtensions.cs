using AR = SOS.Data.SosCrm.MC_AddressesView;
using ARCollection = SOS.Data.SosCrm.MC_AddressesViewCollection;
using ARController = SOS.Data.SosCrm.MC_AddressesViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MC_AddressesViewControllerExtensions
	{
		public static AR GetByAccountId(this ARController cntlr, long accountId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MC_AddressGetPremiseByAccountId(accountId));
		}
	}
}
