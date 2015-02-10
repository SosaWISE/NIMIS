
using System;
using System.Collections.Generic;

namespace NXS.Services.Wcf.AppServices.ErrorHandling.Models
{
	public class ErrorMessage
	{
		public List<MessageStackFrame> MessageStackFrames { get; set; }
		public int ErrorMessageTypeID { get; set; }
		public string Header { get; set; }
		public string Message { get; set; }
		public string MethodCall { get; set; }
		public string ComputerName { get; set; }
		public string SourceUrl { get; set; }
		public string CreatedById { get; set; }
		public int? AccountId { get; set; }
		public bool ShouldPersist { get; set; }
		public string TargetDatabase { get; set; }
		public string TargetSchema { get; set; }
		public string TargetTable { get; set; }
		public int? TargetPrimaryKey { get; set; }
	}
}
