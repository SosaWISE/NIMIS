using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Reporting;

namespace SOS.FunctionalServices.Models.Reporting
{
	public class FnsMsAccountCreditsAndInstalls : IFnsMsAccountCreditsAndInstalls
	{
		#region .ctor

		public FnsMsAccountCreditsAndInstalls(MS_AccountCreditsAndInstallsView item)
		{
			LeadID = item.LeadID;
			OfficeId = item.OfficeID;
			OfficeName = item.OfficeName;
			TeamID = item.TeamID;
			TeamName = item.TeamName;
			SalesRepID = item.SalesRepID;
			SalesRepName = item.SalesRepName;
			Active = item.Active;
			NumInstalls = item.NumInstalls;
			NumCredits = item.NumCredits;
			InstallDate = item.InstallDate;
			CreditDate = item.CreditDate;
		}

		#endregion .ctor

		#region Properties

		public long? LeadID { get; private set; }
		public long? OfficeId { get; private set; }
		public string OfficeName { get; private set; }
		public int? TeamID { get; private set; }
		public string TeamName { get; private set; }
		public string SalesRepID { get; private set; }
		public string SalesRepName { get; private set; }
		public string Active { get; private set; }
		public int? NumInstalls { get; private set; }
		public int? NumCredits { get; private set; }
		public DateTime? InstallDate { get; private set; }
		public DateTime? CreditDate { get; private set; }

		#endregion Properties
	}
}
