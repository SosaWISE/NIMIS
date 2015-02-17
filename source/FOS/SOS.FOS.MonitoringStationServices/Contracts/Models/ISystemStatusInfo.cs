using System.ServiceModel.Security.Tokens;

namespace SOS.FOS.MonitoringStationServices.Contracts.Models
{
	public interface ISystemStatusInfo
	{
		bool InService { get; }
		bool OnTest { get; }
	}
}