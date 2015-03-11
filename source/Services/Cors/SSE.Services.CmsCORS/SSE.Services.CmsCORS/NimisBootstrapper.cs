﻿using Nancy;
using Nancy.Authentication.Token;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.Security;
using Nancy.Serialization.JsonNet;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using SOS.FunctionalServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebModules;

namespace SSE.Services.CmsCORS
{
	public class NimisBootstrapper : DefaultNancyBootstrapper
	{
		// The bootstrapper enables you to reconfigure the composition of the framework,
		// by overriding the various methods and properties.
		// For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

		NancyInternalConfiguration _config;
		public NimisBootstrapper()
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

			//@HACK: to set connection strings
			NXS.Data.Crm.CrmDb.ConnectionString = SubSonic.DataService.Providers[SOS.Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME].DefaultConnectionString;
		}
		protected override NancyInternalConfiguration InternalConfiguration { get { return _config; } }

		protected override DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get { return new DiagnosticsConfiguration { Password = @"bob" }; }
		}

		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			StaticConfiguration.DisableErrorTraces = true;

			var configuration = SosServiceEngine.Instance.FunctionalServices.Instance<TokenAuthenticationConfiguration>();
			container.Register(configuration);

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

	//public class JsonNetSerializer : ISerializer
	//{
	//	readonly JsonSerializer _serializer;
	//
	//	public JsonNetSerializer()
	//	{
	//		_serializer = JsonSerializer.Create(new JsonSerializerSettings());
	//	}
	//
	//	public IEnumerable<string> Extensions { get { yield return "json"; } }
	//
	//	public bool CanSerialize(string contentType)
	//	{
	//		return IsJsonType(contentType);
	//	}
	//
	//	public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
	//	{
	//		using (var writer = new JsonTextWriter(new StreamWriter(outputStream)))
	//		{
	//			_serializer.Serialize(writer, model);
	//			writer.Flush();
	//		}
	//	}
	//
	//	private static bool IsJsonType(string contentType)
	//	{
	//		if (string.IsNullOrEmpty(contentType))
	//		{
	//			return false;
	//		}
	//
	//		var contentMimeType = contentType.Split(';')[0];
	//
	//		return contentMimeType.Equals("application/json", StringComparison.InvariantCultureIgnoreCase) ||
	//		contentMimeType.StartsWith("application/json-", StringComparison.InvariantCultureIgnoreCase) ||
	//		contentMimeType.Equals("text/json", StringComparison.InvariantCultureIgnoreCase) ||
	//		(contentMimeType.StartsWith("application/vnd", StringComparison.InvariantCultureIgnoreCase) &&
	//		contentMimeType.EndsWith("+json", StringComparison.InvariantCultureIgnoreCase));
	//	}
	//}
}