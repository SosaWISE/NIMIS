using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIeProductBarcodeTrackingView : IFnsIeProductBarcodeTrackingView
	{
		#region .ctor
        public FnsIeProductBarcodeTrackingView(IE_ProductBarcodeTrackingView productBarcodeTrackingView)
		{
            ProductBarcodeTrackingID = productBarcodeTrackingView.ProductBarcodeTrackingID;
            ProductBarcodeTrackingTypeId = productBarcodeTrackingView.ProductBarcodeTrackingTypeId;
            ProductBarcodeId = productBarcodeTrackingView.ProductBarcodeId;
            LocationID = productBarcodeTrackingView.LocationID;
            LocationTypeID = productBarcodeTrackingView.LocationID;
          /*  TransferToWarehouseSiteId = productBarcodeTrackingView.TransferToWarehouseSiteId;
            ReturnToVendorId = productBarcodeTrackingView.ReturnToVendorId;
            AssignedToCustomerId = productBarcodeTrackingView.AssignedToCustomerId;
            AssignedToDealerId = productBarcodeTrackingView.AssignedToDealerId;*/


		}


        /*public FnsIeProductBarcodeTracking(
            string productBarcodeTrackingTypeId, 
            string productBarcodeId, 
            string transferToWarehouseSiteId,
            string returnToVendorId,
            long? assignedToCustomerId,
            int? assignedToDealerId,
            string rtmaNumberId,
            string comment
            ) 
        {
            ProductBarcodeTrackingTypeId = productBarcodeTrackingTypeId;
            ProductBarcodeId = productBarcodeId;
            TransferToWarehouseSiteId = transferToWarehouseSiteId;
            ReturnToVendorId =   returnToVendorId;
            AssignedToCustomerId = assignedToCustomerId;
            AssignedToDealerId = assignedToDealerId;
            RtmaNumberId = rtmaNumberId;
            Comment = comment;
            
   
        }*/


		#endregion .ctor

		#region Properties

        public long ProductBarcodeTrackingID { get; set; }
        public string ProductBarcodeTrackingTypeId  { get; set; }

		public string ProductBarcodeId { get; set; }

        public string LocationTypeID { get; set; }

        public string LocationID { get; set; }
      /*  public string TransferToWarehouseSiteId { get; set; }
        public string Location { get; set; }
        public string ReturnToVendorId { get; set; }
        public long? AssignedToCustomerId { get; set; }

        public int? AssignedToDealerId {get;set;}

        public string RtmaNumberId { get; set; }*/

        public string Comment { get; set; }
		#endregion Properties

	}
}
