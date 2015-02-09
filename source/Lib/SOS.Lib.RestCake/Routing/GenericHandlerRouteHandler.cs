using System.Web;
using System.Web.Routing;

namespace SOS.Lib.RestCake.Routing
{
	public class GenericHandlerRouteHandler<T> : IRouteHandler where T : RoutedHttpHandler, new()
	{
		private readonly string _routeUrl;
		private readonly RouteBase m_route;
		private readonly string m_baseUrl;

		public GenericHandlerRouteHandler(RouteBase route, string baseUrl, string routeUrl)
		{
			_routeUrl = routeUrl;
			m_route = route;
			m_baseUrl = baseUrl;
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new T() { Route = m_route, BaseUrl = m_baseUrl, RouteUrl = _routeUrl };
		}
	}
}
