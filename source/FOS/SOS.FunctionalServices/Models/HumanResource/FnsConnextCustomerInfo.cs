using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
    public class FnsConnextCustomerInfo : IFnsConnextCustomerInfo
    {
        #region .ctor

        public FnsConnextCustomerInfo(AE_CustomersGetCustomerInfoConnextView view)
        {
            CustomerMasterFileID = view.CustomerMasterFileID;
            CustomerID = view.CustomerID;
            FirstName = view.FirstName;
            MiddleName = view.MiddleName;
            LastName = view.LastName;
            StreetAddress = view.StreetAddress;
            StreetAddress2 = view.StreetAddress2;
            City = view.City;
            State = view.State;
            PostalCode = view.PostalCode;
            PhoneHome = view.PhoneHome;
            PhoneWork = view.PhoneWork;
            PhoneMobile = view.PhoneMobile;
            Email = view.Email;
            ContractDate = view.ContractDate;
            AccountStatus = view.AccountStatus;
            TotalCommission = view.TotalCommission;
            NumberReferralsMade = view.NumberReferralsMade;
        }
        #endregion .ctor

        #region Properties

        public long CustomerMasterFileID { get; private set; }
        public long CustomerID { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string StreetAddress { get; private set; }
        public string StreetAddress2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string PhoneHome { get; private set; }
        public string PhoneWork { get; private set; }
        public string PhoneMobile { get; private set; }
        public string Email { get; private set; }
        public DateTime ContractDate { get; private set; }
        public string AccountStatus { get; private set; }
        public decimal TotalCommission { get; private set; }
        public int NumberReferralsMade { get; private set; }

        #endregion Properties   
    }

}