using SOS.FunctionalServices.Contracts.Models;

namespace SOS.Lib.Core
{
    public static class ResultExtensions
	{
		public static Result<T> FromFnsResult<T>(this Result<T> result, IFnsResult<T> fnsResult)
		{
			result.Code = fnsResult.Code;
			result.Message = fnsResult.Message;
			result.Value = fnsResult.GetTValue();
			return result;
		}
    }
}
