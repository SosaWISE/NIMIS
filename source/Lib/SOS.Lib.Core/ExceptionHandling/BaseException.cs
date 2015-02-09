using System;

namespace SOS.Lib.Core.ExceptionHandling
{
	public class BaseException : Exception
	{
		#region .ctor

		public BaseException(string message) : base(message)
		{
		}

		#endregion .ctor
	}
}
