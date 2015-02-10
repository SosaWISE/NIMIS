using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SOS.Data;
using SOS.Data.Logging;
using SOS.Lib.Core;
using SSE.Services.CORS.App_Start;
using SSE.Services.CORS.Helpers;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace SSE.Services.CORS
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			/**  Load configuration. */
			string environment = ConfigurationManager.AppSettings["Environment"] ?? Environment.MachineName;
			ConfigurationSettings.Current.SetProperties("Preferences", environment);

			// Setup SubSonic Connections
			SubSonicConfigHelper.SetupConnectionStrings();

			/** Initialize Fos Engine. */
			SOS.FunctionalServices.SosServiceEngine.Instance.Initialize();

			/** Initialize Error Manager. */
			_errorManager = new DBErrorManager(LogSource.SosClientsMVCCorpSite2, null);
			_errorManager.MessageAdded += OnErrorManagerMessageAdded;
			DBErrorManager.SetSingletonInstance(_errorManager);

			/** Mvc stuff. */
			AreaRegistration.RegisterAllAreas();

			GlobalConfiguration.Configuration.MessageHandlers.Add(new ContentTypeHandler());

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			CorsConfig.RegisterCors(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			CacheConfig.RegisterCache();
		}

		#region Error Handler Manager

		private DBErrorManager _errorManager;

		void OnErrorManagerMessageAdded()
		{
			if (_errorManager.MessageCount > 0)
			{
				_errorManager.ClearMessages();
			}
		}
		#endregion Error Handler Manager
	}
}