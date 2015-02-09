using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Swing
{
    public interface IFnsCustomerSwingPremiseAddress
	{
		#region Properties

        [DataMember]
        string StreetAddress1 { get; set; }

        [DataMember]
        string StreetAddress2 { get; set; }

        [DataMember]
        string City { get; set; }

        [DataMember]
        string County { get; set; }

        [DataMember]
        string PostalCode { get; set; }

        [DataMember]
        string State { get; set; }

		#endregion Properties
		 
	}
}