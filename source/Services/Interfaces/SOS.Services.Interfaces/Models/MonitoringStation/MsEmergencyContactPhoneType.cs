using System.Runtime.Serialization;


namespace SOS.Services.Interfaces.Models.MonitoringStation
{
    public class MsEmergencyContactPhoneType : IMsEmergencyContactPhoneType
    {
        #region Properties
        public int PhoneTypeID { get; set; }

        public string MonitoringStationOSId { get; set; }

        public string MsPhoneTypeId { get; set; }

        public string PhoneTypeDescription { get; set; }

        #endregion Properties
    }

    #region Interface

    public interface IMsEmergencyContactPhoneType
    {
        #region Properties
        [DataMember]
        int PhoneTypeID { get; set; }

        [DataMember]
        string MonitoringStationOSId { get; set; }

        [DataMember]
        string MsPhoneTypeId { get; set; }

        [DataMember]
        string PhoneTypeDescription { get; set; }

        #endregion Properties
    }

    #endregion Interface
}
