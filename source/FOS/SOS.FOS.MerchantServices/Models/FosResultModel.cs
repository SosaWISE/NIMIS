using SSE.Lib.Interfaces.FOS;

namespace SOS.FOS.MerchantServices.Models
{
	public class FosResultModel : IFosResult
	{
		#region .ctor

		public FosResultModel(int code, string message)
		{
			Code = code;
			Message = message;
		}

		#endregion .ctor

		#region Properties

		public object Value { get; set; }

		#endregion Properties

		#region Implementation of IFosResult

		public int Code { get; private set; }
		public string Message { get; private set; }
		public object GetValue()
		{
			return Value;
		}

		#endregion Implementation of IFosResult
	}

	public class FosResultModel<TValueType> : IFosResult<TValueType>
	{
		#region .ctor

		public FosResultModel (int nCode, string sMessage)
		{
			Code = nCode;
			Message = sMessage;
		}

		#endregion .ctor

		#region Implementation of IFosResult

		public int Code { get; set; }
		public string Message { get; set; }
		public TValueType Value { get; set; }

		public object GetValue()
		{
			return Value;
		}

		#endregion Implementation of IFosResult

	}
}