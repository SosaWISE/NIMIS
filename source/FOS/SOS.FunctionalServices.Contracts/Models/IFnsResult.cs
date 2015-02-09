/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 01/18/12
 * Time: 21:24
 * 
 * Description:  Describes how the result of service calls interfaces will be.
 *********************************************************************************************************************/

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsResult
	{
		#region Properties

		int Code { get; }
		string Message { get; }

		object GetValue();

		#endregion Properties
	}

	public interface IFnsResult<T> : IFnsResult
	{
        T GetTValue();
    }
}