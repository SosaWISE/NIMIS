using System.Collections.Generic;

namespace NXS.Services.Wcf.AppServices.Dashboard.Models
{
	public class MessageAction
	{
		public string ActionName { get; set; }
		public string Label { get; set; }
		public List<MessageActionParameter> Parameters { get; set; }
	}
}