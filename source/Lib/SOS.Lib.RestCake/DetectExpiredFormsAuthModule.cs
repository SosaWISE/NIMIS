using System;
using System.Web;

namespace SOS.Lib.RestCake
{
	/// <summary>
	/// You will need to configure this module in the web.config file of your
	/// web and register it with IIS before being able to use it. For more information
	/// see the following link: http://go.microsoft.com/?linkid=8101007
	/// </summary>
	public class DetectExpiredFormsAuthModule : IHttpModule
	{
		public void Dispose()
		{ }


		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += context_AuthenticateRequest;
		}


		private static void context_AuthenticateRequest(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;
			RestHttpHandler.FormsAuthOrRedirectMessage(app.Context);
		}
	}
}
