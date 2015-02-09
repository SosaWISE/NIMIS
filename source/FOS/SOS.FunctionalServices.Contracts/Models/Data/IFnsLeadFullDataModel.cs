using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsLeadFullDataModel
	{
		[DataMember]
		IFnsLeadAddressModel Address { get; set; }
		[DataMember]
		IFnsAeCustomerType CustomerType { get; set; }
		[DataMember]
		IFnsAeDealer Dealer { get; set; }
		[DataMember]
		long LeadID { get; set; }
		[DataMember]
		List<IFosLeadProductOffer> ProductSkwIdList { get; set; }
		[DataMember]
		string CustomerTypeId { get; set; }
		[DataMember]
		long CustomerMasterFileId { get; set; }
		[DataMember]
		int DealerId { get; set; }
		[DataMember]
		string LocalizationId { get; set; }
		[DataMember]
		IFnsMcLocalization Localization { get; set; }
		[DataMember]
		int TeamLocationId { get; set; }
		[DataMember]
		int LeadSourceId { get; set; }
		[DataMember]
		string LeadSource { get; set; }
		[DataMember]
		int LeadDispositionId { get; set; }
		[DataMember]
		string LeadDisposition { get; set; }
		[DataMember]
		DateTime? LeadDispositionDateChange { get; set; }
		[DataMember]
		int SeasonId { get; set; }
		[DataMember]
		string SalesRepId { get; set; }
		[DataMember]
		IFnsRuUser SalesRep { get; set; }
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

	public interface IFosLeadProductOffer
	{
		[DataMember]
		long LeadProductOfferedId { get; set; }

		[DataMember]
		long LeadId { get; set; }

		[DataMember]
		string ProductSkwId { get; set; }

		[DataMember]
		string ProductName { get; set; }

		[DataMember]
		string ShortName { get; set; }

		[DataMember]
		string ProductTypeName { get; set; }

		[DataMember]
		string ProductImageName { get; set; }

		[DataMember]
		string SalesRepId { get; set; }

		[DataMember]
		string SalesRepFullName { get; set; }

		[DataMember]
		DateTime OfferDate { get; set; }
	}
}