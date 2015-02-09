using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Swing;

namespace SOS.FunctionalServices.Models.Swing
{
    class FnsCustomerSwingEquipmentInfo : IFnsCustomerSwingEquipmentInfo
    {

		#region .ctor

		public FnsCustomerSwingEquipmentInfo() {}

        public FnsCustomerSwingEquipmentInfo(Action<IFnsCustomerSwingEquipmentInfo, object> fxBindData, object value)
		{
			if (fxBindData == null)
				throw new NotImplementedException();

			fxBindData(this, value);
		}

        public FnsCustomerSwingEquipmentInfo(AE_CustomerSWINGEquipmentView item)
        {
	        RowNumber = item.RowNumber;
	        ZoneNumber = item.ZoneNumber;
            FullName = item.FullName;
			ZoneTypeName = item.ZoneTypeName;
			EquipmentLocationDesc = item.EquipmentLocationDesc;
		}
        
		#endregion .ctor

		#region Properties
		public int? RowNumber { get; set; }
		public string ZoneNumber { get; set; }
        public string FullName { get; set; }
        public string ZoneTypeName { get; set; }
        public string EquipmentLocationDesc { get; set; }
	    	
		#endregion Properties

    }
}
