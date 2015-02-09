using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
    public class AeCustomersConnextGetCustomerInfo : IAeCustomersConnextGetCustomerInfo
	{
        public long CustomerMasterFileID { get; set; }
        public long CustomerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneWork { get; set; }
        public string PhoneMobile { get; set; }
        public string Email { get; set; }
        public DateTime ContractDate { get; set; }
        public string AccountStatus { get; set; }
        public decimal TotalCommission { get; set; }
        public int NumberReferralsMade { get; set; }
	}

	public interface IAeCustomersConnextGetCustomerInfo

	{
        [DataMember]
        long CustomerMasterFileID { get; set; }
        [DataMember]
        long CustomerID { get; set; }
        [DataMember]
        string FirstName { get; set; }
        [DataMember]
        string MiddleName { get; set; }
        [DataMember]
        string LastName { get; set; }
        [DataMember]
        string StreetAddress { get; set; }
        [DataMember]
        string StreetAddress2 { get; set; }
        [DataMember]
        string City { get; set; }
        [DataMember]
        string State { get; set; }
        [DataMember]
        string PostalCode { get; set; }
        [DataMember]
        string PhoneHome { get; set; }
        [DataMember]
        string PhoneWork { get; set; }
        [DataMember]
        string PhoneMobile { get; set; }
        [DataMember]
        string Email { get; set; }
        [DataMember]
        DateTime ContractDate { get; set; }
        [DataMember]
        string AccountStatus { get; set; }
        [DataMember]
        decimal TotalCommission { get; set; }
        [DataMember]
        int NumberReferralsMade { get; set; }
    }
}
