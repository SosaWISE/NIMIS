using AR = SOS.Data.SosCrm.AE_InvoiceItem;
using ARCollection = SOS.Data.SosCrm.AE_InvoiceItemCollection;
using ARController = SOS.Data.SosCrm.AE_InvoiceItemController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_InvoiceItemControllerExtentions
	{
		public static AR CreateFromAeItem(this ARController oCntlr, short mQty, AE_Item oItem, long lInvoiceID, string sUserId)
		{
			/** Initialize. */
			var oInvItem = new AE_InvoiceItem
				{
					Qty = mQty
					, InvoiceId = lInvoiceID
					, ItemId = oItem.ItemID
					, TaxOptionId = oItem.TaxOptionId
					, RetailPrice = oItem.Price
					, Cost = oItem.Cost
				};
			oInvItem.Save(sUserId);

			/** Return result. */
			return oInvItem;
		}
	}
}