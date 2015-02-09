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
	public class UserInfoController : ApiController
	{
		[HttpGet, Route("UserInfo/{id}")]
		public Result<RuUsersConnextSalesRepExtendedInfo> UserInfo(int id, bool isExtended = false)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get UserInfo";

			#endregion Initialize

			// Authenticate session first.
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(id, (id == 0), "<li>'ID' must be passed.</li>")
				};
				Result<RuUsersConnextSalesRepExtendedInfo> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();

					// ** Execute FOS Call
					var fnsResult = service.ConnextSalesRepInfo(id, isExtended);

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
					var resultValue = new RuUsersConnextSalesRepExtendedInfo
					{
						UserID = fnsResultValue.UserID,
						FirstName = fnsResultValue.FirstName,
						MiddleName = fnsResultValue.MiddleName,
						LastName = fnsResultValue.LastName,
						PhotoURL = fnsResultValue.PhotoURL,
						MLMDepth = fnsResultValue.MLMDepth,
						ManagerHasOwnTeam = fnsResultValue.ManagerHasOwnTeam,
						RegionName = fnsResultValue.RegionName,
						OfficeName = fnsResultValue.OfficeName,
						TeamName = fnsResultValue.TeamName,
						CurrentNationalRank = fnsResultValue.CurrentNationalRank,
						PreviousNationalRank = fnsResultValue.PreviousNationalRank,
						CurrentRegionalRank = fnsResultValue.CurrentRegionalRank,
						PreviousRegionalRank = fnsResultValue.PreviousRegionalRank,
						CurrentOfficeRank = fnsResultValue.CurrentOfficeRank,
						PreviousOfficeRank = fnsResultValue.PreviousOfficeRank,
						CurrentTeamRank = fnsResultValue.CurrentTeamRank,
						PreviousTeamRank = fnsResultValue.PreviousTeamRank,
						StartDate = fnsResultValue.StartDate,
						Phone = fnsResultValue.Phone,
						Email = fnsResultValue.Email,
						StreetAddress = fnsResultValue.StreetAddress,
						StreetAddress2 = fnsResultValue.StreetAddress2,
						City = fnsResultValue.City,
						State = fnsResultValue.State,
						Zip = fnsResultValue.Zip,
						WeeklySalesGoal = fnsResultValue.WeeklySalesGoal,
						MonthlySalesGoal = fnsResultValue.MonthlySalesGoal,
						YearlySalesGoal = fnsResultValue.YearlySalesGoal,
						WeeklyQualityGoal = fnsResultValue.WeeklyQualityGoal,
						MonthlyQualityGoal = fnsResultValue.MonthlyQualityGoal,
						YearlyQualityGoal = fnsResultValue.YearlyQualityGoal,
						License1 = fnsResultValue.License1,
						License1URL = fnsResultValue.License1URL,
						License2 = fnsResultValue.License2,
						License2URL = fnsResultValue.License2URL,
						License3 = fnsResultValue.License3,
						License3URL = fnsResultValue.License3URL,
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
