using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IePackingSlipItem : IIePackingSlipItem
	{
        public long PackingSlipItemID { get; set; }
        public int PackingSlipId { get; set; }

        public string ProductSkwId { get; set; }
        public string ItemId { get; set; }

        public int Quantity { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }


        public DateTime ModifiedOn { get; set; }


        public string ModifiedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public string CreatedBy { get; set; }
	}

    public interface IIePackingSlipItem
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
