using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Swing
{
    public interface IFnsCustomerSwingEmergencyContact
    {

        #region Properties

        [DataMember]
        string FirstName { get; set; }

        [DataMember]
        string MiddleInit { get; set; }

        [DataMember]
        string LastName { get; set; }

        [DataMember]
        string Relationship { get; set; }

        [DataMember]
        string PhoneNumber1 { get; set; }

        #endregion Properties

    }
}
