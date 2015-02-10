using System;
using System.Web;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SOS.Clients.MVC.CorpSite2
{
	/// <summary>
	/// Summary description for MailHandler
	/// </summary>
	public class Handler : IHttpHandler 
	{
		public void ProcessRequest (HttpContext context) 
		{
			//var mailClient = new SmtpClient(context.Request.Form.Get("smtpMailServer"));
			//string szOwnerEmail = context.Request.Form.Get("owner_email");
			var mailClient = new SmtpClient("smtpout.secureserver.net");
			const string szOwnerEmail = "Andres@wisearchitects.com";
			string szSubject = "A message from your site visitor " + context.Request.Form.Get("name");
			string szEmail = context.Request.Form.Get("email");
			string szMessageBody = "";
		
			szMessageBody += "<p>Visitor: " + context.Request.Form.Get("name") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Email Address: " + context.Request.Form.Get("email") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Phone Number: " + context.Request.Form.Get("phone") + "</p>\n";
			szMessageBody += "<br>\n";
			szMessageBody += "<p>Message: " + context.Request.Form.Get("message") + "</p>\n";
		
				
			var oMessage = new MailMessage();
		
			try{
				oMessage.From = new MailAddress(szEmail);
			}catch (FormatException e) {
				context.Response.Write(e.Message);
			}
		
			oMessage.To.Add(szOwnerEmail);
			oMessage.Subject = szSubject;
			if(context.Request.Form.Get("stripHTML") == "true"){
				oMessage.IsBodyHtml = false;
	            szMessageBody = Regex.Replace(szMessageBody, "<.*?>", string.Empty);
			}else{
			  	oMessage.IsBodyHtml = true;
			}
			oMessage.Body = szMessageBody;
			
			try
			{
				mailClient.Send(oMessage);
			}
			catch (SmtpException e)
			{
				context.Response.Write("mail failed");
			}
			context.Response.Write("mail sent");
		}
	
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}