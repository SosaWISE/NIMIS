using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IeVendor : IIeVendor
	{
        public string VendorID { get; set; }

        public string VendorName { get; set; }

     
	}

    public interface IIeVendor
	{

        [DataMember]
        string VendorID { get; set; }

        [DataMember]
        string VendorName { get; set; }

    
	}
}
