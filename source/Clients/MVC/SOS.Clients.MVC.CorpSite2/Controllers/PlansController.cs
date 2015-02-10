using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SOS.Clients.MVC.CorpSite2.Controllers
{
	public class PlansController : Controller
	{
		//
		// GET: /Plans/

		public ActionResult Index()
		{
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult SubmitForm()
		{
			/** Get form Values. */
			var mailClient = new SmtpClient("smtpout.secureserver.net")
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("andres@wisearchitects.com", "jugete98")
			};
			const string szOwnerEmail = "Andres@wisearchitects.com";
			string szSubject = "Enroll Form from " + Request.Form.Get("name");
			string szEmail = Request.Form.Get("email");
			//const string szEmail = "Andres@wisearchitects.com";
			string szMessageBody = "";

			szMessageBody += "<p>Visitor: " + Request.Form.Get("name") + " " + Request.Form.Get("lastname") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Email Address: " + Request.Form.Get("email") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Address: " + Request.Form.Get("address") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>City: " + Request.Form.Get("city") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>State: " + Request.Form.Get("state") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Postal: " + Request.Form.Get("postal") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Phone Number: " + Request.Form.Get("phone") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Message: " + Request.Form.Get("message") + "</p>\n";


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
			oMessage.CC.Add("info@freedomsos.com");
			oMessage.Subject = szSubject;
			if (Request.Form.Get("stripHTML") == "true")
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
			}
			catch (SmtpException oEx)
			{
				Response.Write(string.Format("mail failed: {0}", oEx.Message));
			}
			Response.Write("mail sent");

			return new EmptyResult();
		}

	}
}
