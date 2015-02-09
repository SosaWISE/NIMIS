using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIeVendor
	{
        [DataMember]
        string VendorID { get; set; }

        [DataMember]
        string VendorName { get; set; }

     
       
	}
}