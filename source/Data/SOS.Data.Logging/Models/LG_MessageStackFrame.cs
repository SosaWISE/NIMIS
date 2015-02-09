using SOS.Lib.Core.ErrorHandling;

namespace SOS.Data.Logging
{
	public partial class LG_MessageStackFrame : IErrorStackFrame
	{
		#region Constants

		public const int MaxMethodLength = 255;
		public const int MaxFileNameLength = 255;

		#endregion Constants

		#region Implementation of IErrorStackFrame

		public string MethodName { get; set; }

		#endregion Implementation of IErrorStackFrame
	}
}
