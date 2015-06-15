using NXS.Data;
using System;

namespace NXS.Data.HumanResource
{
	public partial class RU_Recruit // RU_Recruits
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return RecruitID; } set { RecruitID = value; } }
		public int RecruitID { get; set; }
		public int UserId { get; set; }
		public short UserTypeId { get; set; }
		public int? ReportsToId { get; set; }
		public int? CurrentAddressId { get; set; }
		public int SeasonId { get; set; }
		public int? OwnerApprovalId { get; set; }
		public int? TeamId { get; set; }
		public int? PayScaleId { get; set; }
		public short? SchoolId { get; set; }
		public int? ShackingUpId { get; set; }
		public int? RecruitCohabbitTypeId { get; set; }
		public int? AlternatePayScheduleId { get; set; }
		public int DealerId { get; set; }
		public string Location { get; set; }
		public DateTime? OwnerApprovalDate { get; set; }
		public DateTime? ManagerApprovalDate { get; set; }
		public string EmergencyName { get; set; }
		public string EmergencyPhone { get; set; }
		public string EmergencyRelationship { get; set; }
		public bool IsRecruiter { get; set; }
		public string PreviousSummer { get; set; }
		public DateTime? SignatureDate { get; set; }
		public DateTime? HireDate { get; set; }
		public int? GPExemptions { get; set; }
		public byte? GPW4Allowances { get; set; }
		public string GPW9Name { get; set; }
		public string GPW9BusinessName { get; set; }
		public string GPW9TIN { get; set; }
		public int SocialSecCardStatusID { get; set; }
		public int DriversLicenseStatusID { get; set; }
		public int W4StatusID { get; set; }
		public int I9StatusID { get; set; }
		public int W9StatusID { get; set; }
		public string SocialSecCardNotes { get; set; }
		public string DriversLicenseNotes { get; set; }
		public string W4Notes { get; set; }
		public string I9Notes { get; set; }
		public string W9Notes { get; set; }
		public string EIN { get; set; }
		public string SUTA { get; set; }
		public string WorkersComp { get; set; }
		public string FedFilingStatus { get; set; }
		public string EICFilingStatus { get; set; }
		public string TaxWitholdingState { get; set; }
		public string StateFilingStatus { get; set; }
		public int? GPDependents { get; set; }
		public bool? CriminalOffense { get; set; }
		public string Offense { get; set; }
		public string OffenseExplanation { get; set; }
		public decimal? Rent { get; set; }
		public decimal? Pet { get; set; }
		public decimal? Utilities { get; set; }
		public decimal? Fuel { get; set; }
		public decimal? Furniture { get; set; }
		public decimal? CellPhoneCredit { get; set; }
		public decimal? GasCredit { get; set; }
		public bool RentExempt { get; set; }
		public bool IsServiceTech { get; set; }
		public string StateId { get; set; }
		public string CountryId { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public bool? CBxSocialSecCard { get; set; }
		public bool? CBxDriversLicense { get; set; }
		public bool? CBxW4 { get; set; }
		public bool? CBxI9 { get; set; }
		public bool? CBxW9 { get; set; }
		public int? PersonalMultiple { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public int? CreatedByID { get; set; }
		public DateTime? CreatedDate { get; set; }
		public int? ModifiedByID { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
	public partial class RU_RoleLocation // RU_RoleLocations
	{
		public enum IDEnum : int
		{
			Sales = 1,
			Installs = 2,
			Corporate = 3,
			Office_Staff = 4,
			Vendors = 5,
		}
		[IgnorePropertyAttribute(true)] public int ID { get { return RoleLocationID; } set { RoleLocationID = value; } }
		public int RoleLocationID { get; set; }
		public string Role { get; set; }
	}
	public partial class RU_Team // RU_Teams
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return TeamID; } set { TeamID = value; } }
		public int TeamID { get; set; }
		public string Description { get; set; }
		public int? CreatedFromTeamId { get; set; }
		public int TeamLocationId { get; set; }
		public int? RoleLocationId { get; set; }
		public int? RegionalManagerRecruitId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
	public partial class RU_User // RU_Users
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return UserID; } set { UserID = value; } }
		public int UserID { get; set; }
		public string FullName { get; set; }
		public string PublicFullName { get; set; }
		public int? RecruitedById { get; set; }
		public string GPEmployeeId { get; set; }
		public string UserEmployeeTypeId { get; set; }
		public int? PermanentAddressId { get; set; }
		public string SSN { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string PreferredName { get; set; }
		public string CompanyName { get; set; }
		public bool? MaritalStatus { get; set; }
		public string SpouseName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public DateTime? BirthDate { get; set; }
		public string HomeTown { get; set; }
		public string BirthCity { get; set; }
		public string BirthState { get; set; }
		public string BirthCountry { get; set; }
		public byte Sex { get; set; }
		public byte? ShirtSize { get; set; }
		public byte? HatSize { get; set; }
		public string DLNumber { get; set; }
		public string DLState { get; set; }
		public string DLCountry { get; set; }
		public DateTime? DLExpiresOn { get; set; }
		public string DLExpiration { get; set; }
		public string Height { get; set; }
		public string Weight { get; set; }
		public string EyeColor { get; set; }
		public string HairColor { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneCell { get; set; }
		public short? PhoneCellCarrierID { get; set; }
		public string PhoneFax { get; set; }
		public string Email { get; set; }
		public string CorporateEmail { get; set; }
		public int? TreeLevel { get; set; }
		public bool HasVerifiedAddress { get; set; }
		public DateTime? RightToWorkExpirationDate { get; set; }
		public string RightToWorkNotes { get; set; }
		public int? RightToWorkStatusID { get; set; }
		public bool IsLocked { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime RecruitedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
	public partial class RU_UserType // RU_UserType
	{
		public enum IDEnum : short
		{
			Administrator = 1,
			Sales_Manager = 2,
			Sales_CoManager = 3,
			Sales_Assistant_Manager = 4,
			Sales_Rep = 5,
			Technician_Lead = 6,
			Technician = 7,
			Regional_Manager__Technician = 8,
			Technician_Assistant_Lead = 10,
			Regional_Manager__Sales = 11,
			Corporate = 12,
			Office_Assistant = 13,
			Inventory_Manager = 14,
			Corporate_Service = 15,
			Senior_Regional__Sales = 18,
			National_Regional__Sales = 19,
			National_Regional__Technician = 20,
			Service_Technician = 22,
			Vendor = 23,
		}
		[IgnorePropertyAttribute(true)] public short ID { get { return UserTypeID; } set { UserTypeID = value; } }
		public short UserTypeID { get; set; }
		public string Description { get; set; }
		public byte SecurityLevel { get; set; }
		public int SpawnTypeID { get; set; }
		public int RoleLocationID { get; set; }
		public int ReportingLevel { get; set; }
		public int UserTypeTeamTypeID { get; set; }
		public bool? IsCommissionable { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public int CreatedByID { get; set; }
		public DateTime CreatedDate { get; set; }
		public int ModifiedByID { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}
