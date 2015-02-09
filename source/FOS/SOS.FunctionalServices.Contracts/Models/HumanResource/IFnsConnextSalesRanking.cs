
using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsConnextSalesRanking
	{
        int UserID { get; }
        string FirstName { get; }
        string MiddleName { get; }
        string LastName { get; }
        string PhotoURL { get; }
        DateTime PeriodEndingDate { get; }
        string ResultsType { get; }
        string RankingGroup { get; }
        string RankingPeriod { get; }
        int CurrentResults { get; }
        int CurrentSequence { get; }
        int CurrentRanking { get; }
        int PreviousResults { get; }
        int PreviousSequence { get; }
        int PreviousRanking { get; }
	}
}