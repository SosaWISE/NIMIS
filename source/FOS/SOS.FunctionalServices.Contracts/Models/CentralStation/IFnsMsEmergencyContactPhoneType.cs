using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
    public interface IFnsMsEmergencyContactPhoneType
    {
        [DataMember]
        int PhoneTypeID { get; set; }

        [DataMember]
        string MonitoringStationOSId { get; set; }

        [DataMember]
        string MsPhoneTypeId { get; set; }

        [DataMember]
        string PhoneTypeDescription { get; set; }

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
    }
}
