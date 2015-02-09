using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIePackingSlip : IFnsIePackingSlip
	{
		#region .ctor
        public FnsIePackingSlip(IE_PackingSlip packingSlip)
		{
            PackingSlipID = packingSlip.PackingSlipID;
            PurchaseOrderId = packingSlip.PurchaseOrderId;
            ArrivalDate = packingSlip.ArrivalDate;
            PackingSlipNumber = packingSlip.PackingSlipNumber;
            CloseDate = packingSlip.CloseDate;
            IsActive = packingSlip.IsActive;
            IsDeleted = packingSlip.IsDeleted;
            ModifiedBy = packingSlip.ModifiedBy;
            ModifiedOn = packingSlip.ModifiedOn;
            CreatedBy = packingSlip.CreatedBy;
            CreatedOn = packingSlip.CreatedOn;
		}


        public FnsIePackingSlip(IE_PackingSlipView packingSlip)
        {
            PackingSlipID = packingSlip.PackingSlipID;
            PurchaseOrderId = packingSlip.PurchaseOrderId;
            ArrivalDate = packingSlip.ArrivalDate;
            PackingSlipNumber = packingSlip.PackingSlipNumber;
            CloseDate = packingSlip.CloseDate;
            IsActive = packingSlip.IsActive;
            IsDeleted = packingSlip.IsDeleted;
            GPPONumber = packingSlip.GPPONumber;

        }

        public FnsIePackingSlip(string packingSlipNumber, long purchaseOrderId) 
        {
            PackingSlipNumber = packingSlipNumber;
            PurchaseOrderId = purchaseOrderId;
        }


		#endregion .ctor

		#region Properties
   
		public long PackingSlipID { get; set; }
        public long PurchaseOrderId { get; set; }

		public  DateTime ArrivalDate { get; set; }
        public string PackingSlipNumber { get; set; }

        public DateTime? CloseDate { get; set; }
        public bool IsActive { get; set; }

        public  bool IsDeleted { get; set; }


        public DateTime ModifiedOn { get; set; }


        public string ModifiedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public string CreatedBy { get; set; }

        public string GPPONumber { get; set; }
		#endregion Properties
	}
}
