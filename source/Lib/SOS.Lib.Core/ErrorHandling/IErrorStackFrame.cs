namespace SOS.Lib.Core.ErrorHandling
{
	public interface IErrorStackFrame
	{
		string MethodName { get; set; }
		string FileName { get; set; }
		int? LineNumber { get; set; }
		int? ColumnNumber { get; set; }
	}
}
