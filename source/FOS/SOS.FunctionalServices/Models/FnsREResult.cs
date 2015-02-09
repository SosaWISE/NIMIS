using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models
{
	public class FnsREResult : IFnsREResult
	{
		#region .ctor

		public FnsREResult(FOS.ReceiverEngineServices.REResult oResult)
		{
			Code = (int) oResult.Code;
			Message = oResult.Message;
			Value = oResult.Value;
		}

		public FnsREResult (int nCode, string szMessage, bool bResult)
		{
			Code = nCode;
			Message = szMessage;
			Value = bResult;
		}

		#endregion .ctor

		#region Implementation of IFnsREResult

		public int Code { get; private set; }
		public string Message { get; private set; }
		public object Value { get; private set; }

		#endregion
	}
}
