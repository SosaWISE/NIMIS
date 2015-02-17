using SOS.FOS.MonitoringStationServices.Contracts.Models;

namespace SOS.FOS.MonitoringStationServices.Monitronics.Models
{
	public class SystemStatusInfo : ISystemStatusInfo
	{
		#region .ctor

		public SystemStatusInfo(bool inService, bool onTest)
		{
			InService = inService;
			OnTest = onTest;
		}

		#endregion .ctor

		#region Properties
		public bool InService { get; private set; }
		public bool OnTest { get; private set; }
		#endregion Properties
	}
}
