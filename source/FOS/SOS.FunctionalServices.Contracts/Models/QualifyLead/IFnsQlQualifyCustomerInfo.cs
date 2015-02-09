using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.QualifyLead
{
	public interface IFnsQlQualifyCustomerInfo
	{
		[DataMember]
		long LeadID { get; }
		[DataMember]
		int SeasonId { get; }
		[DataMember]
		string CustomerName { get; }
		[DataMember]
		string CustomerEmail { get; }
		[DataMember]
		DateTime? DOB { get; }
		[DataMember]
		long AddressID { get; }
		[DataMember]
		string StreetAddress { get; }
		[DataMember]
		string StreetAddress2 { get; }
		[DataMember]
		string City { get; }
		[DataMember]
		string StateId { get; }
		[DataMember]
		string County { get; }
		[DataMember]
		int TimeZoneId { get; }
		[DataMember]
		string TimeZoneName { get; }
		[DataMember]
		string PostalCode { get; }
		[DataMember]
		string Phone { get; }
		[DataMember]
		long CreditReportID { get; }
		[DataMember]
		bool IsHit { get; }
		[DataMember]
		string CRStatus { get; }
		[DataMember]
		int Score { get; }
		[DataMember]
		string CreditGroup { get; }
		[DataMember]
		string BureauName { get; }
		[DataMember]
		int UserID { get; }
		[DataMember]
		string CompanyID { get; }
		[DataMember]
		string FirstName { get; }
		[DataMember]
		string MiddleName { get; }
		[DataMember]
		string LastName { get; }
		[DataMember]
		string PreferredName { get; }
		[DataMember]
		string RepEmail { get; }
		[DataMember]
		string PhoneCell { get; }
		[DataMember]
		short? PhoneCellCarrierID { get; }
		[DataMember]
		string PhoneCellCarrier { get; }
		[DataMember]
		string SeasonName { get; }
	}
}