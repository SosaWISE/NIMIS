using System;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsLeadSearchResult : IFnsLeadSearchResult
	{
		#region .ctor

		public FnsLeadSearchResult(Data.SosCrm.QL_LeadSearchResultView oLeadSearchResult)
		{
			CustomerMasterFileId = oLeadSearchResult.CustomerMasterFileId;
			LeadId = oLeadSearchResult.LeadID;
			DealerId = oLeadSearchResult.DealerId;
			LocalizationId = oLeadSearchResult.LocalizationId;
			FirstName = oLeadSearchResult.FirstName;
			LastName = oLeadSearchResult.LastName;
			DispositionId = oLeadSearchResult.LeadDispositionId;
			Disposition = oLeadSearchResult.LeadDisposition;
			DispositionDateChange = oLeadSearchResult.LeadDispositionDateChange;
			SourceId = oLeadSearchResult.LeadSourceId;
			Source = oLeadSearchResult.LeadSource;
			PhoneHome = oLeadSearchResult.PhoneHome;
			PhoneWork = oLeadSearchResult.PhoneWork;
			PhoneMobile = oLeadSearchResult.PhoneMobile;
			DOB = oLeadSearchResult.DOB;
			SalesRepId = oLeadSearchResult.SalesRepId;
			SSN = oLeadSearchResult.SSN;
			DL = oLeadSearchResult.DL;
			DLStateID = oLeadSearchResult.DLStateID;
			Email = oLeadSearchResult.Email;
			IsCustomer = oLeadSearchResult.IsCustomer;
			RowNum = oLeadSearchResult.RowNum;
		}

		#endregion .ctor

		#region Implementation of IFnsLeadSearchResult

		public long CustomerMasterFileId { get; set; }
		public long LeadId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int DispositionId { get; set; }
		public string Disposition { get; set; }
		public DateTime? DispositionDateChange { get; set; }
		public int SourceId { get; set; }
		public string Source { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public DateTime? DOB { get; set; }
		public string SalesRepId { get; set; }
		public string SSN { get; set; }
		public string DL { get; set; }
		public string DLStateID { get; set; }
		public string Email { get; set; }
		public bool? IsCustomer { get; set; }
		public int? RowNum { get; set; }

		#endregion
	}
}
