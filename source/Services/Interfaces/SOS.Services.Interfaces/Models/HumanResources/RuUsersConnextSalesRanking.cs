using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
	public class RuUsersConnextSalesRanking : IRuUsersConnextSalesRanking
	{
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhotoURL { get; set; }
        public DateTime PeriodEndingDate { get; set; }
        public string ResultsType { get; set; }
        public string RankingGroup { get; set; }
        public string RankingPeriod { get; set; }
        public int CurrentResults { get; set; }
        public int CurrentSequence { get; set; }
        public int CurrentRanking { get; set; }
        public int PreviousResults { get; set; }
        public int PreviousSequence { get; set; }
        public int PreviousRanking { get; set; }
	}

	public interface IRuUsersConnextSalesRanking
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
        DateTime PeriodEndingDate { get; set; }
        [DataMember]
        string ResultsType { get; set; }
        [DataMember]
        string RankingGroup { get; set; }
        [DataMember]
        string RankingPeriod { get; set; }
        [DataMember]
        int CurrentResults { get; set; }
        [DataMember]
        int CurrentSequence { get; set; }
        [DataMember]
        int CurrentRanking { get; set; }
        [DataMember]
        int PreviousResults { get; set; }
        [DataMember]
        int PreviousSequence { get; set; }
        [DataMember]
        int PreviousRanking { get; set; }
    }
}
