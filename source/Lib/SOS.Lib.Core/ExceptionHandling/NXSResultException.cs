namespace SOS.Lib.Core.ExceptionHandling
{
	public class NXSResultException<T> : BaseException
	{
		#region .ctor
		public NXSResultException(Result<T> result) : base(result.Message)
		{
			NXSResult = result;
		}
		#endregion .ctor

		#region Properties

		public Result<T> NXSResult { get; private set; } 
		#endregion Properties
	}
}
