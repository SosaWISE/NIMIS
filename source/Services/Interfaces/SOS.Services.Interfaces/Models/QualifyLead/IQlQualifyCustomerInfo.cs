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
		public long CreditReportID { get; set; }
		public bool IsHit { get; set; }
		public string CRStatus { get; set; }
		public int Score { get; set; }
		public string CreditGroup { get; set; }
		public string BureauName { get; set; }
		public int UserID { get; set; }
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
		[DataMember]
		long LeadID { get; set; }
		[DataMember]
		int SeasonId { get; set; }
		[DataMember]
		string CustomerName { get; set; }
		[DataMember]
		string CustomerEmail { get; set; }
		[DataMember]
		DateTime? DOB { get; set; }
		[DataMember]
		long AddressID { get; set; }
		[DataMember]
		string StreetAddress { get; set; }
		[DataMember]
		string StreetAddress2 { get; set; }
		[DataMember]
		string City { get; set; }
		[DataMember]
		string StateId { get; set; }
		[DataMember]
		string County { get; set; }
		[DataMember]
		int TimeZoneId { get; set; }
		[DataMember]
		string TimeZoneName { get; set; }
		[DataMember]
		string PostalCode { get; set; }
		[DataMember]
		string Phone { get; set; }
		[DataMember]
		long CreditReportID { get; set; }
		[DataMember]
		bool IsHit { get; set; }
		[DataMember]
		string CRStatus { get; set; }
		[DataMember]
		int Score { get; set; }
		[DataMember]
		string CreditGroup { get; set; }
		[DataMember]
		string BureauName { get; set; }
		[DataMember]
		int UserID { get; set; }
		[DataMember]
		string CompanyID { get; set; }
		[DataMember]
		string FirstName { get; set; }
		[DataMember]
		string MiddleName { get; set; }
		[DataMember]
		string LastName { get; set; }
		[DataMember]
		string PreferredName { get; set; }
		[DataMember]
		string RepEmail { get; set; }
		[DataMember]
		string PhoneCell { get; set; }
		[DataMember]
		short? PhoneCellCarrierID { get; set; }
		[DataMember]
		string PhoneCellCarrier { get; set; }
		[DataMember]
		string SeasonName { get; set; }

	}
}