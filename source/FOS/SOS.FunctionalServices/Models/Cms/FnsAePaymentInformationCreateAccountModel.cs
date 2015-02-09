using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models.Cms
{
	public class FnsAePaymentInformationCreateAccountModel : IFnsAePaymentInformationCreateAccountModel
	{
		#region Implementation of IFnsAePaymentInformationCreateAccountModel

		public long LeadId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public string[] ProductSkws { get; set; }
		public string DealerAccountID { get; set; }
		public int ContractTemplateID { get; set; }
		public IFnsBillingInfoModel BillingInfo { get; set; }
		public IFnsBillingAddressModel BillingAddress { get; set; }
		public IFnsPaymentInfoModel PaymentInformation { get; set; }

		#endregion Implementation of IFnsAePaymentInformationCreateAccountModel

		public MC_Address BindToMcAddress(string szUserId)
		{
			/** Initialize. */
			var oAddress = new MC_Address
				{
					DealerId = DealerId
					, ValidationVendorId = !string.IsNullOrWhiteSpace(BillingAddress.ValidationVendorId)
						? BillingAddress.ValidationVendorId
						: MC_AddressValidationVendor.MetaData.No_Vendor_DefaultID
					, AddressValidationStateId = !string.IsNullOrWhiteSpace(BillingAddress.AddressValidationStateId)
						? BillingAddress.AddressValidationStateId
						: MC_AddressValidationState.MetaData.UnverifiedID
					, StreetAddress = BillingAddress.Street
					, AddressTypeId = !string.IsNullOrWhiteSpace(BillingAddress.AddressTypeId)
						? BillingAddress.AddressTypeId
						: MC_AddressType.MetaData.Non_StandardID
					, City = BillingAddress.City
					, StateId = BillingAddress.StateId
					, CountryId = BillingAddress.CountryId
					, TimeZoneId = BillingAddress.TimeZoneId
					, Latitude = BillingAddress.Latitude
					, Longitude = BillingAddress.Longitude
					, DPV = BillingAddress.DPV
				};

			if (!string.IsNullOrWhiteSpace(BillingAddress.Street2)) oAddress.StreetAddress2 = BillingAddress.Street2;
			string[] saPostalCode = BillingAddress.PostalCode.Split('-');
			if (saPostalCode.Length > 1) oAddress.PlusFour = saPostalCode[1];
			oAddress.PostalCode = saPostalCode[0];

			/** Save. */
			oAddress.Save(szUserId);

			/** Return result. */
			return oAddress;
		}

		public AE_Customer BindToAeCustomer(long lAddressID, string szUserId)
		{
			/** Initialize. */
			var oBillingCustomer = new AE_Customer
				{
					CustomerTypeId = AE_CustomerType.MetaData.Billing_CustomerID
					, CustomerMasterFileId = CustomerMasterFileId
					, DealerId = DealerId
					, AddressId = lAddressID
					, LeadId = LeadId
					, LocalizationId = BillingInfo.Language
					, Prefix = !string.IsNullOrWhiteSpace(BillingInfo.Salutation) ? BillingInfo.Salutation : null
					, FirstName = BillingInfo.FirstName
					, MiddleName = !string.IsNullOrWhiteSpace(BillingInfo.MiddleName) ? BillingInfo.MiddleName : null
					, LastName = BillingInfo.LastName
					, Postfix = !string.IsNullOrWhiteSpace(BillingInfo.Suffix) ? BillingInfo.Suffix : null
					, DOB = BillingInfo.DOB
					, SSN = !string.IsNullOrWhiteSpace(BillingInfo.SSN) ? Lib.Util.Cryptography.TripleDES.EncryptString(BillingInfo.SSN, null) : null
					, Gender = BillingInfo.Gender
					, Email = !string.IsNullOrWhiteSpace(BillingInfo.Email) ? BillingInfo.Email : null
					, PhoneHome = !string.IsNullOrWhiteSpace(BillingInfo.PhoneHome) ? BillingInfo.PhoneHome : null
					, PhoneMobile = !string.IsNullOrWhiteSpace(BillingInfo.PhoneMobile) ? BillingInfo.PhoneHome : null
					, PhoneWork = !string.IsNullOrWhiteSpace(BillingInfo.PhoneWork) ? BillingInfo.PhoneHome : null
				};

			/** Save */
			oBillingCustomer.Save(szUserId);

			/** Return result. */
			return oBillingCustomer;
		}
	}

	public class FnsBillingInfoModel : IFnsBillingInfoModel
	{
		#region Implementation of IFnsBillingInfoModel

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

		#endregion Implementation of IFnsBillingInfoModel
	}

	public class FnsBillingAddressModel : IFnsBillingAddressModel
	{
		#region Implementation of IFnsBillingAddressModel

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

		#endregion Implementation of IFnsBillingAddressModel
	}

	public class FnsPaymentInfoModel : IFnsPaymentInfoModel
	{
		#region Implementation of IFnsPaymentInfoModel

		public EnumFnsPaymentMethod PaymentMethod { get; set; }
		public string PONumber { get; set; }
		public string NameOnCard { get; set; }
		public string CCNumber { get; set; }
		public string CCV { get; set; }
		public short? ExpMonth { get; set; }
		public short? ExpYear { get; set; }
		public string RoutingNumber { get; set; }
		public string AccountNumber { get; set; }
		public string CheckNumber { get; set; }

		#endregion Implementation of IFnsPaymentInfoModel
	}
}