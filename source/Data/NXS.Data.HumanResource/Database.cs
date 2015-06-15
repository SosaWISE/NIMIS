using NXS.Data;
using System;

namespace NXS.Data.HumanResource
{
	public partial class DBase : Database<DBase>
	{
		public static readonly string Database = "WISE_HumanResource";
		public readonly Sprocs Sprocs;
		public DBase()
		{
			Sprocs = new Sprocs(this);
		}

		public RU_RecruitTable RU_Recruits { get; set; }
		public RU_RoleLocationTable RU_RoleLocations { get; set; }
		public RU_TeamTable RU_Teams { get; set; }
		public RU_UserTable RU_Users { get; set; }
		public RU_UserTypeTable RU_UserTypes { get; set; }

		public partial class RU_RecruitTable : Table<RU_Recruit, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public RU_RecruitTable(DBase db) : base(db, "RuR", "[WISE_HumanResource].[dbo].[RU_Recruits]", "RecruitID", "int", true) { }
			public string RecruitID { get { return _alias + "[RecruitID]"; } }
			public string UserId { get { return _alias + "[UserId]"; } }
			public string UserTypeId { get { return _alias + "[UserTypeId]"; } }
			public string ReportsToId { get { return _alias + "[ReportsToId]"; } }
			public string CurrentAddressId { get { return _alias + "[CurrentAddressId]"; } }
			public string SeasonId { get { return _alias + "[SeasonId]"; } }
			public string OwnerApprovalId { get { return _alias + "[OwnerApprovalId]"; } }
			public string TeamId { get { return _alias + "[TeamId]"; } }
			public string PayScaleId { get { return _alias + "[PayScaleId]"; } }
			public string SchoolId { get { return _alias + "[SchoolId]"; } }
			public string ShackingUpId { get { return _alias + "[ShackingUpId]"; } }
			public string RecruitCohabbitTypeId { get { return _alias + "[RecruitCohabbitTypeId]"; } }
			public string AlternatePayScheduleId { get { return _alias + "[AlternatePayScheduleId]"; } }
			public string DealerId { get { return _alias + "[DealerId]"; } }
			public string Location { get { return _alias + "[Location]"; } }
			public string OwnerApprovalDate { get { return _alias + "[OwnerApprovalDate]"; } }
			public string ManagerApprovalDate { get { return _alias + "[ManagerApprovalDate]"; } }
			public string EmergencyName { get { return _alias + "[EmergencyName]"; } }
			public string EmergencyPhone { get { return _alias + "[EmergencyPhone]"; } }
			public string EmergencyRelationship { get { return _alias + "[EmergencyRelationship]"; } }
			public string IsRecruiter { get { return _alias + "[IsRecruiter]"; } }
			public string PreviousSummer { get { return _alias + "[PreviousSummer]"; } }
			public string SignatureDate { get { return _alias + "[SignatureDate]"; } }
			public string HireDate { get { return _alias + "[HireDate]"; } }
			public string GPExemptions { get { return _alias + "[GPExemptions]"; } }
			public string GPW4Allowances { get { return _alias + "[GPW4Allowances]"; } }
			public string GPW9Name { get { return _alias + "[GPW9Name]"; } }
			public string GPW9BusinessName { get { return _alias + "[GPW9BusinessName]"; } }
			public string GPW9TIN { get { return _alias + "[GPW9TIN]"; } }
			public string SocialSecCardStatusID { get { return _alias + "[SocialSecCardStatusID]"; } }
			public string DriversLicenseStatusID { get { return _alias + "[DriversLicenseStatusID]"; } }
			public string W4StatusID { get { return _alias + "[W4StatusID]"; } }
			public string I9StatusID { get { return _alias + "[I9StatusID]"; } }
			public string W9StatusID { get { return _alias + "[W9StatusID]"; } }
			public string SocialSecCardNotes { get { return _alias + "[SocialSecCardNotes]"; } }
			public string DriversLicenseNotes { get { return _alias + "[DriversLicenseNotes]"; } }
			public string W4Notes { get { return _alias + "[W4Notes]"; } }
			public string I9Notes { get { return _alias + "[I9Notes]"; } }
			public string W9Notes { get { return _alias + "[W9Notes]"; } }
			public string EIN { get { return _alias + "[EIN]"; } }
			public string SUTA { get { return _alias + "[SUTA]"; } }
			public string WorkersComp { get { return _alias + "[WorkersComp]"; } }
			public string FedFilingStatus { get { return _alias + "[FedFilingStatus]"; } }
			public string EICFilingStatus { get { return _alias + "[EICFilingStatus]"; } }
			public string TaxWitholdingState { get { return _alias + "[TaxWitholdingState]"; } }
			public string StateFilingStatus { get { return _alias + "[StateFilingStatus]"; } }
			public string GPDependents { get { return _alias + "[GPDependents]"; } }
			public string CriminalOffense { get { return _alias + "[CriminalOffense]"; } }
			public string Offense { get { return _alias + "[Offense]"; } }
			public string OffenseExplanation { get { return _alias + "[OffenseExplanation]"; } }
			public string Rent { get { return _alias + "[Rent]"; } }
			public string Pet { get { return _alias + "[Pet]"; } }
			public string Utilities { get { return _alias + "[Utilities]"; } }
			public string Fuel { get { return _alias + "[Fuel]"; } }
			public string Furniture { get { return _alias + "[Furniture]"; } }
			public string CellPhoneCredit { get { return _alias + "[CellPhoneCredit]"; } }
			public string GasCredit { get { return _alias + "[GasCredit]"; } }
			public string RentExempt { get { return _alias + "[RentExempt]"; } }
			public string IsServiceTech { get { return _alias + "[IsServiceTech]"; } }
			public string StateId { get { return _alias + "[StateId]"; } }
			public string CountryId { get { return _alias + "[CountryId]"; } }
			public string StreetAddress { get { return _alias + "[StreetAddress]"; } }
			public string StreetAddress2 { get { return _alias + "[StreetAddress2]"; } }
			public string City { get { return _alias + "[City]"; } }
			public string PostalCode { get { return _alias + "[PostalCode]"; } }
			public string CBxSocialSecCard { get { return _alias + "[CBxSocialSecCard]"; } }
			public string CBxDriversLicense { get { return _alias + "[CBxDriversLicense]"; } }
			public string CBxW4 { get { return _alias + "[CBxW4]"; } }
			public string CBxI9 { get { return _alias + "[CBxI9]"; } }
			public string CBxW9 { get { return _alias + "[CBxW9]"; } }
			public string PersonalMultiple { get { return _alias + "[PersonalMultiple]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedByID { get { return _alias + "[CreatedByID]"; } }
			public string CreatedDate { get { return _alias + "[CreatedDate]"; } }
			public string ModifiedByID { get { return _alias + "[ModifiedByID]"; } }
			public string ModifiedDate { get { return _alias + "[ModifiedDate]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}
		public partial class RU_RoleLocationTable : Table<RU_RoleLocation, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public RU_RoleLocationTable(DBase db) : base(db, "RuRL", "[WISE_HumanResource].[dbo].[RU_RoleLocations]", "RoleLocationID", "int", true) { }
			public string RoleLocationID { get { return _alias + "[RoleLocationID]"; } }
			public string Role { get { return _alias + "[Role]"; } }
		}
		public partial class RU_TeamTable : Table<RU_Team, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public RU_TeamTable(DBase db) : base(db, "RuT", "[WISE_HumanResource].[dbo].[RU_Teams]", "TeamID", "int", true) { }
			public string TeamID { get { return _alias + "[TeamID]"; } }
			public string Description { get { return _alias + "[Description]"; } }
			public string CreatedFromTeamId { get { return _alias + "[CreatedFromTeamId]"; } }
			public string TeamLocationId { get { return _alias + "[TeamLocationId]"; } }
			public string RoleLocationId { get { return _alias + "[RoleLocationId]"; } }
			public string RegionalManagerRecruitId { get { return _alias + "[RegionalManagerRecruitId]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}
		public partial class RU_UserTable : Table<RU_User, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public RU_UserTable(DBase db) : base(db, "RuU", "[WISE_HumanResource].[dbo].[RU_Users]", "UserID", "int", true) { }
			public string UserID { get { return _alias + "[UserID]"; } }
			public string FullName { get { return _alias + "[FullName]"; } }
			public string PublicFullName { get { return _alias + "[PublicFullName]"; } }
			public string RecruitedById { get { return _alias + "[RecruitedById]"; } }
			public string GPEmployeeId { get { return _alias + "[GPEmployeeId]"; } }
			public string UserEmployeeTypeId { get { return _alias + "[UserEmployeeTypeId]"; } }
			public string PermanentAddressId { get { return _alias + "[PermanentAddressId]"; } }
			public string SSN { get { return _alias + "[SSN]"; } }
			public string FirstName { get { return _alias + "[FirstName]"; } }
			public string MiddleName { get { return _alias + "[MiddleName]"; } }
			public string LastName { get { return _alias + "[LastName]"; } }
			public string PreferredName { get { return _alias + "[PreferredName]"; } }
			public string CompanyName { get { return _alias + "[CompanyName]"; } }
			public string MaritalStatus { get { return _alias + "[MaritalStatus]"; } }
			public string SpouseName { get { return _alias + "[SpouseName]"; } }
			public string UserName { get { return _alias + "[UserName]"; } }
			public string Password { get { return _alias + "[Password]"; } }
			public string BirthDate { get { return _alias + "[BirthDate]"; } }
			public string HomeTown { get { return _alias + "[HomeTown]"; } }
			public string BirthCity { get { return _alias + "[BirthCity]"; } }
			public string BirthState { get { return _alias + "[BirthState]"; } }
			public string BirthCountry { get { return _alias + "[BirthCountry]"; } }
			public string Sex { get { return _alias + "[Sex]"; } }
			public string ShirtSize { get { return _alias + "[ShirtSize]"; } }
			public string HatSize { get { return _alias + "[HatSize]"; } }
			public string DLNumber { get { return _alias + "[DLNumber]"; } }
			public string DLState { get { return _alias + "[DLState]"; } }
			public string DLCountry { get { return _alias + "[DLCountry]"; } }
			public string DLExpiresOn { get { return _alias + "[DLExpiresOn]"; } }
			public string DLExpiration { get { return _alias + "[DLExpiration]"; } }
			public string Height { get { return _alias + "[Height]"; } }
			public string Weight { get { return _alias + "[Weight]"; } }
			public string EyeColor { get { return _alias + "[EyeColor]"; } }
			public string HairColor { get { return _alias + "[HairColor]"; } }
			public string PhoneHome { get { return _alias + "[PhoneHome]"; } }
			public string PhoneCell { get { return _alias + "[PhoneCell]"; } }
			public string PhoneCellCarrierID { get { return _alias + "[PhoneCellCarrierID]"; } }
			public string PhoneFax { get { return _alias + "[PhoneFax]"; } }
			public string Email { get { return _alias + "[Email]"; } }
			public string CorporateEmail { get { return _alias + "[CorporateEmail]"; } }
			public string TreeLevel { get { return _alias + "[TreeLevel]"; } }
			public string HasVerifiedAddress { get { return _alias + "[HasVerifiedAddress]"; } }
			public string RightToWorkExpirationDate { get { return _alias + "[RightToWorkExpirationDate]"; } }
			public string RightToWorkNotes { get { return _alias + "[RightToWorkNotes]"; } }
			public string RightToWorkStatusID { get { return _alias + "[RightToWorkStatusID]"; } }
			public string IsLocked { get { return _alias + "[IsLocked]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string RecruitedDate { get { return _alias + "[RecruitedDate]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}
		public partial class RU_UserTypeTable : Table<RU_UserType, short>
		{
			public DBase Db { get { return (DBase)_database; } }
			public RU_UserTypeTable(DBase db) : base(db, "RuUT", "[WISE_HumanResource].[dbo].[RU_UserType]", "UserTypeID", "smallint", true) { }
			public string UserTypeID { get { return _alias + "[UserTypeID]"; } }
			public string Description { get { return _alias + "[Description]"; } }
			public string SecurityLevel { get { return _alias + "[SecurityLevel]"; } }
			public string SpawnTypeID { get { return _alias + "[SpawnTypeID]"; } }
			public string RoleLocationID { get { return _alias + "[RoleLocationID]"; } }
			public string ReportingLevel { get { return _alias + "[ReportingLevel]"; } }
			public string UserTypeTeamTypeID { get { return _alias + "[UserTypeTeamTypeID]"; } }
			public string IsCommissionable { get { return _alias + "[IsCommissionable]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedByID { get { return _alias + "[CreatedByID]"; } }
			public string CreatedDate { get { return _alias + "[CreatedDate]"; } }
			public string ModifiedByID { get { return _alias + "[ModifiedByID]"; } }
			public string ModifiedDate { get { return _alias + "[ModifiedDate]"; } }
		}

	}
}
