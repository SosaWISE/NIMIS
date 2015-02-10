using System.Web.Mvc;
using System.Web.Routing;

// ReSharper disable CheckNamespace
namespace SSE.Clients.MVC.CMS
// ReSharper restore CheckNamespace
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			/** This is commented out because we are not using MVC views.  Since this is a SPA.
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
			 */
		}
	}
}