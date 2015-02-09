using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeCustomerCardInfo
	{
		[DataMember]
		long CustomerID { get; }
		[DataMember]
		string CustomerTypeId { get; }
		[DataMember]
		long CustomerMasterFileID { get; }
		[DataMember]
		string Prefix { get; }
		[DataMember]
		string FirstName { get; }
		[DataMember]
		string MiddleName { get; }
		[DataMember]
		string LastName { get; }
		[DataMember]
		string PostFix { get; }
		[DataMember]
		string FullName { get; }
		[DataMember]
		string Gender { get; }
		[DataMember]
		string PhoneHome { get; }
		[DataMember]
		string PhoneWork { get; }
		[DataMember]
		string PhoneMobile { get; }
		[DataMember]
		string Email { get; }
		[DataMember]
		DateTime? DOB { get; }
		[DataMember]
		string SSN { get; }
		[DataMember]
		string Username { get; }
		[DataMember]
		string Password { get; }
		[DataMember]
		long AddressID { get; }
		[DataMember]
		string StreetAddress { get; }
		[DataMember]
		string StreetAddress2 { get; }
		[DataMember]
		String City { get; }
		[DataMember]
		string StateId { get; }
		[DataMember]
		string PostalCode { get; }
		[DataMember]
		string PlusFour { get; }
		[DataMember]
		string CityStateZip { get; }
		[DataMember]
		string CreditGroup { get; }
	}
}