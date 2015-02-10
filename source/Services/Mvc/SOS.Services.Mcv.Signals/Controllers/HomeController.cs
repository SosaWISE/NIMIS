using System.Collections.Generic;
using System.Web.Mvc;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.Services.Mcv.Signals.Models;

namespace SOS.Services.Mcv.Signals.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		public JsonResult Index()
		{
			/** Initialize. */
			var oRequest = HttpContext.Request;

			/** Check that there is a request. */
			System.Diagnostics.Debug.WriteLine("QueryString: {0}", oRequest.QueryString);

			/** Save request. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IReceiverEngineService>();

			/** Save raw request. */
			var oResult = oService.SaveTxtWireSignal(szTitle: "Raw Request"
											, szCode: "000"
											, szShortCode: "0"
											, szMessage: oRequest.QueryString.ToString()
											, szPhone: "000 000-0000"
											, szCarrier: null
											, szGroupName: null
											, szCustomTicket: null
											, szDefaultKeyword: null
											, szUsername: null
											, szPassword: null
											, szApiKey: null
											, szKeyword: null);

			/** Create json response. */
			var szResponseJson = new List<ResponseDefaultModel>
								{
									new ResponseDefaultModel {QueryString = oRequest.QueryString.ToString()}
								};

			/** Return result. */
			return Json(szResponseJson, JsonRequestBehavior.AllowGet);
		}

	}
}
