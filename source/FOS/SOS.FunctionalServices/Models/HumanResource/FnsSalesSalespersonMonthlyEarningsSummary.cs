using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
    public class FnsSalesSalespersonMonthlyEarningsSummary : IFnsSalesSalespersonMonthlyEarningsSummary
    {
        #region .ctor

        public FnsSalesSalespersonMonthlyEarningsSummary(SAE_SalesSalespersonMonthlyEarningsView view)
        {
            UserID = view.UserID;
            SalesMonth = view.SalesMonth;
            SalesYear = view.SalesYear;
            SalesAmt = view.SalesAmt;
            RecurringAmt = view.RecurringAmt;
            RecruitingAmt = view.RecruitingAmt;
            BonusAmt = view.BonusAmt;
            DeductionAmt = view.DeductionAmt;
            HoldAmt = view.HoldAmt;
            TotalCommissionAmt = view.TotalCommissionAmt;
            YTDIncentiveBonusAmt = view.YTDIncentiveBonusAmt;
        }
        #endregion .ctor

        #region Properties
        public int UserID { get; private set; }
        public int SalesMonth { get; private set; }
        public int SalesYear { get; private set; }
        public decimal SalesAmt { get; private set; }
        public decimal RecurringAmt { get; private set; }
        public decimal RecruitingAmt { get; private set; }
        public decimal BonusAmt { get; private set; }
        public decimal DeductionAmt { get; private set; }
        public decimal HoldAmt { get; private set; }
        public decimal TotalCommissionAmt { get; private set; }
        public decimal YTDIncentiveBonusAmt { get; private set; }
        #endregion Properties
    }
}