using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIeLocation
	{
        [DataMember]
        string LocationID { get; set; }

        [DataMember]
        string LocationName { get; set; }

     
       
	}
}