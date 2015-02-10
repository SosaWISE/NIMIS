using System;
using System.Configuration;

namespace SOS.Clients.MVC.DealerSite
{
	public enum RenderMode
	{
		Unknown,
		Debug,
		Release,
		ReleaseCompressed
	}

	public class Global : System.Web.HttpApplication
	{
		private static RenderMode? _mode;
		public static RenderMode RenderMode
		{
			get
			{
				if (!_mode.HasValue)
				{
					RenderMode mode;
					_mode = Enum.TryParse(ConfigurationManager.AppSettings["RenderMode"], out mode) 
						? mode 
						: RenderMode.Debug;
				}

				return _mode.Value;
			}
		}


		void Application_Start(object sender, EventArgs e)
		{
			// Code that runs on application startup

		}

		void Application_End(object sender, EventArgs e)
		{
			//  Code that runs on application shutdown

		}

		void Application_Error(object sender, EventArgs e)
		{
			// Code that runs when an unhandled error occurs

		}

		void Session_Start(object sender, EventArgs e)
		{
			// Code that runs when a new session is started

		}

		void Session_End(object sender, EventArgs e)
		{
			// Code that runs when a session ends. 
			// Note: The Session_End event is raised only when the sessionstate mode
			// is set to InProc in the Web.config file. If session mode is set to StateServer 
			// or SQLServer, the event is not raised.

		}

	}
}
