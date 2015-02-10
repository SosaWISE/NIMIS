using System.Runtime.Serialization;
using SOS.Data.HumanResource;
using SOS.Lib.Util;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class UsersHistory
	{
		public UsersHistory(HumanResourceDataContext recruitDB, RU_UsersHistory item)
		{
			UserHistoryID = item.UserHistoryID;
			FullName = item.FullName;
			HistoryDate = HistoryHelper.GetDateAndTimeText(item.HistoryDate);
			UserID = item.UserID;
			RecruitedBy = HistoryHelper.GetUserFullName(recruitDB, item.RecruitedByID);
			GPEmployeeID = item.GPEmployeeID;
			UserEmployeeTypeId = item.UserEmployeeTypeId;
			UserEmployeeType = HistoryHelper.GetUserEmployeeTypeNameFromID(recruitDB, item.UserEmployeeTypeId);
			SSN = StringUtility.FormatSsn(HistoryHelper.GetDecryptedText(item.SSN));
			FirstName = item.FirstName;
			MiddleName = item.MiddleName;
			LastName = item.LastName;
			PreferredName = item.PreferredName;
			CompanyName = item.CompanyName;
			MaritalStatus = HistoryHelper.GetMaritalStatusDisplayText(item.MaritalStatus);
			SpouseName = item.SpouseName;
			UserName = item.UserName;
			Password = item.Password;
			BirthDate = HistoryHelper.GetDateOnlyText(item.BirthDate);
			HomeTown = item.HomeTown;
			BirthCity = item.BirthCity;
			BirthState = item.BirthState;
			BirthCountry = item.BirthCountry;
			Sex = HistoryHelper.GetDisplaySex(item.Sex);
			ShirtSize = HistoryHelper.GetDisplayShirtSize(item.ShirtSize);
			HatSize = HistoryHelper.GetDisplayHatSize(item.HatSize);
			DLNumber = item.DLNumber;
			DLState = item.DLState;
			DLCountry = item.DLCountry;
			DLExpiration = item.DLExpiration;
			Height = item.Height;
			Weight = item.Weight;
			EyeColor = item.EyeColor;
			HairColor = item.HairColor;
			PhoneHome = StringUtility.FormatPhoneNumber(item.PhoneHome);
			PhoneCell = StringUtility.FormatPhoneNumber(item.PhoneCell);
			PhoneCellCarrier = HistoryHelper.GetCellCarrierDisplayName(recruitDB, item.PhoneCellCarrierID);
			Email = item.Email;
			CorporateEmail = item.CorporateEmail;
			HasVerifiedAddress = item.HasVerifiedAddress;
			RightToWorkExpirationDate = HistoryHelper.GetDateOnlyText(item.RightToWorkExpirationDate);
			RightToWorkNotes = item.RightToWorkNotes;
			RightToWorkStatus = HistoryHelper.GetDocStatusDisplayName(recruitDB, item.RightToWorkStatusID);
			//this.IsActive = item.IsActive;
			//this.IsDeleted = item.IsDeleted;
			Status = item.IsDeleted ? "Inactive" : "Active";
			RecruitedDate = HistoryHelper.GetDateOnlyText(item.RecruitedDate);
			CreatedBy = HistoryHelper.GetUserEditorDisplayName(recruitDB, item.CreatedBy);
			CreatedOn = HistoryHelper.GetDateAndTimeText(item.CreatedOn);
			ModifiedBy = HistoryHelper.GetUserEditorDisplayName(recruitDB, item.ModifiedBy);
			ModifiedDate = HistoryHelper.GetDateAndTimeText(item.ModifiedOn);
		}

		#region Properties

		[DataMember]
		public long UserHistoryID { get; set; }

		[DataMember]
		public string FullName { get; set; }

		[DataMember]
		public string HistoryDate { get; set; }

		[DataMember]
		public int UserID { get; set; }

		[DataMember]
		public string RecruitedBy { get; set; }

		[DataMember]
		public string GPEmployeeID { get; set; }

		[DataMember]
		public string UserEmployeeTypeId { get; set; }

		[DataMember]
		public string UserEmployeeType { get; set; }

		[DataMember]
		public string SSN { get; set; }

		[DataMember]
		public string FirstName { get; set; }

		[DataMember]
		public string MiddleName { get; set; }

		[DataMember]
		public string LastName { get; set; }

		[DataMember]
		public string PreferredName { get; set; }

		[DataMember]
		public string CompanyName { get; set; }

		[DataMember]
		public string MaritalStatus { get; set; }

		[DataMember]
		public string SpouseName { get; set; }

		[DataMember]
		public string UserName { get; set; }

		[DataMember]
		public string Password { get; set; }

		[DataMember]
		public string BirthDate { get; set; }

		[DataMember]
		public string HomeTown { get; set; }

		[DataMember]
		public string BirthCity { get; set; }

		[DataMember]
		public string BirthState { get; set; }

		[DataMember]
		public string BirthCountry { get; set; }

		[DataMember]
		public string Sex { get; set; }

		[DataMember]
		public string ShirtSize { get; set; }

		[DataMember]
		public string HatSize { get; set; }

		[DataMember]
		public string DLNumber { get; set; }

		[DataMember]
		public string DLState { get; set; }

		[DataMember]
		public string DLCountry { get; set; }

		[DataMember]
		public string DLExpiration { get; set; }

		[DataMember]
		public string Height { get; set; }

		[DataMember]
		public string Weight { get; set; }

		[DataMember]
		public string EyeColor { get; set; }

		[DataMember]
		public string HairColor { get; set; }

		[DataMember]
		public string PhoneHome { get; set; }

		[DataMember]
		public string PhoneCell { get; set; }

		[DataMember]
		public string PhoneCellCarrier { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public string CorporateEmail { get; set; }

		[DataMember]
		public bool HasVerifiedAddress { get; set; }

		[DataMember]
		public string RightToWorkExpirationDate { get; set; }

		[DataMember]
		public string RightToWorkNotes { get; set; }

		[DataMember]
		public string RightToWorkStatus { get; set; }

		//[DataMember]
		//public bool IsActive { get; set; }
		//[DataMember]
		//public bool IsDeleted { get; set; }
		[DataMember]
		public string Status { get; set; }

		[DataMember]
		public string RecruitedDate { get; set; }

		[DataMember]
		public string CreatedBy { get; set; }

		[DataMember]
		public string CreatedOn { get; set; }

		[DataMember]
		public string ModifiedBy { get; set; }

		[DataMember]
		public string ModifiedDate { get; set; }

		#endregion //Properties
	}

}
