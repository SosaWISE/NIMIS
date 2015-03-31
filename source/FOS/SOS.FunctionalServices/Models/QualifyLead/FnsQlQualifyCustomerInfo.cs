using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using System;

namespace SOS.FunctionalServices.Models.QualifyLead
{
	public class FnsQlQualifyCustomerInfo : IFnsQlQualifyCustomerInfo
	{
		#region .ctor

		public FnsQlQualifyCustomerInfo(QL_QualifyCustomerInfoView item)
		{
			LeadID = item.LeadID;
			SeasonId = item.SeasonId;
			CustomerName = item.CustomerName;
			CustomerEmail = item.CustomerEmail;
			DOB = item.DOB;
			AddressID = item.AddressID;
			StreetAddress = item.StreetAddress;
			StreetAddress2 = item.StreetAddress2;
			City = item.City;
			StateId = item.StateId;
			County = item.County;
			TimeZoneId = item.TimeZoneId;
			TimeZoneName = item.TimeZoneName;
			PostalCode = item.PostalCode;
			Phone = item.Phone;
			CreditReportID = item.CreditReportID;
			IsHit = item.IsHit;
			CRStatus = item.CRStatus;
			Score = item.Score;
			CreditGroup = Lib.Core.CreditReportService.CreditHelper.GetCreditScoreGroup(item.Score, item.IsHit,
				excellentCreditScore: item.ExcellentCreditScoreThreshold,
				passCreditScore: item.PassCreditScoreThreshold,
				subCreditScore: item.SubCreditScoreThreshold).ToString();
			BureauName = item.BureauName;
			UserID = item.UserID;
			CompanyID = item.CompanyID;
			FirstName = item.FirstName;
			MiddleName = item.MiddleName;
			LastName = item.LastName;
			PreferredName = item.PreferredName;
			RepEmail = item.RepEmail;
			PhoneCell = item.PhoneCell;
			PhoneCellCarrierID = item.PhoneCellCarrierID;
			PhoneCellCarrier = item.PhoneCellCarrier;
			SeasonName = item.SeasonName;
		}

		#endregion .ctor

		#region Properties
		public long LeadID { get; private set; }
		public int SeasonId { get; private set; }
		public string CustomerName { get; private set; }
		public string CustomerEmail { get; private set; }
		public DateTime? DOB { get; private set; }
		public long AddressID { get; private set; }
		public string StreetAddress { get; private set; }
		public string StreetAddress2 { get; private set; }
		public string City { get; private set; }
		public string StateId { get; private set; }
		public string County { get; private set; }
		public int TimeZoneId { get; private set; }
		public string TimeZoneName { get; private set; }
		public string PostalCode { get; private set; }
		public string Phone { get; private set; }
		public long CreditReportID { get; private set; }
		public bool IsHit { get; private set; }
		public string CRStatus { get; private set; }
		public int Score { get; private set; }
		public string CreditGroup { get; private set; }
		public string BureauName { get; private set; }
		public int? UserID { get; private set; }
		public string CompanyID { get; private set; }
		public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public string LastName { get; private set; }
		public string PreferredName { get; private set; }
		public string RepEmail { get; private set; }
		public string PhoneCell { get; private set; }
		public short? PhoneCellCarrierID { get; private set; }
		public string PhoneCellCarrier { get; private set; }
		public string SeasonName { get; private set; }
		#endregion Properties
	}
}
