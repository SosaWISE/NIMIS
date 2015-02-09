using SOS.Data.HumanResource;
using SOS.Lib.Core.CreditReportService;

namespace NSE.FOS.RunCreditServices.Models
{
	public class WSSeason : IWSSeason
	{
		#region .ctor

		public WSSeason(RU_Season season)
		{
			SeasonID = season.SeasonID;
			PreSeasonID = season.SeasonID;
			SeasonName = season.SeasonName;
			ExcellentCreditScoreThreshold = season.ExcellentCreditScoreThreshold;
			PassCreditScoreThreshold = season.PassCreditScoreThreshold;
			SubCreditScoreThreshold = season.SubCreditScoreThreshold;
		}
		#endregion .ctor

		#region Properties
		public int SeasonID { get; set; }
		public int PreSeasonID { get; set; }
		public string SeasonName { get; set; }
		public int ExcellentCreditScoreThreshold { get; set; }
		public int PassCreditScoreThreshold { get; set; }
		public int SubCreditScoreThreshold { get; set; }
		#endregion Properties
	}
}
