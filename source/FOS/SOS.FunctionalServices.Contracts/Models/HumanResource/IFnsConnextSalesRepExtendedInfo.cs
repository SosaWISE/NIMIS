using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsConnextSalesRepExtendedInfo
	{
        int UserID { get; }
        string FirstName { get; }
        string MiddleName { get; }
        string LastName { get; }
        string PhotoURL { get; }
        long? MLMDepth { get; }
        bool? ManagerHasOwnTeam { get; }
        string RegionName { get; }
        string OfficeName { get; }
        string TeamName { get; }
        long? CurrentNationalRank { get; }
        long? PreviousNationalRank { get; }
        long? CurrentRegionalRank { get; }
        long? PreviousRegionalRank { get; }
        long? CurrentOfficeRank { get; }
        long? PreviousOfficeRank { get; }
        long? CurrentTeamRank { get; }
        long? PreviousTeamRank { get; }
        DateTime? StartDate { get; }
        string Phone { get; }
        string Email { get; }
        string StreetAddress { get; }
        string StreetAddress2 { get; }
        string City { get; }
        string State { get; }
        string Zip { get; }
        int? WeeklySalesGoal { get; }
        int? MonthlySalesGoal { get; }
        int? YearlySalesGoal { get; }
        double? WeeklyQualityGoal { get; }
        double? MonthlyQualityGoal { get; }
        double? YearlyQualityGoal { get; }
        string License1 { get; }
        string License1URL { get; }
        string License2 { get; }
        string License2URL { get; }
        string License3 { get; }
        string License3URL { get; }
    }
}