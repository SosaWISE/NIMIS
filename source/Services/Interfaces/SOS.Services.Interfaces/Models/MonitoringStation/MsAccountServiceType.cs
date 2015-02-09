using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
    public class MsAccountServiceType : IMsAccountServiceType
    {
        #region Properties
        public string SystemTypeID { get; set; }
        public string SystemTypeName { get; set; }
        #endregion Properties
    }

    #region Interface

    public interface IMsAccountServiceType
    {
        #region Properties
        [DataMember]
        string SystemTypeID { get; set; }
        [DataMember]
        string SystemTypeName { get; set; }

        #endregion Properties
    }

    #endregion Interface
}
