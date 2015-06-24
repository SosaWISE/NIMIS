namespace SOS.Lib.Core
{
	public class Result<T>
	{
		#region .ctor
		public Result() { }
		public Result(int code = 0, string message = "", T value = default(T))
		{
			Code = code;
			Message = message;
			Value = value;
			ResultType = code == 0 ? ResultTypes.Success : ResultTypes.Failure;
		}
		#endregion .ctor

		#region Properties
		public int Code { get; set; }
		public string Message { get; set; }
		public T Value { get; set; }

		public ResultTypes ResultType { get; private set; }
		#endregion Properties


		public bool Success
		{
			get { return Code == 0; }
		}
		public bool Failure
		{
			get { return Code != 0; }
		}

		public bool ShouldSerializeSuccess() { return false; }
		public bool ShouldSerializeFailure() { return false; }

		public Result<T> Fail(int code, string message)
		{
			this.Code = code;
			this.Message = message;
			this.ResultType = ResultTypes.Failure;
			return this;
		}
		public Result<T> Warn(string message)
		{
			this.Code = 0;
			this.Message = message;
			this.ResultType = ResultTypes.Warning;
			return this;
		}
	}

	public enum ResultTypes
	{
		Success = 0,
		Info,
		Warning,
		Failure
	}
}
