using Nxs.Services.CorsConnext.Helpers;
using Nxs.Services.CorsConnext.Models;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.Lib.Core;
using SOS.Services.Interfaces.Models.HumanResources;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Nxs.Services.CorsConnext.Controllers.HumanResourceSrvcs
{
	[RoutePrefix("HumanResourceSrvcs")]
	public class CustomerInfoController : ApiController
	{
		[HttpGet, Route("CustomerInfo/{id}")]
		public Result<AeCustomersConnextGetCustomerInfo> CustomerInfoGet(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get CustomerInfo";

			#endregion Initialize

			// Authenticate session first.
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(id, (id == 0), "<li>'CustomerMasterFileID' must be passed.</li>")
				};
				Result<AeCustomersConnextGetCustomerInfo> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();

					// ** Execute FOS Call
					var fnsResult = service.ConnextCustomerInfo(id);

					// ** Save result
					result.Code = fnsResult.Code;
					//						result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					//                        var fnsResultValue = (IFnsConnextSalesRepExtendedInfo)fnsResult.GetTValue();
					var fnsResultValue = fnsResult.GetTValue();
					var resultValue = new AeCustomersConnextGetCustomerInfo
					{
						CustomerMasterFileID = fnsResultValue.CustomerMasterFileID,
						CustomerID = fnsResultValue.CustomerID,
						FirstName = fnsResultValue.FirstName,
						MiddleName = fnsResultValue.MiddleName,
						LastName = fnsResultValue.LastName,
						StreetAddress = fnsResultValue.StreetAddress,
						StreetAddress2 = fnsResultValue.StreetAddress2,
						City = fnsResultValue.City,
						State = fnsResultValue.State,
						PostalCode = fnsResultValue.PostalCode,
						PhoneHome = fnsResultValue.PhoneHome,
						PhoneWork = fnsResultValue.PhoneWork,
						PhoneMobile = fnsResultValue.PhoneMobile,
						Email = fnsResultValue.Email,
						ContractDate = fnsResultValue.ContractDate,
						AccountStatus = fnsResultValue.AccountStatus,
						TotalCommission = fnsResultValue.TotalCommission,
						NumberReferralsMade = fnsResultValue.NumberReferralsMade,
					};

					result.Value = resultValue;

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
	}
}
