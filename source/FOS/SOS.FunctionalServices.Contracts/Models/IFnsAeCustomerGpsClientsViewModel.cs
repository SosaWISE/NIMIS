using System;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsAeCustomerGpsClientsViewModel
	{
		long CustomerID { get; set; }
		string CustomerTypeId { get; set; }
		long CustomerMasterFileId { get; set; }
		int LeadSourceId { get; set; }
		int LeadDispositionId { get; set; }
		int DealerId { get; set; }
		string SalesRepId { get; set; }
		int SeasonId { get; set; }
		int TeamLocationId { get; set; }
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
	    string StateId { get; set; }
	    string CountryId { get; set; }
	    int TimezoneId { get; set; }
	    string StreetAddress { get; set; }
	    string StreetAddress2 { get; set; }
	    string County { get; set; }
	    string City { get; set; }
	    string PostalCode { get; set; }
	    string PlusFour { get; set; }
        bool IsActive { get; set; }
        DateTime ModifiedOn { get; set; }
        string ModifiedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }
    }
}