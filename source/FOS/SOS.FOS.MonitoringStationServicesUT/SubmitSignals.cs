using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;

namespace SOS.FOS.MonitoringStationServicesUT
{
	/// <summary>
	/// Summary description for SubmitSignals
	/// </summary>
	[TestClass]
	public class SubmitSignals
	{
		public SubmitSignals()
		{
			/** Initialize. */
			Helpers.InitializeAndConfigure.Instance.Initialize();
		}

		private TestContext _testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return _testContextInstance;
			}
			set
			{
				_testContextInstance = value;
			}
		}

		private static IMonitoringStationService _service;

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		[ClassInitialize()]
		public static void MyClassInitialize(TestContext testContext)
		{

			Helpers.InitializeAndConfigure.Instance.Initialize();
			_service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

		}
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void SendSignalRaw()
		{
			/** Initialise. */
			var oReceiver = _service.SendEmergencySignal(
				"XX0502"
				, "What up dog?"
				, 0
				, 0
				, false
				);

			Assert.IsTrue(oReceiver.Code == 0, "Signal failed");
		}
	}
}
