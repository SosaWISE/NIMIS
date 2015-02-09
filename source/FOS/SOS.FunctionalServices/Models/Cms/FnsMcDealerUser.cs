using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models.Cms
{
	public class FnsMcDealerUser : IFnsMcDealerUser
	{
		public FnsMcDealerUser(MC_DealerUser oItem)
		{
			DealerUserID = oItem.DealerUserID;
			DealerUserTypeId = oItem.DealerUserTypeId;
			DealerId = oItem.DealerId;
			AuthUserId = oItem.AuthUserId;
			UserID = oItem.UserID;
			Firstname = oItem.Firstname;
			Middlename = oItem.Middlename;
			Lastname = oItem.Lastname;
			FullName = oItem.FullName;
			Email = oItem.Email;
			PhoneWork = oItem.PhoneWork;
			PhoneCell = oItem.PhoneCell;
			ADUsername = oItem.ADUsername;
			Username = oItem.Username;
			Password = oItem.Password;
			LastLoginOn = oItem.LastLoginOn;
			IsActive = oItem.IsActive;
			IsDeleted = oItem.IsDeleted;
			ModifiedOn = oItem.ModifiedOn;
			ModifiedBy = oItem.ModifiedBy;
			CreatedOn = oItem.CreatedOn;
			CreatedBy = oItem.CreatedBy;
		}

		#region Implementation of IFnsMcDealerUser

		public int DealerUserID { get; set; }
		public byte DealerUserTypeId { get; set; }
		public string DealerUserType { get; set; }
		public int DealerId { get; set; }
		public int? AuthUserId { get; set; }
		public string UserID { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }
		public string Lastname { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneCell { get; set; }
		public string ADUsername { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime? LastLoginOn { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		#endregion Implementation of IFnsMcDealerUser
	}
}