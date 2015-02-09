using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIeVendor : IFnsIeVendor
	{
		#region .ctor
        public FnsIeVendor(IE_Vendor vendor)
		{
            VendorID = vendor.VendorID;
            VendorName = vendor.VendorName;

		}

		#endregion .ctor

		#region Properties
   
		public string VendorID { get; set; }
        public string VendorName { get; set; }

		#endregion Properties
	}
}
