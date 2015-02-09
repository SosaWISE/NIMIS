using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIePurchaseOrderItem : IFnsIePurchaseOrderItem
	{
		#region .ctor
        public FnsIePurchaseOrderItem(IE_PurchaseOrderItemsView purchaseOrderItem)
		{
            PurchaseOrderItemID = purchaseOrderItem.PurchaseOrderItemID;
            PurchaseOrderId = purchaseOrderItem.PurchaseOrderId;
			//ProductSkwId = purchaseOrderItem.ProductSkwId;
	        ProductSKU = purchaseOrderItem.ProductSKU;
            ItemId = purchaseOrderItem.ItemId;
            ItemDesc = purchaseOrderItem.ItemDesc;
            Quantity = purchaseOrderItem.Quantity;
            WithBarcodeCount = purchaseOrderItem.WithBarcodeCount;
            WithoutBarcodeCount = purchaseOrderItem.WithoutBarcodeCount;
		}
		#endregion .ctor

		#region Properties


     
        public long PurchaseOrderItemID { get; set; }

        public int PurchaseOrderId { get; set; }

		//public string ProductSkwId { get; set; }
		public string ProductSKU { get; set; }

        public string ItemId { get; set; }

        public string ItemDesc { get; set; }
        public int Quantity { get; set; }

        public int? WithBarcodeCount { get; set; }

        public int? WithoutBarcodeCount { get; set; }



		#endregion Properties
	}
}
