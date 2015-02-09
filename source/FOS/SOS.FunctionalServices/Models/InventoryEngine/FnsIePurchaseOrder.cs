using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIePurchaseOrder : IFnsIePurchaseOrder
	{
		#region .ctor
        public FnsIePurchaseOrder(IE_PurchaseOrder purchaseOrder)
		{
            PurchaseOrderID = purchaseOrder.PurchaseOrderID;
			//WarehouseSiteId = purchaseOrder.WarehouseSiteId;
            VendorId = purchaseOrder.VendorId;
			//CloseDate = purchaseOrder.CloseDate;
            GPPONumber = purchaseOrder.GPPONumber;
            IsActive = purchaseOrder.IsActive;
            IsDeleted = purchaseOrder.IsDeleted;
            ModifiedBy = purchaseOrder.ModifiedBy;
            ModifiedOn = purchaseOrder.ModifiedOn;
            CreatedBy = purchaseOrder.CreatedBy;
            CreatedOn = purchaseOrder.CreatedOn;
		}
		#endregion .ctor

		#region Properties
   
		public long PurchaseOrderID { get; set; }
		//public string WarehouseSiteId  { get; set; }
		public string VendorId { get; set; }
		//public  DateTime? CloseDate { get; set; }

        public string GPPONumber { get; set; }
        public bool IsActive { get; set; }

        public  bool IsDeleted { get; set; }


        public DateTime ModifiedOn { get; set; }


        public string ModifiedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public string CreatedBy { get; set; }
		#endregion Properties
	}
}
