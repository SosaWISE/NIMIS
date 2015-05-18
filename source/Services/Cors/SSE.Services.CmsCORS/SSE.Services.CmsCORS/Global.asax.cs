using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SOS.Data;
using SOS.Data.Logging;
using SOS.Lib.Core;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;
using NXS.Lib.Web;
using Api.Core;

namespace SSE.Services.CmsCORS
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
			WebConfig.Init(Server.MapPath("~"), val =>
			{
				var decryptedVal = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(val, null);
				// if decryption failed, return passed in value
				return decryptedVal.StartsWith("Error: ") ? val : decryptedVal;
			});

			// Setup SubSonic Connections
			SubSonicConfigHelper.SetupConnectionStrings();
			//@HACK: to set connection strings
			NXS.Data.Crm.DBase.ConnectionString = SubSonic.DataService.Providers[SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME].DefaultConnectionString;
			NXS.Data.AuthenticationControl.DBase.ConnectionString = SubSonic.DataService.Providers[SubSonicConfigHelper.SOS_AUTH_CONTROL_PROVIDER_NAME].DefaultConnectionString;
			NXS.Data.HumanResource.DBase.ConnectionString = SubSonic.DataService.Providers[SubSonicConfigHelper.SOS_HUMAN_RESOURCE_PROVIDER_NAME].DefaultConnectionString;

			/** Initialize Fos Engine. */
			SOS.FunctionalServices.SosServiceEngine.Instance.Initialize();
			//
			AuthService authService;
			var configuration = AuthServiceConfig.Configure(ctx =>
			{
				//@HACK: for integration with non Nancy requests
				if (ctx == null)
					return System.Web.HttpContext.Current.Request.Headers["User-Agent"];
				return ctx.Request.Headers.UserAgent;
			}, out authService);
			SOS.FunctionalServices.SosServiceEngine.Instance.FunctionalServices.Register(() => configuration);
			SOS.FunctionalServices.SosServiceEngine.Instance.FunctionalServices.Register(() => authService);

			/** Initialize Error Manager. */
			_errorManager = new DBErrorManager(LogSource.NXSServicesCmsCORS, null);
			_errorManager.MessageAdded += OnErrorManagerMessageAdded;
			DBErrorManager.SetSingletonInstance(_errorManager);

			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			GlobalConfiguration.Configuration.EnsureInitialized();
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