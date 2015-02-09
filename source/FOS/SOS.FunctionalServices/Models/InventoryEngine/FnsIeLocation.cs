using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIeLocation : IFnsIeLocation
	{
		#region .ctor
        public FnsIeLocation(IE_LocationView location)
		{
            LocationID = location.LocationID;
            LocationName = location.LocationName;

		}

		#endregion .ctor

		#region Properties
   
		public string LocationID { get; set; }
        public string LocationName { get; set; }

		#endregion Properties
	}
}
