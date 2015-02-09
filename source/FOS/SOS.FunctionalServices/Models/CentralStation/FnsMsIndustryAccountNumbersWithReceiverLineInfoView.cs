using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsIndustryAccountNumbersWithReceiverLineInfoView : IFnsMsIndustryAccountNumbersWithReceiverLineInfoView
	{
		#region .ctor

		public FnsMsIndustryAccountNumbersWithReceiverLineInfoView(MS_IndustryAccountNumbersWithReceiverLineInfoView view)
		{
			IndustryAccountID = view.IndustryAccountID;
			AccountId = view.AccountId;
			ReceiverNumber = view.ReceiverNumber;
			Designator = view.Designator;
			SubscriberNumber = view.SubscriberNumber;
			IndustryAccount = view.IndustryAccount;
			MonitoringStationOSID = view.MonitoringStationOSID;
			OSDescription = view.OSDescription;
			MonitoringStationName = view.MonitoringStationName;
			PrimaryCSID = view.PrimaryCSID;
			SecondaryCSID = view.SecondaryCSID;
		}

		#endregion .ctor

		#region Properties
		public long IndustryAccountID { get; private set; }
		public long AccountId { get; private set; }
		public string ReceiverNumber { get; private set; }
		public string Designator { get; private set; }
		public string SubscriberNumber { get; private set; }
		public string IndustryAccount { get; private set; }
		public string MonitoringStationOSID { get; private set; }
		public string OSDescription { get; private set; }
		public string MonitoringStationName { get; private set; }
		public string PrimaryCSID { get; private set; }
		public string SecondaryCSID { get; private set; }
		#endregion Properties
	}
}
