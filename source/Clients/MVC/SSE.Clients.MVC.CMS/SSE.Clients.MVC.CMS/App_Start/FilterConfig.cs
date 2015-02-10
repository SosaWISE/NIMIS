using System.Web.Mvc;

// ReSharper disable CheckNamespace
namespace SSE.Clients.MVC.CMS
// ReSharper restore CheckNamespace
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}