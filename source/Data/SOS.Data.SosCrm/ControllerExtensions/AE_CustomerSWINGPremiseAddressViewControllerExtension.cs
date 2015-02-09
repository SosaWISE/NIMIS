using AR = SOS.Data.SosCrm.AE_CustomerSWINGPremiseAddressView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerSWINGPremiseAddressViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerSWINGPremiseAddressViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_CustomerSWINGPremiseAddressViewControllerExtensions
	{
// ReSharper disable once InconsistentNaming
		public static AR GetCustomerSWINGPremiseAddress(this ARController cntlr, long interimAccountID)
		{
            return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerSWINGGetPrimeseAddress(interimAccountID));
		}
	}
}