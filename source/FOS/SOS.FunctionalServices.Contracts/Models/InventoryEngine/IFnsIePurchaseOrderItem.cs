using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIePurchaseOrderItem
	{
        [DataMember]
        long PurchaseOrderItemID { get; set; }

        [DataMember]
        int PurchaseOrderId { get; set; }
        
		//[DataMember]
		//string ProductSkwId { get; set; }

		[DataMember]
		string ProductSKU { get; set; }
        
        [DataMember]
        string ItemId { get; set; }


        [DataMember]
        int Quantity { get; set; }

        [DataMember]
        string ItemDesc { get; set; }

        [DataMember]
        int? WithBarcodeCount { get; set; }

        [DataMember]
        int? WithoutBarcodeCount { get; set; }

	}
}