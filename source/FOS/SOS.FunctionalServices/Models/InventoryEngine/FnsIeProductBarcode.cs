using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIeProductBarcode : IFnsIeProductBarcode
	{
		#region .ctor
        public FnsIeProductBarcode(IE_ProductBarcode productBarcode)
		{
            ProductBarcodeID = productBarcode.ProductBarcodeID;
            PurchaseOrderItemId = productBarcode.PurchaseOrderItemId;
            LastProductBarcodeTrackingId = productBarcode.LastProductBarcodeTrackingId;
            ProductBarcodeBundleId = productBarcode.ProductBarcodeBundleId;
            SimGUID = productBarcode.SimGUID;
            IsDeleted = productBarcode.IsDeleted;
            ModifiedBy = productBarcode.ModifiedBy;
            ModifiedOn = productBarcode.ModifiedOn;
            CreatedBy = productBarcode.CreatedBy;
            CreatedOn = productBarcode.CreatedOn;
		}

        public FnsIeProductBarcode(string productBarcodeID , long purchaseOrderItemId)
        {
            ProductBarcodeID = productBarcodeID;
            PurchaseOrderItemId = purchaseOrderItemId;

        }

		#endregion .ctor

		#region Properties

        public string ProductBarcodeID { get; set; }
        public long PurchaseOrderItemId { get; set; }
        public long? LastProductBarcodeTrackingId { get; set; }
        public string ProductBarcodeBundleId { get; set; }


        public string SimGUID { get; set; }

        public  bool IsDeleted { get; set; }


        public DateTime ModifiedOn { get; set; }


        public string ModifiedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public string CreatedBy { get; set; }
		#endregion Properties
	}
}
