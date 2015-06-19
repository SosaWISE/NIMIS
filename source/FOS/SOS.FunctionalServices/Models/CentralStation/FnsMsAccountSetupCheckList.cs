using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountSetupCheckList : IFnsMsAccountSetupCheckList
	{
		#region .ctor

		public FnsMsAccountSetupCheckList(MS_AccountSetupCheckList item)
		{
			AccountID = item.AccountID;
			Qualify = item.Qualify;
			SalesInfo = item.SalesInfo;
			PreSurvey = item.PreSurvey;
			IndustryNumbers = item.IndustryNumbers;
			EmergencyContacts = item.EmergencyContacts;
			SystemDetails = item.SystemDetails;
			RegisterCell = item.RegisterCell;
			SystemTest = item.SystemTest;
			TechInspection = item.TechInspection;
			PostSurvey = item.PostSurvey;
			InitialPayment = item.InitialPayment;
			SubmitAccountOnline = item.SubmitAccountOnline;
		}

		#endregion .ctor

		#region Properties
		public long AccountID { get; private set; }
		public DateTime? Qualify { get; set; }
		public DateTime? SalesInfo { get; set; }
		public DateTime? PreSurvey { get; set; }
		public DateTime? IndustryNumbers { get; set; }
		public DateTime? EmergencyContacts { get; set; }
		public DateTime? SystemDetails { get; set; }
		public DateTime? RegisterCell { get; set; }
		public DateTime? SystemTest { get; set; }
		public DateTime? TechInspection { get; set; }
		public DateTime? PostSurvey { get; set; }
		public DateTime? InitialPayment { get; set; }
		public DateTime? SubmitAccountOnline { get; set; }
		#endregion Properties
	}
}
