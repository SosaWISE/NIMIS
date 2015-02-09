using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
    public class RuUsersConnextGetDetailedStatistics : IRuUsersGetDetailedStatisticsConnext
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? SalesYear { get; set; }
        public int? SalesMonth { get; set; }
        public int? RegionID { get; set; }
        public string RegionName { get; set; }
        public int? TeamID { get; set; }
        public string TeamName { get; set; }
        public int? OfficeID { get; set; }
        public string OfficeName { get; set; }
        public bool? HasRecruits { get; set; }
        public int? NumberCreditReportsPulled { get; set; }
        public int? NumberCreditsPassed { get; set; }
        public int? NumberExcellentCreditScores { get; set; }
        public int? NumberGoodCreditScores { get; set; }
        public int? NumberBadCreditScores { get; set; }
        public int? AverageCreditScore { get; set; }
        public decimal? CreditPassPercentage { get; set; }
        public decimal? PassAndInstallPercentage { get; set; }
        public int? NumberCancels { get; set; }
        public int? NumberNetSales { get; set; }
        public int? NumberPresurveys { get; set; }
        public int? NumberPostsurveys { get; set; }
        public int? NumberInstallations { get; set; }
        public int? NumberSameDayInstallations { get; set; }
        public decimal? SameDayInstallationPercentage { get; set; }
        public int? NumberActivationsWaived { get; set; }
        public decimal? ActivationsWaivedPercentage { get; set; }
        public int? NumberCCPayments { get; set; }
        public int? NumberACHPayments { get; set; }
        public int? NumberInvoicePayments { get; set; }
        public int? NumberSystemsOver8Points { get; set; }
        public int? NumberFreePointsGivenBySalesRep { get; set; }
        public int? NumberFreePointsGivenByTech { get; set; }
	}

    public interface IRuUsersGetDetailedStatisticsConnext
	{
        [DataMember]
        int UserID { get; set; }
        [DataMember]
        string FirstName { get; set; }
        [DataMember]
        string LastName { get; set; }
        [DataMember]
        int? SalesYear { get; set; }
        [DataMember]
        int? SalesMonth { get; set; }
        [DataMember]
        int? RegionID { get; set; }
        [DataMember]
        string RegionName { get; set; }
        [DataMember]
        int? TeamID { get; set; }
        [DataMember]
        string TeamName { get; set; }
        [DataMember]
        int? OfficeID { get; set; }
        [DataMember]
        string OfficeName { get; set; }
        [DataMember]
        bool? HasRecruits { get; set; }
        [DataMember]
        int? NumberCreditReportsPulled { get; set; }
        [DataMember]
        int? NumberCreditsPassed { get; set; }
        [DataMember]
        int? NumberExcellentCreditScores { get; set; }
        [DataMember]
        int? NumberGoodCreditScores { get; set; }
        [DataMember]
        int? NumberBadCreditScores { get; set; }
        [DataMember]
        int? AverageCreditScore { get; set; }
        [DataMember]
        decimal? CreditPassPercentage { get; set; }
        [DataMember]
        decimal? PassAndInstallPercentage { get; set; }
        [DataMember]
        int? NumberCancels { get; set; }
        [DataMember]
        int? NumberNetSales { get; set; }
        [DataMember]
        int? NumberPresurveys { get; set; }
        [DataMember]
        int? NumberPostsurveys { get; set; }
        [DataMember]
        int? NumberInstallations { get; set; }
        [DataMember]
        int? NumberSameDayInstallations { get; set; }
        [DataMember]
        decimal? SameDayInstallationPercentage { get; set; }
        [DataMember]
        int? NumberActivationsWaived { get; set; }
        [DataMember]
        decimal? ActivationsWaivedPercentage { get; set; }
        [DataMember]
        int? NumberCCPayments { get; set; }
        [DataMember]
        int? NumberACHPayments { get; set; }
        [DataMember]
        int? NumberInvoicePayments { get; set; }
        [DataMember]
        int? NumberSystemsOver8Points { get; set; }
        [DataMember]
        int? NumberFreePointsGivenBySalesRep { get; set; }
        [DataMember]
        int? NumberFreePointsGivenByTech { get; set; }
    }
}
