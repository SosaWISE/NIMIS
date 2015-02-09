/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 03/20/14
 * Time: 15:12
 * 
 * Description:  Describes how the result of service calls interfaces will be.
 *********************************************************************************************************************/

namespace NSE.FOS.Contracts.Models
{
	public interface IFosResult
	{
		#region Properties

		int Code { get; }
		string Message { get; }

		object GetValue();

		#endregion Properties
	}

	public interface IFosResult<T> : IFosResult
	{
	}

}