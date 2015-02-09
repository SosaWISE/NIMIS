using System;
using System.Collections.Generic;
using SOS.Services.Interfaces.Models.AccountingEngine;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region QlLeadFullData

	public interface IQlLeadFullData
	{
		QlAddress Address { get; set; }
		AeCustomerType CustomerType { get; set; }
		List<QlLeadProductOffer> ProductSkwIdList { get; set; }
		long LeadID { get; set; }
		string CustomerTypeId { get; set; }
		long CustomerMasterFileId { get; set; }
		int DealerId { get; set; }
		AeDealer Dealer { get; set; }
		string LocalizationId { get; set; }
		McLocalization Localization { get; set; }
		int TeamLocationId { get; set; }
		int LeadSourceId { get; set; }
		string LeadSource { get; set; }
		int LeadDispositionId { get; set; }
		string LeadDisposition { get; set; }
		DateTime? LeadDispositionDateChange { get; set; }
		int SeasonId { get; set; }
		string SalesRepId { get; set; }
		RuModels.IRuUser SalesRep { get; set; }
		string Salutation { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string Suffix { get; set; }
		string Gender { get; set; }
		string SSN { get; set; }
		DateTime? DOB { get; set; }
		string DL { get; set; }
		string DLStateID { get; set; }
		string Email { get; set; }
		string PhoneHome { get; set; }
		string PhoneWork { get; set; }
		string PhoneMobile { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		DateTime CreatedOn { get; set; }
		string CreatedBy { get; set; }
	}

	public class QlLeadFullData : IQlLeadFullData
	{
		#region Implementation of IQlLeadFullData

		public QlAddress Address { get; set; }
		public AeCustomerType CustomerType { get; set; }
		public List<QlLeadProductOffer> ProductSkwIdList { get; set; }
		public long LeadID { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public AeDealer Dealer { get; set; }
		public string LocalizationId { get; set; }
		public McLocalization Localization { get; set; }
		public int TeamLocationId { get; set; }
		public int LeadSourceId { get; set; }
		public string LeadSource { get; set; }
		public int LeadDispositionId { get; set; }
		public string LeadDisposition { get; set; }
		public DateTime? LeadDispositionDateChange { get; set; }
		public int SeasonId { get; set; }
		public string SalesRepId { get; set; }
		public RuModels.IRuUser SalesRep { get; set; }
		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string Gender { get; set; }
		public string SSN { get; set; }
		public DateTime? DOB { get; set; }
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

		#endregion Implementation of IQlLeadFullData
	}

	#endregion QlLeadFullData
}
