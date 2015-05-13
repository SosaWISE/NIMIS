using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsConnextSalesRepInfo : IFnsConnextSalesRepInfo
	{
		#region .ctor

		//public FnsConnextSalesRepInfo(RU_UsersSalesInfoConnextView view)
		//{
		//	UserID = view.UserID;
		//	FirstName = view.FirstName;
		//	MiddleName = view.MiddleName;
		//	LastName = view.LastName;
		//	PhotoURL = view.PhotoURL;
		//	MLMDepth = view.MLMDepth;
		//	ManagerHasOwnTeam = view.ManagerHasOwnTeam;
		//	RegionName = view.RegionName;
		//	OfficeName = view.OfficeName;
		//	TeamName = view.TeamName;
		//	CurrentNationalRank = view.CurrentNationalRank;
		//	PreviousNationalRank = view.PreviousNationalRank;
		//	CurrentRegionalRank = view.CurrentRegionalRank;
		//	PreviousRegionalRank = view.PreviousRegionalRank;
		//	CurrentOfficeRank = view.CurrentOfficeRank;
		//	PreviousOfficeRank = view.PreviousOfficeRank;
		//	CurrentTeamRank = view.CurrentTeamRank;
		//	PreviousTeamRank = view.PreviousTeamRank;
		//	StartDate = view.StartDate;
		//	Email = view.Email;

		//}

		#endregion .ctor

		#region Properties
		public int UserID  { get; private set; }
		public string FirstName  { get; private set; }
		public string MiddleName  { get; private set; }
		public string LastName  { get; private set; }
        public string PhotoURL  { get; private set; }
		public long? MLMDepth  { get; private set; }
		public bool? ManagerHasOwnTeam  { get; private set; }
	    public string RegionName  { get; private set; }
        public string OfficeName  { get; private set; }
		public string TeamName  { get; private set; }
	    public long? CurrentNationalRank  { get; private set; }
        public long? PreviousNationalRank  { get; private set; }
        public long? CurrentRegionalRank  { get; private set; }
	    public long? PreviousRegionalRank  { get; private set; }
	    public long? CurrentOfficeRank  { get; private set; }
	    public long? PreviousOfficeRank  { get; private set; }
	    public long? CurrentTeamRank  { get; private set; }
	    public long? PreviousTeamRank  { get; private set; }
        public DateTime? StartDate  { get; private set; }
		public string Email  { get; private set; }
		#endregion Properties
	}
}
