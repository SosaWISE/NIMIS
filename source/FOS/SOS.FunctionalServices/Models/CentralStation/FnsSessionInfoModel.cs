using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsSessionInfoModel : IFnsSessionInfoModel
	{
		#region .ctor

		public FnsSessionInfoModel (int nSessionInfo)
		{
			SessionId = nSessionInfo;
		}

		#endregion .ctor

		#region Member Variables
		public int SessionId { get; private set; }
		#endregion Member Variables

	}
}
