using System.Runtime.Serialization;
using SOS.Data.HumanResource;
using SOS.Lib.Util;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class RecruitsHistory
	{
		public RecruitsHistory(HumanResourceDataContext recruitDB, SOS.Data.SosCrm.SosCrmDataContext interimDB, RU_RecruitsHistory item)
		{
			RecruitHistoryID = item.RecruitHistoryID;
			HistoryDate = HistoryHelper.GetDateAndTimeText(item.HistoryDate);

			RecruitID = item.RecruitID;
			UserID = item.UserID;

			UserType = HistoryHelper.GetUserTypeDisplayName(recruitDB, item.UserTypeId);
			ReportsTo = HistoryHelper.GetRecruitFullName(recruitDB, item.ReportsToID);

			if (item.StateId != null)
			{
				State = interimDB.MC_PoliticalStates.LoadByPrimaryKey(item.StateId).StateName;
			}
			if (item.CountryId != null)
			{
				Country = interimDB.MC_PoliticalCountries.LoadByPrimaryKey(item.CountryId).CountryName;
			}
			StreetAddress = item.StreetAddress;
			StreetAddress2 = item.StreetAddress2;
			City = item.City;
			PostalCode = item.PostalCode;

			Season = HistoryHelper.GetSeasonDisplayName(recruitDB, item.SeasonID);
			OwnerApproval = HistoryHelper.GetUserFullName(recruitDB, item.OwnerApprovalId);
			Team = HistoryHelper.GetTeamDisplayName(recruitDB, item.TeamID);
			PayScale = HistoryHelper.GetPayScaleDisplayName(recruitDB, item.PayScaleID);
			School = HistoryHelper.GetSchoolDisplayName(recruitDB, item.SchoolId);

			Buddy = HistoryHelper.GetRecruitFullName(recruitDB, item.ShackingUpId);
			RecruitCohabbitType = HistoryHelper.GetRecruitCohabbitTypeDisplayName(recruitDB, item.RecruitCohabbitTypeId);
			AlternatePayScheduleID = item.AlternatePayScheduleID;
			Location = item.Location;
			OwnerApprovalDate = HistoryHelper.GetDateOnlyText(item.OwnerApprovalDate);
			ManagerApprovalDate = HistoryHelper.GetDateOnlyText(item.ManagerApprovalDate);
			EmergencyName = item.EmergencyName;
			EmergencyPhone = StringUtility.FormatPhoneNumber(item.EmergencyPhone);
			EmergencyRelationship = item.EmergencyRelationship;
			RecruiterStatus = item.IsRecruiter ? "Recruiter" : "Non recruiter";
			PreviousSummer = item.PreviousSummer;
			SignatureDate = HistoryHelper.GetDateOnlyText(item.SignatureDate);

			//this.GPW4Allowances = item.GPW4Allowances;
			//this.GPW9Name = item.GPW9Name;
			//this.GPW9BusinessName = item.GPW9BusinessName;
			//this.GPW9TIN = item.GPW9TIN;

			//this.SocialSecCardStatus = HistoryHelper.GetDocStatusEnumDisplayName(item.SocialSecCardStatusID);
			//this.SocialSecCardNotes = item.SocialSecCardNotes;

			DriversLicenseStatus = HistoryHelper.GetDocStatusEnumDisplayName(item.DriversLicenseStatusID);
			DriversLicenseNotes = item.DriversLicenseNotes;

			W4Status = HistoryHelper.GetDocStatusEnumDisplayName(item.W4StatusID);
			W4Notes = item.W4Notes;

			I9Status = HistoryHelper.GetDocStatusEnumDisplayName(item.I9StatusID);
			I9Notes = item.I9Notes;

			W9Status = HistoryHelper.GetDocStatusEnumDisplayName(item.W9StatusID);
			W9Notes = item.W9Notes;

			EIN = item.EIN;
			FedFilingStatus = item.FedFilingStatus;
			SUTA = item.SUTA;
			EICFilingStatus = item.EICFilingStatus;
			WorkersComp = item.WorkersComp;
			StateFilingStatus = item.StateFilingStatus;
			TaxWitholdingState = item.TaxWitholdingState;
			GPDependents = item.GPDependents;

			CriminalOffense = item.CriminalOffense;
			Offense = item.Offense;
			OffenseExplanation = item.OffenseExplanation;
			Rent = HistoryHelper.GetMoneyDisplayValue(item.Rent);
			Pet = HistoryHelper.GetMoneyDisplayValue(item.Pet);
			Utilities = HistoryHelper.GetMoneyDisplayValue(item.Utilities);
			Fuel = HistoryHelper.GetMoneyDisplayValue(item.Fuel);
			Furniture = HistoryHelper.GetMoneyDisplayValue(item.Furniture);
			CellPhoneCredit = HistoryHelper.GetMoneyDisplayValue(item.CellPhoneCredit);
			GasCredit = HistoryHelper.GetMoneyDisplayValue(item.GasCredit);
			RentExempt = item.RentExempt;
			IsServiceTech = item.IsServiceTech;

			Status = item.IsDeleted ? "Deleted" : "Active";
			CreatedBy = item.CreatedBy;
			CreatedOn = HistoryHelper.GetDateAndTimeText(item.CreatedOn);
			ModifiedBy = item.ModifiedBy;
			ModifiedOn = HistoryHelper.GetDateAndTimeText(item.ModifiedOn);
		}

		#region Properties

		[DataMember]
		public long RecruitHistoryID { get; set; }

		[DataMember]
		public string HistoryDate { get; set; }


		[DataMember]
		public string Status { get; set; }


		[DataMember]
		public int RecruitID { get; set; }

		[DataMember]
		public int UserID { get; set; }

		[DataMember]
		public string UserType { get; set; }

		[DataMember]
		public string ReportsTo { get; set; }

		//[DataMember]
		//public int? CurrentAddressID { get; set; }
		[DataMember]
		public string State { get; set; }

		[DataMember]
		public string Country { get; set; }

		[DataMember]
		public string StreetAddress { get; set; }

		[DataMember]
		public string StreetAddress2 { get; set; }

		[DataMember]
		public string City { get; set; }

		[DataMember]
		public string PostalCode { get; set; }

		[DataMember]
		public string Season { get; set; }

		[DataMember]
		public string OwnerApproval { get; set; }

		[DataMember]
		public string Team { get; set; }

		[DataMember]
		public string PayScale { get; set; }

		[DataMember]
		public string School { get; set; }

		[DataMember]
		public string Buddy { get; set; }

		[DataMember]
		public string RecruitCohabbitType { get; set; }

		[DataMember]
		public int? AlternatePayScheduleID { get; set; }

		[DataMember]
		public string Location { get; set; }

		[DataMember]
		public string OwnerApprovalDate { get; set; }

		[DataMember]
		public string ManagerApprovalDate { get; set; }

		[DataMember]
		public string EmergencyName { get; set; }

		[DataMember]
		public string EmergencyPhone { get; set; }

		[DataMember]
		public string EmergencyRelationship { get; set; }

		[DataMember]
		public string RecruiterStatus { get; set; }

		[DataMember]
		public string PreviousSummer { get; set; }

		[DataMember]
		public string SignatureDate { get; set; }

		//[DataMember]
		//public byte? GPW4Allowances { get; set; }
		//[DataMember]
		//public string GPW9Name { get; set; }
		//[DataMember]
		//public string GPW9BusinessName { get; set; }
		//[DataMember]
		//public string GPW9TIN { get; set; }

		[DataMember]
		public string W4Status { get; set; }

		[DataMember]
		public string W4Notes { get; set; }

		[DataMember]
		public string I9Status { get; set; }

		[DataMember]
		public string I9Notes { get; set; }

		[DataMember]
		public string W9Status { get; set; }

		[DataMember]
		public string W9Notes { get; set; }

		//[DataMember]
		//public string SocialSecCardStatus { get; set; }
		//[DataMember]
		//public string SocialSecCardNotes { get; set; }

		[DataMember]
		public string DriversLicenseStatus { get; set; }

		[DataMember]
		public string DriversLicenseNotes { get; set; }


		[DataMember]
		public bool? CriminalOffense { get; set; }

		[DataMember]
		public string Offense { get; set; }

		[DataMember]
		public string OffenseExplanation { get; set; }

		[DataMember]
		public string Rent { get; set; }

		[DataMember]
		public string Pet { get; set; }

		[DataMember]
		public string Utilities { get; set; }

		[DataMember]
		public string Fuel { get; set; }

		[DataMember]
		public string Furniture { get; set; }

		[DataMember]
		public string CellPhoneCredit { get; set; }

		[DataMember]
		public string GasCredit { get; set; }

		[DataMember]
		public bool RentExempt { get; set; }

		[DataMember]
		public bool IsServiceTech { get; set; }

		[DataMember]
		public string CreatedBy { get; set; }

		[DataMember]
		public string CreatedOn { get; set; }

		[DataMember]
		public string ModifiedBy { get; set; }

		[DataMember]
		public string ModifiedOn { get; set; }

		[DataMember]
		public string EIN { get; set; }

		[DataMember]
		public string FedFilingStatus { get; set; }

		[DataMember]
		public string SUTA { get; set; }

		[DataMember]
		public string EICFilingStatus { get; set; }

		[DataMember]
		public string WorkersComp { get; set; }

		[DataMember]
		public string StateFilingStatus { get; set; }

		[DataMember]
		public string TaxWitholdingState { get; set; }

		[DataMember]
		public int? GPDependents { get; set; }

		#endregion //Properties
	}
}
