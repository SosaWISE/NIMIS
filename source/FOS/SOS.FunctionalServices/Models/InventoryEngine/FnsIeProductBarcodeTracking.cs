using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIeProductBarcodeTracking : IFnsIeProductBarcodeTracking
	{
		#region .ctor
        public FnsIeProductBarcodeTracking(IE_ProductBarcodeTracking productBarcodeTracking)
		{
            ProductBarcodeTrackingID = productBarcodeTracking.ProductBarcodeTrackingID;
            ProductBarcodeTrackingTypeId = productBarcodeTracking.ProductBarcodeTrackingTypeId;
            ProductBarcodeId = productBarcodeTracking.ProductBarcodeId;
            LocationTypeID = productBarcodeTracking.LocationTypeId;
            LocationID = productBarcodeTracking.LocationId;
           /* TransferToWarehouseSiteId = productBarcodeTracking.TransferToWarehouseSiteId;
            ReturnToVendorId = productBarcodeTracking.ReturnToVendorId;
            AssignedToCustomerId = productBarcodeTracking.AssignedToCustomerId;
            AssignedToDealerId = productBarcodeTracking.AssignedToDealerId;*/

            IsDeleted = productBarcodeTracking.IsDeleted;
            ModifiedBy = productBarcodeTracking.ModifiedBy;
            ModifiedOn = productBarcodeTracking.ModifiedOn;
            CreatedBy = productBarcodeTracking.CreatedBy;
            CreatedOn = productBarcodeTracking.CreatedOn;
		}


        public FnsIeProductBarcodeTracking(
            string productBarcodeTrackingTypeId, 
            string productBarcodeId, 
            string locationTypeID,
            string locationID,
           /* string transferToWarehouseSiteId,
            string returnToVendorId,
            long? assignedToCustomerId,
            int? assignedToDealerId,
            string rtmaNumberId,*/
            string comment
            ) 
        {
            ProductBarcodeTrackingTypeId = productBarcodeTrackingTypeId;
            ProductBarcodeId = productBarcodeId;
            LocationTypeID = locationTypeID;
            LocationID = locationID;
            /*TransferToWarehouseSiteId = transferToWarehouseSiteId;
            ReturnToVendorId =   returnToVendorId;
            AssignedToCustomerId = assignedToCustomerId;
            AssignedToDealerId = assignedToDealerId;
            RtmaNumberId = rtmaNumberId;*/
            Comment = comment;
            
   
        }


		#endregion .ctor

		#region Properties

        public long ProductBarcodeTrackingID { get; set; }
        public string ProductBarcodeTrackingTypeId  { get; set; }

		public string ProductBarcodeId { get; set; }

        public string LocationTypeID { get; set; }

        public string LocationID { get; set; }
        /*public string TransferToWarehouseSiteId { get; set; }

        public string ReturnToVendorId { get; set; }
        public long? AssignedToCustomerId { get; set; }

        public int? AssignedToDealerId {get;set;}

        public string RtmaNumberId { get; set; }*/

        public string Comment { get; set; }

        public  bool IsDeleted { get; set; }


        public DateTime ModifiedOn { get; set; }


        public string ModifiedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public string CreatedBy { get; set; }
		#endregion Properties

	}
}
