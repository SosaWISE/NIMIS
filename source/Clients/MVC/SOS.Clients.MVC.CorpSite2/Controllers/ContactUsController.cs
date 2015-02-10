using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.Services.Interfaces.Models;
using SosResult = SOS.Clients.MVC.CorpSite2.Models.SosResult;

namespace SOS.Clients.MVC.CorpSite2.Controllers
{
	public class ContactUsController : Controller
	{
		//
		// GET: /ContactUs/

		public ActionResult Index()
		{
			return View();
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult SubmitForm(string name, string email, string phone, string message, string stripHTML)
		{
			/** Get form Values. */
			var oResult = new SosResult((int)SosResult.MessageCodes.GeneralWarning, "Initializing", string.Empty);
			var mailClient = new SmtpClient("smtpout.secureserver.net")
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("andres@wisearchitects.com", "jugete98")
			};
			const string szOwnerEmail = "Andres@wisearchitects.com";
			string szSubject = "A message from your site visitor " + name;
			string szEmail = email;
			//const string szEmail = "Andres@wisearchitects.com";
			string szMessageBody = "";

			szMessageBody += "<p>Visitor: " + name + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Email Address: " + email + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Phone Number: " + phone + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Message: " + message + "</p>\n";


			var oMessage = new MailMessage();

			try
			{
				oMessage.From = new MailAddress(szEmail);
			}
			catch (FormatException e)
			{
				Response.Write(e.Message);
			}

			oMessage.To.Add(szOwnerEmail);
			oMessage.CC.Add("andy@freedomsos.com");
			oMessage.Subject = szSubject;
			if (stripHTML == "true")
			{
				oMessage.IsBodyHtml = false;
				szMessageBody = Regex.Replace(szMessageBody, "<.*?>", string.Empty);
			}
			else
			{
				oMessage.IsBodyHtml = true;
			}
			oMessage.Body = szMessageBody;

			try
			{
				mailClient.Send(oMessage);

				oResult.Code = (int)SosResult.MessageCodes.Success;
				oResult.Message = "Success";
			}
			catch (SmtpException oEx)
			{
				//Response.Write(string.Format("mail failed: {0}", oEx.Message));
				oResult.Code = (int) SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("mail failed: {0}", oEx.Message);
			}

			//Response.Write("mail sent");

			/** Return result.*/
			return Json(oResult, JsonRequestBehavior.AllowGet);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult NewLeadSubmitForm(string name, string lastname, string address, string city, string state, string postal, string email, string phone, string message, int sourceId = 11)
		{
			/** Initialize. */
			var oResult = new SosResult((int)SosResult.MessageCodes.GeneralWarning, "Initializing", string.Empty);
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			var szMessage = !message.Equals("nope") ? message : null;

			/** Execute the create lead. */
			try
			{
				/** call create lead. */
				IFnsResult<IFnsLeadModel> oSrvResult = oService.CreateLeadBasic(
					5000
					, name
					, lastname
					, !address.Equals(string.Empty) ? address : "[No Street Provided]"
					, city
					, state
					, postal
					, email
					, phone
					, szMessage
					, sourceId /**  Internet source */
					/**, 1 Interested. */);

				/** Validate services result. */
				if (oSrvResult == null)
				{
					return Json(new SosResult<CmsModels.QlLeadBasicView>((int)SosResult.MessageCodes.GeneralWarning
								, string.Format("No value was returned.")
								, null) { Value = null }
						, JsonRequestBehavior.AllowGet);
				}
				/** Check that there was an error code returned. */
				if (oSrvResult.Code > (int)SosResult.MessageCodes.Success)
				{
					return Json(new SosResult<CmsModels.QlLeadBasicView>(oSrvResult.Code
								, oSrvResult.Message
								, null)
							, JsonRequestBehavior.AllowGet);
				}

				/** Get lead info. */
				var oLead = (IFnsLeadModel)oSrvResult.GetValue();

				/** Send email to notify of new lead. */
				Helpers.EmailManager.SubmitForm(oLead.CustomerMasterFileId, oLead.LeadID, string.Format("{0} {1}", name, lastname)
					, email, phone, "New Brochure download request.", false);

				/** Create return package. */
				oResult.Code = (int)SosResult.MessageCodes.Success;
				oResult.Message = "Success on NewLeadSubmitForm";
			}
			catch (Exception oEx)
			{
				oResult.Code = (int)SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("New Lead failed: {0}", oEx.Message);
			}

			/** Return result. */
			//Response.Write("Successfully submitted your information.");
			return Json(oResult, JsonRequestBehavior.AllowGet);
		}
	}
}