using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Swing
{
    public class CustomerSwingInfo : ICustomerSwingInfo
	{
		#region Properties
		public string Salutation { get; set; }
        public string FirstName { get; set; }
	    public string MiddleName { get; set; }
	    public string LastName { get; set; }
	    public string Suffix { get; set; }
	    public string SSN { get; set; }
	    public DateTime? DOB { get; set; }
	    public string Email { get; set; }
	    

		#endregion Properties
	}

    public interface ICustomerSwingInfo
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