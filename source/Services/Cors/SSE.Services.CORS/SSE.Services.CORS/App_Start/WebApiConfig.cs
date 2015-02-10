using System.Web.Http;

// ReSharper disable CheckNamespace
namespace SSE.Services.CORS
// ReSharper restore CheckNamespace
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			//config.Routes.MapHttpRoute(
			//    name: "AppSrv",
			//    routeTemplate: "AppSrv/{action}/{oCMFID}",
			//    defaults: new
			//                {
			//                    oCMFID = RouteParameter.Optional,
			//                    controller = "AppSrv",
			//                    action = "GetDeviceList"
			//                }
			//    );

			//config.Routes.MapHttpRoute(
			//    name: "AuthSrv",
			//    routeTemplate: "AuthSrv/{action}/{appInfo}",
			//    defaults: new
			//                {
			//                    appInfo = RouteParameter.Optional,
			//                    controller = "AuthSrv",
			//                    action = "SosStart"
			//                }
			//);
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

		}
	}
}
