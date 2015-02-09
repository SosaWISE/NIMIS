using System;

namespace SOS.Lib.TxtWire.ErrorHandling
{
	public enum TxtWireCode
	{
		Success = 0,
		InvalidArgumentLength = 100
	}

	[Obsolete("Not used anymore", true)]
	public class TxtWireExceptionInvalidLengthExceptions : Exception
	{
		#region .ctor

		public TxtWireExceptionInvalidLengthExceptions(string message) : base(message)
		{
			Code = TxtWireCode.InvalidArgumentLength;
		}

		#endregion .ctor

		#region Properties

		public TxtWireCode Code { get; private set; }

		#endregion Properties
	}
}
