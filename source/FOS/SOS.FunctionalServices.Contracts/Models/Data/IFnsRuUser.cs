using System;

namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsRuUser
	{
		int UserID { get; set; }
		int? RecruitedByID { get; set; }
		string GPEmployeeID { get; set; }
		string UserEmployeeTypeId { get; set; }
		int? PermanentAddressID { get; set; }
		string SSN { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string PreferredName { get; set; }
		string CompanyName { get; set; }
		bool? MaritalStatus { get; set; }
		string SpouseName { get; set; }
		string UserName { get; set; }
		string Password { get; set; }
		DateTime? BirthDate { get; set; }
		string HomeTown { get; set; }
		string BirthCity { get; set; }
		string BirthState { get; set; }
		string BirthCountry { get; set; }
		byte Sex { get; set; }
		byte? ShirtSize { get; set; }
		byte? HatSize { get; set; }
		string DLNumber { get; set; }
		string DLState { get; set; }
		string DLCountry { get; set; }
		DateTime? DLExpiresOn { get; set; }
		string Height { get; set; }
		string Weight { get; set; }
		string EyeColor { get; set; }
		string HairColor { get; set; }
		string PhoneHome { get; set; }
		string PhoneCell { get; set; }
		short? PhoneCellCarrierID { get; set; }
		string PhoneFax { get; set; }
		string Email { get; set; }
		string CorporateEmail { get; set; }
		int? TreeLevel { get; set; }
		bool HasVerifiedAddress { get; set; }
		DateTime? RightToWorkExpirationDate { get; set; }
		string RightToWorkNotes { get; set; }
		int? RightToWorkStatusID { get; set; }
		bool IsLocked { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		DateTime RecruitedDate { get; set; }
		string CreatedBy { get; set; }
		DateTime? CreatedOn { get; set; }
		string ModifiedBy { get; set; }
		DateTime? ModifiedOn { get; set; }
		//string TeamLocation { get; set; }
		//int TeamLocationId { get; set; }
		//int SeasonId { get; set; }
		//string SeasonName { get; set; }
	}
}