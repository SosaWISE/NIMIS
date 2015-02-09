using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IeProductBarcode : IIeProductBarcode
	{
		public string ProductBarcodeID { get; set; }

        public long PurchaseOrderItemId { get; set; }

        public long? LastProductBarcodeTrackingId { get; set; }

        public string ProductBarcodeBundleId { get; set; }

        public string SimGUID { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
	}

    public interface IIeProductBarcode
	{
        [DataMember]
        string ProductBarcodeID { get; set; }

        [DataMember]
        long PurchaseOrderItemId { get; set; }

        [DataMember]
        long? LastProductBarcodeTrackingId { get; set; }

        [DataMember]
        string ProductBarcodeBundleId { get; set; }

        [DataMember]
        string SimGUID { get; set; }

        [DataMember]
        bool IsDeleted { get; set; }

        [DataMember]
        DateTime ModifiedOn { get; set; }

        [DataMember]
        string ModifiedBy { get; set; }

        [DataMember]
        DateTime CreatedOn { get; set; }

        [DataMember]
        string CreatedBy { get; set; }
	}
}
