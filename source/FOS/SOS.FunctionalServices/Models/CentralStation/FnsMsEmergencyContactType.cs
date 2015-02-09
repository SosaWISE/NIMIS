using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
    public class FnsMsEmergencyContactType : IFnsMsEmergencyContactType
    {
        #region .ctor

        public FnsMsEmergencyContactType(MS_EmergencyContactType relItem)
        {
            EmergencyContactTypeID = relItem.EmergencyContactTypeID;
            MonitoringStationOSId = relItem.MonitoringStationOSId;
            MsContactTypeId = relItem.MsContactTypeId;
            ContactTypeDescription = relItem.ContactTypeDescription;
            IsActive = relItem.IsActive;
            IsDeleted = relItem.IsDeleted;
            ModifiedOn = relItem.ModifiedOn;
            ModifiedBy = relItem.ModifiedBy;
            CreatedOn = relItem.CreatedOn;
            CreatedBy = relItem.CreatedBy;
        }

        #endregion .ctor

        #region Properties
        public int EmergencyContactTypeID { get; private set; }
        public string MonitoringStationOSId { get; private set; }
        public string MsContactTypeId { get; private set; }
        public string ContactTypeDescription { get; private set; }
        public bool IsEVC { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        public string ModifiedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        #endregion Properties
    }
}
