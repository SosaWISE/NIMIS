using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
	public class RuUsersConnextSalesRepInfo : IRuUsersConnextSalesRepInfo
	{
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhotoURL { get; set; }
        public long? MLMDepth { get; set; }
        public bool? ManagerHasOwnTeam { get; set; }
        public string RegionName { get;  set; }
        public string OfficeName { get;  set; }
        public string TeamName { get;  set; }
        public long? CurrentNationalRank { get;  set; }
        public long? PreviousNationalRank { get;  set; }
        public long? CurrentRegionalRank { get;  set; }
        public long? PreviousRegionalRank { get;  set; }
        public long? CurrentOfficeRank { get;  set; }
        public long? PreviousOfficeRank { get;  set; }
        public long? CurrentTeamRank { get;  set; }
        public long? PreviousTeamRank { get;  set; }
        public DateTime? StartDate { get;  set; }
        public string Email { get;  set; }

	}

	public interface IRuUsersConnextSalesRepInfo
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
        string Email { get; set; }

    }
}
