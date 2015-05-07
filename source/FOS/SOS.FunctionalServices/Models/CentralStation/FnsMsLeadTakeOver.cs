using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsLeadTakeOver : IFnsMsLeadTakeOver
	{
		#region .ctor

		public FnsMsLeadTakeOver(MS_LeadTakeOversView view)
		{
			AccountId = view.AccountId;
			LeadID = view.LeadID;
			FullName = view.FullName;
			StreetAddress = view.StreetAddress;
			CityStZip = view.CityStZip;
			AlarmCompanyId = view.AlarmCompanyId;
			AlarmCompanyName = view.AlarmCompanyName;
		}
		#endregion .ctor

		#region Properties
		public long AccountId { get; private set; }
		public long LeadID { get; private set; }
		public string FullName { get; private set; }
		public string StreetAddress { get; private set; }
		public string CityStZip { get; private set; }
		public int AlarmCompanyId { get; private set; }
		public string AlarmCompanyName { get; private set; }

		#endregion Properties
	}
}
