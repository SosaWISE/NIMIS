using AR = SOS.Data.SosCrm.AE_CustomerSWINGView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerSWINGViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerSWINGViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_CustomerSWINGViewControllerExtensions
	{
		public static AR GetCustomerInfo(this ARController cntlr, long interimAccountId, string customerType)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerSWINGGetInfo(interimAccountId, customerType));
		}
	}
}