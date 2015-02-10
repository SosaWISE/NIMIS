using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using SOS.Data;
using SOS.Data.Logging;
using SOS.Lib.Core;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace SOS.Clients.MVC.CorpSite2
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_BeginRequest(object sender, EventArgs oArgs)
		{
		}

		protected void Application_Start()
		{
			/**  Load configuration. */
			string environment = ConfigurationManager.AppSettings["Environment"] ?? Environment.MachineName;
			ConfigurationSettings.Current.SetProperties("Preferences", environment);

			// Setup SubSonic Connections
			SubSonicConfigHelper.SetupConnectionStrings();

			/** Initialize Fos Engine. */
			FunctionalServices.SosServiceEngine.Instance.Initialize();

			/** Initialize Error Manager. */
			_errorManager = new DBErrorManager(LogSource.SosClientsMVCCorpSite2, null);
			_errorManager.MessageAdded += OnErrorManagerMessageAdded;
			DBErrorManager.SetSingletonInstance(_errorManager);

			/** Mvc stuff. */
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		private DBErrorManager _errorManager;

		void OnErrorManagerMessageAdded()
		{
			if (_errorManager.MessageCount > 0)
			{
				_errorManager.ClearMessages();
			}
		}
	}
}