using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsIndustryAccount : IFnsMsIndustryAccount
	{
		#region .ctor

		public FnsMsIndustryAccount(MS_IndustryAccountNumbersView view)
		{
			IndustryAccountID = view.IndustryAccountID;
			AccountId = view.AccountId;
			ReceiverLineId = view.ReceiverLineId;
			ReceiverLineBlockId = view.ReceiverLineBlockId;
			IndustryAccount = view.IndustryAccount;
			Designator = view.Designator;
			SubscriberNumber = view.SubscriberNumber;
			ReceiverNumber = view.ReceiverNumber;
			IsActive = view.IsActive;
		}

		#endregion .ctor

		#region Properties
	
		public long IndustryAccountID { get; private set; }
		public long AccountId { get; private set; }
		public string ReceiverLineId { get; private set; }
		public string ReceiverLineBlockId { get; private set; }
		public string IndustryAccount { get; private set; }
		public string Designator { get; private set; }
		public string SubscriberNumber { get; private set; }
		public string ReceiverNumber { get; private set; }
		public bool IsActive { get;private set; }

		#endregion Properties
	}
}
