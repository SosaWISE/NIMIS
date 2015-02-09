using System;
using SOS.Lib.Core.ErrorHandling;

namespace SOS.Lib.Core.CreditReportService.Message
{

	public interface IMessage
	{
		#region Properties
		string DisplayMessage { get; }
		string Title { get; }
		ErrorMessageType MessageType { get; }
		Exception Ex { get; }

		#endregion Properties

	}

	public class Message : IMessage
	{
		public string DisplayMessage { get; set; }
		public string Title { get; set; }
		public ErrorMessageType MessageType { get; set; }
		public Exception Ex { get; set; }
	}
}
