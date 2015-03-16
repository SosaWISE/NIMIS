using System;

namespace SOS.FunctionalServices.Contracts.Models.Reporting
{
	public interface IFnsMsAccountCreditsAndInstalls
	{
		long? LeadID { get; }
		long? OfficeId { get; }
		string OfficeName { get; }
		int? TeamID { get; }
		string TeamName { get; }
		string SalesRepID { get; }
		string SalesRepName { get; }
		string Active { get; }
		int? NumInstalls { get; }
		int? NumCredits { get; }
		DateTime? InstallDate { get; }
		DateTime? CreditDate { get; }

	}
}
