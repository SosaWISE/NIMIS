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
	public class SalespersonMonthlyEarningsController : ApiController
	{
		[HttpGet, Route("SalespersonMonthlyEarnings/{userID}")]
		public Result<SAESalesSalespersonMonthlyEarnings> SalespersonMonthlyEarnings(int userID, int salesMonth, int salesYear)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SalespersonMonthlyEarnings";

			#endregion Initialize

			// Authenticate session first.
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(userID, (userID == 0), "<li>'userID' must be passed.</li>")
				};
				Result<SAESalesSalespersonMonthlyEarnings> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();

					// ** Execute FOS Call
					var fnsResult = service.SalespersonMonthlyEarningsSummary(userID, salesMonth, salesYear);

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
					var resultValue = new SAESalesSalespersonMonthlyEarnings
					{
						UserID = fnsResultValue.UserID,
						SalesMonth = fnsResultValue.SalesMonth,
						SalesYear = fnsResultValue.SalesYear,
						SalesAmt = fnsResultValue.SalesAmt,
						RecurringAmt = fnsResultValue.RecurringAmt,
						RecruitingAmt = fnsResultValue.RecruitingAmt,
						BonusAmt = fnsResultValue.BonusAmt,
						DeductionAmt = fnsResultValue.DeductionAmt,
						HoldAmt = fnsResultValue.HoldAmt,
						TotalCommissionAmt = fnsResultValue.TotalCommissionAmt,
						YTDIncentiveBonusAmt = fnsResultValue.YTDIncentiveBonusAmt,
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
