using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IePurchaseOrderItem : IIePurchaseOrderItem
	{
		public long PurchaseOrderItemID { get; set; }

		public int PurchaseOrderId { get; set; }

		//public string ProductSkwId { get; set; }
		public string ProductSKU { get; set; }

        public string ItemId { get; set; }

        public int Quantity { get; set; }

        public string ItemDesc { get; set; }

        public int? WithBarcodeCount { get; set; }

        public int? WithoutBarcodeCount { get; set; }
	}

    public interface IIePurchaseOrderItem
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
