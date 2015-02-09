using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsAePaymentInformationCreateAccountModel
	{
		[DataMember]
		long LeadId { get; set; }

		[DataMember]
		long CustomerMasterFileId { get; set; }

		[DataMember]
		int DealerId { get; set; }

		[DataMember]
		string[] ProductSkws { get; set; }

		[DataMember]
		string DealerAccountID { get; set; }

		[DataMember]
		int ContractTemplateID { get; set; }

		[DataMember]
		IFnsBillingInfoModel BillingInfo { get; set; }

		[DataMember]
		IFnsBillingAddressModel BillingAddress { get; set; }

		[DataMember]
		IFnsPaymentInfoModel PaymentInformation { get; set; }
	}

	#region BillingInfoModel

	public interface IFnsBillingInfoModel
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

	#endregion BillingInfoModel

	#region BillingAddressModel

	public interface IFnsBillingAddressModel
	{
		bool SameAsCustomer { get; set; }
		string ValidationVendorId { get; set; }
		string AddressValidationStateId { get; set; }
		string Street { get; set; }
		string AddressTypeId { get; set; }
		string Street2 { get; set; }
		string City { get; set; }
		string StateId { get; set; }
		string PostalCode { get; set; }
		string CountryId { get; set; }
		int TimeZoneId { get; set; }
		double Latitude { get; set; }
		double Longitude { get; set; }
		bool DPV { get; set; }
	}

	#endregion BillingAddressModel

	#region PaymentInfoModel

	public enum EnumFnsPaymentMethod
	{
		FnsCreditCard
		, FnsCheck
	}

    public static class AePaymentInfo
    {
        public static string Message(this EnumFnsPaymentMethod cntl)
        {
            switch (cntl)
            {
                case EnumFnsPaymentMethod.FnsCheck:
                    return "Check";
                case EnumFnsPaymentMethod.FnsCreditCard:
                    return "CreditCard";
            }
            return null;
        }
    }

    public interface IFnsPaymentInfoModel
	{
		EnumFnsPaymentMethod PaymentMethod { get; set; }
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

	#endregion PaymentInfoModel

}