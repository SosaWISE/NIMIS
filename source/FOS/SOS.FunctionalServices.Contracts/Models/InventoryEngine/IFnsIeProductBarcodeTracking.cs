using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIeProductBarcodeTracking
	{
        [DataMember]
        long ProductBarcodeTrackingID { get; set; }

        [DataMember]
        string ProductBarcodeTrackingTypeId { get; set; }

        [DataMember]
        string ProductBarcodeId { get; set; }

        [DataMember]
        string LocationTypeID { get; set; }


        [DataMember]
        string LocationID { get; set; }

        
       /* [DataMember]
        string TransferToWarehouseSiteId { get; set; }
        
        [DataMember]
        string ReturnToVendorId { get; set; }

        [DataMember]
        long? AssignedToCustomerId { get; set; }


        [DataMember]
        int? AssignedToDealerId { get; set; }

        [DataMember]
        string RtmaNumberId { get; set; }*/

        [DataMember]
        string Comment { get; set; }

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