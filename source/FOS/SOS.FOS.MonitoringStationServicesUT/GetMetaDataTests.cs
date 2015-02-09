using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;

namespace SOS.FOS.MonitoringStationServicesUT
{
	[TestClass]
	public class GetMetaDataTests
	{
		private static IMonitoringStationService _service;

		[ClassInitialize]
		public static void MyClassInitialize(TestContext testContext)
		{

			Helpers.InitializeAndConfigure.Instance.Initialize();
			_service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

		}
		[TestMethod]
		public void MoniGetMetaDataTest()
		{
			// ** Initialize
			var moniService = new MonitoringStationServices.Monitronics.CentralStation();

			moniService.GenerateMetaData(null, "SYSTEM-UT");
		}
	}
}
