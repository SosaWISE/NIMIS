using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIeProductBarcodeLocation
	{
        [DataMember]
        string ProductBarcodeId { get; set; }

        [DataMember]
        string ItemDesc { get; set; }


	}
}