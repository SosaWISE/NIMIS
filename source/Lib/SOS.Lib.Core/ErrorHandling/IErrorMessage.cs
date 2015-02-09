using System.Collections.Generic;

namespace SOS.Lib.Core.ErrorHandling
{
	public interface IErrorMessage
	{
		ErrorMessageType Type { get; set; }
		string Header { get; set; }
		string Message { get; set; }
		string MethodCall { get; set; }
		string ComputerName { get; set; }
		string SourceUrl { get; set; }
		string CreatedBy { get; set; }
		int? AccountId { get; set; }
		bool ShouldPersist { get; set; }
		LogSource Source { get; set; }

		string TargetDatabase { get; set; }
		string TargetSchema { get; set; }
		string TargetTable { get; set; }
		string TargetPrimaryKey { get; set; }

		ICollection<IErrorStackFrame> StackFrames { get; }
	}
}
