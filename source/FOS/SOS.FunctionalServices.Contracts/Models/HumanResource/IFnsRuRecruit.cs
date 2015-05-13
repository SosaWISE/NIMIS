using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsRuRecruit
	{
		int RecruitID { get; set; }
		int UserID { get; set; }
		int SeasonID { get; set; }

		short UserTypeId { get; set; }
		int? ReportsToID { get; set; }
		int? TeamID { get; set; }
		int? PayScaleID { get; set; }
		string PreviousSummer { get; set; }
		DateTime? SignatureDate { get; set; }
		DateTime? ManagerApprovalDate { get; set; }
		DateTime? OwnerApprovalDate { get; set; }
		int? OwnerApprovalId { get; set; }
		short? SchoolId { get; set; }

		int DriversLicenseStatusID { get; set; }
		string DriversLicenseNotes { get; set; }
		int I9StatusID { get; set; }
		string I9Notes { get; set; }
		int W9StatusID { get; set; }
		string W9Notes { get; set; }
		int W4StatusID { get; set; }
		string W4Notes { get; set; }

		string EmergencyName { get; set; }
		string EmergencyRelationship { get; set; }
		string EmergencyPhone { get; set; }

		string CountryId { get; set; }
		string StreetAddress { get; set; }
		string City { get; set; }
		string StateId { get; set; }
		string PostalCode { get; set; }

		int? RecruitCohabbitTypeId { get; set; }
		int? ShackingUpId { get; set; }
		decimal? Rent { get; set; }
		decimal? Pet { get; set; }
		decimal? Utilities { get; set; }
		decimal? Fuel { get; set; }

		string EIN { get; set; }
		string FedFilingStatus { get; set; }
		string SUTA { get; set; }
		string EICFilingStatus { get; set; }
		string WorkersComp { get; set; }
		string StateFilingStatus { get; set; }
		string TaxWitholdingState { get; set; }
		int? GPDependents { get; set; }

		int? PersonalMultiple { get; set; }
		int DealerId { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		string CreatedBy { get; set; }
		DateTime CreatedOn { get; set; }
		string ModifiedBy { get; set; }
		DateTime ModifiedOn { get; set; }
	}
}
