using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIePurchaseOrder
	{
        [DataMember]
        long PurchaseOrderID { get; set; }

		//[DataMember]
		//string WarehouseSiteId { get; set; }
        
        [DataMember]
        string VendorId { get; set; }
        
		//[DataMember]
		//DateTime? CloseDate { get; set; }
        [DataMember]
        string GPPONumber { get; set; }

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