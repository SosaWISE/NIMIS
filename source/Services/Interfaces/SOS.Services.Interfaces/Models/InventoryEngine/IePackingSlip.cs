using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IePackingSlip : IIePackingSlip
	{
		public long PackingSlipID { get; set; }

        public long PurchaseOrderId { get; set; }
        public DateTime ArrivalDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public string PackingSlipNumber { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string GPPONumber {get;set;}
	}

    public interface IIePackingSlip
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
