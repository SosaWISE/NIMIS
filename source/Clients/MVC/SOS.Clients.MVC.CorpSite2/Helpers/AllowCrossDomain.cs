using System.Web.Mvc;

namespace SOS.Clients.MVC.CorpSite2.Helpers
{
	public class AllowCrossDomain : ActionFilterAttribute
	{
		public const string ALL_DOMAINS = "*";

		private readonly string[] _allowMethods;
		private string _allowOrigin;

		public AllowCrossDomain()
			: this(null, null)
		{

		}

		public AllowCrossDomain(string allowOrigin, params string[] allowMethods)
		{
			_allowMethods = allowMethods;
			_allowOrigin = allowOrigin;

			if (string.IsNullOrWhiteSpace(_allowOrigin))
			{
				_allowOrigin = ALL_DOMAINS;
			}
			if (_allowMethods == null || _allowMethods.Length == 0)
			{
				_allowMethods = new[] { "GET", "POST", "OPTIONS" };
			}
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", _allowOrigin);
			filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
			filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", string.Join(", ", _allowMethods));
			filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
			filterContext.HttpContext.Response.Headers.Add("Access-Control-Max-Age", "86400");
		}
	}
}