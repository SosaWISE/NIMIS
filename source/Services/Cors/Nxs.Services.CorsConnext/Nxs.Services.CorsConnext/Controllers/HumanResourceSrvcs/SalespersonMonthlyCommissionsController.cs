using System;
using System.Collections.Generic;
using System.Web.Http;
using Nxs.Services.CorsConnext.Helpers;
using Nxs.Services.CorsConnext.Models;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.Services.Interfaces.Models.HumanResources;
using SOS.Lib.Core;

namespace Nxs.Services.CorsConnext.Controllers.HumanResourceSrvcs
{
	[RoutePrefix("HumanResourceSrvcs")]
	public class SalespersonMonthlyCommissionsController : ApiController
	{
		[HttpGet, Route("SalespersonMonthlyCommissions/{userID}")]
		public Result<SAESalesSalespersonMonthlyCommissions> SalespersonMonthlyCommissions(int userID, int salesMonth, int salesYear)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SalespersonMonthlyCommissions";

			#endregion Initialize

			// Authenticate session first.
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(userID, (userID == 0), "<li>'userID' must be passed.</li>")
				};
				Result<SAESalesSalespersonMonthlyCommissions> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();

					// ** Execute FOS Call
					var fnsResult = service.SalespersonMonthlyCommissions(userID, salesMonth, salesYear);

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
					var resultValue = new SAESalesSalespersonMonthlyCommissions
					{
						UserID = fnsResultValue.UserID,
						ContractDate = fnsResultValue.ContractDate,
						SalesMonth = fnsResultValue.SalesMonth,
						SalesYear = fnsResultValue.SalesYear,
						CustomerMasterFileID = fnsResultValue.CustomerMasterFileID,
						AccountId = fnsResultValue.AccountId,
						CustomerFirstName = fnsResultValue.CustomerFirstName,
						CustomerMiddleName = fnsResultValue.CustomerMiddleName,
						CustomerLastName = fnsResultValue.CustomerLastName,
						CreditRating = fnsResultValue.CreditRating,
						ActivationFeeAmt = fnsResultValue.ActivationFeeAmt,
						ContractLength = fnsResultValue.ContractLength,
						ServiceType = fnsResultValue.ServiceType,
						MonthlyPaymentAmt = fnsResultValue.MonthlyPaymentAmt,
						PaymentMethod = fnsResultValue.PaymentMethod,
						SalesCommissionAmt = fnsResultValue.SalesCommissionAmt,
						RecurringCommissionAmt = fnsResultValue.RecurringCommissionAmt,
						IsActive = fnsResultValue.IsActive,
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
