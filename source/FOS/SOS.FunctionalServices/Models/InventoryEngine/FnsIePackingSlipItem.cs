using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIePackingSlipItem : IFnsIePackingSlipItem
	{
		#region .ctor
        public FnsIePackingSlipItem(IE_PackingSlipItem packingSlipItem)
		{
            PackingSlipItemID = packingSlipItem.PackingSlipItemID;
            PackingSlipId = packingSlipItem.PackingSlipId;
            ProductSkwId = packingSlipItem.ProductSkwId;
            ItemId = packingSlipItem.ItemId;
            Quantity = packingSlipItem.Quantity;
            IsActive = packingSlipItem.IsActive;
            IsDeleted = packingSlipItem.IsDeleted;
            ModifiedBy = packingSlipItem.ModifiedBy;
            ModifiedOn = packingSlipItem.ModifiedOn;
            CreatedBy = packingSlipItem.CreatedBy;
            CreatedOn = packingSlipItem.CreatedOn;
		}


        public FnsIePackingSlipItem(int packingSlipId, string productSkwId, string itemId, int quantity) 
        {
            PackingSlipId = packingSlipId;
            ProductSkwId = productSkwId;
            ItemId = itemId;
            Quantity = quantity;
        }


		#endregion .ctor

		#region Properties

        public long PackingSlipItemID { get; set; }
        public int PackingSlipId { get; set; }

        public string ProductSkwId { get; set; }
        public string ItemId { get; set; }

        public int Quantity { get; set; }
        public bool IsActive { get; set; }

        public  bool IsDeleted { get; set; }


        public DateTime ModifiedOn { get; set; }


        public string ModifiedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public string CreatedBy { get; set; }
		#endregion Properties
	}
}
