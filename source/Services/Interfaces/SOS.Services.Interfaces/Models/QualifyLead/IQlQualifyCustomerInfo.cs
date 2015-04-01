using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.QualifyLead
{
	public class QlQualifyCustomerInfo : IQlQualifyCustomerInfo
	{
		#region Properties
		public long LeadID { get; set; }
		public int SeasonId { get; set; }
		public string CustomerName { get; set; }
		public string CustomerEmail { get; set; }
		public DateTime? DOB { get; set; }
		public long AddressID { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string County { get; set; }
		public int TimeZoneId { get; set; }
		public string TimeZoneName { get; set; }
		public string PostalCode { get; set; }
		public string Phone { get; set; }
		public DateTime CreditCreatedOn { get; set; }
		public long CreditReportID { get; set; }
		public bool IsHit { get; set; }
		public string CRStatus { get; set; }
		public int Score { get; set; }
		public string CreditGroup { get; set; }
		public string BureauName { get; set; }
		public int? UserID { get; set; }
		public string CompanyID { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string PreferredName { get; set; }
		public string RepEmail { get; set; }
		public string PhoneCell { get; set; }
		public short? PhoneCellCarrierID { get; set; }
		public string PhoneCellCarrier { get; set; }
		public string SeasonName { get; set; }
		#endregion Properties
	}

	public interface IQlQualifyCustomerInfo
	{
		long LeadID { get; set; }
		int SeasonId { get; set; }
		string CustomerName { get; set; }
		string CustomerEmail { get; set; }
		DateTime? DOB { get; set; }
		long AddressID { get; set; }
		string StreetAddress { get; set; }
		string StreetAddress2 { get; set; }
		string City { get; set; }
		string StateId { get; set; }
		string County { get; set; }
		int TimeZoneId { get; set; }
		string TimeZoneName { get; set; }
		string PostalCode { get; set; }
		string Phone { get; set; }
		DateTime CreditCreatedOn { get; set; }
		long CreditReportID { get; set; }
		bool IsHit { get; set; }
		string CRStatus { get; set; }
		int Score { get; set; }
		string CreditGroup { get; set; }
		string BureauName { get; set; }
		int? UserID { get; set; }
		string CompanyID { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string PreferredName { get; set; }
		string RepEmail { get; set; }
		string PhoneCell { get; set; }
		short? PhoneCellCarrierID { get; set; }
		string PhoneCellCarrier { get; set; }
		string SeasonName { get; set; }

	}
}