using Nxs.Services.CorsConnext.Helpers;
using Nxs.Services.CorsConnext.Models;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.Lib.Core;
using SOS.Services.Interfaces.Models.HumanResources;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Nxs.Services.CorsConnext.Controllers.HumanResourceSrvcs
{
	[RoutePrefix("HumanResourceSrvcs")]
	public class MonthlySalesDetailsController : ApiController
	{
		[HttpGet, Route("MonthlySalesDetails/{userID}")]
		public Result<RuUsersConnextGetCombinedStatistics> MonthlySalesDetailsInfo(int userID, int salesMonth, int salesYear)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get MonthlySalesDetails";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(userID, (userID == 0), "<li>'userID' must be passed.</li>")
				};
				Result<RuUsersConnextGetCombinedStatistics> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME))
					return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Init CORS Objects
					var combinedResult = new RuUsersConnextGetCombinedStatistics();

					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();

					// ** Execute FOS Call
					var fnsResult = service.ConnextCombinedMonthlySalesDetails(userID, salesMonth, salesYear);

					// ** Save result
					result.Code = fnsResult.Code;

					// result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					//repeat this
					// Step 1: Get OfficeStats
					combinedResult.OfficeStats = new List<RuUsersConnextGetDetailedStatistics>();

					foreach (IFnsConnextMonthlySalesDetails offitem in ((IFnsConnextCombinedMonthlySalesDetails)fnsResult.GetValue()).OfficeStats)
					{
						combinedResult.OfficeStats.Add(new RuUsersConnextGetDetailedStatistics
						{
							UserID = offitem.UserID,
							FirstName = offitem.FirstName,
							LastName = offitem.LastName,
							SalesYear = offitem.SalesYear,
							SalesMonth = offitem.SalesMonth,
							RegionID = offitem.RegionID,
							RegionName = offitem.RegionName,
							TeamID = offitem.TeamID,
							TeamName = offitem.TeamName,
							OfficeID = offitem.OfficeID,
							OfficeName = offitem.OfficeName,
							HasRecruits = offitem.HasRecruits,
							NumberCreditReportsPulled = offitem.NumberCreditReportsPulled,
							NumberCreditsPassed = offitem.NumberCreditsPassed,
							NumberExcellentCreditScores = offitem.NumberExcellentCreditScores,
							NumberGoodCreditScores = offitem.NumberGoodCreditScores,
							NumberBadCreditScores = offitem.NumberBadCreditScores,
							AverageCreditScore = offitem.AverageCreditScore,
							CreditPassPercentage = offitem.CreditPassPercentage,
							PassAndInstallPercentage = offitem.PassAndInstallPercentage,
							NumberCancels = offitem.NumberCancels,
							NumberNetSales = offitem.NumberNetSales,
							NumberPresurveys = offitem.NumberPresurveys,
							NumberPostsurveys = offitem.NumberPostsurveys,
							NumberInstallations = offitem.NumberInstallations,
							NumberSameDayInstallations = offitem.NumberSameDayInstallations,
							SameDayInstallationPercentage = offitem.SameDayInstallationPercentage,
							NumberActivationsWaived = offitem.NumberActivationsWaived,
							ActivationsWaivedPercentage = offitem.ActivationsWaivedPercentage,
							NumberCCPayments = offitem.NumberCCPayments,
							NumberACHPayments = offitem.NumberACHPayments,
							NumberInvoicePayments = offitem.NumberInvoicePayments,
							NumberSystemsOver8Points = offitem.NumberSystemsOver8Points,
							NumberFreePointsGivenBySalesRep = offitem.NumberFreePointsGivenBySalesRep,
							NumberFreePointsGivenByTech = offitem.NumberFreePointsGivenByTech,
						});
					}

					// Get RepStats
					// Step 1: Get OfficeStats
					combinedResult.RepStats = new List<RuUsersConnextGetDetailedStatistics>();

					foreach (IFnsConnextMonthlySalesDetails repitem in ((IFnsConnextCombinedMonthlySalesDetails)fnsResult.GetValue()).RepStats)
					{
						combinedResult.RepStats.Add(new RuUsersConnextGetDetailedStatistics
						{
							UserID = repitem.UserID,
							FirstName = repitem.FirstName,
							LastName = repitem.LastName,
							SalesYear = repitem.SalesYear,
							SalesMonth = repitem.SalesMonth,
							RegionID = repitem.RegionID,
							RegionName = repitem.RegionName,
							TeamID = repitem.TeamID,
							TeamName = repitem.TeamName,
							OfficeID = repitem.OfficeID,
							OfficeName = repitem.OfficeName,
							HasRecruits = repitem.HasRecruits,
							NumberCreditReportsPulled = repitem.NumberCreditReportsPulled,
							NumberCreditsPassed = repitem.NumberCreditsPassed,
							NumberExcellentCreditScores = repitem.NumberExcellentCreditScores,
							NumberGoodCreditScores = repitem.NumberGoodCreditScores,
							NumberBadCreditScores = repitem.NumberBadCreditScores,
							AverageCreditScore = repitem.AverageCreditScore,
							CreditPassPercentage = repitem.CreditPassPercentage,
							PassAndInstallPercentage = repitem.PassAndInstallPercentage,
							NumberCancels = repitem.NumberCancels,
							NumberNetSales = repitem.NumberNetSales,
							NumberPresurveys = repitem.NumberPresurveys,
							NumberPostsurveys = repitem.NumberPostsurveys,
							NumberInstallations = repitem.NumberInstallations,
							NumberSameDayInstallations = repitem.NumberSameDayInstallations,
							SameDayInstallationPercentage = repitem.SameDayInstallationPercentage,
							NumberActivationsWaived = repitem.NumberActivationsWaived,
							ActivationsWaivedPercentage = repitem.ActivationsWaivedPercentage,
							NumberCCPayments = repitem.NumberCCPayments,
							NumberACHPayments = repitem.NumberACHPayments,
							NumberInvoicePayments = repitem.NumberInvoicePayments,
							NumberSystemsOver8Points = repitem.NumberSystemsOver8Points,
							NumberFreePointsGivenBySalesRep = repitem.NumberFreePointsGivenBySalesRep,
							NumberFreePointsGivenByTech = repitem.NumberFreePointsGivenByTech,
						});
					}

					result.Value = combinedResult;

					// ** Create result value package
					// ReSharper disable once RedundantCast


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
