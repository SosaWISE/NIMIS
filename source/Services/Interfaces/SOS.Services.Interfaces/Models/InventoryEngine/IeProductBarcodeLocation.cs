using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IeProductBarcodeLocation : IIeProductBarcodeLocation
	{
        public string ProductBarcodeId { get; set; }

        public string ItemDesc { get; set; }
      
	}

    public interface IIeProductBarcodeLocation
	{
        [DataMember]
        string ProductBarcodeId { get; set; }

        [DataMember]
        string ItemDesc { get; set; }

	}
}
