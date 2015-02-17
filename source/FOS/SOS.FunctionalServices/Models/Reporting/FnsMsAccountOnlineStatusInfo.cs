using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Reporting;

namespace SOS.FunctionalServices.Models.Reporting
{
	public class FnsMsAccountOnlineStatusInfo : IFnsMsAccountOnlineStatusInfo
	{
		#region .ctor

		public FnsMsAccountOnlineStatusInfo(MS_AccountOnlineStatusInfoView item)
		{
			KeyName = item.KeyName;
			Text = item.TextX;
			Value = item.ValueX;
			Status = item.Status;
		}

		public FnsMsAccountOnlineStatusInfo() { }

		#endregion .ctor

		#region Properties
		public string KeyName { get; set; }
		public string Text { get; set; }
		public string Value { get; set; }
		public string Status { get; set; }
		#endregion Properties
	}
}
