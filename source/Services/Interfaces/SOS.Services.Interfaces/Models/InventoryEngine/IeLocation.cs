using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IeLocation : IIeLocation
	{
        public string LocationID { get; set; }

        public string LocationName { get; set; }

	}

    public interface IIeLocation
	{

        [DataMember]
        string LocationID { get; set; }

        [DataMember]
        string LocationName { get; set; }

    
       
	}
}
