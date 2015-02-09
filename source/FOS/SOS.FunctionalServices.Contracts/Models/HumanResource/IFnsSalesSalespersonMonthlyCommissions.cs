using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
 
{
    public interface IFnsSalesSalespersonMonthlyCommissions
    {
        int UserID { get; }
        DateTime ContractDate { get; }
        int SalesMonth { get; }
        int SalesYear { get; }
        long CustomerMasterFileID { get; }
        long AccountId { get; }
        string CustomerFirstName { get; }
        string CustomerMiddleName { get; }
        string CustomerLastName { get; }
        string CreditRating { get; }
        decimal ActivationFeeAmt { get; }
        int ContractLength { get; }
        string ServiceType { get; }
        decimal MonthlyPaymentAmt { get; }
        string PaymentMethod { get; }
        decimal SalesCommissionAmt { get; }
        decimal RecurringCommissionAmt { get; }
        bool IsActive { get; }
    }
}
