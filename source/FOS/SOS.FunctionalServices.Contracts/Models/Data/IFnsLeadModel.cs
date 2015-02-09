using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsLeadModel
	{
		[DataMember]
		long LeadID { get; set; }
		[DataMember]
		long AddressId { get; set; }
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
		string SSN { get; set; }
		[DataMember]
		DateTime? DOB { get; set; }
		[DataMember]
		string DL { get; set; }
		[DataMember]
		string DLStateID { get; set; }
		[DataMember]
		string Email { get; set; }
		[DataMember]
		string PhoneHome { get; set; }
		[DataMember]
		string PhoneWork { get; set; }
		[DataMember]
		string PhoneMobile { get; set; }
		[DataMember]
		string PremisePhone { get; set; }
		[DataMember]
		string StreetAddress { get; set; }
		[DataMember]
		string City { get; set; }
		[DataMember]
		string StateId { get; set; }
		[DataMember]
		string Postal { get; set; }
		[DataMember]
		bool IsActive { get; set; }
		[DataMember]
		bool IsDeleted { get; set; }
	}
}
