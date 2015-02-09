using System.Web.Http;

namespace Nxs.Services.CorsConnext
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
            config.MapHttpAttributeRoutes(); // added 12/5/14 by Shumway //
            //config.Routes.MapHttpRoute(
			//	name: "DefaultApi",
			//	routeTemplate: "api/{controller}/{id}",
			//	defaults: new { id = RouteParameter.Optional }
			//);
			config.Routes.MapHttpRoute(
				name: "HumanResourceSrvcs",
				routeTemplate: "HumanResourceSrvcs/{controller}/{id}/{action}",
				defaults: new
				{
//					id = RouteParameter.Optional,
					action = RouteParameter.Optional
				}
			);
			config.Routes.MapHttpRoute(
				name: "HumanResourceSrvcsGet",
				routeTemplate: "HumanResourceSrvcs/{controller}/{id}",
				defaults: new
				{
					id = RouteParameter.Optional,
				}
			);

			config.Routes.MapHttpRoute(
				name: "AppSrv",
				routeTemplate: "{controller}/{action}/{param}",
				defaults: new
				{
					param = RouteParameter.Optional,
					controller = "AppSrv",
					action = "GetDeviceList"
				}
			);

			// Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
			// To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
			// For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
			//config.EnableQuerySupport();

			// To disable tracing in your application, please comment out or remove the following line of code
			// For more information, refer to: http://www.asp.net/web-api
			config.EnableSystemDiagnosticsTracing();
		}
	}
}
