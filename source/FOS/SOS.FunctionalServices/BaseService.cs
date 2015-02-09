using System;
using SOS.Data.Logging;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Models;

namespace SOS.FunctionalServices
{
	public class BaseService
	{

		#region GenericServiceWrapper
		protected object GenericServiceWrapper<T>(string methodName, Func<FnsResult<T>> action)
		{
			/** Initialize. */
			#region INITIALIZATION

			// ReSharper disable RedundantAssignment
			var oResult = new FnsResult<T>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", methodName)
			};
			// ReSharper restore RedundantAssignment

			#endregion INITIALIZATION

			#region TRY
			try
			{
				return action();
			}
			#endregion TRY

			#region CATCH
			catch (Exception oEx)
			{
				var sMsg = string.Format("Exception thrown at ExecuteSentence: {0}", oEx.Message);
				DBErrorManager.Instance.AddCriticalMessage(oEx,
					string.Format("****Exception in {0}", methodName)
					, sMsg);
				oResult = new FnsResult<T>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = sMsg
				};
			}
			#endregion CATCH

			/** Call action and return result. */
			return oResult;
		}
		#endregion GenericServiceWrapper

	}
}