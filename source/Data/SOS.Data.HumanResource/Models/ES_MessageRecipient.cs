using System;
using SOS.Lib.Util;

// ReSharper disable once CheckNamespace
namespace SOS.Data.HumanResource
{
	public partial class ES_MessageRecipientCollection
	{
		public void SetMessageID(int messageID)
		{
			foreach (ES_MessageRecipient item in this)
			{
				item.MessageID = messageID;
			}
		}
	}

	public partial class ES_MessageRecipient
	{
		public static ES_MessageRecipient NewRecipient(string address)
		{
			return NewRecipient(address, null);
		}
		public static ES_MessageRecipient NewRecipient(string address, string szName)
		{
			if (address == null)
				throw new ArgumentNullException("address");

			if (!MailUtility.IsValidEmail(address))
				throw new Exception("Invalid email address");

			var result = new ES_MessageRecipient();
			result.Address = address;
			result.Name = szName;

			return result;
		}
	}
}
