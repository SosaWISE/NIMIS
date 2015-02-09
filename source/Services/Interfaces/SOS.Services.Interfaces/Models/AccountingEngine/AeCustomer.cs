using System;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	#region AeCustomer

	public class AeCustomer : IAeCustomer
	{
		public long CustomerID { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public string SalesRepId { get; set; }
		public int SeasonId { get; set; }
		public int TeamLocationId { get; set; }
		public int DealerId { get; set; }
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
		public bool IsActive { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public string StateId { get; set; }
		public string CountryId { get; set; }
		public string TimezoneId { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string County { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string Phone { get; set; }

	}

	public interface IAeCustomer
	{
		long CustomerID { get; set; }
		string CustomerTypeId { get; set; }
		long CustomerMasterFileId { get; set; }
		int DealerId { get; set; }
		long AddressId { get; set; }
		long LeadId { get; set; }
		string LocalizationId { get; set; }
		string Prefix { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string Postfix { get; set; }
		string Gender { get; set; }
		string PhoneHome { get; set; }
		string PhoneWork { get; set; }
		string PhoneMobile { get; set; }
		string Email { get; set; }
		DateTime? DOB { get; set; }
		string SSN { get; set; }
		string Username { get; set; }
		string Password { get; set; }
		bool IsActive { get; set; }
		DateTime ModifiedOn { get; set; }
		string ModifiedBy { get; set; }
		DateTime CreatedOn { get; set; }
		string CreatedBy { get; set; }
	}

	#endregion AeCustomer
}
