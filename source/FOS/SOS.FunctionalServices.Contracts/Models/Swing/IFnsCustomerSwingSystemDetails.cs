using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Swing
{
    public interface IFnsCustomerSwingSystemDetails
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