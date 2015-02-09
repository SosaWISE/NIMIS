using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
    public class MsEmergencyContactType : IMsEmergencyContactType
    {
        #region Properties
        public int EmergencyContactTypeID { get; set; }
        public string MonitoringStationOSId { get; set; }
        public string MsContactTypeId { get; set; }
        public string ContactTypeDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        #endregion Properties
    }

    public interface IMsEmergencyContactType
    {
        [DataMember]
        int EmergencyContactTypeID { get; set; }

        [DataMember]
        string MonitoringStationOSId { get; set; }

        [DataMember]
        string MsContactTypeId { get; set; }

        [DataMember]
        string ContactTypeDescription { get; set; }

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

    }
}
