using System;
using System.Configuration;
using System.Web.Routing;
using SOS.Data;
using SOS.Data.Logging;
using SOS.Lib.Core;
using SOS.Lib.RestCake.Routing;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace SOS.Services.Wcf.Crm
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			/**  Load configuration. */
			string environment = ConfigurationManager.AppSettings["Environment"] ?? Environment.MachineName;
			ConfigurationSettings.Current.SetProperties("Preferences", environment);

			// Setup SubSonic Connections
			SubSonicConfigHelper.SetupConnectionStrings();

			/** Initialize Fos Engine. */
			FunctionalServices.SosServiceEngine.Instance.Initialize();

			/** Initialize Error Manager. */
			_errorManager = new DBErrorManager(LogSource.SosServicesWcfCrm, null);
			_errorManager.MessageAdded += OnErrorManagerMessageAdded;
			DBErrorManager.SetSingletonInstance(_errorManager);

			/** Route Services. */
			RouteTable.Routes.Add(new GenericHandlerRoute<AuthSvc>("Auth.service"));
			RouteTable.Routes.Add(new GenericHandlerRoute<CmsSvc>("Cms.service"));
		}

		private DBErrorManager _errorManager;

		void OnErrorManagerMessageAdded()
		{
			if (_errorManager.MessageCount > 0)
			{
				_errorManager.ClearMessages();
			}
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}