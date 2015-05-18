using Api.Core;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.Serialization.JsonNet;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using NXS.Data;
using NXS.Lib.Web;
using System;
using WebModules;
using AuthDBase = NXS.Data.AuthenticationControl.DBase;

namespace Api.Sales
{
	public class SalesBootstrapper : DefaultNancyBootstrapper
	{
		// The bootstrapper enables you to reconfigure the composition of the framework,
		// by overriding the various methods and properties.
		// For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

		NancyInternalConfiguration _config;
		public SalesBootstrapper()
		{
			_config = NancyInternalConfiguration.WithOverrides(c =>
			{
				//c.Serializers.Clear();
				//c.Serializers.Add(typeof(Nancy.Serialization.JsonNet.JsonNetSerializer));

				c.ResponseProcessors.Clear();
				c.ResponseProcessors.Add(typeof(Nancy.Responses.Negotiation.JsonProcessor));
				//c.ResponseProcessors.Add(typeof(FormattedJsonProcessor)); // default processor always gets priority...
				// if we include this, xml is returned by default in the browser...not what i want
				//c.ResponseProcessors.Add(typeof(Nancy.Responses.Negotiation.XmlProcessor));

				c.StatusCodeHandlers.Clear();
				c.StatusCodeHandlers.Add(typeof(WebModules.StatusCodeHandler));
			});
		}
		protected override NancyInternalConfiguration InternalConfiguration { get { return _config; } }

		protected override DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get { return new DiagnosticsConfiguration { Password = @"bob" }; }
		}

		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			WebConfig.Init(AppDomain.CurrentDomain.BaseDirectory, val =>
			{
				var decryptedVal = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(val, null);
				// if decryption failed, return passed in value
				return decryptedVal.StartsWith("Error: ") ? val : decryptedVal;
			});

			// set connection strings
			var host = WebConfig.Instance.GetConfig("DBHost");
			var username = WebConfig.Instance.GetConfig("DBUsername");
			var password = WebConfig.Instance.GetConfig("DBPassword");
			var appName = WebConfig.Instance.GetConfig("ApplicationName");
			AuthDBase.ConnectionString = DatabaseHelper.FormatConnectionString(AuthDBase.Database, host, username, password, appName);

			//
			AuthService authService;
			container.Register(AuthServiceConfig.Configure(null, out authService));
			BaseModule.AuthService = authService;
		}

		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			StaticConfiguration.DisableErrorTraces = true;

			//
			container.Register<ISerializer>(new JsonNetSerializer(container.Resolve<JsonSerializer>()));

			//container.Register<System.Data.IDbConnection, System.Data.SqlClient.SqlConnection>().AsMultiInstance();
		}
		//protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
		//{
		//	base.ConfigureRequestContainer(container, context);
		//}

		protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
		{
			//// CORS
			//AllowAccessToConsumingSite(pipelines);

			//
			JsonErrors.Enable(pipelines, container.Resolve<ISerializer>());

			// Auth
			TokenAuthentication.Enable(pipelines, container.Resolve<TokenAuthenticationConfiguration>());
		}

		// NOT TESTED
		//static void AllowAccessToConsumingSite(IPipelines pipelines)
		//{
		//	pipelines.AfterRequest.AddItemToEndOfPipeline(x =>
		//	{
		//		x.Response.Headers.Add("Access-Control-Allow-Origin", "*");
		//		x.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS");
		//	});
		//}
	}
}