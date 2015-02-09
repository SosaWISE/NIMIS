using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Swing
{
    public interface IFnsCustomerSwingInfo
	{
		#region Properties

        [DataMember]
        string Salutation { get; set; }

        [DataMember]
        string FirstName { get; set; }

        [DataMember]
        string MiddleName { get; set; }

        [DataMember]
        string LastName { get; set; }

        [DataMember]
        string Suffix { get; set; }

        [DataMember]
        string SSN { get; set; }

        [DataMember]
        DateTime? DOB { get; set; }

        [DataMember]
        string Email { get; set; }

		#endregion Properties
		 
	}
}