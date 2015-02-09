using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Swing
{
    public interface IFnsCustomerSwingEquipmentInfo
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
