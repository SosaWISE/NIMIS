using System;
using System.Collections.Generic;
using System.Web.Http;
using Nxs.Services.CorsConnext.Helpers;
using Nxs.Services.CorsConnext.Models;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.Services.Interfaces.Models.HumanResources;
using SOS.Lib.Core;

namespace Nxs.Services.CorsConnext.Controllers.HumanResourceSrvcs
{
	[RoutePrefix("HumanResourceSrvcs")]
	public class SalesRankingController : ApiController
	{
		[HttpGet, Route("SalesRanking/{id}")]
		public Result<List<RuUsersConnextSalesRanking>> SalesRanking(int id, string resultstype, string rankinggroup, string rankingperiod, int rows)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SalesRanking";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(id, (id == 0), "<li>'ID' must be passed.</li>")
				};
				Result<List<RuUsersConnextSalesRanking>> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();

					// ** Execute FOS Call
					var fnsResult = service.ConnextSalesRanking(id, resultstype, rankinggroup, rankingperiod, rows);

					// ** Save result
					result.Code = fnsResult.Code;
					//						result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsResultList = (List<IFnsConnextSalesRanking>)fnsResult.GetTValue();
					var resultList = new List<RuUsersConnextSalesRanking>();
					foreach (IFnsConnextSalesRanking ranking in fnsResultList)
					{
						var item = new RuUsersConnextSalesRanking
						{
							UserID = ranking.UserID,
							FirstName = ranking.FirstName,
							MiddleName = ranking.MiddleName,
							LastName = ranking.LastName,
							PhotoURL = ranking.PhotoURL,
							PeriodEndingDate = ranking.PeriodEndingDate,
							ResultsType = ranking.ResultsType,
							RankingGroup = ranking.RankingGroup,
							RankingPeriod = ranking.RankingPeriod,
							CurrentResults = ranking.CurrentResults,
							CurrentSequence = ranking.CurrentSequence,
							CurrentRanking = ranking.CurrentRanking,
							PreviousResults = ranking.PreviousResults,
							PreviousSequence = ranking.PreviousSequence,
							PreviousRanking = ranking.PreviousRanking,
						};
						resultList.Add(item);
					}

					result.Value = resultList;

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
