namespace NXS.Services.Wcf.AppServices.ErrorHandling.Models
{
	public class MessageStackFrame
	{
		public string MethodName { get; set; }
		public string FileName { get; set; }
		public int? LineNumber { get; set; }
		public int? ColumnNumber { get; set; }
	}
}
