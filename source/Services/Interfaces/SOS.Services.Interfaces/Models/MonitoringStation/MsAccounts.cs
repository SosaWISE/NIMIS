using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
    public class MsAccounts : IMsAccounts
    {
        #region Properties
        public long AccountID { get; set; }
        public string SystemTypeId { get; set; }
        public string CellularTypeId { get; set; }
		public string PanelTypeId { get; set; }
        public string AccountPassword { get; set; }
        public short? DslSeizureId { get; set; }
		public string TechId { get; set; }

        #endregion Properties
    }

    #region Interface

    public interface IMsAccounts
    {
        [DataMember]
        long AccountID { get; set; }
        [DataMember]
        string SystemTypeId { get; set; }
        [DataMember]
        string CellularTypeId { get; set; }
		[DataMember]
		string PanelTypeId { get; set; }
        [DataMember]
        string AccountPassword { get; set; }
        [DataMember]
        short? DslSeizureId { get; set; }
		[DataMember]
		string TechId { get; set; }
    }

    #endregion Interface
}
