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
	public class SalespersonMonthlyHoldsController : ApiController
	{
		[HttpGet, Route("SalespersonMonthlyHolds/{userID}")]
		public Result<List<SAESalesSalespersonMonthlyHolds>> SalespersonMonthlyHolds(int userID, int salesMonth, int salesYear)
		{
			#region Initialize

			const string METHOD_NAME = "Get SalespersonMonthlyHolds";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(userID, (userID == 0), "<li>'UserID' must be passed.</li>")
				};
				Result<List<SAESalesSalespersonMonthlyHolds>> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();

					// ** Execute FOS Call
					var fnsResult = service.SalespersonMonthlyHolds(userID, salesMonth, salesYear);

					// ** Save result
					result.Code = fnsResult.Code;
					//						result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsResultList = (List<IFnsSalesSalespersonMonthlyHolds>)fnsResult.GetTValue();
					var resultList = new List<SAESalesSalespersonMonthlyHolds>();
					foreach (IFnsSalesSalespersonMonthlyHolds hold in fnsResultList)
					{
						var item = new SAESalesSalespersonMonthlyHolds
						{
							UserID = hold.UserID,
							ContractDate = hold.ContractDate,
							SalesMonth = hold.SalesMonth,
							SalesYear = hold.SalesYear,
							CustomerMasterFileID = hold.CustomerMasterFileID,
							AccountId = hold.AccountID,
							CustomerFirstName = hold.CustomerFirstName,
							CustomerMiddleName = hold.CustomerMiddleName,
							CustomerLastName = hold.CustomerLastName,
							HoldName = hold.HoldName,
							HoldDescription = hold.HoldDescription,
							HoldAmt = hold.HoldAmt,
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
