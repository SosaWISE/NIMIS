using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
    public interface IFnsMsEmergencyContactAuthority
    {
        [DataMember]
        int AuthorityID { get; }

        [DataMember]
        string MonitoringStationOSId { get; }

        [DataMember]
        string MsAuthorityId { get; }

        [DataMember]
        string AuthorityDescription { get; }

        [DataMember]
        bool IsActive { get; }

        [DataMember]
        bool IsDeleted { get; }

        [DataMember]
        DateTime ModifiedOn { get; }

        [DataMember]
        string ModifiedBy { get; }

        [DataMember]
        DateTime CreatedOn { get; }

        [DataMember]
        string CreatedBy { get; }
    }
}