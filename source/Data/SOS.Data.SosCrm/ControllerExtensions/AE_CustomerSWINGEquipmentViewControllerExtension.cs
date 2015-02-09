using AR = SOS.Data.SosCrm.AE_CustomerSWINGEquipmentView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerSWINGEquipmentViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerSWINGEquipmentViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_CustomerSWINGEquipmentViewControllerExtensions
	{
        public static ARCollection GetCustomerSWINGEquipment(this ARController cntlr, long interimAccountID)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_CustomerSWINGGetEquipments(interimAccountID));
		}
	}
}