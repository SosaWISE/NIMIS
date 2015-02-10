using System.Collections.Generic;
using System.Web.Mvc;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.Services.Interfaces.Helpers;
using SOS.Services.Mcv.Signals.Models;

namespace SOS.Services.Mcv.Signals.Controllers
{
	/// <summary>
	/// This is where we get the messages from the device via SMS
	/// i.e. http://maps.google.com/maps?q=40.319262,+-111.675942(Fence%20Breached)&hl=en&z=19
	///		this is lat,lon where North and East are positive, and South and West are negative.
	/// </summary>
	public class ExecuteController : Controller
	{
		//
		// GET: /Execute/
		public virtual ActionResult Index()
		{
			var oModel = new TxtWirePostInfo();

			return View(oModel);
		}

// ReSharper disable InconsistentNaming
		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult Index(string title, string code, string shortcode, string message, string phone
			, string carrier, string keyword, string group, string custom_ticket, string default_keyword, string user_name
			, string password, string api_key)
// ReSharper restore InconsistentNaming
		{
			/** Initialize. */
			var oRequest = System.Web.HttpContext.Current.Request;
			message = Server.UrlDecode(message);
			group = Server.UrlDecode(group);

			/** Check that there is a request. */
			System.Diagnostics.Debug.WriteLine("QueryString: {0}", oRequest.QueryString);

			/** Save request. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IReceiverEngineService>();

			var oResult = oService.SaveTxtWireSignal(szTitle:title
				, szCode:code, szShortCode: shortcode, szMessage: message, szPhone:phone, szCarrier:carrier, szGroupName: group
				, szCustomTicket: custom_ticket, szDefaultKeyword: default_keyword, szUsername: user_name, szPassword: password
				, szApiKey: api_key, szKeyword:keyword);
			var szResponse = string.Format("QS: {0} | Result: {1}"
				, oRequest.QueryString
				, oResult.Message);

			System.Diagnostics.Debug.WriteLine("Saved Signal: {0}", szResponse);

			/** Execute the Signal if needed. */
			if (oResult.Code != (decimal) SosErrors.Success)
				return Json(new List<ResponseDefaultModel>
								{
									new ResponseDefaultModel
										{
											QueryString = oRequest.QueryString.ToString(),
											Code = oResult.Code,
											Message = oResult.Message
										}
								}, JsonRequestBehavior.AllowGet);

			/** Initialize this. */
			var oResultJson = new List<TxtWirePostInfo>
				{
					new TxtWirePostInfo
						{
							title = title,
							code = code,
							shortcode = shortcode,
							message = message,
							phone = phone,
							carrier = carrier,
							keyword = keyword,
							group = group,
							custom_ticket = custom_ticket,
							default_keyword = default_keyword,
							user_name = user_name,
							password = password,
							api_key = api_key
						}
				};

			/** Return result. */
			return Json(oResultJson, JsonRequestBehavior.AllowGet);
		}

	}
}
