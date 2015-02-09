/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/15/11
 * Time: 23:02
 * 
 * Description:  This declares the base functional service base interface.
 *********************************************************************************************************************/

using System;

namespace SOS.FunctionalServices.Contracts
{
	public interface IFunctionalServiceBase
	{
		/// <summary>
		///  Keep track of the service engine interface.
		/// </summary>
		IServiceEngine ServiceEngine { get; }

		/// <summary>
		/// Return the contest ID from the service engine.
		/// </summary>
		Guid ContextId { get; }
	}
}