using System.Web.Mvc;

namespace SOS.Clients.MVC.CorpSite2.Controllers
{
	public class ErrorController : Controller
	{
		//
		// GET: /Error/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult PageNotFound()
		{
			return View();
		}
	}
}
