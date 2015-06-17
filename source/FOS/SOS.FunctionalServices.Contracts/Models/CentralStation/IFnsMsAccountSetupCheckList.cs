using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountSetupCheckList
	{
		#region Properties
		[DataMember]
		long AccountID { get; }

		[DataMember]
		DateTime? Qualify { get; set; }

		[DataMember]
		DateTime? SalesInfo { get; set; }

		[DataMember]
		DateTime? PreSurvey { get; set; }

		[DataMember]
		DateTime? IndustryNumbers { get; set; }

		[DataMember]
		DateTime? EmergencyContacts { get; set; }

		[DataMember]
		DateTime? SystemDetails { get; set; }

		[DataMember]
		DateTime? RegisterCell { get; set; }

		[DataMember]
		DateTime? SystemTest { get; set; }

		[DataMember]
		DateTime? TechInspection { get; set; }

		[DataMember]
		DateTime? PostSurvey { get; set; }

		[DataMember]
		DateTime? InitialPayment { get; set; }

		[DataMember]
		DateTime? SubmitAccountOnline { get; set; }
		#endregion Properties
	}
}