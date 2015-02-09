using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIeProductBarcodeLocation : IFnsIeProductBarcodeLocation
	{
		#region .ctor
        public FnsIeProductBarcodeLocation(IE_ProductBarcodeLocationView productBarcodeTrackingView)
		{
            ProductBarcodeId = productBarcodeTrackingView.ProductBarcodeId;
            ItemDesc = productBarcodeTrackingView.ItemDesc;
          
		}


		#endregion .ctor

		#region Properties

        public string ProductBarcodeId { get; set; }
        public string ItemDesc  { get; set; }

		#endregion Properties

	}
}
