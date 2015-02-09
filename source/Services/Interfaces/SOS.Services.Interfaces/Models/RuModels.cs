using System;

namespace SOS.Services.Interfaces.Models
{
	public static class RuModels
	{
		#region RuUser

		public interface IRuUser
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
			byte? Sex { get; set; }
			byte? ShirtSize { get; set; }
			byte? HatSize { get; set; }
			string DLNumber { get; set; }
			string DLState { get; set; }
			string DLCountry { get; set; }
			string DLExpiration { get; set; }
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
			string CreatedByID { get; set; }
			DateTime? CreatedDate { get; set; }
			string ModifiedByID { get; set; }
			DateTime? ModifiedDate { get; set; }
		}

		public class RuUser : IRuUser
		{
			#region Implementation of IRuUser

			public int UserID { get; set; }
			public int? RecruitedByID { get; set; }
			public string GPEmployeeID { get; set; }
			public string UserEmployeeTypeId { get; set; }
			public int? PermanentAddressID { get; set; }
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
			public byte? Sex { get; set; }
			public byte? ShirtSize { get; set; }
			public byte? HatSize { get; set; }
			public string DLNumber { get; set; }
			public string DLState { get; set; }
			public string DLCountry { get; set; }
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
			public string CreatedByID { get; set; }
			public DateTime? CreatedDate { get; set; }
			public string ModifiedByID { get; set; }
			public DateTime? ModifiedDate { get; set; }

			#endregion
		}

		#endregion RuUser
	}
}