using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsCustomerFullDataModel
	{
		[DataMember]
		long CustomerID { get; set; }

		[DataMember]
		long AddressId { get; set; }
		
		[DataMember]
		IFnsCustomerAddressModel Address { get; set; }
		
		[DataMember]
		IFnsAeCustomerType CustomerType { get; set; }
		[DataMember]
		long LeadId { get; set; }
		[DataMember]
		string CustomerTypeId { get; set; }
		[DataMember]
		long CustomerMasterFileId { get; set; }
		[DataMember]
		int DealerId { get; set; }
		[DataMember]
		IFnsAeDealer Dealer { get; set; }
		[DataMember]
		string LocalizationId { get; set; }
		[DataMember]
		IFnsMcLocalization Localization { get; set; }
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
		string Email { get; set; }
		[DataMember]
		string PhoneHome { get; set; }
		[DataMember]
		string PhoneWork { get; set; }
		[DataMember]
		string PhoneMobile { get; set; }
		[DataMember]
		string Username { get; set; }
		[DataMember]
		bool IsActive { get; set; }
		[DataMember]
		bool IsDeleted { get; set; }
		[DataMember]
		DateTime ModifiedOn { get; set; }
		[DataMember]
		string ModifiedBy { get; set; }
		[DataMember]
		DateTime CreatedOn { get; set; }
		[DataMember]
		string CreatedBy { get; set; }
	}
}