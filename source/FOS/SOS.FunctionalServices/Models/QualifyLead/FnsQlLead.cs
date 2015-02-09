using System;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;

namespace SOS.FunctionalServices.Models.QualifyLead
{
	public class FnsQlLead : IFnsQlLead
	{
		public long LeadID { get; set; }
		public long AddressID { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		public int TeamLocationId { get; set; }
		public int SeasonId { get; set; }
		public string SalesRepId { get; set; }
		public int LeadSourceId { get; set; }
		public int LeadDispostionId { get; set; }
		public DateTime LeadDispositionDateChange { get; set; }
		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string Gender { get; set; }
		public string SSN { get; set; }
		public DateTime? DOB { get; set; }
		public string DL { get; set; }
		public string DLStateId { get; set; }
		public string Email { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneMobile { get; set; }
		public string ProductSkwId { get; set; }
	}
}
