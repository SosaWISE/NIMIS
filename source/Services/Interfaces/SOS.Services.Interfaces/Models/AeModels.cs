using System;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.CmsModels;

namespace SOS.Services.Interfaces.Models
{
	public static class AeModels
	{
		#region InvoiceFilePath

		public interface IInvoice
		{
			string InvoiceFilePath { get; set; }
		}

		public class Invoice : IInvoice
		{
			#region Implementation of IAeInvoice

			public string InvoiceFilePath { get; set; }

			#endregion
		}

		#endregion InvoiceFilePath

		#region AePaymentInformationCreateAccountModel
		
		public interface IAePaymentInformation
		{
			long CustomerMasterFileId { get; set; }
			long LeadId { get; set; }
			int DealerId { get; set; }
			string ProductSkws { get; set; }
			string DealerAccountID { get; set; }
			int ContractTemplateID { get; set; }
			BillingInfoModel BillingInfo { get; set; }
			BillingAddressModel BillingAddress { get; set; }
			PaymentInfoModel PaymentInformation { get; set; }
		}

		public class AePaymentInformationCreateAccountModel : IAePaymentInformation
		{
			#region Implementation of IAePaymentInformation

			public long CustomerMasterFileId { get; set; }
			public long LeadId { get; set; }
			public int DealerId { get; set; }
			public string ProductSkws { get; set; }
			public string DealerAccountID { get; set; }
			public int ContractTemplateID { get; set; }
			public BillingInfoModel BillingInfo { get; set; }
			public BillingAddressModel BillingAddress { get; set; }
			public PaymentInfoModel PaymentInformation { get; set; }

			#endregion Implementation of IAePaymentInformation
		}

		#region BillingInfoModel

		public interface IBillingInfoModel
		{
			bool SameAsCustomer { get; set; }
			string Salutation { get; set; }
			string FirstName { get; set; }
			string MiddleName { get; set; }
			string LastName { get; set; }
			string Suffix { get; set; }
			DateTime? DOB { get; set; }
			string SSN { get; set; }
			string Gender { get; set; }
			string Email { get; set; }
			string Language { get; set; }
			string PhoneHome { get; set; }
			string PhoneMobile { get; set; }
			string PhoneWork { get; set; }
		}

		public class BillingInfoModel : IBillingInfoModel
		{
			#region Implementation of IBillingInfoModel

			public bool SameAsCustomer { get; set; }
			public string Salutation { get; set; }
			public string FirstName { get; set; }
			public string MiddleName { get; set; }
			public string LastName { get; set; }
			public string Suffix { get; set; }
			public DateTime? DOB { get; set; }
			public string SSN { get; set; }
			public string Gender { get; set; }
			public string Email { get; set; }
			public string Language { get; set; }
			public string PhoneHome { get; set; }
			public string PhoneMobile { get; set; }
			public string PhoneWork { get; set; }

			#endregion
		}

		#endregion BillingInfoModel

		#region BillingAddressModel

		public interface IBillingAddressModel
		{
			bool SameAsCustomer { get; set; }
			string ValidationVendorId { get; set; }
			string AddressValidationStateId { get; set; }
			string Street { get; set; }
			string Street2 { get; set; }
			string AddressTypeId { get; set; }
			string City { get; set; }
			string StateId { get; set; }
			string PostalCode { get; set; }
			string CountryId { get; set; }
			int TimeZoneId { get; set; }
			double Latitude { get; set; }
			double Longitude { get; set; }
			bool DPV { get; set; }
		}

		public class BillingAddressModel : IBillingAddressModel
		{
			#region Implementation of IBillingAddressModel

			public bool SameAsCustomer { get; set; }
			public string ValidationVendorId { get; set; }
			public string AddressValidationStateId { get; set; }
			public string Street { get; set; }
			public string AddressTypeId { get; set; }
			public string Street2 { get; set; }
			public string City { get; set; }
			public string StateId { get; set; }
			public string PostalCode { get; set; }
			public string CountryId { get; set; }
			public int TimeZoneId { get; set; }
			public double Latitude { get; set; }
			public double Longitude { get; set; }
			public bool DPV { get; set; }

			#endregion
		}

		#endregion BillingAddressModel

		#region PaymentInfoModel

		public enum EnumPaymentMethod
		{
			CreditCard
			, Check
		}

		public interface IPaymentInfoModel
		{
			EnumPaymentMethod PaymentMethod { get; set; }
			string PONumber { get; set; }
			string NameOnCard { get; set; }
			string CCNumber { get; set; }
			string CCV { get; set; }
			short? ExpMonth { get; set; }
			short? ExpYear { get; set; }
			string RoutingNumber { get; set; }
			string AccountNumber { get; set; }
			string CheckNumber { get; set; }
		}

		public class PaymentInfoModel : IPaymentInfoModel
		{
			#region Implementation of IPaymentInfoModel

			public EnumPaymentMethod PaymentMethod { get; set; }
			public string PONumber { get; set; }
			public string NameOnCard { get; set; }
			public string CCNumber { get; set; }
			public string CCV { get; set; }
			public short? ExpMonth { get; set; }
			public short? ExpYear { get; set; }
			public string RoutingNumber { get; set; }
			public string AccountNumber { get; set; }
			public string CheckNumber { get; set; }

			#endregion
		}
		#endregion PaymentInfoModel

		#endregion AePaymentInformationCreateAccountModel

		#region AeCustomerFullData

		public class AeCustomerFullData : IAeCustomerFullData
		{
			#region Implementation of IAeCustomerFullData

			public long CustomerID { get; set; }
			public string CustomerTypeId { get; set; }
			public AeCustomerType CustomerType { get; set; }
			public long CustomerMasterFileId { get; set; }
			public int DealerId { get; set; }
			public McAddress Address { get; set; }
			public AeDealer Dealer { get; set; }
			public long LeadId { get; set; }
			public QlLeadBasicView LeadBasicView { get; set; }
			public string LocalizationId { get; set; }
			public McLocalization Localization { get; set; }
			public string Salutation { get; set; }
			public string FirstName { get; set; }
			public string MiddleName { get; set; }
			public string LastName { get; set; }
			public string Suffix { get; set; }
			public string Gender { get; set; }
			public string SSN { get; set; }
			public DateTime? DOB { get; set; }
			public string Email { get; set; }
			public string PhoneHome { get; set; }
			public string PhoneWork { get; set; }
			public string PhoneMobile { get; set; }
			public bool IsActive { get; set; }
			public bool IsDeleted { get; set; }
			public DateTime ModifiedOn { get; set; }
			public string ModifiedBy { get; set; }
			public DateTime CreatedOn { get; set; }
			public string CreatedBy { get; set; }

			#endregion Implementation of IAeCustomerFullData
		}

		public interface IAeCustomerFullData
		{
			long CustomerID { get; set; }
			string CustomerTypeId { get; set; }
			AeCustomerType CustomerType { get; set; }
			long CustomerMasterFileId { get; set; }
			int DealerId { get; set; }
			McAddress Address { get; set; }
			AeDealer Dealer { get; set; }
			long LeadId { get; set; }
			QlLeadBasicView LeadBasicView { get; set; }
			string LocalizationId { get; set; }
			McLocalization Localization { get; set; }
			string Salutation { get; set; }
			string FirstName { get; set; }
			string MiddleName { get; set; }
			string LastName { get; set; }
			string Suffix { get; set; }
			string Gender { get; set; }
			string SSN { get; set; }
			DateTime? DOB { get; set; }
			string Email { get; set; }
			string PhoneHome { get; set; }
			string PhoneWork { get; set; }
			string PhoneMobile { get; set; }
			bool IsActive { get; set; }
			bool IsDeleted { get; set; }
			DateTime ModifiedOn { get; set; }
			string ModifiedBy { get; set; }
			DateTime CreatedOn { get; set; }
			string CreatedBy { get; set; }
		}

		#endregion AeCustomerFullData

	}
}
