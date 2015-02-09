using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Swing
{
    public interface IFnsCustomerSwingAddDnc{

        #region Properties

        [DataMember]
        string Dnc_Status { get; set; }

        #endregion Properties

    }
   
}