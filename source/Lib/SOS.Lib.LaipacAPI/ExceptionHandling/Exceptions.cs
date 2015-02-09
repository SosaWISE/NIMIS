using SOS.Lib.Core.ExceptionHandling;

namespace SOS.Lib.LaipacAPI.ExceptionHandling
{
	public class LaipacException : BaseException
	{
		#region .ctor

		public LaipacException(string message)
			: base(message)
		{
		}

		#endregion .ctor
	}

	public class LaipacSentenceLengthException : LaipacException
	{
		#region .ctor

		public LaipacSentenceLengthException(string message)
			: base(message)
		{
		}

		#endregion .ctor
	}

	public class LaipacParameterFixedLengthException : LaipacException
	{
		#region .ctor

		public LaipacParameterFixedLengthException(string message)
			: base(message)
		{
		}

		#endregion .ctor
	}

	public class LaipacParameterVariableLengthException : LaipacException
	{
		#region .ctor

		public LaipacParameterVariableLengthException(string message)
			: base(message)
		{
		}

		#endregion .ctor
	}

	public class LaipacSentenceMissingChkSum : LaipacException
	{
		#region .ctor

		public LaipacSentenceMissingChkSum(string message)
			: base(message)
		{
		}

		#endregion .ctor
	}

	public class LaipacChkSumFailed : LaipacException
	{
		#region .ctor
		public LaipacChkSumFailed(string chkSumReceived, string chkSumGenerated, string message) 
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

	public class LaipacSentenceMissingDollarSign : LaipacException
	{
		#region .ctor

		public LaipacSentenceMissingDollarSign(string message)
			: base(message)
		{
		}

		#endregion .ctor
	}

	public class LaipacDeviceNotFoundInCrm : LaipacException
	{
		public LaipacDeviceNotFoundInCrm(string message, string sUnitID) : base(message)
		{
			UnitID = sUnitID;
		}

		#region Member Properties

		public string UnitID { get; private set; }

		#endregion Member Properties
	}

	public class LaipacDeviceMissingPasswordInCrm : LaipacException
	{
		public LaipacDeviceMissingPasswordInCrm(string message, string sUnitID)
			: base(message)
		{
			UnitID = sUnitID;
		}

		#region Member Properties

		public string UnitID { get; private set; }

		#endregion Member Properties
	}

	public class LaipacCommandNotSupported : LaipacException
	{
		public LaipacCommandNotSupported(string message, string unsupportedCommand) :base (message)
		{
			UnsupportedCommand = unsupportedCommand;
		}

		#region Member Properties

		public string UnsupportedCommand { get; private set; }
		#endregion Member Properties
	}

	public class LaipacConversionToLatitude : LaipacException
	{
		public LaipacConversionToLatitude(string message, string conversionValue) : base(message)
		{
			ConversionValue = conversionValue;
		}
	
		#region Member Properties

		public string ConversionValue { get; private set; }
		#endregion Member Properties
	}
}
