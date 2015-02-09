using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIeWarehouseSite
	{
        [DataMember]
        string WarehouseSiteID { get; set; }

        [DataMember]
        string WarehouseSiteName { get; set; }

        [DataMember]
        bool IsActive { get; set; }

        [DataMember]
        bool IsDeleted { get; set; }

        /*
        [DataMember]
        DateTime ModifiedOn { get; set; }

        [DataMember]
        string ModifiedBy { get; set; }

        [DataMember]
        DateTime CreatedOn { get; set; }

        [DataMember]
        string CreatedBy { get; set; }*/
	}
}