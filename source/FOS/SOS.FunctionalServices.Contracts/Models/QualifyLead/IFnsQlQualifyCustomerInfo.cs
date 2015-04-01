using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.QualifyLead
{
	public interface IFnsQlQualifyCustomerInfo
	{
		long LeadID { get; }
		int SeasonId { get; }
		string CustomerName { get; }
		string CustomerEmail { get; }
		DateTime? DOB { get; }
		long AddressID { get; }
		string StreetAddress { get; }
		string StreetAddress2 { get; }
		string City { get; }
		string StateId { get; }
		string County { get; }
		int TimeZoneId { get; }
		string TimeZoneName { get; }
		string PostalCode { get; }
		string Phone { get; }
		DateTime CreditCreatedOn { get; }
		long CreditReportID { get; }
		bool IsHit { get; }
		string CRStatus { get; }
		int Score { get; }
		string CreditGroup { get; }
		string BureauName { get; }
		int? UserID { get; }
		string CompanyID { get; }
		string FirstName { get; }
		string MiddleName { get; }
		string LastName { get; }
		string PreferredName { get; }
		string RepEmail { get; }
		string PhoneCell { get; }
		short? PhoneCellCarrierID { get; }
		string PhoneCellCarrier { get; }
		string SeasonName { get; }
	}
}