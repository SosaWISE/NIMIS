using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsConnextSalesRanking : IFnsConnextSalesRanking
	{
		#region .ctor

		//public FnsConnextSalesRanking (RU_UsersSalesRankingConnextView view)
		//{
		//	UserID = view.UserID;
		//	FirstName = view.FirstName;
		//	MiddleName = view.MiddleName;
		//	LastName = view.LastName;
		//	PhotoURL = view.PhotoURL;
		//	PeriodEndingDate = view.PeriodEndingDate;
		//	ResultsType = view.ResultsType;
		//	RankingGroup = view.RankingGroup;
		//	RankingPeriod = view.RankingPeriod;
		//	CurrentResults = view.CurrentResults;
		//	CurrentSequence = view.CurrentSequence;
		//	CurrentRanking = view.CurrentRanking;
		//	PreviousResults = view.PreviousResults;
		//	PreviousSequence = view.PreviousSequence;
		//	PreviousRanking = view.PreviousRanking;

		//}

		#endregion .ctor

		#region Properties
        public int UserID { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string PhotoURL { get; private set; }
        public DateTime PeriodEndingDate { get; private set; }
        public string ResultsType { get; private set; }
        public string RankingGroup { get; private set; }
        public string RankingPeriod { get; private set; }
        public int CurrentResults { get; private set; }
        public int CurrentSequence { get; private set; }
        public int CurrentRanking { get; private set; }
        public int PreviousResults { get; private set; }
        public int PreviousSequence { get; private set; }
        public int PreviousRanking { get; private set; }
		#endregion Properties
	}
}
