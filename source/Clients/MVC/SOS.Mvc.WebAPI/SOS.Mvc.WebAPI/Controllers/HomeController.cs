using System.Web.Mvc;

namespace SOS.Mvc.WebAPI.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
