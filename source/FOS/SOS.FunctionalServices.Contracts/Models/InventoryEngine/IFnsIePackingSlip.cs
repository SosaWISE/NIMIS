using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIePackingSlip
	{
        [DataMember]
        long PackingSlipID { get; set; }

        [DataMember]
        long PurchaseOrderId { get; set; }

        [DataMember]
        DateTime ArrivalDate { get; set; }
        
        [DataMember]
        DateTime? CloseDate { get; set; }
        
        [DataMember]
        string PackingSlipNumber { get; set; }

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

        [DataMember]
        string GPPONumber { get; set; }
	}
}