using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeCustomerCardInfo
	{
		long CustomerID { get; }
		string CustomerTypeId { get; }
		long CustomerMasterFileID { get; }
		string Prefix { get; }
		string FirstName { get; }
		string MiddleName { get; }
		string LastName { get; }
		string PostFix { get; }
		string FullName { get; }
		string Gender { get; }
		string PhoneHome { get; }
		string PhoneWork { get; }
		string PhoneMobile { get; }
		string Email { get; }
		DateTime? DOB { get; }
		string SSN { get; }
		string Username { get; }
		string Password { get; }
		long AddressID { get; }
		string StreetAddress { get; }
		string StreetAddress2 { get; }
		String City { get; }
		string StateId { get; }
		string PostalCode { get; }
		string PlusFour { get; }
		string CityStateZip { get; }
		string CreditGroup { get; }
		double Latitude { get; }
		double Longitude { get; }
	}
}