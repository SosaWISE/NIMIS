using System;
using System.Collections.Generic;

namespace NXS.Services.Wcf.AppServices.Dashboard.Models
{
	public class Message
	{
		public int MessageID { get; set; }
		public string RecipientID { get; set; }
		public string Subject { get; set; }
		public string MessageText { get; set; }
		public DateTime? ReadOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsDeleted { get; set; }
		public List<MessageAction> Actions { get; set; }

	}
}