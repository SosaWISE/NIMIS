/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/15/11
 * Time: 23:04
 * 
 * Description:  This declares the base functional service base interface.
 *********************************************************************************************************************/

using System;

namespace SOS.FunctionalServices.Contracts
{
	public interface IFunctionalServiceFactory
	{
		/// <summary>
		/// Register will register the functional service with the factory and the delegate to create the functional
		/// service.
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <param name="createMethod"></param>
		void Register<TServiceType>(Func<TServiceType> createMethod);// where TServiceType : IFunctionalService;

		/// <summary>
		/// Create will lookup the concrete factory based on the TServiceType, call the constructor and return the 
		/// TServiceType interface.
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <returns></returns>
		TServiceType Instance<TServiceType>();// where TServiceType : IFunctionalService;
	}
}