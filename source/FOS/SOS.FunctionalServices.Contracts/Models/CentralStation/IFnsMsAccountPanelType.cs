using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
    public interface IFnsMsAccountPanelType
    {
        [DataMember]
        string PanelTypeID { get; set; }
        [DataMember]
        string PanelTypeName { get; set; }
		[DataMember]
		string UIName { get; set; }
        [DataMember]
        bool IsActive { get; set; }
        [DataMember]
        bool IsDeleted { get; set; }
        [DataMember]
        DateTime ModifiedOn { get; set; }
        [DataMember]
        string ModifiedBy { get; set; }
        [DataMember]
        DateTime CreatedOn { get; set; }
        [DataMember]
        string CreatedBy { get; set; }
        [DataMember]
        DateTime DexRowTs { get; set; }
        [DataMember]
        int DexRowId { get; set; }
    }
}
