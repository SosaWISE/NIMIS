using SOS.Lib.Core.ExceptionHandling;

namespace SOS.Lib.KW621API.ExceptionHandling
{
	public class KW621Exceptions : BaseException
	{
		#region .ctor
		public KW621Exceptions(string message)
			: base(message)
		{
		}
		#endregion .ctor
	}

	public class KW621ExceptionsFixedLengthException : KW621Exceptions
	{
		#region .ctor
		public KW621ExceptionsFixedLengthException(string message)
			: base(message)
		{
		}
		#endregion .ctor
	}
}
