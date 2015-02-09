using SOS.Lib.Core.ExceptionHandling;

namespace SSE.Lib.HE910API.ExceptionHandling
{
	public class HE910Exception : BaseException
	{
		#region .ctor
		public HE910Exception(string message) : base(message) {}
		#endregion .ctor
	}

	public class HE910SentenceLengthException : HE910Exception
	{
		#region .ctor
		public HE910SentenceLengthException(string message) : base(message) {}
		#endregion .ctor
	}

	public class HE910SentenceMissingDollarSign : HE910Exception
	{
		#region .ctor
		public HE910SentenceMissingDollarSign (string message) : base(message) { }
		#endregion .ctor
	}

	public class HE910SentenceMissingChkSum : HE910Exception
	{
		#region .ctor
		public HE910SentenceMissingChkSum (string message) : base(message) {}
		#endregion .ctor
	}

	public class HE910ChkSumFailed : HE910Exception
	{
		#region .ctor
		public HE910ChkSumFailed(string chkSumReceived, string chkSumGenerated, string message)
			: base(string.Format("Received: {0} | Generated: {1} : {2}"
				, chkSumReceived, chkSumGenerated, message))
		{
			ChkSumReceived = chkSumReceived;
			ChkSumGenerated = chkSumGenerated;
		}
		#endregion .ctor

		#region Member Variables

		protected string ChkSumReceived { get; private set; }
		protected string ChkSumGenerated { get; private set; }

		#endregion Member Variables
	}

}
