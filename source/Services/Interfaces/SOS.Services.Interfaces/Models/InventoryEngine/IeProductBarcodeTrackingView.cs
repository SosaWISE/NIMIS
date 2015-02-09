using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IeProductBarcodeTrackingView : IIeProductBarcodeTrackingView
	{
        public long ProductBarcodeTrackingID { get; set; }

        public string ProductBarcodeTrackingTypeId { get; set; }
        public string ProductBarcodeId { get; set; }

        public string LocationTypeID { get; set; }

        public string LocationID { get; set; }

        /*public string TransferToWarehouseSiteId { get; set; }

        public string Location { get; set; }
        public string ReturnToVendorId { get; set; }

        public long? AssignedToCustomerId { get; set; }

        public int? AssignedToDealerId { get; set; }

        public string RtmaNumberId { get; set; }*/
        public string Comment { get; set; }
       }

    public interface IIeProductBarcodeTrackingView
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
        string Location { get; set; }
        
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

	}
}
