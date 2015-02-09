using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IeWarehouseSite : IIeWarehouseSite
	{
        public string WarehouseSiteID { get; set; }

        public string WarehouseSiteName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
	}

    public interface IIeWarehouseSite
	{

        [DataMember]
        string WarehouseSiteID { get; set; }

        [DataMember]
        string WarehouseSiteName { get; set; }

     
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
