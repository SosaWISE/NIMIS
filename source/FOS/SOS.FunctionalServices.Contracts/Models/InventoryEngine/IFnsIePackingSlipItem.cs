using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIePackingSlipItem
	{
        [DataMember]
        long PackingSlipItemID { get; set; }

        [DataMember]
        int PackingSlipId { get; set; }

        [DataMember]
        string ProductSkwId { get; set; }
        
        [DataMember]
        string ItemId { get; set; }

        [DataMember]
        int Quantity { get; set; }
    
        [DataMember]
        bool IsActive { get; set; }

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