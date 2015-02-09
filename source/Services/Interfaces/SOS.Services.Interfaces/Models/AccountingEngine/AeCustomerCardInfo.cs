using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class AeCustomerCardInfo : IAeCustomerCardInfo
	{
		#region .ctor
		#endregion .ctor

		#region Properties
		public long CustomerID { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileID { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string PostFix { get; set; }
		public string FullName { get; set; }
		public string Gender { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string Email { get; set; }
		public DateTime? DOB { get; set; }
		public string SSN { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public long AddressID { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string CityStateZip { get; set; }
		public string CreditGroup { get; set; }
		#endregion Properties
	}

	public interface IAeCustomerCardInfo
	{
		[DataMember]
		long CustomerID { get; set; }
		[DataMember]
		string CustomerTypeId { get; set; }
		[DataMember]
		long CustomerMasterFileID { get; set; }
		[DataMember]
		string Prefix { get; set; }
		[DataMember]
		string FirstName { get; set; }
		[DataMember]
		string MiddleName { get; set; }
		[DataMember]
		string LastName { get; set; }
		[DataMember]
		string PostFix { get; set; }
		[DataMember]
		string FullName { get; set; }
		[DataMember]
		string Gender { get; set; }
		[DataMember]
		string PhoneHome { get; set; }
		[DataMember]
		string PhoneWork { get; set; }
		[DataMember]
		string PhoneMobile { get; set; }
		[DataMember]
		string Email { get; set; }
		[DataMember]
		DateTime? DOB { get; set; }
		[DataMember]
		string SSN { get; set; }
		[DataMember]
		string Username { get; set; }
		[DataMember]
		string Password { get; set; }
		[DataMember]
		long AddressID { get; set; }
		[DataMember]
		string StreetAddress { get; set; }
		[DataMember]
		string StreetAddress2 { get; set; }
		[DataMember]
		String City { get; set; }
		[DataMember]
		string StateId { get; set; }
		[DataMember]
		string PostalCode { get; set; }
		[DataMember]
		string PlusFour { get; set; }
		[DataMember]
		string CityStateZip { get; set; }
		[DataMember]
		string CreditGroup { get; set; }
	}

}
