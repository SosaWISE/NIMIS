using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util;

namespace SOS.FOS.EmailSupport
{
	public class SmtpSender
	{
		#region .ctor

		public SmtpSender(string smtpServer, int sendRetryCount,
			int sendRetryDelay, string recipientList,
			string senderAddress = DefaultSenderAddress,
			string senderDisplayName = DefaultSenderDisplayName)
		{
			if (smtpServer.Contains(":"))
			{
				var parts = smtpServer.Split(':');
				_smtpServer = parts[0];
				_smtpServerPort = int.Parse(parts[1]);
			}
			else
			{
				_smtpServer = smtpServer;
				_smtpServerPort = 25;
			}

			_sendRetryCount = sendRetryCount;
			_sendRetryDelay = sendRetryDelay;
			_recipientList = recipientList;
			_senderAddress = new MailAddress(senderAddress, senderDisplayName);
		}

		public SmtpSender(string smtpServer, int smtpServerPort, int sendRetryCount,
			int sendRetryDelay, string recipientList,
			string senderAddress = DefaultSenderAddress,
			string senderDisplayName = DefaultSenderDisplayName)
		{
			_smtpServer = smtpServer;
			_smtpServerPort = smtpServerPort;
			_sendRetryCount = sendRetryCount;
			_sendRetryDelay = sendRetryDelay;
			_recipientList = recipientList;
			_senderAddress = new MailAddress(senderAddress, senderDisplayName);
		}

		#endregion .ctor

		#region Member Properties

		#region Private

		private readonly string _smtpServer;
		private readonly int _smtpServerPort;
		private readonly int _sendRetryCount;
		private readonly int _sendRetryDelay;
		private readonly string _recipientList;
		private readonly MailAddress _senderAddress;

		#endregion Private

		#region Public

		public const string DefaultSenderAddress = "andres@wisearchitects.com";
		public const string DefaultSenderDisplayName = "WISE Architects";

		#endregion Public

		#endregion Member Properties

		#region Member Functions

		public void SendMessage(string subject, string htmlBody, string textBody)
		{
			if (string.IsNullOrEmpty(_smtpServer))
			{
				Data.Logging.DBErrorManager.Instance.AddMessage(ErrorMessageType.Warning
					, "Unable to send message"
					, "Can't send email - no smtp server configured");
				return;
			}

			if (string.IsNullOrEmpty(_recipientList))
			{
				Data.Logging.DBErrorManager.Instance.AddMessage(ErrorMessageType.Warning
					, "Unable to send message"
					, "Can't send email - no recipient list configured");
				return;
			}

			var msg = new MailMessage
			{
				From = _senderAddress,
				Subject = subject,
				Body = textBody,
				IsBodyHtml = false
			};

			var recipients = _recipientList.Split(',');
			foreach (var recip in recipients)
			{
				msg.To.Add(new MailAddress(recip.Trim()));
			}

			if (!htmlBody.IsNullOrEmpty())
			{
				var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, new ContentType("text/html"));
				msg.AlternateViews.Add(htmlView);
			}

			var smtpClient = new SmtpClient(_smtpServer, _smtpServerPort)
			{
				UseDefaultCredentials = true
			};

			var retryCount = 0;

			do
			{
				try
				{
					smtpClient.Send(msg);
					break;
				}
				catch (Exception oEx)
				{
					retryCount++;
					if (retryCount <= _sendRetryCount)
					{
						Data.Logging.DBErrorManager.Instance.AddMessage(ErrorMessageType.Warning
							, oEx
							, "Unable to send message"
							, string.Format("Problem sending '{0}', retrying", subject));
						Thread.Sleep(_sendRetryDelay);
					}
					else
					{
						Data.Logging.DBErrorManager.Instance.AddMessage(ErrorMessageType.Warning
							, oEx
							, "Unable to send message"
							, string.Format("Problem sending '{0}'", subject));
						break;
					}
				}
			} while (true);
		}

		#endregion Member Functions

	}
}
