using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Swing
{
    public class CustomerSwingEquipmentInfo : ICustomerSwingEquipmentInfo
    {
        #region Properties
		public int? RowNumber { get; set; }
		public string ZoneNumber { get; set; }
        public string FullName { get; set; }
        public string ZoneTypeName { get; set; }
        public string EquipmentLocationDesc { get; set; }
   
        #endregion Properties
    }

    public interface ICustomerSwingEquipmentInfo
    {
        #region Properties

		[DataMember]
		int? RowNumber { get; set; }

		[DataMember]
		string ZoneNumber { get; set; }

        [DataMember]
        string FullName { get; set; }

        [DataMember]
        string ZoneTypeName { get; set; }

        [DataMember]
        string EquipmentLocationDesc { get; set; }


        #endregion Properties

    }
}
