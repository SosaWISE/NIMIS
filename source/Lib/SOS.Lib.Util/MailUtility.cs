using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;

namespace SOS.Lib.Util
{
	public enum MailAddressTypes
	{
		To,
		CC,
		Bcc
	}

	public class MailUtility
	{
		#region Constants

		//public const string EMAIL_REGEX = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
		public const string EMAIL_REGEX =
			@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

		public const string EMAIL_DELIM = ";";

		#endregion

		#region Properties

		#region Static

		#endregion Static

		#endregion Properties

		#region Methods

		#region Static

		public static bool IsValidEmail(string textAddress)
		{
			var regexObj = new Regex(EMAIL_REGEX);
			return regexObj.IsMatch(textAddress);
		}

		public static void GetValidEmails(string text, out List<string> validList, out List<string> invalidList)
		{
			GetValidEmails(text, EMAIL_DELIM, out validList, out invalidList);
		}

		public static void GetValidEmails(string text, string delimeter, out List<string> validList,
		                                  out List<string> invalidList)
		{
			var uniqueEmails = new Dictionary<string, string>();

			invalidList = new List<string>();

			text = StringUtility.NullIfWhiteSpace(text);
			if (text != null)
			{
				string[] ray = text.Split(new[] {delimeter}, StringSplitOptions.RemoveEmptyEntries);
				if (ray.Length > 0)
				{
					foreach (string s in ray)
					{
						if (IsValidEmail(s))
						{
							if (!uniqueEmails.ContainsKey(s))
							{
								uniqueEmails.Add(s, s);
							}
						}
						else
						{
							invalidList.Add(s);
						}
					}
				}
			}

			validList = new List<string>();

			foreach (string email in uniqueEmails.Keys)
			{
				validList.Add(email);
			}
		}

		public static List<string> SplitEmails(string text)
		{
			return SplitEmails(text, EMAIL_DELIM);
		}

		public static List<string> SplitEmails(string text, string delimeter)
		{
			var uniqueEmails = new Dictionary<string, string>();

			text = StringUtility.NullIfWhiteSpace(text);
			if (text != null)
			{
				string[] ray = text.Split(new[] {delimeter}, StringSplitOptions.RemoveEmptyEntries);
				if (ray.Length > 0)
				{
					foreach (string s in ray)
					{
						if (!uniqueEmails.ContainsKey(s))
						{
							uniqueEmails.Add(s, s);
						}
					}
				}
			}

			return uniqueEmails.Keys.ToList();
		}

		public static MailAddress GetTextAddress(string displayName, string phoneCell, string szCarrierDomain)
		{
			string textAddress = string.Format("{0}@{1}", phoneCell, szCarrierDomain).Replace(" ", string.Empty);
			if (IsValidEmail(textAddress))
				return new MailAddress(textAddress, displayName);
			else
				return null;
		}

		public static MailMessage GetDefaultMailMessage(string szSubject, string szBody, bool bIsBodyHtml)
		{
			var oMail = new MailMessage();
			oMail.Subject = szSubject;
			oMail.Body = szBody;
			oMail.IsBodyHtml = bIsBodyHtml;
			return oMail;
		}

		public static void SendBulkMessage(MailMessage oMail, MailAddressCollection to, MailAddressCollection cc,
		                                   MailAddressCollection bcc, int nSleepSeconds, int maxEmails)
		{
			if (to != null && to.Count > 0)
			{
				throw new NotImplementedException();
			}
			if (cc != null && cc.Count > 0)
			{
				throw new NotImplementedException();
			}
			if (bcc != null && bcc.Count > 0)
			{
				SendBulkMessage(oMail, bcc, MailAddressTypes.Bcc, nSleepSeconds, maxEmails);
			}
		}

		public static void SendBulkMessage(MailMessage oMail, MailAddressCollection oMac, MailAddressTypes eAddressType,
		                                   int nSleepSeconds, int nMaxEmails)
		{
			ClearRecipients(oMail);

			MailAddressCollection mailAddressList;
			switch (eAddressType)
			{
				case MailAddressTypes.To:
					mailAddressList = oMail.To;
					break;
				case MailAddressTypes.CC:
					mailAddressList = oMail.CC;
					break;
				case MailAddressTypes.Bcc:
					mailAddressList = oMail.Bcc;
					break;
				default:
					throw new NotImplementedException();
			}

			foreach (MailAddress address in oMac)
			{
				mailAddressList.Add(address);
				if (mailAddressList.Count >= nMaxEmails)
				{
					SendMessage(oMail, nSleepSeconds);
					ClearRecipients(oMail);
				}
			}

			//send to left overs
			SendMessage(oMail, nSleepSeconds);
		}

		public static void ClearRecipients(MailMessage oMail)
		{
			oMail.To.Clear();
			oMail.CC.Clear();
			oMail.Bcc.Clear();
		}

		public static void SendMessage(MailMessage oMail)
		{
			SendMessage(oMail, 0);
		}

		public static void SendMessage(MailMessage oMail, int nSleepSeconds)
		{
			if (oMail.To.Count > 0 || oMail.CC.Count > 0 || oMail.Bcc.Count > 0)
			{
				var oClient = new SmtpClient(); //get info from <system.net> in web.config
				oClient.Send(oMail);

				if (nSleepSeconds > 0)
					Thread.Sleep(nSleepSeconds*1000);
			}
		}

		#endregion Static

		#endregion Methods

		public static void TellAaron(string szSubject, string szBody, bool bIsBodyHtml)
		{
			MailMessage mail = GetDefaultMailMessage(szSubject, szBody, bIsBodyHtml);
			mail.To.Add("ashumway@pprotect.com");
			SendMessage(mail);
		}
	}
}