using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
 
{
    public interface IFnsConnextAccountList
    {
        int UserID { get; }
        long CustomerID { get; }
        string CustomerFirstName { get; }
        string CustomerMiddleName { get; }
        string CustomerLastName { get; }
        DateTime ContractDate { get; }
        string CreditQuality { get; }
        decimal ActivationFee { get; }
        int ContractLength { get; }
        string ServiceType { get; }
        decimal MonthlyPayment { get; }
        string PaymentMethod { get; }
        decimal TotalCommission { get; }
        bool IsActive { get; }
    }
}
