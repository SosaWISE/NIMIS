using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
    public class SAESalesSalespersonMonthlyEarnings : ISAESalesSalespersonMonthlyEarnings
	{
        public int UserID { get; set; }
        public int SalesMonth { get; set; }
        public int SalesYear { get; set; }
        public decimal SalesAmt { get; set; }
        public decimal RecurringAmt { get; set; }
        public decimal RecruitingAmt { get; set; }
        public decimal BonusAmt { get; set; }
        public decimal DeductionAmt { get; set; }
        public decimal HoldAmt { get; set; }
        public decimal TotalCommissionAmt { get; set; }
        public decimal YTDIncentiveBonusAmt { get; set; }
    }

	public interface ISAESalesSalespersonMonthlyEarnings

	{
        [DataMember]
        int UserID { get; set; }
        [DataMember]
        int SalesMonth { get; set; }
        [DataMember]
        int SalesYear { get; set; }
        [DataMember]
        decimal SalesAmt { get; set; }
        [DataMember]
        decimal RecurringAmt { get; set; }
        [DataMember]
        decimal RecruitingAmt { get; set; }
        [DataMember]
        decimal BonusAmt { get; set; }
        [DataMember]
        decimal DeductionAmt { get; set; }
        [DataMember]
        decimal HoldAmt { get; set; }
        [DataMember]
        decimal TotalCommissionAmt { get; set; }
        [DataMember]
        decimal YTDIncentiveBonusAmt { get; set; }
    }
}
