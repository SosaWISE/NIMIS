namespace SSE.Lib.Interfaces.FOS
{
	public class FosResult<T> : IFosResult<T>
	{
		#region .ctor
		public FosResult(ResultCodes code, string message)
		{
			Code = (int) code;
			Message = message;
		}
		#endregion .ctor

		#region Properties
		public int Code { get; set; }
		public string Message { get; set; }

		public T Value { get; set; }

		#endregion Properties

		#region Methods
		public object GetValue()
		{
			return Value;
		}
		#endregion Methods
	}
}
