using System;
using System.Configuration;
using System.Web.Routing;
using SOS.Data;
using SOS.Lib.RestCake.Routing;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace SOS.services.Wcf.Signals
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

			/** Route Services. */
			//RouteTable.Routes.Add(new GenericHandlerRoute<ExecuteSvc>("Execute.service"));

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