/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/15/11
 * Time: 23:12
 * 
 * Description:  Implements the base functional services.
 *********************************************************************************************************************/

using System;
using SOS.FunctionalServices.Contracts;

namespace SOS.FunctionalServices
{
	public class FunctionalServiceBase : IFunctionalServiceBase
	{
		#region .ctor
		
		public FunctionalServiceBase(IServiceEngine oServiceEngine)
		{
			ServiceEngine = oServiceEngine;
		}

		#endregion .ctor

		#region Member Properties

		/// <summary>
		/// Instance of the Service Engine.
		/// </summary>
		public IServiceEngine ServiceEngine { get; private set; }

		public Guid ContextId
		{
			get { return ServiceEngine.ContextId; }
		}

		#endregion Member Properties
	}
}
