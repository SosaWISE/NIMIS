using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIeProductBarcode
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