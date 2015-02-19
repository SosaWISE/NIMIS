using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models
{
	public class FnsRuUser : IFnsRuUser
	{
		#region .ctor
		public FnsRuUser() { }
		public FnsRuUser(RU_User oUser)
		{
			UserID = oUser.UserID;
			RecruitedByID = oUser.RecruitedById;
			GPEmployeeID = oUser.GPEmployeeId;
			UserEmployeeTypeId = oUser.UserEmployeeTypeId;
			PermanentAddressID = oUser.PermanentAddressId;
			SSN = oUser.SSN;
			FirstName = oUser.FirstName;
			MiddleName = oUser.MiddleName;
			LastName = oUser.LastName;
			PreferredName = oUser.PreferredName;
			CompanyName = oUser.CompanyName;
			MaritalStatus = oUser.MaritalStatus;
			SpouseName = oUser.SpouseName;
			UserName = oUser.UserName;
			Password = oUser.Password;
			BirthDate = oUser.BirthDate;
			HomeTown = oUser.HomeTown;
			BirthCity = oUser.BirthCity;
			BirthState = oUser.BirthState;
			BirthCountry = oUser.BirthCountry;
			Sex = oUser.Sex;
			ShirtSize = oUser.ShirtSize;
			HatSize = oUser.HatSize;
			DLNumber = oUser.DLNumber;
			DLState = oUser.DLState;
			DLCountry = oUser.DLCountry;
			DLExpiresOn = oUser.DLExpiresOn;
			Height = (oUser.Height == null) ? (int?)null : int.Parse(oUser.Height);
			Weight = (oUser.Weight == null) ? (int?)null : int.Parse(oUser.Weight);
			EyeColor = oUser.EyeColor;
			HairColor = oUser.HairColor;
			PhoneHome = oUser.PhoneHome;
			PhoneCell = oUser.PhoneCell;
			PhoneCellCarrierID = oUser.PhoneCellCarrierID;
			PhoneFax = oUser.PhoneFax;
			Email = oUser.Email;
			CorporateEmail = oUser.CorporateEmail;
			TreeLevel = oUser.TreeLevel;
			HasVerifiedAddress = oUser.HasVerifiedAddress;
			RightToWorkExpirationDate = oUser.RightToWorkExpirationDate;
			RightToWorkNotes = oUser.RightToWorkNotes;
			RightToWorkStatusID = oUser.RightToWorkStatusID;
			IsLocked = oUser.IsLocked;
			IsActive = oUser.IsActive;
			IsDeleted = oUser.IsDeleted;
			RecruitedDate = oUser.RecruitedDate;
			CreatedBy = oUser.CreatedBy;
			CreatedOn = oUser.CreatedOn;
			ModifiedBy = oUser.ModifiedBy;
			ModifiedOn = oUser.ModifiedOn;
		}

		#endregion .ctor

		#region Implementation of IFnsRuUser

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
		public byte Sex { get; set; }
		public byte? ShirtSize { get; set; }
		public byte? HatSize { get; set; }
		public string DLNumber { get; set; }
		public string DLState { get; set; }
		public string DLCountry { get; set; }
		public DateTime? DLExpiresOn { get; set; }
		public int? Height { get; set; }
		public int? Weight { get; set; }
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
		//public string TeamLocation { get; set; }
		//public int TeamLocationId { get; set; }
		//public int SeasonId { get; set; }
		//public string SeasonName { get; set; }

		#endregion Implementation of IFnsRuUser
	}
}