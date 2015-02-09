using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IePurchaseOrder : IIePurchaseOrder
	{
		public long PurchaseOrderID { get; set; }

		//public string WarehouseSiteId { get; set; }

        public string VendorId { get; set; }

		//public DateTime? CloseDate { get; set; }

        public string GPPONumber { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
	}

    public interface IIePurchaseOrder
	{

        [DataMember]
        long PurchaseOrderID { get; set; }
        
		//[DataMember]
		//string WarehouseSiteId { get; set; }
        
        [DataMember]
        string VendorId { get; set; }


        [DataMember]
        string GPPONumber { get; set; }
		//[DataMember]
		//DateTime? CloseDate { get; set; }

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
