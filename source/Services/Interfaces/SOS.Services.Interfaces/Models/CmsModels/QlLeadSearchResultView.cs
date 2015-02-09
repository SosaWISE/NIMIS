using System;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region QlLeadSearchResultView

	public interface IQlLeadSearchResultView
	{
		long CustomerMasterFileId { get; set; }
		long LeadId { get; set; }
		int DealerId { get; set; }
		string LocalizationId { get; set; }
		int DispositionId { get; set; }
		string Disposition { get; set; }
		int SourceId { get; set; }
		string Source { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		string PhoneHome { get; set; }
		string PhoneWork { get; set; }
		string PhoneMobile { get; set; }
		DateTime? DOB { get; set; }
		string SalesRepId { get; set; }
		string SSN { get; set; }
		string DL { get; set; }
		string DLStateID { get; set; }
		string Email { get; set; }
		bool? IsCustomer { get; set; }
		int? RowNum { get; set; }
	}

	public class QlLeadSearchResultView : IQlLeadSearchResultView
	{
		#region .ctor

		#endregion .ctor

		#region Implementation of IQlLeadSearchResultView

		public long CustomerMasterFileId { get; set; }
		public long LeadId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		public string Disposition { get; set; }
		public int DispositionId { get; set; }
		public string Source { get; set; }
		public int SourceId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
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

	#endregion QlLeadSearchResultView
}
