using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Swing
{
    public class CustomerSwingAddDnc : ICustomerSwingAddDnc
    {
        #region Properties
        public string Dnc_Status { get; set; }

        #endregion Properties
    }

    public interface ICustomerSwingAddDnc
    {
        #region Properties

        [DataMember]
        string Dnc_Status { get; set; }

        #endregion Properties

    }


}