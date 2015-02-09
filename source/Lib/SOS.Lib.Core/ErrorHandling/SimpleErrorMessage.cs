using System.Collections.Generic;

namespace SOS.Lib.Core.ErrorHandling
{
	public class SimpleErrorMessage : IErrorMessage
	{
		public ErrorMessageType Type { get; set; }
		public string Header { get; set; }
		public string Message { get; set; }
		public string MethodCall { get; set; }
		public string ComputerName { get; set; }
		public string SourceUrl { get; set; }
		public string CreatedBy { get; set; }
		public int? AccountId { get; set; }
		public bool ShouldPersist { get; set; }
		public LogSource Source { get; set; }
		public string TargetDatabase { get; set; }
		public string TargetSchema { get; set; }
		public string TargetTable { get; set; }
		public string TargetPrimaryKey { get; set; }
		public ICollection<IErrorStackFrame> StackFrames { get; set; }

		public SimpleErrorMessage()
		{
			StackFrames = new List<IErrorStackFrame>();
		}
	}
}