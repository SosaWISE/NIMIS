namespace SOS.Lib.Core.CreditReportService
{
	public interface IWSSeason
	{
		int SeasonID { get; set; }
		int PreSeasonID { get; set; }
		string SeasonName { get; set; }
		int ExcellentCreditScoreThreshold { get; set; }
		int PassCreditScoreThreshold { get; set; }
		int SubCreditScoreThreshold { get; set; }
	}
}