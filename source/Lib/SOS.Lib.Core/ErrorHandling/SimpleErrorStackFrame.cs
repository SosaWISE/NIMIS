namespace SOS.Lib.Core.ErrorHandling
{
	public class SimpleErrorStackFrame : IErrorStackFrame
	{
		public string MethodName { get; set; }
		public string FileName { get; set; }
		public int? LineNumber { get; set; }
		public int? ColumnNumber { get; set; }
	}
}