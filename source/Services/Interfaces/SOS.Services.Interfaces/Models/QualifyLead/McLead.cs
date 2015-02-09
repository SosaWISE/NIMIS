using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.QualifyLead
{
	public class McLead : IMcLead
	{
		#region Properties
		public long LeadID { get; set; }
		public long AddressId { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		public int TeamLocationId { get; set; }
		public int SeasonId { get; set; }
		public string SalesRepId { get; set; }
		public int LeadSourceId { get; set; }
		public int LeadDispositionId { get; set; }
		public DateTime LeadDispositionDateChange { get; set; }
		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string Gender { get; set; }
		public string SSN { get; set; }
		public DateTime DOB { get; set; }
		public string DL { get; set; }
		public string DLStateID { get; set; }
		public string Email { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		#endregion Properties
	}

	public interface IMcLead
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
		int LeadSourceId { get; set; }
		[DataMember]
		int LeadDispositionId { get; set; }
		[DataMember]
		DateTime LeadDispositionDateChange { get; set; }
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
		DateTime DOB { get; set; }
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
		bool IsActive { get; set; }
		[DataMember]
		bool IsDeleted { get; set; }
		[DataMember]
		DateTime CreatedOn { get; set; }
		[DataMember]
		string CreatedBy { get; set; }
	}
}
