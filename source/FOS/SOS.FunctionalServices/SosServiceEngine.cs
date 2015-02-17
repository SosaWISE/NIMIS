/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/16/11
 * Time: 11:45
 * 
 * Description:  Implementes the SOS Service Engine
 *********************************************************************************************************************/

using System;
using SOS.FunctionalServices.Contracts;

namespace SOS.FunctionalServices
{
	public class SosServiceEngine : IServiceEngine
	{

		#region .ctor

		/// <summary>
		/// Prevent public constructor
		/// </summary>
		private SosServiceEngine()
		{
			FunctionalServices = new FunctionalServiceFactory();
			ContextId = Guid.Empty;
		}

		#endregion .ctor

		#region Properties

		/// <summary>
		/// Singleton instance saved here.
		/// </summary>
		private static volatile IServiceEngine _sosCoreEngine;

		private readonly static object SyncInstance = new object();
		/// <summary>
		/// Singleton instance.
		/// </summary>
		public static IServiceEngine Instance
		{
			get
			{
				if (_sosCoreEngine == null)
				{
					lock (SyncInstance)
					{
						if (_sosCoreEngine == null)
						{
							/** Temp var for avoiding read access in double checked locking. */
							var oTemp = new SosServiceEngine { ContextId = Guid.Empty };

							/** Assign and release the lock. */
							_sosCoreEngine = oTemp;
						}
					}
				}
				return _sosCoreEngine;
			}
		}

		#endregion Properties

		#region Implementation of IServiceEngine

		/// <summary>
		/// Initialize the engine.
		/// </summary>
		public void Initialize()
		{
			/** Initialize engine and register FOS. */
			IServiceEngine oEngine = Instance;

			/** Register FOS. */
			oEngine.FunctionalServices.Register(
				(Func<IAuthenticationService>)(() => new AuthenticationService()));
			oEngine.FunctionalServices.Register(
				(Func<IWiseCrmService>)(() => new WiseCrmService()));
			oEngine.FunctionalServices.Register(
				(Func<IMonitoringStationService>)(() => new MonitoringStationService()));
			oEngine.FunctionalServices.Register(
				(Func<IReceiverEngineService>)(() => new ReceiverEngineService()));
			oEngine.FunctionalServices.Register(
				(Func<IMerchantService>)(() => new MerchantService()));
			oEngine.FunctionalServices.Register(
				(Func<IGpsTrackingSerivces>)(() => new GpsTrackingSerivces()));
			oEngine.FunctionalServices.Register(
				(Func<ILaipacDeviceServices>)(() => new LaipacDeviceServices()));
			oEngine.FunctionalServices.Register(
				(Func<IKW621DeviceServices>)(() => new KW621DeviceServices()));
			oEngine.FunctionalServices.Register(
				(Func<ISSEGpsDeviceServices>)(() => new SSEGpsDeviceServices()));
			oEngine.FunctionalServices.Register(
				(Func<ISurveyEngineService>)(() => new SurveyEngineService()));
			oEngine.FunctionalServices.Register(
				(Func<IHumanResourceService>)(() => new HumanResourceService()));
			oEngine.FunctionalServices.Register(
				(Func<IAccountingEngineService>)(() => new AccountingEngineService()));
			oEngine.FunctionalServices.Register(
				(Func<IMainCoreService>)(() => new MainCoreService()));
			oEngine.FunctionalServices.Register(
				(Func<ICellStationService>)(() => new CellStationService()));
			oEngine.FunctionalServices.Register(
				(Func<IBarcodeService>)(() => new BarcodeService()));
			oEngine.FunctionalServices.Register(
				(Func<IDoNotCallService>)(() => new DoNotCallService()));
			oEngine.FunctionalServices.Register(
				(Func<ISwingService>)(() => new SwingService()));
			oEngine.FunctionalServices.Register(
				(Func<IQualifyLeadServices>)(() => new QualifyLeadServices()));
			oEngine.FunctionalServices.Register(
				(Func<IInventoryEngineService>)(() => new InventoryEngineService()));
            oEngine.FunctionalServices.Register(
                (Func<IScheduleEngineService>)(() => new ScheduleEngineService()));
			oEngine.FunctionalServices.Register(
				(Func<IReportingService>)(() => new ReportingService()));

			//oEngine.FunctionalServices.Register(
			//	(Func<IInsideSalesService>)(() => new InsideSalesService()));

			oEngine.FunctionalServices.Register(() => new TicketService());

			AuthServiceConfig.Configure(oEngine.FunctionalServices);
		}

		/// <summary>
		/// Exposes the functional services.
		/// </summary>
		public IFunctionalServiceFactory FunctionalServices { get; private set; }

		/// <summary>
		/// ContextId is the caller context Guid default value for the service.
		/// </summary>
		public Guid ContextId { get; set; }

		#endregion Implementation of IServiceEngine
	}
}