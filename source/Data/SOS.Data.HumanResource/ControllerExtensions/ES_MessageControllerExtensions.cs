using System;
using System.Collections.Generic;
using SOS.Lib.Util;
using AR = SOS.Data.HumanResource.ES_Message;
using ARCollection = SOS.Data.HumanResource.ES_MessageCollection;
using ARController = SOS.Data.HumanResource.ES_MessageController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class ES_MessageControllerExtensions
	{
		public static ES_Message CreateAndSaveMessage(this ARController controller, string emails, string subject, string body
			, bool isHtml, string senderName, string senderAddress, bool destroyAfter, string creator, bool throwOnInvalidEmail, bool setReady)
		{
			return controller.CreateAndSaveMessage(MailUtility.SplitEmails(emails), subject, body, isHtml, senderName, senderAddress, destroyAfter, creator, throwOnInvalidEmail, setReady);
		}

		public static ES_Message CreateAndSaveMessage(this ARController controller, List<string> emailList, string subject, string body
			, bool isHtml, string senderName, string senderAddress, bool destroyAfter, string creator, bool throwOnInvalidEmail, bool setReady)
		{
			var validList = new List<string>();
			foreach (var email in emailList)
			{
				if (MailUtility.IsValidEmail(email))
				{
					validList.Add(email);
				}
				else if (throwOnInvalidEmail)
				{
					throw new Exception("Invalid email in emails");
				}
			}

			//create message
			ES_Message message = CreateAndSaveMessage(subject, body, isHtml, senderName, senderAddress, destroyAfter, creator);

			//add recipients
			var recipients = new ES_MessageRecipientCollection();
			foreach (var email in emailList)
			{
				recipients.Add(ES_MessageRecipient.NewRecipient(email));
			}
			recipients.SetMessageID(message.MessageID);
			recipients.SaveAll(creator);

			//set ready to email
			if (setReady)
			{
				message.SetReady(creator);
			}

			return message;
		}
		private static ES_Message CreateAndSaveMessage(string subject, string body, bool isHtml, string senderName, string senderAddress, bool destroyAfter, string creatorUserName)
		{
			if (string.IsNullOrEmpty(subject))
				throw new ArgumentNullException("subject");
			if (string.IsNullOrEmpty(body))
				throw new ArgumentNullException("body");
			if (string.IsNullOrEmpty(senderName))
				throw new ArgumentNullException("senderName");
			if (string.IsNullOrEmpty(senderAddress))
				throw new ArgumentNullException("senderAddress");
			if (string.IsNullOrEmpty(creatorUserName))
				throw new ArgumentNullException("creatorUserName");

			if (!MailUtility.IsValidEmail(senderAddress))
				throw new Exception("SenderAddress is invalid email");

			var message = new ES_Message
			{
				Subject = subject,
				Body = body,
				IsHtml = isHtml,
				SenderName = senderName,
				SenderAddress = senderAddress,
				DestroyAfter = destroyAfter,
				IsReady = false
			};
			message.Save(creatorUserName);

			return message;
		}
	}
}
