using AR = SOS.Data.SosCrm.AE_Item;
using ARCollection = SOS.Data.SosCrm.AE_ItemCollection;
using ARController = SOS.Data.SosCrm.AE_ItemController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_ItemControllerExtensions
	{
		public static ARCollection ActivationFeesGet(this ARController cntlr)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_ItemActivationFeesGet());
		}

		public static ARCollection EquipmentByPointsGet(this ARController cntlr, long invoiceTemplateId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_ItemByInvoiceTemplateId(invoiceTemplateId));
		}
	}
}
