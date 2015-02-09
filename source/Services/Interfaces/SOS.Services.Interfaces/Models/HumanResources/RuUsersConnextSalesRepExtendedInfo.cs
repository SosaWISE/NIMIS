using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
    public class RuUsersConnextSalesRepExtendedInfo : IRuUsersConnextSalesRepExtendedInfo

    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhotoURL { get; set; }
        public long? MLMDepth { get; set; }
        public bool? ManagerHasOwnTeam { get; set; }
        public string RegionName { get; set; }
        public string OfficeName { get; set; }
        public string TeamName { get; set; }
        public long? CurrentNationalRank { get; set; }
        public long? PreviousNationalRank { get; set; }
        public long? CurrentRegionalRank { get; set; }
        public long? PreviousRegionalRank { get; set; }
        public long? CurrentOfficeRank { get; set; }
        public long? PreviousOfficeRank { get; set; }
        public long? CurrentTeamRank { get; set; }
        public long? PreviousTeamRank { get; set; }
        public DateTime? StartDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? WeeklySalesGoal { get; set; }
        public int? MonthlySalesGoal { get; set; }
        public int? YearlySalesGoal { get; set; }
        public double? WeeklyQualityGoal { get; set; }
        public double? MonthlyQualityGoal { get; set; }
        public double? YearlyQualityGoal { get; set; }
        public string License1 { get; set; }
        public string License1URL { get; set; }
        public string License2 { get; set; }
        public string License2URL { get; set; }
        public string License3 { get; set; }
        public string License3URL { get; set; }

    }

    public interface IRuUsersConnextSalesRepExtendedInfo
    {
        [DataMember]
        int UserID { get; set; }
        [DataMember]
        string FirstName { get; set; }
        [DataMember]
        string MiddleName { get; set; }
        [DataMember]
        string LastName { get; set; }
        [DataMember]
        string PhotoURL { get; set; }
        [DataMember]
        long? MLMDepth { get; set; }
        [DataMember]
        bool? ManagerHasOwnTeam { get; set; }
        [DataMember]
        string RegionName { get; set; }
        [DataMember]
        string OfficeName { get; set; }
        [DataMember]
        string TeamName { get; set; }
        [DataMember]
        long? CurrentNationalRank { get; set; }
        [DataMember]
        long? PreviousNationalRank { get; set; }
        [DataMember]
        long? CurrentRegionalRank { get; set; }
        [DataMember]
        long? PreviousRegionalRank { get; set; }
        [DataMember]
        long? CurrentOfficeRank { get; set; }
        [DataMember]
        long? PreviousOfficeRank { get; set; }
        [DataMember]
        long? CurrentTeamRank { get; set; }
        [DataMember]
        long? PreviousTeamRank { get; set; }
        [DataMember]
        DateTime? StartDate { get; set; }
        [DataMember]
        string Phone { get; set; }
        [DataMember]
        string Email { get; set; }
        [DataMember]
        string StreetAddress { get; set; }
        [DataMember]
        string StreetAddress2 { get; set; }
        [DataMember]
        string City { get; set; }
        [DataMember]
        string State { get; set; }
        [DataMember]
        string Zip { get; set; }
        [DataMember]
        int? WeeklySalesGoal { get; set; }
        [DataMember]
        int? MonthlySalesGoal { get; set; }
        [DataMember]
        int? YearlySalesGoal { get; set; }
        [DataMember]
        double? WeeklyQualityGoal { get; set; }
        [DataMember]
        double? MonthlyQualityGoal { get; set; }
        [DataMember]
        double? YearlyQualityGoal { get; set; }
        [DataMember]
        string License1 { get; set; }
        [DataMember]
        string License1URL { get; set; }
        [DataMember]
        string License2 { get; set; }
        [DataMember]
        string License2URL { get; set; }
        [DataMember]
        string License3 { get; set; }
        [DataMember]
        string License3URL { get; set; }

    }

}
