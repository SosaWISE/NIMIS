using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
 
{
    public interface IFnsConnextCustomerInfo
    {
        long CustomerMasterFileID { get; }
        long CustomerID { get; }
        string FirstName { get; }
        string MiddleName { get; }
        string LastName { get; }
        string StreetAddress { get; }
        string StreetAddress2 { get; }
        string City { get; }
        string State { get; }
        string PostalCode { get; }
        string PhoneHome { get; }
        string PhoneWork { get; }
        string PhoneMobile { get; }
        string Email { get; }
        DateTime ContractDate { get; }
        string AccountStatus { get; }
        decimal TotalCommission { get; }
        int NumberReferralsMade { get; }
    }
}
