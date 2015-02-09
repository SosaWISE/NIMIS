using SSE.FOS.AddressVerification.Interfaces;
using SSE.Lib.Interfaces.FOS;

namespace SSE.FOS.AddressVerification.Models
{
	public class FosAVResult<T> : IFosAVResult<T>
	{
		#region .ctor

		public FosAVResult (ResultCodes resultCode, string message, int reminingHits = 0) : this((int)resultCode, message, reminingHits)
		{
		}

		public FosAVResult (int code, string message, int remainingHits = 0)
		{
			Code = code;
			Message = message;
			RemainingHits = remainingHits;
		}

		#endregion .ctor

		#region Properties
		public int Code { get; set; }
		public string Message { get; set; }
		public T Value { get; set; }
		public int RemainingHits { get; set; }
		#endregion Properties

		#region Methods
		public object GetValue()
		{
			return Value;
		}

		public static IFosAVResult<T> GetFromBase(IFosResult<T> source)
		{
			// ** Initialize. 
			var dest = new FosAVResult<T>(source.Code, source.Message);


			// ** Return result.
			return dest;
		}

		#endregion Methods
	}
}
