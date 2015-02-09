using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsConnextSalesRepExtendedInfo : IFnsConnextSalesRepExtendedInfo
	{
		#region .ctor
		public FnsConnextSalesRepExtendedInfo(RU_UsersSalesInfoConnextView view)
		{
			UserID = view.UserID;
			FirstName = view.FirstName;
			MiddleName = view.MiddleName;
			LastName = view.LastName;
            PhotoURL = view.PhotoURL;
            MLMDepth = view.MLMDepth;
			ManagerHasOwnTeam = view.ManagerHasOwnTeam;
		    RegionName = view.RegionName;
		    OfficeName = view.OfficeName;
		    TeamName = view.TeamName;
		    CurrentNationalRank = view.CurrentNationalRank;
		    PreviousNationalRank = view.PreviousNationalRank;
		    CurrentRegionalRank = view.CurrentRegionalRank;
		    PreviousRegionalRank = view.PreviousRegionalRank;
		    CurrentOfficeRank = view.CurrentOfficeRank;
		    PreviousOfficeRank = view.PreviousOfficeRank;
		    CurrentTeamRank = view.CurrentTeamRank;
		    PreviousTeamRank = view.PreviousTeamRank;
		    StartDate = view.StartDate;
		    Email = view.Email;

		}

		public FnsConnextSalesRepExtendedInfo(RU_UsersSalesInfoExtendedConnextView view)
		{
    	    UserID = view.UserID;
	        FirstName = view.FirstName;
	        MiddleName = view.MiddleName;
	        LastName = view.LastName;
	        PhotoURL = view.PhotoURL;
	        MLMDepth = view.MLMDepth;
	        ManagerHasOwnTeam = view.ManagerHasOwnTeam;
	        RegionName = view.RegionName;
	        OfficeName = view.OfficeName;
	        TeamName = view.TeamName;
	        CurrentNationalRank = view.CurrentNationalRank;
	        PreviousNationalRank = view.PreviousNationalRank;
	        CurrentRegionalRank = view.CurrentRegionalRank;
	        PreviousRegionalRank = view.PreviousRegionalRank;
	        CurrentOfficeRank = view.CurrentOfficeRank;
	        PreviousOfficeRank = view.PreviousOfficeRank;
	        CurrentTeamRank = view.CurrentTeamRank;
	        PreviousTeamRank = view.PreviousTeamRank;
	        StartDate = view.StartDate;
	        Phone = view.Phone;
	        Email = view.Email;
	        StreetAddress = view.StreetAddress;
	        StreetAddress2 = view.StreetAddress2;
	        City = view.City;
	        State = view.State;
	        Zip = view.Zip;
	        WeeklySalesGoal = view.WeeklySalesGoal;
	        MonthlySalesGoal = view.MonthlySalesGoal;
	        YearlySalesGoal = view.YearlySalesGoal;
	        WeeklyQualityGoal = view.WeeklyQualityGoal;
	        MonthlyQualityGoal = view.MonthlyQualityGoal;
	        YearlyQualityGoal = view.YearlyQualityGoal;
	        License1 = view.License1;
	        License1URL = view.License1URL;
	        License2 = view.License2;
	        License2URL = view.License2URL;
	        License3 = view.License3;
	        License3URL = view.License3URL;

		}

		#endregion .ctor

		#region Properties
		public int UserID { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string PhotoURL { get; private set; }
        public long? MLMDepth { get; private set; }
        public bool? ManagerHasOwnTeam { get; private set; }
        public string RegionName { get; private set; }
        public string OfficeName { get; private set; }
        public string TeamName { get; private set; }
        public long? CurrentNationalRank { get; private set; }
        public long? PreviousNationalRank { get; private set; }
        public long? CurrentRegionalRank { get; private set; }
        public long? PreviousRegionalRank { get; private set; }
        public long? CurrentOfficeRank { get; private set; }
        public long? PreviousOfficeRank { get; private set; }
        public long? CurrentTeamRank { get; private set; }
        public long? PreviousTeamRank { get; private set; }
        public DateTime? StartDate { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string StreetAddress { get; private set; }
        public string StreetAddress2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }
        public int? WeeklySalesGoal { get; private set; }
        public int? MonthlySalesGoal { get; private set; }
        public int? YearlySalesGoal { get; private set; }
        public double? WeeklyQualityGoal { get; private set; }
        public double? MonthlyQualityGoal { get; private set; }
        public double? YearlyQualityGoal { get; private set; }
        public string License1 { get; private set; }
        public string License1URL { get; private set; }
        public string License2 { get; private set; }
        public string License2URL { get; private set; }
        public string License3 { get; private set; }
        public string License3URL { get; private set; }
    }
    #endregion Properties
}
