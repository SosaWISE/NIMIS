using System;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.DoNotCall;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.DoNotCall;

namespace SOS.FunctionalServices
{
	public class DoNotCallService : IDoNotCallService
	{
		public IFnsResult<IFnsDncPhoneNumber> PhoneNumberRead(IFnsDncPhoneNumber fnsDncPhoneNumber, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "PhoneNumberRead";
			var result = new FnsResult<IFnsDncPhoneNumber>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Create new MsAccount. */
				var dcPhoneNumber = SosCrmDataContext.Instance.DC_PhoneNumbers.Find(fnsDncPhoneNumber.PhoneNumber);

				// ** Check result
				if (dcPhoneNumber == null)
				{
					result.Code = (int)ErrorCodes.GeneralError;
					result.Message = "Phone Number not found in DNC List";
				}
				else
				{
					var resultValue = new FnsDncPhoneNumber(dcPhoneNumber);

					// ** Save result information
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = resultValue;
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsDncPhoneNumber>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

	}
}
