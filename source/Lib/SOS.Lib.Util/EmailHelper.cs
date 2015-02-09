using System;
using System.Net;
using System.Net.Mail;

namespace SOS.Lib.Util
{
	public class EmailHelper
	{
		#region .ctor

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="szSmtpServer">string</param>
		/// <param name="szUsername">string</param>
		/// <param name="szPassword">string</param>
		public EmailHelper(string szSmtpServer, string szUsername, string szPassword)
		{
			SmtpServer = szSmtpServer;
			Username = szUsername;
			Password = szPassword;
		}

		#endregion .ctor

		#region Fields

		public string SmtpServer { get; private set; }
		public string Username { get; private set; }
		public string Password { get; private set; }

		#endregion Fields

		#region Methods

		/// <summary>
		/// Given the parameters it will send an email.
		/// </summary>
		/// <param name="szTo">string</param>
		/// <param name="szFrom">string</param>
		/// <param name="szSubject">string</param>
		/// <param name="szBody">string</param>
		/// <param name="bIsBodyHtml">bool</param>
		/// <param name="szErrorMang">string</param>
		/// <returns>bool</returns>
		public bool SendMessage(string szTo, string szFrom, string szSubject, string szBody, bool bIsBodyHtml,
		                        out string szErrorMang)
		{
			// Locals
			bool bResult = false;
			szErrorMang = string.Empty;

			try
			{
				// Locals
				var oMsg = new MailMessage(szFrom, szTo, szSubject, szBody);
				var SMTPUserInfo = new NetworkCredential(Username, Password);
				var oClient = new SmtpClient(SmtpServer);
				oClient.UseDefaultCredentials = false;
				oClient.Credentials = SMTPUserInfo;

				// Init
				oMsg.IsBodyHtml = bIsBodyHtml;
				oClient.Send(oMsg);

				oMsg.Dispose();
				bResult = true;
			}
			catch (Exception oEx)
			{
				szErrorMang = string.Format("An error occurred when sending email:\r\n{0}", oEx.Message);
			}

			// return result
			return bResult;
		}

		#endregion Methods
	}
}