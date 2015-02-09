/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/16/11
 * Time: 08:18
 * 
 * Description:  Describes how the result of service calls interfaces will be.
 *********************************************************************************************************************/

namespace SOS.Services.Interfaces.Models
{
	public interface IServiceResult
	{
		int Code { get; set; }
		string Message { get; set; }
		string ValueType { get; set; }
		long SessionId { get; set; }

		object GetValue();
	}

	public interface IServiceResult <T> : IServiceResult
	{
		
	}
}