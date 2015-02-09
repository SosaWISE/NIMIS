using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;

namespace SOS.FunctionalServices.Models.InventoryEngine
{
	public class FnsIeWarehouseSite : IFnsIeWarehouseSite
	{
		#region .ctor
        public FnsIeWarehouseSite(IE_WarehouseSite warehouseSite)
		{
            WarehouseSiteID = warehouseSite.WarehouseSiteID;
            WarehouseSiteName = warehouseSite.WarehouseSiteName;
            /*IsDeleted = warehouseSite.IsDeleted;
            ModifiedBy = warehouseSite.ModifiedBy;
            ModifiedOn = warehouseSite.ModifiedOn;
            CreatedBy = warehouseSite.CreatedBy;
            CreatedOn = warehouseSite.CreatedOn;*/
		}

		#endregion .ctor

		#region Properties
   
		public string WarehouseSiteID { get; set; }
        public string WarehouseSiteName { get; set; }

        public bool IsActive{ get; set; }

        public  bool IsDeleted { get; set; }


        public DateTime ModifiedOn { get; set; }


        public string ModifiedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public string CreatedBy { get; set; }
		#endregion Properties
	}
}
