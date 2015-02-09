using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.QualifyLead
{
	public interface IFnsQlLead
	{
		[DataMember]
		long LeadID { get; set; }
		[DataMember]
		long AddressID { get; set; }
		[DataMember]
		string CustomerTypeId { get; set; }
		[DataMember]
		long CustomerMasterFileId { get; set; }
		[DataMember]
		int DealerId { get; set; }
		[DataMember]
		string LocalizationId { get; set; }
		[DataMember]
		int TeamLocationId { get; set; }
		[DataMember]
		int SeasonId { get; set; }
		[DataMember]
		string SalesRepId { get; set; }
		[DataMember]
		int LeadSourceId { get; set; }
		[DataMember]
		int LeadDispositionId { get; set; }
		[DataMember]
		DateTime? LeadDispositionDateChange { get; set; }
		[DataMember]
		string Salutation { get; set; }
		[DataMember]
		string FirstName { get; set; }
		[DataMember]
		string MiddleName { get; set; }
		[DataMember]
		string LastName { get; set; }
		[DataMember]
		string Suffix { get; set; }
		[DataMember]
		string Gender { get; set; }
		[DataMember]
		string SSN { get; set; }
		[DataMember]
		DateTime? DOB { get; set; }
		[DataMember]
		string DL { get; set; }
		[DataMember]
		string DLStateId { get; set; }
		[DataMember]
		string Email { get; set; }
		[DataMember]
		string PhoneWork { get; set; }
		[DataMember]
		string PhoneHome { get; set; }
		[DataMember]
		string PhoneMobile { get; set; }
		[DataMember]
		string ProductSkwId { get; set; }
	}
}