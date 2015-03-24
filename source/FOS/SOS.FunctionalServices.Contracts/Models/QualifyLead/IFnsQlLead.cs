using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.QualifyLead
{
	public interface IFnsQlLead
	{
		long LeadID { get; set; }
		long AddressId { get; set; }
		string CustomerTypeId { get; set; }
		long CustomerMasterFileId { get; set; }
		int DealerId { get; set; }
		string LocalizationId { get; set; }
		int TeamLocationId { get; set; }
		int SeasonId { get; set; }
		string SalesRepId { get; set; }
		int LeadSourceId { get; set; }
		int LeadDispositionId { get; set; }
		DateTime? LeadDispositionDateChange { get; set; }
		string Salutation { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string Suffix { get; set; }
		string Gender { get; set; }
		string SSN { get; set; }
		DateTime? DOB { get; set; }
		string DL { get; set; }
		string DLStateId { get; set; }
		string Email { get; set; }
		string PhoneWork { get; set; }
		string PhoneHome { get; set; }
		string PhoneMobile { get; set; }
		string ProductSkwId { get; set; }
	}
}