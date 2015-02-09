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
			this.RecruitID = item.RecruitID;
			this.UserID = item.UserId;
			this.SeasonID = item.SeasonId;

			this.UserTypeId = item.UserTypeId;
			this.ReportsToID = item.ReportsToId;
			this.TeamID = item.TeamId;
			this.PayScaleID = item.PayScaleId;
			this.PreviousSummer = item.PreviousSummer;
			this.SignatureDate = item.SignatureDate;
			this.ManagerApprovalDate = item.ManagerApprovalDate;
			this.OwnerApprovalDate = item.OwnerApprovalDate;
			this.OwnerApprovalId = item.OwnerApprovalId;
			this.SchoolId = item.SchoolId;

			this.DriversLicenseStatusID = item.DriversLicenseStatusID;
			this.DriversLicenseNotes = item.DriversLicenseNotes;
			this.I9StatusID = item.I9StatusID;
			this.I9Notes = item.I9Notes;
			this.W9StatusID = item.W9StatusID;
			this.W9Notes = item.W9Notes;
			this.W4StatusID = item.W4StatusID;
			this.W4Notes = item.W4Notes;

			this.EmergencyName = item.EmergencyName;
			this.EmergencyRelationship = item.EmergencyRelationship;
			this.EmergencyPhone = item.EmergencyPhone;

			this.CountryId = item.CountryId;
			this.StreetAddress = item.StreetAddress;
			this.City = item.City;
			this.StateId = item.StateId;
			this.PostalCode = item.PostalCode;

			this.RecruitCohabbitTypeId = item.RecruitCohabbitTypeId;
			this.ShackingUpId = item.ShackingUpId;
			this.Rent = item.Rent;
			this.Pet = item.Pet;
			this.Utilities = item.Utilities;
			this.Fuel = item.Fuel;

			this.EIN = item.EIN;
			this.FedFilingStatus = item.FedFilingStatus;
			this.SUTA = item.SUTA;
			this.EICFilingStatus = item.EICFilingStatus;
			this.WorkersComp = item.WorkersComp;
			this.StateFilingStatus = item.StateFilingStatus;
			this.TaxWitholdingState = item.TaxWitholdingState;
			this.GPDependents = item.GPDependents;



			this.DealerId = item.DealerId;
			this.IsActive = item.IsActive;
			this.IsDeleted = item.IsDeleted;
			this.CreatedBy = item.CreatedBy;
			this.CreatedOn = item.CreatedOn;
			this.ModifiedBy = item.ModifiedBy;
			this.ModifiedOn = item.ModifiedOn;
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



		public int DealerId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }                  
	}
}
