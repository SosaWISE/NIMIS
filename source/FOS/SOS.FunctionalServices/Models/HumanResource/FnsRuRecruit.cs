using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using System;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsRuRecruit : IFnsRuRecruit
	{
		public FnsRuRecruit(){}
		public FnsRuRecruit(RU_Recruit item)
		{
			RecruitID = item.RecruitID;
			UserID = item.UserId;
			SeasonID = item.SeasonId;

			UserTypeId = item.UserTypeId;
			ReportsToID = item.ReportsToId;
			TeamID = item.TeamId;
			PayScaleID = item.PayScaleId;
			PreviousSummer = item.PreviousSummer;
			SignatureDate = item.SignatureDate;
			ManagerApprovalDate = item.ManagerApprovalDate;
			OwnerApprovalDate = item.OwnerApprovalDate;
			OwnerApprovalId = item.OwnerApprovalId;
			SchoolId = item.SchoolId;

			DriversLicenseStatusID = item.DriversLicenseStatusID;
			DriversLicenseNotes = item.DriversLicenseNotes;
			I9StatusID = item.I9StatusID;
			I9Notes = item.I9Notes;
			W9StatusID = item.W9StatusID;
			W9Notes = item.W9Notes;
			W4StatusID = item.W4StatusID;
			W4Notes = item.W4Notes;

			EmergencyName = item.EmergencyName;
			EmergencyRelationship = item.EmergencyRelationship;
			EmergencyPhone = item.EmergencyPhone;

			CountryId = item.CountryId;
			StreetAddress = item.StreetAddress;
			City = item.City;
			StateId = item.StateId;
			PostalCode = item.PostalCode;

			RecruitCohabbitTypeId = item.RecruitCohabbitTypeId;
			ShackingUpId = item.ShackingUpId;
			Rent = item.Rent;
			Pet = item.Pet;
			Utilities = item.Utilities;
			Fuel = item.Fuel;

			EIN = item.EIN;
			FedFilingStatus = item.FedFilingStatus;
			SUTA = item.SUTA;
			EICFilingStatus = item.EICFilingStatus;
			WorkersComp = item.WorkersComp;
			StateFilingStatus = item.StateFilingStatus;
			TaxWitholdingState = item.TaxWitholdingState;
			GPDependents = item.GPDependents;

			DealerId = item.DealerId;

			PersonalMultiple = item.PersonalMultiple;

			IsActive = item.IsActive;
			IsDeleted = item.IsDeleted;
			CreatedBy = item.CreatedBy;
			CreatedOn = item.CreatedOn;
			ModifiedBy = item.ModifiedBy;
			ModifiedOn = item.ModifiedOn;
		}


		public int RecruitID { get; set; }
		public int UserID { get; set; }
		public int SeasonID { get; set; }

		public short UserTypeId { get; set; }
		public int? ReportsToID { get; set; }
		public int? TeamID { get; set; }
		public int? PayScaleID { get; set; }
		public string PreviousSummer { get; set; }
		public DateTime? SignatureDate { get; set; }
		public DateTime? ManagerApprovalDate { get; set; }
		public DateTime? OwnerApprovalDate { get; set; }
		public int? OwnerApprovalId { get; set; }
		public short? SchoolId { get; set; }

		public int DriversLicenseStatusID { get; set; }
		public string DriversLicenseNotes { get; set; }
		public int I9StatusID { get; set; }
		public string I9Notes { get; set; }
		public int W9StatusID { get; set; }
		public string W9Notes { get; set; }
		public int W4StatusID { get; set; }
		public string W4Notes { get; set; }

		public string EmergencyName { get; set; }
		public string EmergencyRelationship { get; set; }
		public string EmergencyPhone { get; set; }

		public string CountryId { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string PostalCode { get; set; }

		public int? RecruitCohabbitTypeId { get; set; }
		public int? ShackingUpId { get; set; }
		public decimal? Rent { get; set; }
		public decimal? Pet { get; set; }
		public decimal? Utilities { get; set; }
		public decimal? Fuel { get; set; }

		public string EIN { get; set; }
		public string FedFilingStatus { get; set; }
		public string SUTA { get; set; }
		public string EICFilingStatus { get; set; }
		public string WorkersComp { get; set; }
		public string StateFilingStatus { get; set; }
		public string TaxWitholdingState { get; set; }
		public int? GPDependents { get; set; }

		public int? PersonalMultiple { get; set; }

		public int DealerId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }                  
	}
}
