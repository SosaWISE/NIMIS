using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;

namespace SOS.FOS.MonitoringStationServicesUT
{
	[TestClass]
	public class SignalHistory
	{

		private static IMonitoringStationService _service;

		[ClassInitialize]
		public static void MyClassInitialize(TestContext testContext)
		{

			Helpers.InitializeAndConfigure.Instance.Initialize();
			_service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

		}

		[TestMethod]
		public void GetSignalHistory()
		{
			var startDate = new DateTime(2014, 1, 1);
			DateTime endDate = DateTime.Now;
			var result = _service.GetSignalHistory(130532, startDate, endDate, "SOSA001");

			Assert.IsTrue(result.Code == 0, result.Message);
		}

		[TestMethod]
		public void GetSignalHistoryMoni()
		{
			// ** Initialize.
			var startDate = new DateTime(2014, 1, 1);
			DateTime endDate = DateTime.Now;
			var result = _service.GetSignalHistory(181257, startDate, endDate, "SOSA001");

			Assert.IsTrue(result.Code == 0, result.Message);
		}
	}
}