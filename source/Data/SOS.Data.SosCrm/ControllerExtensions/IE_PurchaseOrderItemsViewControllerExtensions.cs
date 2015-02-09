using AR = SOS.Data.SosCrm.IE_PurchaseOrderItemsView;
using ARCollection = SOS.Data.SosCrm.IE_PurchaseOrderItemsViewCollection;
using ARController = SOS.Data.SosCrm.IE_PurchaseOrderItemsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{

    public static class IE_PurchaseOrderItemsViewControllerExtensions

	{
		public static ARCollection GetPurchaseOrderItemsList(this ARController oCntlr, long purchaseOrderID)
		{
			/** Initialize. */
            var oQry = AR.Query().WHERE(AR.Columns.PurchaseOrderId, purchaseOrderID);

			/** Return result. */
			return oCntlr.LoadCollection(oQry);
		}

	}
}
