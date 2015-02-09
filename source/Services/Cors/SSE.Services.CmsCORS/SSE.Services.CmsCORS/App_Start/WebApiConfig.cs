using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace SSE.Services.CmsCORS
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			//@HACK: for json on non matching routes
			config.Routes.MapHttpRoute(
				name: "HackForJson",
				routeTemplate: "{p1}/{p2}/{p3}/{p4}/{p5}/{p6}/{p7}/{p8}/{p9}",
				defaults: new
				{
					p1 = RouteParameter.Optional,
					p2 = RouteParameter.Optional,
					p3 = RouteParameter.Optional,
					p4 = RouteParameter.Optional,
					p5 = RouteParameter.Optional,
					p6 = RouteParameter.Optional,
					p7 = RouteParameter.Optional,
					p8 = RouteParameter.Optional,
					p9 = RouteParameter.Optional,
				}
			);
		}
	}
}
