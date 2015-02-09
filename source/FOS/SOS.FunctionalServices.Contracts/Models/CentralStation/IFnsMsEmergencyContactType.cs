using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
    public interface IFnsMsEmergencyContactType
    {
        [DataMember]
        int EmergencyContactTypeID { get; }

        [DataMember]
        string MonitoringStationOSId { get; }

        [DataMember]
        string MsContactTypeId { get; }

        [DataMember]
        string ContactTypeDescription { get; }

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