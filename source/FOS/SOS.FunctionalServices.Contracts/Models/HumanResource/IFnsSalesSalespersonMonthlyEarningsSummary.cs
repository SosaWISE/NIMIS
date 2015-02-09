namespace SOS.FunctionalServices.Contracts.Models.HumanResource
 
{
    public interface IFnsSalesSalespersonMonthlyEarningsSummary
    {
        int UserID { get; }
        int SalesMonth { get; }
        int SalesYear { get; }
        decimal SalesAmt { get; }
        decimal RecurringAmt { get; }
        decimal RecruitingAmt { get; }
        decimal BonusAmt { get; }
        decimal DeductionAmt { get; }
        decimal HoldAmt { get; }
        decimal TotalCommissionAmt { get; }
        decimal YTDIncentiveBonusAmt { get; }
    }
}
