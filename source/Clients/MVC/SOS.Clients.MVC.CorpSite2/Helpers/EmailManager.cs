using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using SOS.Clients.MVC.CorpSite2.Models;
using SOS.FunctionalServices.Models.Cms;

namespace SOS.Clients.MVC.CorpSite2.Helpers
{
	public static class EmailManager
	{
		public static SosResult SubmitForm(long lCMFID, long lLeadID, string name, string email, string phone, string message, bool bStripHTML)
		{
			/** Get form Values. */
			string sDomainName = ConfigurationManager.AppSettings["DomainName"] ?? "dealers.freedomsos.com";
			var oResult = new SosResult((int)SosResult.MessageCodes.GeneralWarning, "Initializing", string.Empty);
			var mailClient = new SmtpClient("smtpout.secureserver.net")
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("andres@wisearchitects.com", "jugete98")
			};
			const string szOwnerEmail = "andres@wisearchitects.com";
			string szSubject = "New Brochure download lead generated: " + name;
			string szEmail = email;
			//const string szEmail = "Andres@wisearchitects.com";
			string szMessageBody = "";

			szMessageBody += string.Format("<p><a href=\"http://" + sDomainName + "/?ld={1}-{0}\">Lead ID: {0}</a></p>\n", lCMFID, lLeadID);
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Lead: " + name + "</p>\n";
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
				oResult.Code = (int) SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("The following error was thrown:\r\n{0}.", e.Message);
				return oResult;
			}

			oMessage.To.Add(szOwnerEmail);
			oMessage.CC.Add("andy@freedomsos.com");
			oMessage.Subject = szSubject;
			if (bStripHTML)
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
				oResult.Code = (int)SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("mail failed: {0}", oEx.Message);
			}

			//Response.Write("mail sent");

			/** Return result.*/
			return oResult;
		}

		public static SosResult SubmitDealerForm(FnsQlDealerLeadModel oDealerLeadModel, string szMessage, bool bStripHTML = false)
		{
			/** Initializing. */
			string sDomainName = ConfigurationManager.AppSettings["DomainName"] ?? "dealers.freedomsos.com";
			var oResult = new SosResult((int)SosResult.MessageCodes.GeneralWarning, "Initializing", string.Empty);
			var mailClient = new SmtpClient("smtpout.secureserver.net")
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("andres@wisearchitects.com", "jugete98")
			};
			const string szOwnerEmail = "andres@wisearchitects.com";
			string szName = string.Format("{0} {1}", oDealerLeadModel.ContactFirstName, oDealerLeadModel.ContactLastName);
			string szSubject = string.Format("New Brochure download Dealer Lead generated: {0}", szName);
			string szEmail = oDealerLeadModel.ContactEmail;
			string szMessageBody = "";

			szMessageBody += string.Format("<p><a href=\"http://" + sDomainName + "/?ldr={1}-{0}\">Dealer Lead ID: {0}</a></p>\n", 1234, oDealerLeadModel.DealerLeadID);
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Lead: " + szName + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Email Address: " + oDealerLeadModel.ContactEmail + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Phone Number: " + oDealerLeadModel.PhoneMobile + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Message: " + szMessage + "</p>\n";


			var oMessage = new MailMessage();

			try
			{
				oMessage.From = new MailAddress(szEmail);
			}
			catch (FormatException e)
			{
				oResult.Code = (int)SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("The following error was thrown:\r\n{0}.", e.Message);
				return oResult;
			}

			oMessage.To.Add(szOwnerEmail);
			oMessage.CC.Add("andy@freedomsos.com");
			oMessage.Subject = szSubject;
			if (bStripHTML)
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
				oResult.Code = (int)SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("mail failed: {0}", oEx.Message);
			}

			//Response.Write("mail sent");

			/** Return result.*/
			return oResult;
		}
	}
}