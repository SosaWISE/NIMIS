using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIeLocationType : IFnsIeLocationType
	{
		#region .ctor
        public FnsIeLocationType(IE_LocationType LocationType)
		{
            LocationTypeID = LocationType.LocationTypeID;
            LocationTypeName = LocationType.LocationTypeName;

		}

		#endregion .ctor

		#region Properties
   
		public string LocationTypeID { get; set; }
        public string LocationTypeName { get; set; }

        public string TableName { get; set; }

        public string FieldName { get; set; }

        public string Comment { get; set; }

        public  bool IsDeleted { get; set; }


		#endregion Properties
	}
}
