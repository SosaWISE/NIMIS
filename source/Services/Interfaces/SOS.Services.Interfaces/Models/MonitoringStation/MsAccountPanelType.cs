using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
    public class MsAccountPanelType : IMsAccountPanelType
    {
        #region Properties
        public string PanelTypeID { get; set; }
        public string PanelTypeName { get; set; }
		public string UIName { get; set; }
        #endregion Properties
    }

    #region Interface

    public interface IMsAccountPanelType
    {
        #region Properties
        [DataMember]
        string PanelTypeID { get; set; }
        [DataMember]
        string PanelTypeName { get; set; }

		[DataMember]
		string UIName { get; set; }

        #endregion Properties
    }

    #endregion Interface
}
