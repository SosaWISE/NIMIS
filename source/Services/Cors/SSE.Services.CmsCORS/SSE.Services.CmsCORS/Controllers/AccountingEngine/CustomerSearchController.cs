using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.AccountingEngine
{
	[RoutePrefix("AccountingEngineSrv")]
	public class CustomerSearchController : ApiController
	{
		[Route("CustomerSearches")]
		[HttpPost]
		public CmsCORSResult<List<AeCustomerMasterFileGeneral>> Post(ArgsCustomerSearch args)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "General Customer Search";
			var result = new CmsCORSResult<List<AeCustomerMasterFileGeneral>>((int)ErrorCodes.Initializing,
				string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					if (args == null)
					{
						result.Code = (int)ErrorCodes.ArgumentValidation;
						result.Message = string.Format("<li>Please pass some argument to search for.</li>");
						return result;
					}

					var argArray = new List<CORSArg>
					{
						new CORSArg(args.City,
							(string.IsNullOrEmpty(args.City) && string.IsNullOrEmpty(args.StateId) && string.IsNullOrEmpty(args.PostalCode) &&
							 string.IsNullOrEmpty(args.Email) && string.IsNullOrEmpty(args.FirstName) && string.IsNullOrEmpty(args.LastName) &&
							 string.IsNullOrEmpty(args.PhoneNumber)),
							"<li>'No Arguments' At least one argument should be passed.</li>"),
					};
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region TRY

					try
					{
						// ** Create Service
						IAccountingEngineService mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsCustomerMasterFileGeneral>> fnsResult = mcService.CustomerGeneralSearch(user.DealerId,
							args.City, args.StateId, args.PostalCode, args.Email, args.FirstName, args.LastName, args.PhoneNumber,
							args.PageSize, args.PageNumber, user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						var fnsResultValue = (List<IFnsCustomerMasterFileGeneral>)fnsResult.GetValue();
						if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
						{
							var resultValue = (from fnsToken in fnsResultValue
											   select new AeCustomerMasterFileGeneral
											   {
												   CustomerMasterFileID = fnsToken.CustomerMasterFileID,
												   FkId = fnsToken.FkId,
												   ResultType = fnsToken.ResultType,
												   Fullname = fnsToken.Fullname,
												   City = fnsToken.City,
												   Phone = fnsToken.Phone,
												   Email = fnsToken.Email,
												   AccountTypes = fnsToken.AccountTypes
											   }).ToList();

							result.Value = resultValue;
						}
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});

		}



		[HttpGet]
		[Route("Customers/{customerId}/Addresses/{customerAddressTypeId}")]
		public CmsCORSResult<object> Addresses(long customerId, string customerAddressTypeId)
		{
			return CORSSecurity.AuthenticationWrapper("Addresses", user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
				var fnsResult = service.CustomerAddress(customerId, customerAddressTypeId);
				return new CmsCORSResult<object>(fnsResult.Code, fnsResult.Message)
				{
					Value = fnsResult.GetValue(),
				};
			});
		}
	}
}
