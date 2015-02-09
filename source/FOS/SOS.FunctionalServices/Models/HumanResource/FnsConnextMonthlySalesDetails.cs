using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
    public class FnsConnextMonthlySalesDetails : IFnsConnextMonthlySalesDetails
    {
        #region .ctor

        public FnsConnextMonthlySalesDetails(RU_UsersGetDetailedStatisticsConnextView view)
        {
            UserID = view.UserID;
            FirstName = view.FirstName;
            LastName = view.LastName;
            SalesYear = view.SalesYear;
            SalesMonth = view.SalesMonth;
            RegionID = view.RegionID;
            RegionName = view.RegionName;
            TeamID = view.TeamID;
            TeamName = view.TeamName;
            OfficeID = view.OfficeID;
            OfficeName = view.OfficeName;
            HasRecruits = view.HasRecruits;
            NumberCreditReportsPulled = view.NumberCreditReportsPulled;
            NumberCreditsPassed = view.NumberCreditsPassed;
            NumberExcellentCreditScores = view.NumberExcellentCreditScores;
            NumberGoodCreditScores = view.NumberGoodCreditScores;
            NumberBadCreditScores = view.NumberBadCreditScores;
            AverageCreditScore = view.AverageCreditScore;
            CreditPassPercentage = view.CreditPassPercentage;
            PassAndInstallPercentage = view.PassAndInstallPercentage;
            NumberCancels = view.NumberCancels;
            NumberNetSales = view.NumberNetSales;
            NumberPresurveys = view.NumberPresurveys;
            NumberPostsurveys = view.NumberPostsurveys;
            NumberInstallations = view.NumberInstallations;
            NumberSameDayInstallations = view.NumberSameDayInstallations;
            SameDayInstallationPercentage = view.SameDayInstallationPercentage;
            NumberActivationsWaived = view.NumberActivationsWaived;
            ActivationsWaivedPercentage = view.ActivationsWaivedPercentage;
            NumberCCPayments = view.NumberCCPayments;
            NumberACHPayments = view.NumberACHPayments;
            NumberInvoicePayments = view.NumberInvoicePayments;
            NumberSystemsOver8Points = view.NumberSystemsOver8Points;
            NumberFreePointsGivenBySalesRep = view.NumberFreePointsGivenBySalesRep;
            NumberFreePointsGivenByTech = view.NumberFreePointsGivenByTech;

        }

        #endregion .ctor

        #region Properties
        public int UserID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int? SalesYear { get; private set; }
        public int? SalesMonth { get; private set; }
        public int? RegionID { get; private set; }
        public string RegionName { get; private set; }
        public int? TeamID { get; private set; }
        public string TeamName { get; private set; }
        public int? OfficeID { get; private set; }
        public string OfficeName { get; private set; }
        public bool HasRecruits { get; private set; }
        public int? NumberCreditReportsPulled { get; private set; }
        public int? NumberCreditsPassed { get; private set; }
        public int? NumberExcellentCreditScores { get; private set; }
        public int? NumberGoodCreditScores { get; private set; }
        public int? NumberBadCreditScores { get; private set; }
        public int? AverageCreditScore { get; private set; }
        public decimal? CreditPassPercentage { get; private set; }
        public decimal? PassAndInstallPercentage { get; private set; }
        public int? NumberCancels { get; private set; }
        public int? NumberNetSales { get; private set; }
        public int? NumberPresurveys { get; private set; }
        public int? NumberPostsurveys { get; private set; }
        public int? NumberInstallations { get; private set; }
        public int? NumberSameDayInstallations { get; private set; }
        public decimal? SameDayInstallationPercentage { get; private set; }
        public int? NumberActivationsWaived { get; private set; }
        public decimal? ActivationsWaivedPercentage { get; private set; }
        public int? NumberCCPayments { get; private set; }
        public int? NumberACHPayments { get; private set; }
        public int? NumberInvoicePayments { get; private set; }
        public int? NumberSystemsOver8Points { get; private set; }
        public int? NumberFreePointsGivenBySalesRep { get; private set; }
        public int? NumberFreePointsGivenByTech { get; private set; }
        #endregion Properties
    }

}
