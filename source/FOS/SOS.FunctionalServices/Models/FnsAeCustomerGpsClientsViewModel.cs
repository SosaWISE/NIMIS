using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models
{
	public class FnsAeCustomerGpsClientsViewModel : IFnsAeCustomerGpsClientsViewModel
	{
		#region .ctor

		public FnsAeCustomerGpsClientsViewModel() {}

		public FnsAeCustomerGpsClientsViewModel(AE_CustomerGpsClientsView oItem)
		{
			CustomerID = oItem.CustomerID;
			CustomerTypeId = oItem.CustomerTypeId;
			CustomerMasterFileId = oItem.CustomerMasterFileId;
			DealerId = oItem.DealerId;
			AddressId = oItem.AddressId;
			LeadId = oItem.LeadId;
			LocalizationId = oItem.LocalizationId;
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
			IsActive = oItem.IsActive;
			ModifiedOn = oItem.ModifiedOn;
			ModifiedBy = oItem.ModifiedBy;
			CreatedOn = oItem.CreatedOn;
			CreatedBy = oItem.CreatedBy;
		}

		public FnsAeCustomerGpsClientsViewModel(AE_Customer oItem)
		{
			CustomerID = oItem.CustomerID;
			CustomerTypeId = oItem.CustomerTypeId;
			CustomerMasterFileId = oItem.CustomerMasterFileId;
			DealerId = oItem.DealerId;
			AddressId = oItem.AddressId;
			LeadId = oItem.LeadId;
			LocalizationId = oItem.LocalizationId;
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
			IsActive = oItem.IsActive;
			ModifiedOn = oItem.ModifiedOn;
			ModifiedBy = oItem.ModifiedBy;
			CreatedOn = oItem.CreatedOn;
			CreatedBy = oItem.CreatedBy;
		}

		#endregion .ctor

		#region Properties
		public long CustomerID { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int LeadSourceId { get; set; }
		public int LeadDispositionId { get; set; }
		public int DealerId { get; set; }
		public string SalesRepId { get; set; }
		public int SeasonId { get; set; }
		public int TeamLocationId { get; set; }
		public long AddressId { get; set; }
		public long LeadId { get; set; }
		public string LocalizationId { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Postfix { get; set; }
		public string Gender { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string Email { get; set; }
		public DateTime? DOB { get; set; }
		public string SSN { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	    public string StateId { get; set; }
	    public string CountryId { get; set; }
	    public int TimezoneId { get; set; }
	    public string StreetAddress { get; set; }
	    public string StreetAddress2 { get; set; }
	    public string County { get; set; }
	    public string City { get; set; }
	    public string PostalCode { get; set; }
	    public string PlusFour { get; set; }
	    public bool IsActive { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		#endregion Properties

	}
}
