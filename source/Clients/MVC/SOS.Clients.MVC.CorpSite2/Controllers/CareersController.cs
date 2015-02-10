using System;
using System.Web.Mvc;
using SOS.Clients.MVC.CorpSite2.Models;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Models.Cms;

namespace SOS.Clients.MVC.CorpSite2.Controllers
{
    public class CareersController : Controller
    {
        //
        // GET: /Careers/

        public ActionResult Index()
        {
            return View();
        }

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult NewDealerLeadSubmitForm(string name, string lastname, string address, string city, string state, string postal, string email, string phone, string message, string dealerTypeId)
		{
			/** Initialize. */
			var oResult = new SosResult((int)SosResult.MessageCodes.GeneralWarning, "Initializing", string.Empty);
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			var szMessage = !message.Equals("nope") ? message : null;

			/** Execute the create lead. */
			try
			{
				/** Save data. */
				var oItem = new FnsQlDealerLeadModel
				            	{
				            		DealerLeadTypeId = dealerTypeId,
									DealerName = string.Format("{0} {1}", name, lastname),
				            		ContactFirstName = name,
				            		ContactLastName = lastname,
				            		Address = address,
				            		City = city,
				            		StateAB = state,
				            		PostalCode = postal,
				            		ContactEmail = email,
									PhoneWork = phone,
				            		PhoneMobile = phone,
									Memo = szMessage,
									Username = email,
									Password = Lib.Util.StringUtility.GetRandomPassword(15)
				            	};
				var oResultValue = oService.DealerLeadCreateUpdate(oItem);
				oResult.Code = (int) SosResult.MessageCodes.Success;
				oResult.Message = "Successful on NewDealerLeadSubmitForm";
				oResult.Value = oResultValue.GetValue();


				/** Send email to notify of new lead. */
				Helpers.EmailManager.SubmitDealerForm((FnsQlDealerLeadModel) oResultValue.GetValue(), "New Brochure download request.");

			}
			catch (Exception oEx)
			{
				oResult.Code = (int)SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("New dealer lead failed: {0}", oEx.Message);
			}

			/** Return result. */
			//Response.Write("Successfully submitted your information.");
			return Json(oResult, JsonRequestBehavior.AllowGet);
		}
    }
}