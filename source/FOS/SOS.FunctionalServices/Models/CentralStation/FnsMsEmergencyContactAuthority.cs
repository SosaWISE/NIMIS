using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
    public class FnsMsEmergencyContactAuthority : IFnsMsEmergencyContactAuthority
    {
        #region .ctor

        public FnsMsEmergencyContactAuthority(MS_EmergencyContactAuthority relItem)
        {
            AuthorityID = relItem.AuthorityID;
            MonitoringStationOSId = relItem.MonitoringStationOSId;
            MsAuthorityId = relItem.MsAuthorityId;
            AuthorityDescription = relItem.AuthorityDescription;
            IsActive = relItem.IsActive;
            IsDeleted = relItem.IsDeleted;
            ModifiedOn = relItem.ModifiedOn;
            ModifiedBy = relItem.ModifiedBy;
            CreatedOn = relItem.CreatedOn;
            CreatedBy = relItem.CreatedBy;
        }

        #endregion .ctor

        #region Properties
        public int AuthorityID { get; private set; }
        public string MonitoringStationOSId { get; private set; }
        public string MsAuthorityId { get; private set; }
        public string AuthorityDescription { get; private set; }
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
