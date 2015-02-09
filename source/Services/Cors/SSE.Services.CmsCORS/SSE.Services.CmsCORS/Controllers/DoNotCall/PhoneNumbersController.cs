using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.DoNotCall;
using SOS.FunctionalServices.Models.DoNotCall;
using SOS.Services.Interfaces.Models.DoNotCall;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.DoNotCall
{
	[RoutePrefix("DoNotCallSrv")]
	public class PhoneNumbersController : ApiController
	{
		// GET api/phonenumbers/5
		[Route("PhoneNumbers/{phoneNumber}")]
		[HttpGet]
		public CmsCORSResult<bool> Get(string phoneNumber)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Post DncPhoneNumber";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(phoneNumber,
						(string.IsNullOrEmpty(phoneNumber)), "<li>'PhoneNumber' can not be blank.</li>")
				};
				CmsCORSResult<bool> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var doNotCallService = SosServiceEngine.Instance.FunctionalServices.Instance<IDoNotCallService>();

					// ** Bind new data
					var fnsDncPhoneNumber = new FnsDncPhoneNumber
					{
						PhoneNumber = phoneNumber,
					};

					IFnsResult<IFnsDncPhoneNumber> fnsResult = doNotCallService.PhoneNumberRead(fnsDncPhoneNumber,
						user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					result.Value = (result.Code == (int)CmsResultCodes.Success);
				}
				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int)CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
						METHOD_NAME,
						ex.Message);
				}

				#endregion CATCH

				#region Result

				return result;

				#endregion Result
			});
		}

		[HttpPost]
		public CmsCORSResult<bool> Post(DncPhoneNumber phoneNumber)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Post DncPhoneNumber";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(phoneNumber.PhoneNumber,
						(string.IsNullOrEmpty(phoneNumber.PhoneNumber)), "<li>'PhoneNumber' can not be blank.</li>"),
				};
				CmsCORSResult<bool> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var doNotCallService = SosServiceEngine.Instance.FunctionalServices.Instance<IDoNotCallService>();

					// ** Bind new data
					var fnsDncPhoneNumber = new FnsDncPhoneNumber
					{
						AreaCodeId = phoneNumber.AreaCodeId,
						PhoneNumber = phoneNumber.PhoneNumber,

					};

					IFnsResult<IFnsDncPhoneNumber> fnsResult = doNotCallService.PhoneNumberRead(fnsDncPhoneNumber,
						user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					result.Value = (result.Code == (int)CmsResultCodes.Success);
				}
				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int)CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
						METHOD_NAME,
						ex.Message);
				}

				#endregion CATCH

				#region Result

				return result;

				#endregion Result
			});
		}
	}
}
