/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/15/11
 * Time: 23:00
 * 
 * Description:  This is the main interface to the whole shabang.  Declares the base service engine interface.
 *********************************************************************************************************************/

using System;

namespace SOS.FunctionalServices.Contracts
{
	public interface IServiceEngine
	{
		#region Member Methods

		/// <summary>
		/// Initialize the engine.
		/// </summary>
		void Initialize();

		#endregion Member Methods

		#region Member Properties

		/// <summary>
		/// Exposes the functional services.
		/// </summary>
		IFunctionalServiceFactory FunctionalServices { get; }

		/// <summary>
		/// ContextId is the caller context Guid default value for the service.
		/// </summary>
		Guid ContextId { get; set; }

		#endregion Member Properties
	}
}
