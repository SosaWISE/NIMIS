using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.Services.Interfaces.Models.HumanResources;

namespace SOS.FunctionalServices.Models.HumanResource
{
    public class FnsSalesSalespersonMonthlyCommissions : IFnsSalesSalespersonMonthlyCommissions
    {
        #region .ctor

        public FnsSalesSalespersonMonthlyCommissions(SAE_SalesSalespersonMonthlyCommissionsView view)
        {
	        UserID = view.UserID;
	        ContractDate = view.ContractDate;
	        SalesMonth = view.SalesMonth;
	        SalesYear = view.SalesYear;
	        CustomerMasterFileID = view.CustomerMasterFileID;
	        CustomerFirstName = view.CustomerFirstName;
	        CustomerMiddleName = view.CustomerMiddleName;
	        CustomerLastName = view.CustomerLastName;
	        CreditRating = view.CreditRating;
	        ActivationFeeAmt = view.ActivationFeeAmt;
	        ContractLength = view.ContractLength;
	        ServiceType = view.ServiceType;
	        MonthlyPaymentAmt = view.MonthlyPaymentAmt;
	        PaymentMethod = view.PaymentMethod;
	        SalesCommissionAmt = view.SalesCommissionAmt;
	        RecurringCommissionAmt = view.RecurringCommissionAmt;
	        IsActive = view.isActive;
        }
        #endregion .ctor

        #region Properties
        public int UserID { get; private set; }
        public DateTime ContractDate { get; private set; }
        public int SalesMonth { get; private set; }
        public int SalesYear { get; private set; }
        public long CustomerMasterFileID { get; private set; }
        public long AccountId { get; private set; }
        public string CustomerFirstName { get; private set; }
        public string CustomerMiddleName { get; private set; }
        public string CustomerLastName { get; private set; }
        public string CreditRating { get; private set; }
        public decimal ActivationFeeAmt { get; private set; }
        public int ContractLength { get; private set; }
        public string ServiceType { get; private set; }
        public decimal MonthlyPaymentAmt { get; private set; }
        public string PaymentMethod { get; private set; }
        public decimal SalesCommissionAmt { get; private set; }
        public decimal RecurringCommissionAmt { get; private set; }
        public bool IsActive { get; private set; }

        #endregion Properties   
    }

}