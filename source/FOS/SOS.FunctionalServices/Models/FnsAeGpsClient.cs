using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models
{
	public class FnsAeGpsClient : IFnsAeGpsClient
	{
		public FnsAeGpsClient(AE_GpsClientToCustomerMasterView oItem)
		{
			CustomerID = oItem.CustomerID;
			IsCurrent = oItem.IsCurrent;
			CustomerMasterFileId = oItem.CustomerMasterFileId;
			CustomerTypeId = oItem.CustomerTypeId;
			CustomerTypeUi = oItem.CustomerTypeUi;
			DealerId = oItem.DealerId;
			DealerName = oItem.DealerName;
			AddressId = oItem.AddressId;
			LeadId = oItem.LeadId;
			LocalizationId = oItem.LocalizationId;
			LocalizationName = oItem.LocalizationName;
			Prefix = oItem.Prefix;
			FirstName = oItem.FirstName;
			MiddleName = oItem.MiddleName;
			LastName = oItem.LastName;
			Postfix = oItem.Postfix;
			Gender = oItem.Gender;
			PhoneHome = oItem.PhoneHome;
			PhoneWork = oItem.PhoneWork;
			PhoneMobile = oItem.PhoneMobile;
			Email = oItem.Email;
			DOB = oItem.DOB;
			SSN = oItem.SSN;
			Username = oItem.Username;
			Password = oItem.Password;
			LastLoginOn = oItem.LastLoginOn;
			IsActive = oItem.IsActive;
			IsDeleted = oItem.IsDeleted;
			ModifiedOn = oItem.ModifiedOn;
			ModifiedBy = oItem.ModifiedBy;
			CreatedOn = oItem.CreatedOn;
			CreatedBy = oItem.CreatedBy;
			DexRowTs = oItem.DEX_ROW_TS;
		}


		#region Properties

		public long CustomerID { get; private set; }
		public bool? IsCurrent { get; private set; }
		public long CustomerMasterFileId { get; private set; }
		public string CustomerTypeId { get; private set; }
		public string CustomerTypeUi { get; private set; }
		public int DealerId { get; private set; }
		public string DealerName { get; private set; }
		public long AddressId { get; private set; }
		public long LeadId { get; private set; }
		public string LocalizationId { get; private set; }
		public string LocalizationName { get; private set; }
		public string Prefix { get; private set; }
		public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public string LastName { get; private set; }
		public string Postfix { get; private set; }
		public string Gender { get; private set; }
		public string PhoneHome { get; private set; }
		public string PhoneWork { get; private set; }
		public string PhoneMobile { get; private set; }
		public string Email { get; private set; }
		public DateTime? DOB { get; private set; }
		public string SSN { get; private set; }
		public string Username { get; private set; }
		public string Password { get; private set; }
		public DateTime? LastLoginOn { get; private set; }
		public bool IsActive { get; private set; }
		public bool IsDeleted { get; private set; }
		public DateTime ModifiedOn { get; private set; }
		public string ModifiedBy { get; private set; }
		public DateTime CreatedOn { get; private set; }
		public string CreatedBy { get; private set; }
		public DateTime DexRowTs { get; private set; }

		#endregion Properties
				}
}