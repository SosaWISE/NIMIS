using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Swing
{
    public class CustomerSwingPremiseAddress : ICustomerSwingPremiseAddress
	{
		#region Properties
		public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
	    public string City { get; set; }
	    public string County { get; set; }
	    public string PostalCode { get; set; }
	    public string State { get; set; }

	    

		#endregion Properties
	}

    public interface ICustomerSwingPremiseAddress
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