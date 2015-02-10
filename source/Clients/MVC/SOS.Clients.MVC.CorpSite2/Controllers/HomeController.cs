using System.Web.Mvc;

namespace SOS.Clients.MVC.CorpSite2.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Monitoring()
		{
			return View();
		}

		public ActionResult PrivacyStatement()
		{
			return View();
		}

		public ActionResult TermsOfUse()
		{
			return View();
		}
	}
}
