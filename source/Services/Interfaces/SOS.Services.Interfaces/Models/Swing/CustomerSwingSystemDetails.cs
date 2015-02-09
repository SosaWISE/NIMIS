using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Swing
{
    public class CustomerSwingSystemDetails : ICustomerSwingSystemDetails
    {
        #region Properties
        public string ServiceType { get; set; }
        public string CellularType { get; set; }
        public string PassPhrase { get; set; }
        public string PanelType { get; set; }
        public string DslSeizure { get; set; }


        #endregion Properties
    }

    public interface ICustomerSwingSystemDetails
    {
        #region Properties

        [DataMember]
        string ServiceType { get; set; }

        [DataMember]
        string CellularType { get; set; }

        [DataMember]
        string PassPhrase { get; set; }

        [DataMember]
        string PanelType { get; set; }

        [DataMember]
        string DslSeizure { get; set; }


        #endregion Properties

    }


}