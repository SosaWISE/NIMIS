using SOS.FunctionalServices.Contracts.Models.Reporting;

namespace SOS.FunctionalServices.Models.Reporting
{
	public class FnsMsSystemStatusInfo : IFnsMsSystemStatusInfo
	{
		public FnsMsSystemStatusInfo(bool inService, bool onTest)
		{
			InService = inService;
			OnTest = onTest;
		}
		public bool InService { get; private set; }
		public bool OnTest { get; private set; }
	}
}
