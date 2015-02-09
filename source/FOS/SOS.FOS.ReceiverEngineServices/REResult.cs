namespace SOS.FOS.ReceiverEngineServices
{
	public class REResult
	{
		#region .ctor
		public REResult(REErrorCodes eCode, string szMessage, object oValue)
		{
			Code = eCode;
			Message = szMessage;
			Value = oValue;
		}

		#endregion .ctor

		#region Properties

		public REErrorCodes Code { get; private set; }
		public string Message { get; private set; }
		public object Value { get; private set; }

		#endregion Properties
	}

	public enum REErrorCodes
	{
		Success = 0,
		Exception = 1,
		GeneralError = 100
	}
}