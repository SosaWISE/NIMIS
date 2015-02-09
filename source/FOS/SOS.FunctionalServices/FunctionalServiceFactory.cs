/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/16/11
 * Time: 11:56
 * 
 * Description:  Implementes the SOS Service Engine
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using SOS.FunctionalServices.Contracts;

namespace SOS.FunctionalServices
{
	public class FunctionalServiceFactory : IFunctionalServiceFactory
	{
		#region Properties
		private readonly Dictionary<Type, Func<object>> _registeredServices = new Dictionary<Type, Func<object>>();
		#endregion Properties

		#region Implementation of IFunctionalServiceFactory

		/// <summary>
		/// Register will register the functional service with the factory and the delegate to create the functional
		/// service.
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <param name="createMethod"></param>
		public void Register<TServiceType>(Func<TServiceType> createMethod)// where TServiceType : IFunctionalService
		{
			Type type = typeof (TServiceType);
			if (_registeredServices.ContainsKey(type))
				throw new ArgumentException(string.Format("The ServiceType '{0}' already exists in service registry.", type));

			_registeredServices.Add(type, createMethod as Func<object>);
		}

		/// <summary>
		/// Create will lookup the concrete factory based on the TServiceType, call the constructor and return the 
		/// TServiceType interface.
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <returns></returns>
		public TServiceType Instance<TServiceType>()// where TServiceType : IFunctionalService
		{
			/** Initialize. */
			Type type = typeof (TServiceType);
			if (!_registeredServices.ContainsKey(type))
				throw new ArgumentException("The requested functional services does not exist in service registry.");

			var createMethod = _registeredServices[type];
			if (createMethod == null)
				throw new ArgumentException("ServiceType does not have a factory delegate");

			return (TServiceType) createMethod();
		}

		#endregion
	}
}