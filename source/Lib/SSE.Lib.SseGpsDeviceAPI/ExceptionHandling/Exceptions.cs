using SOS.Lib.Core.ExceptionHandling;

namespace SSE.Lib.SseGpsDeviceAPI.ExceptionHandling
{
	public class SseGpsDeviceException : BaseException
	{
		#region .ctor
		public SseGpsDeviceException(string message) : base(message) {}
		#endregion .ctor
	}

	public class SseGpsDeviceSentenceLengthException : SseGpsDeviceException
	{
		#region .ctor
		public SseGpsDeviceSentenceLengthException(string message) : base(message) {}
		#endregion .ctor
	}

	public class SseGpsDeviceSentenceMissingDollarSign : SseGpsDeviceException
	{
		#region .ctor
		public SseGpsDeviceSentenceMissingDollarSign (string message) : base(message) { }
		#endregion .ctor
	}

	public class SseGpsDeviceSentenceMissingChkSum : SseGpsDeviceException
	{
		#region .ctor
		public SseGpsDeviceSentenceMissingChkSum (string message) : base(message) {}
		#endregion .ctor
	}

	public class SseGpsDeviceChkSumFailed : SseGpsDeviceException
	{
		#region .ctor
		public SseGpsDeviceChkSumFailed(string chkSumReceived, string chkSumGenerated, string message)
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

	public class SseInvalidCommandName : BaseException
	{
		#region .ctor
		
		public SseInvalidCommandName(string commandPassed)
			: base(string.Format("The command name '{0}' passed is invalid and not supported.", commandPassed))
		{
			CommandName = commandPassed;
		}

		#endregion .ctor

		#region Member Variables
		
		public string CommandName { get; private set; }

		#endregion Member Variables
	}

	public class SseInvalidCheckSum : BaseException
	{
		#region .ctor
		public SseInvalidCheckSum(string sentence, string originalChkSum, string genChkSum)
			: base(string.Format("The sentence '{0}' came with checksum '{1}' which did not pass the checksum of '{2}'."
				, sentence, originalChkSum, genChkSum))
		{
			Sentence = sentence;
			OriginalChkSum = originalChkSum;
			GenChkSum = genChkSum;
		}
		#endregion .ctor

		#region Member Variables

		public string Sentence { get; private set; }
		public string OriginalChkSum { get; private set; }
		public string GenChkSum { get; private set; }

		#endregion Member Variables
	}

	public class SseInvalidAccountIDException : BaseException
	{
		#region .ctor

		public SseInvalidAccountIDException(string accountID, string calledFrom) : base(string.Format(_MESSAGE_TEMP, accountID, calledFrom))
		{
			AccountID = accountID;
			CalledFrom = calledFrom;
		}
		
		#endregion .ctor

		#region Properties

		private const string _MESSAGE_TEMP =
			"AccountID '{0}' does not conform to a valid account ID.  Error occured in '{1}'.";
		public string AccountID { get; private set; }
		public string CalledFrom { get; private set; }

		#endregion Properties
	}

	public class SseInvalidPPMException : BaseException
	{
		#region .ctor

		public SseInvalidPPMException(string ppm, string calledFrom) : base(string.Format(_MESSAGE_TEMP, ppm, calledFrom))
		{
			PPM = ppm;
			CalledFrom = calledFrom;
		}

		#endregion .ctor

		#region Properties

		private const string _MESSAGE_TEMP = "PPM '{0}' does not conform to a valid PPM.  Error occuired in '{1}'.";
		public string PPM { get; private set; }
		public string CalledFrom { get; private set; }

		#endregion Properties
	}

	public class SseInvalidLBAException : BaseException
	{
		#region .ctor

		public SseInvalidLBAException(string lba, string calledFrom) : base(string.Format(_MESSAGE_TEMP, lba, calledFrom))
		{
			LBA = lba;
			CalledFrom = calledFrom;
		}

		#endregion .ctor

		#region Properties

		private const string _MESSAGE_TEMP = "LBA '{0}' does not conform to a valid LBA.  Error occuired in '{1}'.";
		public string LBA { get; private set; }
		public string CalledFrom { get; private set; }

		#endregion Properties
	}

	public class SseInvalidSPAException : BaseException
	{
		#region .ctor

		public SseInvalidSPAException(string spa, string calledFrom) : base(string.Format(_MESSAGE_TEMP, spa, calledFrom))
		{
			SPA = spa;
			CalledFrom = calledFrom;
		}

		#endregion .ctor

		#region Properties

		private const string _MESSAGE_TEMP = "SPA '{0}' does not conform to a valid SPA.  Error occuired in '{1}'.";
		public string SPA { get; private set; }
		public string CalledFrom { get; private set; }

		#endregion Properties
	}

	public class SseInvalidGFAException : BaseException
	{
		#region .ctor

		public SseInvalidGFAException(string gfa, string calledFrom) : base(string.Format(_MESSAGE_TEMP, gfa, calledFrom))
		{
			GFA = gfa;
			CalledFrom = calledFrom;
		}

		#endregion .ctor

		#region Properties

		private const string _MESSAGE_TEMP = "GFA '{0}' does not conform to a valid GFA.  Error occuired in '{1}'.";
		public string GFA { get; private set; }
		public string CalledFrom { get; private set; }

		#endregion Properties
	}
}
