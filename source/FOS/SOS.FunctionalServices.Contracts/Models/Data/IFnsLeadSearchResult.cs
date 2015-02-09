using System;

namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsLeadSearchResult
	{
		long CustomerMasterFileId { get; set; }
		long LeadId { get; set; }
		int DealerId { get; set; }
		string LocalizationId { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		int DispositionId { get; set; }
		string Disposition { get; set; }
		DateTime? DispositionDateChange { get; set; }
		int SourceId { get; set; }
		string Source { get; set; }
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
}