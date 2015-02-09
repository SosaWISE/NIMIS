using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
 
{
    public interface IFnsSalesSalespersonMonthlyHolds
    {
        int UserID { get; }
        DateTime ContractDate { get; }
        int SalesMonth { get; }
        int SalesYear { get; }
        long CustomerMasterFileID { get; }
        long AccountID { get; }
        string CustomerFirstName { get; }
        string CustomerMiddleName { get; }
        string CustomerLastName { get; }
        string HoldName { get; }
        string HoldDescription { get; }
        decimal HoldAmt { get; }
    }
}
