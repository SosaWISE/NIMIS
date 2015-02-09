namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsConnextMonthlySalesDetails
	{
        int UserID { get; }
        string FirstName { get; }
        string LastName { get; }
        int? SalesYear { get; }
        int? SalesMonth { get; }
        int? RegionID { get; }
        string RegionName { get; }
        int? TeamID { get; }
        string TeamName { get; }
        int? OfficeID { get; }
        string OfficeName { get; }
        bool HasRecruits { get; }
        int? NumberCreditReportsPulled { get; }
        int? NumberCreditsPassed { get; }
        int? NumberExcellentCreditScores { get; }
        int? NumberGoodCreditScores { get; }
        int? NumberBadCreditScores { get; }
        int? AverageCreditScore { get; }
        decimal? CreditPassPercentage { get; }
        decimal? PassAndInstallPercentage { get; }
        int? NumberCancels { get; }
        int? NumberNetSales { get; }
        int? NumberPresurveys { get; }
        int? NumberPostsurveys { get; }
        int? NumberInstallations { get; }
        int? NumberSameDayInstallations { get; }
        decimal? SameDayInstallationPercentage { get; }
        int? NumberActivationsWaived { get; }
        decimal? ActivationsWaivedPercentage { get; }
        int? NumberCCPayments { get; }
        int? NumberACHPayments { get; }
        int? NumberInvoicePayments { get; }
        int? NumberSystemsOver8Points { get; }
        int? NumberFreePointsGivenBySalesRep { get; }
        int? NumberFreePointsGivenByTech { get; }
	}

}